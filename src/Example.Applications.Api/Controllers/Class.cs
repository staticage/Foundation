using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Applications.Api.Controllers
{
    public class AccountApiController : Controller
    {
        readonly IMediator _mediator;
        readonly IFetcher _fetcher;
        readonly IHostingEnvironment _env;
        private readonly ILifetimeScope _lifetimeScope;
        readonly DbContext _dbContext;
        readonly IRepository _repository;
        private readonly IVmsClient _vmsClient;
        private readonly IMemoryCache _memoryCache;
        private readonly IWeChatClient _client;
        private readonly IDomainCodeGenerator _codeGenerator;
        private readonly IAppDomainContext _context;


        public AccountApiController(IMediator mediator, IFetcher fetcher, DbContext dbContext, IHostingEnvironment env,
            ILifetimeScope lifetimeScope, IRepository repository, IVmsClient vmsClient,
            IMemoryCache memoryCache, IWeChatClient client, IAppDomainContext context, IDomainCodeGenerator codeGenerator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _fetcher = fetcher;
            _env = env;
            _lifetimeScope = lifetimeScope;
            _repository = repository;
            _vmsClient = vmsClient;
            _memoryCache = memoryCache;
            _client = client;
            _codeGenerator = codeGenerator;
            _context = context;
        }

        [HttpGet("api/account/test")]
        public string Test()
        {
            try
            {
                _dbContext.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            using (var scope = _lifetimeScope.BeginLifetimeScope())
            {
                var db = scope.Resolve<SunsetDbContext>();

                var users = db.Users.Include(x => x.UserPermissions).ToList();
                Dictionary<Domain.Shared.Permission.Module, List<Permission>> Permissions
                    = new Dictionary<Domain.Shared.Permission.Module, List<Permission>>();
                Permissions.Add(Domain.Shared.Permission.Module.订单服务统计, new List<Permission>() { Permission.列表 });
                Permissions.Add(Domain.Shared.Permission.Module.客户服务统计, new List<Permission>() { Permission.列表 });
                foreach (var user in users)
                {
                    user.UserPermissions.ForEach(x =>
                    {
                        if (x.Name == (int)Domain.Shared.Permission.Module.订单服务统计 || x.Name == (int)Domain.Shared.Permission.Module.客户服务统计)
                        {
                            db.Remove(x);
                        }
                    });
                    Permissions.ForEach(x =>
                    {
                        if (x.Value.Count > 0)
                        {
                            db.Add(UserPermission.Create(user.Id, (int)x.Key, x.Value.Select(y => (int)y).JoinString(",")));
                        }
                    });
                }
                db.SaveChanges();
            }

            return "OK";
        }

        [HttpPostRoute("api/account/login")]
        public LoginResource Login([FromBody] LoginCommand command)
        {
            if (!_fetcher.Query<User>().Any())
            {
                var create = new CreateUserCommand
                {
                    Username = "admin",
                    Email = "zxglyh@gmail.com",
                    Id = Guid.NewGuid(),
                    RoleType = Domain.Shared.Enum.UserRoleType.Administrator,
                    DisplayName = "超级管理员",
                    TakeEffect = true,
                    CanLoginWeb = true,
                    PosthouseIds = new List<Guid>()
                };
                _mediator.Send(create);
            }

            _mediator.Send(command);

            var user = _fetcher.Query<User>()
                .Include(x => x.UserPermissions)
                .Single(x => x.Username == command.Username);
            var token = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Get())
                .AddSubject("Sunset")
                .AddIssuer("Sunset.Security.Bearer")
                .AddAudience("Sunset.Security.Bearer")
                .AddClaim("MembershipId", user.Id.ToString())
                .AddExpiry(100)
                .Build();

            var result = new LoginResource
            {
                AuthenticationToken = token.Value,
                User = UserResource.WebLoginResource(user),
                AudioSimilarPercent = Convert.ToInt32(_fetcher.Query<AudioSimilarPercent>().SingleOrDefault()?.Value)
            };
            return result;
        }


        [HttpGet("api/user/verify-code")]
        public void VerifyCode(Guid id, string phone)
        {
            if (!phone.CheckPhone()) throw new DomainValidationException("手机格式不正确");
            var vc = MakeVerifyCode();
            _vmsClient.SendSms(id.ToString("N"), phone, "SMS_123291356", new { code = vc });
            _memoryCache.Set(id.ToString("N"), vc, DateTimeOffset.UtcNow.AddMinutes(2));
        }

        [HttpPostRoute("api/user/reset-password")]
        public void ResetUserPassword([FromBody] ResetPasswrodCommand command)
        {
            _mediator.Send(command);
        }


        [HttpGet("api/user/pre-reset-password")]
        public Guid PreResetPassword(Guid id, string phone, string username)
        {
            if (!phone.CheckPhone()) throw new DomainValidationException("手机格式不正确");

            var user = _repository.Query<User>().SingleOrDefault(x =>
                string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
            {
                throw new InvalidActionException("用户名不存在");
            }

            if (string.IsNullOrEmpty(user.Phone))
            {
                throw new InvalidActionException("您未登记手机号码，请联系管理员处理");
            }

            if (user.Phone != phone)
            {
                throw new InvalidActionException("该手机号码已被其他用户使用，请确认手机号码是否正确");
            }

            var vc = MakeVerifyCode();
            _vmsClient.SendSms(id.ToString("N"), phone, "SMS_123291360", new { code = vc });
            _memoryCache.Set(id.ToString("N"), vc, DateTimeOffset.UtcNow.AddMinutes(2));
            return user.Id;
        }


        [HttpGet("api/user/check-verify-code")]
        public void CheckVerifyCode(Guid id, string code)
        {
            var vc = _memoryCache.Get(id.ToString("N"));
            if (vc == null) throw new DomainValidationException("验证码已过期");
            if (vc.ToString() != code) throw new DomainValidationException("验证码不正确");
            _memoryCache.Remove(id);
        }

        [HttpPostRoute("api/user/register")]
        public object RegidsUser([FromBody] MobileRegisterUserCommand command)
        {
            _mediator.Send(command);
            var user = _repository.Get<User>(command.Id);
            return new
            {
                user.Username
            };
        }

        [HttpGet("api/company/select-list")]
        public CompanySelectListResource SelectCompanyList()
        {
            return new CompanySelectListResource
            {
                Items = _fetcher.Query<Company>().Select(x => new CompanySelectItemResource(x)).ToList()
            };
        }

        [HttpGet("api/supplier/select-list")]
        public SupplierSelectListResource SelectSupplierList()
        {
            return new SupplierSelectListResource
            {
                Items = _fetcher.Query<Supplier>().Select(x => new SupplierSelectItemResource(x)).ToList()
            };
        }

        [HttpGet("api/posthouse/select-list")]
        public PosthouseSelectListResource SelectPosthouseList()
        {
            return new PosthouseSelectListResource
            {
                Items = _fetcher.Query<Posthouse>().Select(x => new PosthouseSelectItemResource(x)).ToList()
            };
        }

        [HttpPostRoute("api/account/mobile-login")]
        public LoginResource MobileLogin([FromBody] MobileLoginCommand command)
        {
            if (!_fetcher.Query<User>().Any())
            {
                var create = new CreateUserCommand
                {
                    Username = "admin",
                    Email = "zxglyh@gmail.com",
                    Id = Guid.NewGuid(),
                    RoleType = Domain.Shared.Enum.UserRoleType.Administrator,
                    DisplayName = "超级管理员",
                    TakeEffect = true,
                    CanLoginWeb = true,
                    PosthouseIds = new List<Guid>()
                };
                _mediator.Send(create);
            }

            _mediator.Send(command);

            var user = _fetcher.Query<User>()
                .Include(x => x.UserPosthouses).ThenInclude(x => x.Posthouse)
                .Include(x => x.UserPermissions)
                .Include(x => x.UserMakeOrderPosthouses)
                .Include(x => x.UserTakeOrderPosthouses)
                .Include(x => x.UserTakeOrderRanges)
                .Include(x => x.UserMakeOrderRanges)
                .Include(x => x.Supplier).ThenInclude(x => x.SupplierPosthouses)
                .Include(x => x.Supplier).ThenInclude(x => x.SupplierServiceTemplates)
                .ThenInclude(x => x.ServiceTemplate)
                .Include(x => x.Company)
                .Include(x => x.UserfavoriteCustomers).ThenInclude(x => x.Customer)
                .Single(x => x.Username == command.Username);
            var token = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Get())
                .AddSubject("Sunset")
                .AddIssuer("Sunset.Security.Bearer")
                .AddAudience("Sunset.Security.Bearer")
                .AddClaim("MembershipId", user.Id.ToString())
                .AddExpiry(1440)
                .Build();

            var result = new LoginResource
            {
                AuthenticationToken = token.Value,
                User = new UserResource(user, Url, true),
                AudioSimilarPercent = Convert.ToInt32(_fetcher.Query<AudioSimilarPercent>().SingleOrDefault()?.Value)
            };
            if (result.User.RoleType == UserRoleType.Administrator)
            {
                result.User.Posthouses =
                    _fetcher.Query<Posthouse>().Select(x => new UserPosthouseResource(x)).ToList();
            }

            return result;
        }

        [HttpGet("api/shop/login")]
        public string ShopLogin(string membershipCardNo, string password)
        {
            membershipCardNo = membershipCardNo.Trim();
            password = password.Trim();
            string errorMsg = "";
            var customer = _fetcher.Query<Customer>().FirstOrDefault(x => x.MembershipCardNo == membershipCardNo);
            if (customer != null)
            {
                if (!string.IsNullOrEmpty(customer.IdentityCardId))
                {
                    if (customer.IdentityCardId.ToLowerInvariant().EndsWith(password.ToLowerInvariant()))
                    {
                        return JsonConvert.SerializeObject(new { Success = true, Msg = membershipCardNo });
                    }
                    else
                    {
                        errorMsg = "密码错误！";
                    }
                }
                else
                {
                    errorMsg = string.Format("会员卡号【{0}】，初始密码无效，请联系驿站管理员做身份证验证！", membershipCardNo);
                }
            }
            else
            {
                errorMsg = string.Format("会员卡号【{0}】无效，请查看该卡号是否存在！", membershipCardNo);
            }

            return JsonConvert.SerializeObject(new { Success = false, Msg = errorMsg });
        }

        [HttpPostRoute("api/user/change-bind")]
        public void ChangeBindCommand([FromBody] ChangeMobileBindCommand command)
        {
            _mediator.Send(command);
        }

        [HttpPostRoute("api/wechat/register")]
        public WeChatResponse Register()
        {
            WeChatRegisterCommand command = new WeChatRegisterCommand();
            command.Code = HttpContext.Request.Form["code"];
            if (command.Code.IsNullOrWhiteSpace())
            {
                throw new UnauthorizedException("微信授权失败,请重试");
            }

            var result = _client.GetOpenId(command.Code).Result;
            if (result.OpenId.IsNullOrWhiteSpace())
            {
                throw new UnauthorizedException("微信授权失败,请重试");
            }

            command.OpenId = result.OpenId;
            command.SessionKey = result.SessionKey;

            if (HttpContext.Request.Form.Files == null)
            {
                throw new ArgumentNullException("上传文件不存在");
            }

            var uploadFile = HttpContext.Request.Form.Files[0];
            using (var stream = uploadFile.OpenReadStream())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    var bytes = new byte[ms.Length];
                    // 设置当前流的位置为流的开始
                    ms.Seek(0, SeekOrigin.Begin);
                    try
                    {
                        command.IdCard = _client.BindCustomer(ms).Result;
                    }
                    catch (Exception e)
                    {
                        throw new InvalidActionException(e.InnerException.Message);
                    }
                }
            }

            _mediator.Send(command);
            var customerWeChat = _fetcher.Query<CustomerWeChat>().Include(x => x.Customer)
                .Single(x => x.OpenId == command.OpenId);

            if (!result.OpenId.IsNullOrWhiteSpace())
            {
                result.Token = new JwtTokenBuilder()
                    .AddSecurityKey(JwtSecurityKey.Get())
                    .AddSubject("Sunset")
                    .AddIssuer("Sunset.Security.Bearer")
                    .AddAudience("Sunset.Security.Bearer")
                    .AddClaim("CustomerWeChatId", customerWeChat.Customer.Id.ToString())
                    .AddExpiry(120)
                    .Build().Value;
            }

            if (customerWeChat.Customer.Area1 != null)
                result.Addresses.Add(
                    new
                    {
                        area = customerWeChat.Customer.Area1,
                        address = customerWeChat.Customer.Address1,
                        fullAddress = customerWeChat.Customer.Area1.Replace(",", "") + customerWeChat.Customer.Address1
                    });
            if (customerWeChat.Customer.Area2 != null)
                result.Addresses.Add(
                    new
                    {
                        area = customerWeChat.Customer.Area2,
                        address = customerWeChat.Customer.Address2,
                        fullAddress = customerWeChat.Customer.Area2.Replace(",", "") + customerWeChat.Customer.Address2
                    });
            if (customerWeChat.Customer.Area3 != null)
                result.Addresses.Add(
                    new
                    {
                        area = customerWeChat.Customer.Area3,
                        address = customerWeChat.Customer.Address3,
                        fullAddress = customerWeChat.Customer.Area3.Replace(",", "") + customerWeChat.Customer.Address3
                    });


            return result;
        }

        [HttpGet("api/guid")]
        public string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }


        [HttpPostRoute("api/wechat/login")]
        public WeChatResponse Login([FromBody] WeChatLoginCommand command)
        {
            var result = _client.GetOpenId(command.Code).Result;
            if (result.OpenId.IsNullOrWhiteSpace())
            {
                throw new UnauthorizedException("微信授权失败,请重试");
            }

            command.OpenId = result.OpenId;
            command.SessionKey = result.SessionKey;
            _mediator.Send(command);

            var customerWeChat = _fetcher.Query<CustomerWeChat>().Include(x => x.Customer)
                .Single(x => x.OpenId == command.OpenId);

            if (!result.OpenId.IsNullOrWhiteSpace())
            {
                result.Token = new JwtTokenBuilder()
                    .AddSecurityKey(JwtSecurityKey.Get())
                    .AddSubject("Sunset")
                    .AddIssuer("Sunset.Security.Bearer")
                    .AddAudience("Sunset.Security.Bearer")
                    .AddClaim("CustomerWeChatId", customerWeChat.Customer.Id.ToString())
                    .AddExpiry(120)
                    .Build().Value;
            }

            if (customerWeChat.Customer.Area1 != null)
                result.Addresses.Add(
                    new
                    {
                        area = customerWeChat.Customer.Area1,
                        address = customerWeChat.Customer.Address1,
                        fullAddress = customerWeChat.Customer.Area1.Replace(",", "") + customerWeChat.Customer.Address1
                    });
            if (customerWeChat.Customer.Area2 != null)
                result.Addresses.Add(
                    new
                    {
                        area = customerWeChat.Customer.Area2,
                        address = customerWeChat.Customer.Address2,
                        fullAddress = customerWeChat.Customer.Area2.Replace(",", "") + customerWeChat.Customer.Address2
                    });
            if (customerWeChat.Customer.Area3 != null)
                result.Addresses.Add(
                    new
                    {
                        area = customerWeChat.Customer.Area3,
                        address = customerWeChat.Customer.Address3,
                        fullAddress = customerWeChat.Customer.Area3.Replace(",", "") + customerWeChat.Customer.Address3
                    });
            return result;
        }

        [HttpGet("api/wechat/check-token")]
        [Authorize(Policy = "CustomerWeChat")]
        public string CheckWeChatToken()
        {
            return "ok";
        }

        [HttpGet("api/wechat/update-token/{openid}")]
        public string CheckWeChatToken(string openid)
        {
            var customerWeChat = _fetcher.Query<CustomerWeChat>().Include(x => x.Customer)
                .SingleOrDefault(x => x.OpenId == openid);
            if (customerWeChat == null)
            {
                throw new InvalidActionException("");
            }

            return new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Get())
                .AddSubject("Sunset")
                .AddIssuer("Sunset.Security.Bearer")
                .AddAudience("Sunset.Security.Bearer")
                .AddClaim("CustomerWeChatId", customerWeChat.Customer.Id.ToString())
                .AddExpiry(120)
                .Build().Value;
        }

        [HttpGet("api/grade-list")]
        public IEnumerable<object> GetGradeList()
        {
            return _fetcher.Query<MemberGrade>().OrderBy(x => x.Level).Select(x => new { x.Id, x.Name }).ToList();
        }


        [HttpGet("api/check-token")]
        [Authorize(Policy = "Member")]
        public void CheckToken()
        {
        }

        public string MakeVerifyCode()
        {
            var vc = "";
            Random rNum = new Random();
            for (int i = 1; i <= 6; i++)
            {
                vc += rNum.Next(0, 9).ToString();
            }

            return vc;
        }


        [HttpGet("api/get-guid")]
        public string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }

        [HttpGet("api/check/idcard")]
        public object CheckIdCardNumber(string idCard)
        {
            if (!idCard.CheckIdCard()) throw new DomainValidationException("身份证格式不正确");
            var user = _repository.Query<User>().SingleOrDefault(x =>
                string.Equals(x.IdentityCardId, idCard, StringComparison.CurrentCultureIgnoreCase));
            if (user != null)
            {
                return new
                {
                    Exists = true
                };
            }

            return new
            {
                Exists = false
            };
        }

        [HttpGet("api/check/phone")]
        public object CheckPhone(string phone)
        {
            if (!phone.CheckPhone()) throw new DomainValidationException("手机格式不正确");
            var users = _repository.Query<User>().Where(x =>
                string.Equals(x.Phone, phone, StringComparison.CurrentCultureIgnoreCase));
            if (users.Any())
            {
                return new
                {
                    Exists = true
                };
            }

            return new
            {
                Exists = false
            };
        }

        [HttpGet("api/fix-services")]
        public async Task<object> FixServices()
        {
            var sameNames = new[] { "06家政服务", "家政服务" };
            var errors = new List<string>();
            using (var scope = _lifetimeScope.BeginLifetimeScope())
            {
                var db = scope.Resolve<SunsetDbContext>();
                var categoryMap = sameNames.ToDictionary(x => x,
                    x => db.ServiceCategoryTemplates.Include(t => t.ServiceTemplates)
                        .SingleOrDefault(t => t.Name == x));


                var finalCategory = categoryMap.First().Value;

                using (var connection = db.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (var duplicateCategoryName in categoryMap.Keys.Skip(1))
                        {
                            var category = categoryMap[duplicateCategoryName];
                            foreach (var service in category.ServiceTemplates)
                            {
                                var sameService =
                                    finalCategory.ServiceTemplates.SingleOrDefault(x => x.Name == service.Name);
                                if (sameService != null)
                                {
                                    connection.Execute(
                                        $"UDPATE Services SET ServiceTemplateId='{finalCategory.Id}' WHERE ServiceTemplateId='{sameService.Id}'");

                                    connection.Execute(
                                        $"UDPATE SupplierServiceTemplates SET ServiceTemplateId='{finalCategory.Id}' WHERE ServiceTemplateId='{sameService.Id}'");
                                    connection.Execute(
                                        $"UDPATE U8CodeServiceTemplates SET ServiceTemplateId='{finalCategory.Id}' WHERE ServiceTemplateId='{sameService.Id}'");
                                    connection.Execute(
                                        $"UDPATE UserTakeOrderRanges SET ServiceTemplateId='{finalCategory.Id}' WHERE ServiceTemplateId='{sameService.Id}'");
                                    connection.Execute(
                                        $"UDPATE UserMakeOrderRanges SET ServiceTemplateId='{finalCategory.Id}' WHERE ServiceTemplateId='{sameService.Id}'");

                                    connection.Execute(
                                        $"DELETE FROM ServiceTemplates WHERE Id='{sameService.Id}'");
                                }
                                else
                                {
                                    errors.Add($"{category.Name}/{service.Name}");
                                }
                            }
                        }

                        transaction.Commit();
                    }
                }
            }

            return new
            {
                errors
            };
        }

        [HttpGet("api/wxpay")]
        public void WxTest()
        {
            //            string str = Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
            //
            //
            //            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            //            if (string.IsNullOrEmpty(ip))
            //            {
            //                ip = HttpContext.Connection.RemoteIpAddress.ToString();
            //            }
            //
            //            _wxPay.Unifiedorder(Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString());
        }

        [HttpGet("api/wxpay-notifyurl")]
        public void WxPayNotifyUrl()
        {
        }
    }

    public class LoginResource
    {
        public string AuthenticationToken { get; set; }
        public UserResource User { get; set; }
        public int AudioSimilarPercent { get; set; }
    }
}
