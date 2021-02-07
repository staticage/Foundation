using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Example.Applications.Api.Models
{
    public partial class litemallContext : DbContext
    {
        public litemallContext()
        {
        }

        public litemallContext(DbContextOptions<litemallContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LitemallAd> LitemallAd { get; set; }
        public virtual DbSet<LitemallAddress> LitemallAddress { get; set; }
        public virtual DbSet<LitemallAdmin> LitemallAdmin { get; set; }
        public virtual DbSet<LitemallBrand> LitemallBrand { get; set; }
        public virtual DbSet<LitemallCart> LitemallCart { get; set; }
        public virtual DbSet<LitemallCategory> LitemallCategory { get; set; }
        public virtual DbSet<LitemallCollect> LitemallCollect { get; set; }
        public virtual DbSet<LitemallComment> LitemallComment { get; set; }
        public virtual DbSet<LitemallCoupon> LitemallCoupon { get; set; }
        public virtual DbSet<LitemallCouponUser> LitemallCouponUser { get; set; }
        public virtual DbSet<LitemallFeedback> LitemallFeedback { get; set; }
        public virtual DbSet<LitemallFootprint> LitemallFootprint { get; set; }
        public virtual DbSet<LitemallGoods> LitemallGoods { get; set; }
        public virtual DbSet<LitemallGoodsAttribute> LitemallGoodsAttribute { get; set; }
        public virtual DbSet<LitemallGoodsProduct> LitemallGoodsProduct { get; set; }
        public virtual DbSet<LitemallGoodsSpecification> LitemallGoodsSpecification { get; set; }
        public virtual DbSet<LitemallGroupon> LitemallGroupon { get; set; }
        public virtual DbSet<LitemallGrouponRules> LitemallGrouponRules { get; set; }
        public virtual DbSet<LitemallIssue> LitemallIssue { get; set; }
        public virtual DbSet<LitemallKeyword> LitemallKeyword { get; set; }
        public virtual DbSet<LitemallLog> LitemallLog { get; set; }
        public virtual DbSet<LitemallOrder> LitemallOrder { get; set; }
        public virtual DbSet<LitemallOrderGoods> LitemallOrderGoods { get; set; }
        public virtual DbSet<LitemallPermission> LitemallPermission { get; set; }
        public virtual DbSet<LitemallRegion> LitemallRegion { get; set; }
        public virtual DbSet<LitemallRole> LitemallRole { get; set; }
        public virtual DbSet<LitemallSearchHistory> LitemallSearchHistory { get; set; }
        public virtual DbSet<LitemallStorage> LitemallStorage { get; set; }
        public virtual DbSet<LitemallSystem> LitemallSystem { get; set; }
        public virtual DbSet<LitemallTopic> LitemallTopic { get; set; }
        public virtual DbSet<LitemallUser> LitemallUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user id=root;password=sasa;database=litemall", x => x.ServerVersion("5.7.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LitemallAd>(entity =>
            {
                entity.ToTable("litemall_ad");

                entity.HasComment("广告表");

                entity.HasIndex(e => e.Enabled)
                    .HasName("enabled");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("活动内容")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Enabled)
                    .HasColumnName("enabled")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否启动");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime")
                    .HasComment("广告结束时间");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasColumnName("link")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("所广告的商品页面或者活动页面链接地址")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("广告标题")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("广告位置：1则是首页");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime")
                    .HasComment("广告开始时间");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)")
                    .HasComment("广告宣传图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<LitemallAddress>(entity =>
            {
                entity.ToTable("litemall_address");

                entity.HasComment("收货地址表");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.AddressDetail)
                    .IsRequired()
                    .HasColumnName("address_detail")
                    .HasColumnType("varchar(127)")
                    .HasDefaultValueSql("''")
                    .HasComment("详细收货地址")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.AreaCode)
                    .HasColumnName("area_code")
                    .HasColumnType("char(6)")
                    .HasComment("地区编码")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("varchar(63)")
                    .HasComment("行政区域表的市ID")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasColumnName("county")
                    .HasColumnType("varchar(63)")
                    .HasComment("行政区域表的区县ID")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.IsDefault)
                    .HasColumnName("is_default")
                    .HasComment("是否默认地址");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("收货人名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postal_code")
                    .HasColumnType("char(6)")
                    .HasComment("邮政编码")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasColumnName("province")
                    .HasColumnType("varchar(63)")
                    .HasComment("行政区域表的省ID")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasColumnName("tel")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("手机号码")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户表的用户ID");
            });

            modelBuilder.Entity<LitemallAdmin>(entity =>
            {
                entity.ToTable("litemall_admin");

                entity.HasComment("管理员表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''''")
                    .HasComment("头像图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.LastLoginIp)
                    .HasColumnName("last_login_ip")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("最近一次登录IP地址")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnName("last_login_time")
                    .HasColumnType("datetime")
                    .HasComment("最近一次登录时间");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("管理员密码")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.RoleIds)
                    .HasColumnName("role_ids")
                    .HasColumnType("varchar(127)")
                    .HasDefaultValueSql("'[]'")
                    .HasComment("角色列表")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("管理员名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<LitemallBrand>(entity =>
            {
                entity.ToTable("litemall_brand");

                entity.HasComment("品牌商表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasColumnName("desc")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("品牌商简介")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.FloorPrice)
                    .HasColumnName("floor_price")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("品牌商的商品低价，仅用于页面展示");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("品牌商名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PicUrl)
                    .IsRequired()
                    .HasColumnName("pic_url")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("品牌商页的品牌商图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'50'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallCart>(entity =>
            {
                entity.ToTable("litemall_cart");

                entity.HasComment("购物车商品表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Checked)
                    .HasColumnName("checked")
                    .HasDefaultValueSql("'1'")
                    .HasComment("购物车中商品是否选择状态");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.GoodsId)
                    .HasColumnName("goods_id")
                    .HasColumnType("int(11)")
                    .HasComment("商品表的商品ID");

                entity.Property(e => e.GoodsName)
                    .HasColumnName("goods_name")
                    .HasColumnType("varchar(127)")
                    .HasComment("商品名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.GoodsSn)
                    .HasColumnName("goods_sn")
                    .HasColumnType("varchar(63)")
                    .HasComment("商品编号")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("商品货品的数量");

                entity.Property(e => e.PicUrl)
                    .HasColumnName("pic_url")
                    .HasColumnType("varchar(255)")
                    .HasComment("商品图片或者商品货品图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("商品货品的价格");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)")
                    .HasComment("商品货品表的货品ID");

                entity.Property(e => e.Specifications)
                    .HasColumnName("specifications")
                    .HasColumnType("varchar(1023)")
                    .HasComment("商品规格值列表，采用JSON数组格式")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户表的用户ID");
            });

            modelBuilder.Entity<LitemallCategory>(entity =>
            {
                entity.ToTable("litemall_category");

                entity.HasComment("类目表");

                entity.HasIndex(e => e.Pid)
                    .HasName("parent_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Desc)
                    .HasColumnName("desc")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("类目广告语介绍")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.IconUrl)
                    .HasColumnName("icon_url")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("类目图标")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Keywords)
                    .IsRequired()
                    .HasColumnName("keywords")
                    .HasColumnType("varchar(1023)")
                    .HasDefaultValueSql("''")
                    .HasComment("类目关键字，以JSON数组格式")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'L1'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("类目名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PicUrl)
                    .HasColumnName("pic_url")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("类目图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Pid)
                    .HasColumnName("pid")
                    .HasColumnType("int(11)")
                    .HasComment("父类目ID");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'50'")
                    .HasComment("排序");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallCollect>(entity =>
            {
                entity.ToTable("litemall_collect");

                entity.HasComment("收藏表");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.HasIndex(e => e.ValueId)
                    .HasName("goods_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3)")
                    .HasComment("收藏类型，如果type=0，则是商品ID；如果type=1，则是专题ID");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户表的用户ID");

                entity.Property(e => e.ValueId)
                    .HasColumnName("value_id")
                    .HasColumnType("int(11)")
                    .HasComment("如果type=0，则是商品ID；如果type=1，则是专题ID");
            });

            modelBuilder.Entity<LitemallComment>(entity =>
            {
                entity.ToTable("litemall_comment");

                entity.HasComment("评论表");

                entity.HasIndex(e => e.ValueId)
                    .HasName("id_value");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("varchar(1023)")
                    .HasComment("评论内容")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.HasPicture)
                    .HasColumnName("has_picture")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否含有图片");

                entity.Property(e => e.PicUrls)
                    .HasColumnName("pic_urls")
                    .HasColumnType("varchar(1023)")
                    .HasComment("图片地址列表，采用JSON数组格式")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Star)
                    .HasColumnName("star")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("评分， 1-5");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3)")
                    .HasComment("评论类型，如果type=0，则是商品评论；如果是type=1，则是专题评论；如果type=3，则是订单商品评论。");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户表的用户ID");

                entity.Property(e => e.ValueId)
                    .HasColumnName("value_id")
                    .HasColumnType("int(11)")
                    .HasComment("如果type=0，则是商品评论；如果是type=1，则是专题评论。");
            });

            modelBuilder.Entity<LitemallCoupon>(entity =>
            {
                entity.ToTable("litemall_coupon");

                entity.HasComment("优惠券信息及规则表");

                entity.HasIndex(e => e.Code)
                    .HasName("code");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(63)")
                    .HasComment("优惠券兑换码")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Days)
                    .HasColumnName("days")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("基于领取时间的有效天数days。");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Desc)
                    .HasColumnName("desc")
                    .HasColumnType("varchar(127)")
                    .HasDefaultValueSql("''")
                    .HasComment("优惠券介绍，通常是显示优惠券使用限制文字")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("优惠金额，");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime")
                    .HasComment("使用券截至时间");

                entity.Property(e => e.GoodsType)
                    .HasColumnName("goods_type")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("商品限制类型，如果0则全商品，如果是1则是类目限制，如果是2则是商品限制。");

                entity.Property(e => e.GoodsValue)
                    .HasColumnName("goods_value")
                    .HasColumnType("varchar(1023)")
                    .HasDefaultValueSql("'[]'")
                    .HasComment("商品限制值，goods_type如果是0则空集合，如果是1则是类目集合，如果是2则是商品集合。")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Limit)
                    .HasColumnName("limit")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("用户领券限制数量，如果是0，则是不限制；默认是1，限领一张.");

                entity.Property(e => e.Min)
                    .HasColumnName("min")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("最少消费金额才能使用优惠券。");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(63)")
                    .HasComment("优惠券名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime")
                    .HasComment("使用券开始时间");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("优惠券状态，如果是0则是正常可用；如果是1则是过期; 如果是2则是下架。");

                entity.Property(e => e.Tag)
                    .HasColumnName("tag")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("优惠券标签，例如新人专用")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.TimeType)
                    .HasColumnName("time_type")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("有效时间限制，如果是0，则基于领取时间的有效天数days；如果是1，则start_time和end_time是优惠券有效期；");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("int(11)")
                    .HasComment("优惠券数量，如果是0，则是无限量");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("优惠券赠送类型，如果是0则通用券，用户领取；如果是1，则是注册赠券；如果是2，则是优惠券码兑换；");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallCouponUser>(entity =>
            {
                entity.ToTable("litemall_coupon_user");

                entity.HasComment("优惠券用户使用表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CouponId)
                    .HasColumnName("coupon_id")
                    .HasColumnType("int(11)")
                    .HasComment("优惠券ID");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime")
                    .HasComment("有效期截至时间");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("int(11)")
                    .HasComment("订单ID");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime")
                    .HasComment("有效期开始时间");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("使用状态, 如果是0则未使用；如果是1则已使用；如果是2则已过期；如果是3则已经下架；");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UsedTime)
                    .HasColumnName("used_time")
                    .HasColumnType("datetime")
                    .HasComment("使用时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户ID");
            });

            modelBuilder.Entity<LitemallFeedback>(entity =>
            {
                entity.ToTable("litemall_feedback");

                entity.HasComment("意见反馈表");

                entity.HasIndex(e => e.Status)
                    .HasName("id_value");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("varchar(1023)")
                    .HasComment("反馈内容")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.FeedType)
                    .IsRequired()
                    .HasColumnName("feed_type")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("反馈类型")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.HasPicture)
                    .HasColumnName("has_picture")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否含有图片");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasColumnName("mobile")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("手机号")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PicUrls)
                    .HasColumnName("pic_urls")
                    .HasColumnType("varchar(1023)")
                    .HasComment("图片地址列表，采用JSON数组格式")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(3)")
                    .HasComment("状态");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户表的用户ID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("用户名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<LitemallFootprint>(entity =>
            {
                entity.ToTable("litemall_footprint");

                entity.HasComment("用户浏览足迹表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.GoodsId)
                    .HasColumnName("goods_id")
                    .HasColumnType("int(11)")
                    .HasComment("浏览商品ID");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户表的用户ID");
            });

            modelBuilder.Entity<LitemallGoods>(entity =>
            {
                entity.ToTable("litemall_goods");

                entity.HasComment("商品基本信息表");

                entity.HasIndex(e => e.BrandId)
                    .HasName("brand_id");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("cat_id");

                entity.HasIndex(e => e.GoodsSn)
                    .HasName("goods_sn");

                entity.HasIndex(e => e.SortOrder)
                    .HasName("sort_order");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.BrandId)
                    .HasColumnName("brand_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Brief)
                    .HasColumnName("brief")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("商品简介")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("商品所属类目ID");

                entity.Property(e => e.CounterPrice)
                    .HasColumnName("counter_price")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("专柜价格");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Detail)
                    .HasColumnName("detail")
                    .HasColumnType("text")
                    .HasComment("商品详细介绍，是富文本格式")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Gallery)
                    .HasColumnName("gallery")
                    .HasColumnType("varchar(1023)")
                    .HasComment("商品宣传图片列表，采用JSON数组格式")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.GoodsSn)
                    .IsRequired()
                    .HasColumnName("goods_sn")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("商品编号")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.IsHot)
                    .HasColumnName("is_hot")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否人气推荐，如果设置则可以在人气推荐页面展示");

                entity.Property(e => e.IsNew)
                    .HasColumnName("is_new")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否新品首发，如果设置则可以在新品首发页面展示");

                entity.Property(e => e.IsOnSale)
                    .HasColumnName("is_on_sale")
                    .HasDefaultValueSql("'1'")
                    .HasComment("是否上架");

                entity.Property(e => e.Keywords)
                    .HasColumnName("keywords")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("商品关键字，采用逗号间隔")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(127)")
                    .HasDefaultValueSql("''")
                    .HasComment("商品名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PicUrl)
                    .HasColumnName("pic_url")
                    .HasColumnType("varchar(255)")
                    .HasComment("商品页面商品图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.RetailPrice)
                    .HasColumnName("retail_price")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'100000.00'")
                    .HasComment("零售价格");

                entity.Property(e => e.ShareUrl)
                    .HasColumnName("share_url")
                    .HasColumnType("varchar(255)")
                    .HasComment("商品分享朋友圈图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasColumnType("smallint(4)")
                    .HasDefaultValueSql("'100'");

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasColumnType("varchar(31)")
                    .HasDefaultValueSql("'’件‘'")
                    .HasComment("商品单位，例如件、盒")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallGoodsAttribute>(entity =>
            {
                entity.ToTable("litemall_goods_attribute");

                entity.HasComment("商品参数表");

                entity.HasIndex(e => e.GoodsId)
                    .HasName("goods_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Attribute)
                    .IsRequired()
                    .HasColumnName("attribute")
                    .HasColumnType("varchar(255)")
                    .HasComment("商品参数名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.GoodsId)
                    .HasColumnName("goods_id")
                    .HasColumnType("int(11)")
                    .HasComment("商品表的商品ID");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar(255)")
                    .HasComment("商品参数值")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<LitemallGoodsProduct>(entity =>
            {
                entity.ToTable("litemall_goods_product");

                entity.HasComment("商品货品表");

                entity.HasIndex(e => e.GoodsId)
                    .HasName("goods_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.GoodsId)
                    .HasColumnName("goods_id")
                    .HasColumnType("int(11)")
                    .HasComment("商品表的商品ID");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasColumnType("int(11)")
                    .HasComment("商品货品数量");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("商品货品价格");

                entity.Property(e => e.Specifications)
                    .IsRequired()
                    .HasColumnName("specifications")
                    .HasColumnType("varchar(1023)")
                    .HasComment("商品规格值列表，采用JSON数组格式")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(125)")
                    .HasComment("商品货品图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<LitemallGoodsSpecification>(entity =>
            {
                entity.ToTable("litemall_goods_specification");

                entity.HasComment("商品规格表");

                entity.HasIndex(e => e.GoodsId)
                    .HasName("goods_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.GoodsId)
                    .HasColumnName("goods_id")
                    .HasColumnType("int(11)")
                    .HasComment("商品表的商品ID");

                entity.Property(e => e.PicUrl)
                    .IsRequired()
                    .HasColumnName("pic_url")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("商品规格图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Specification)
                    .IsRequired()
                    .HasColumnName("specification")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("商品规格名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("商品规格值")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<LitemallGroupon>(entity =>
            {
                entity.ToTable("litemall_groupon");

                entity.HasComment("团购活动表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreatorUserId)
                    .HasColumnName("creator_user_id")
                    .HasColumnType("int(11)")
                    .HasComment("开团用户ID");

                entity.Property(e => e.CreatorUserTime)
                    .HasColumnName("creator_user_time")
                    .HasColumnType("datetime")
                    .HasComment("开团时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.GrouponId)
                    .HasColumnName("groupon_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("如果是开团用户，则groupon_id是0；如果是参团用户，则groupon_id是团购活动ID");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("int(11)")
                    .HasComment("关联的订单ID");

                entity.Property(e => e.RulesId)
                    .HasColumnName("rules_id")
                    .HasColumnType("int(11)")
                    .HasComment("团购规则ID，关联litemall_groupon_rules表ID字段");

                entity.Property(e => e.ShareUrl)
                    .HasColumnName("share_url")
                    .HasColumnType("varchar(255)")
                    .HasComment("团购分享图片地址")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("团购活动状态，开团未支付则0，开团中则1，开团失败则2");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户ID");
            });

            modelBuilder.Entity<LitemallGrouponRules>(entity =>
            {
                entity.ToTable("litemall_groupon_rules");

                entity.HasComment("团购规则表");

                entity.HasIndex(e => e.GoodsId)
                    .HasName("goods_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("decimal(63,0)")
                    .HasComment("优惠金额");

                entity.Property(e => e.DiscountMember)
                    .HasColumnName("discount_member")
                    .HasColumnType("int(11)")
                    .HasComment("达到优惠条件的人数");

                entity.Property(e => e.ExpireTime)
                    .HasColumnName("expire_time")
                    .HasColumnType("datetime")
                    .HasComment("团购过期时间");

                entity.Property(e => e.GoodsId)
                    .HasColumnName("goods_id")
                    .HasColumnType("int(11)")
                    .HasComment("商品表的商品ID");

                entity.Property(e => e.GoodsName)
                    .IsRequired()
                    .HasColumnName("goods_name")
                    .HasColumnType("varchar(127)")
                    .HasComment("商品名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PicUrl)
                    .HasColumnName("pic_url")
                    .HasColumnType("varchar(255)")
                    .HasComment("商品图片或者商品货品图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("团购规则状态，正常上线则0，到期自动下线则1，管理手动下线则2");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallIssue>(entity =>
            {
                entity.ToTable("litemall_issue");

                entity.HasComment("常见问题表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Answer)
                    .HasColumnName("answer")
                    .HasColumnType("varchar(255)")
                    .HasComment("问题答案")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Question)
                    .HasColumnName("question")
                    .HasColumnType("varchar(255)")
                    .HasComment("问题标题")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallKeyword>(entity =>
            {
                entity.ToTable("litemall_keyword");

                entity.HasComment("关键字表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.IsDefault)
                    .HasColumnName("is_default")
                    .HasComment("是否是默认关键字");

                entity.Property(e => e.IsHot)
                    .HasColumnName("is_hot")
                    .HasComment("是否是热门关键字");

                entity.Property(e => e.Keyword)
                    .IsRequired()
                    .HasColumnName("keyword")
                    .HasColumnType("varchar(127)")
                    .HasDefaultValueSql("''")
                    .HasComment("关键字")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'100'")
                    .HasComment("排序");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("关键字的跳转链接")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<LitemallLog>(entity =>
            {
                entity.ToTable("litemall_log");

                entity.HasComment("操作日志表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasColumnType("varchar(45)")
                    .HasComment("操作动作")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Admin)
                    .HasColumnName("admin")
                    .HasColumnType("varchar(45)")
                    .HasComment("管理员")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("varchar(255)")
                    .HasComment("补充信息")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasColumnType("varchar(45)")
                    .HasComment("管理员地址")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Result)
                    .HasColumnName("result")
                    .HasColumnType("varchar(127)")
                    .HasComment("操作结果，或者成功消息，或者失败消息")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("操作状态");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("int(11)")
                    .HasComment("操作分类");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallOrder>(entity =>
            {
                entity.ToTable("litemall_order");

                entity.HasComment("订单表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActualPrice)
                    .HasColumnName("actual_price")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("实付费用， = order_price - integral_price");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(127)")
                    .HasComment("收货具体地址")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("待评价订单商品数量");

                entity.Property(e => e.ConfirmTime)
                    .HasColumnName("confirm_time")
                    .HasColumnType("datetime")
                    .HasComment("用户确认收货时间");

                entity.Property(e => e.Consignee)
                    .IsRequired()
                    .HasColumnName("consignee")
                    .HasColumnType("varchar(63)")
                    .HasComment("收货人名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CouponPrice)
                    .HasColumnName("coupon_price")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("优惠券减免");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime")
                    .HasComment("订单关闭时间");

                entity.Property(e => e.FreightPrice)
                    .HasColumnName("freight_price")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("配送费用");

                entity.Property(e => e.GoodsPrice)
                    .HasColumnName("goods_price")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("商品总费用");

                entity.Property(e => e.GrouponPrice)
                    .HasColumnName("groupon_price")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("团购优惠价减免");

                entity.Property(e => e.IntegralPrice)
                    .HasColumnName("integral_price")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("用户积分减免");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message")
                    .HasColumnType("varchar(512)")
                    .HasDefaultValueSql("''")
                    .HasComment("用户订单留言")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasColumnName("mobile")
                    .HasColumnType("varchar(63)")
                    .HasComment("收货人手机号")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.OrderPrice)
                    .HasColumnName("order_price")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("订单费用， = goods_price + freight_price - coupon_price");

                entity.Property(e => e.OrderSn)
                    .IsRequired()
                    .HasColumnName("order_sn")
                    .HasColumnType("varchar(63)")
                    .HasComment("订单编号")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.OrderStatus)
                    .HasColumnName("order_status")
                    .HasColumnType("smallint(6)")
                    .HasComment("订单状态");

                entity.Property(e => e.PayId)
                    .HasColumnName("pay_id")
                    .HasColumnType("varchar(63)")
                    .HasComment("微信付款编号")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PayTime)
                    .HasColumnName("pay_time")
                    .HasColumnType("datetime")
                    .HasComment("微信付款时间");

                entity.Property(e => e.RefundAmount)
                    .HasColumnName("refund_amount")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("实际退款金额，（有可能退款金额小于实际支付金额）");

                entity.Property(e => e.RefundContent)
                    .HasColumnName("refund_content")
                    .HasColumnType("varchar(127)")
                    .HasComment("退款备注")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.RefundTime)
                    .HasColumnName("refund_time")
                    .HasColumnType("datetime")
                    .HasComment("退款时间");

                entity.Property(e => e.RefundType)
                    .HasColumnName("refund_type")
                    .HasColumnType("varchar(63)")
                    .HasComment("退款方式")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ShipChannel)
                    .HasColumnName("ship_channel")
                    .HasColumnType("varchar(63)")
                    .HasComment("发货快递公司")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ShipSn)
                    .HasColumnName("ship_sn")
                    .HasColumnType("varchar(63)")
                    .HasComment("发货编号")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ShipTime)
                    .HasColumnName("ship_time")
                    .HasColumnType("datetime")
                    .HasComment("发货开始时间");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户表的用户ID");
            });

            modelBuilder.Entity<LitemallOrderGoods>(entity =>
            {
                entity.ToTable("litemall_order_goods");

                entity.HasComment("订单商品表");

                entity.HasIndex(e => e.GoodsId)
                    .HasName("goods_id");

                entity.HasIndex(e => e.OrderId)
                    .HasName("order_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("订单商品评论，如果是-1，则超期不能评价；如果是0，则可以评价；如果其他值，则是comment表里面的评论ID。");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.GoodsId)
                    .HasColumnName("goods_id")
                    .HasColumnType("int(11)")
                    .HasComment("商品表的商品ID");

                entity.Property(e => e.GoodsName)
                    .IsRequired()
                    .HasColumnName("goods_name")
                    .HasColumnType("varchar(127)")
                    .HasDefaultValueSql("''")
                    .HasComment("商品名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.GoodsSn)
                    .IsRequired()
                    .HasColumnName("goods_sn")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("商品编号")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasColumnType("smallint(5)")
                    .HasComment("商品货品的购买数量");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("int(11)")
                    .HasComment("订单表的订单ID");

                entity.Property(e => e.PicUrl)
                    .IsRequired()
                    .HasColumnName("pic_url")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("商品货品图片或者商品图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)")
                    .HasComment("商品货品的售价");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)")
                    .HasComment("商品货品表的货品ID");

                entity.Property(e => e.Specifications)
                    .IsRequired()
                    .HasColumnName("specifications")
                    .HasColumnType("varchar(1023)")
                    .HasComment("商品货品的规格列表")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallPermission>(entity =>
            {
                entity.ToTable("litemall_permission");

                entity.HasComment("权限表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Permission)
                    .HasColumnName("permission")
                    .HasColumnType("varchar(63)")
                    .HasComment("权限")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)")
                    .HasComment("角色ID");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallRegion>(entity =>
            {
                entity.ToTable("litemall_region");

                entity.HasComment("行政区域表");

                entity.HasIndex(e => e.Code)
                    .HasName("agency_id");

                entity.HasIndex(e => e.Pid)
                    .HasName("parent_id");

                entity.HasIndex(e => e.Type)
                    .HasName("region_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("int(11)")
                    .HasComment("行政区域编码");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(120)")
                    .HasDefaultValueSql("''")
                    .HasComment("行政区域名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Pid)
                    .HasColumnName("pid")
                    .HasColumnType("int(11)")
                    .HasComment("行政区域父ID，例如区县的pid指向市，市的pid指向省，省的pid则是0");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3)")
                    .HasComment("行政区域类型，如如1则是省， 如果是2则是市，如果是3则是区县");
            });

            modelBuilder.Entity<LitemallRole>(entity =>
            {
                entity.ToTable("litemall_role");

                entity.HasComment("角色表");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Desc)
                    .HasColumnName("desc")
                    .HasColumnType("varchar(1023)")
                    .HasComment("角色描述")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Enabled)
                    .HasColumnName("enabled")
                    .HasDefaultValueSql("'1'")
                    .HasComment("是否启用");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(63)")
                    .HasComment("角色名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallSearchHistory>(entity =>
            {
                entity.ToTable("litemall_search_history");

                entity.HasComment("搜索历史表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.From)
                    .IsRequired()
                    .HasColumnName("from")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("搜索来源，如pc、wx、app")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Keyword)
                    .IsRequired()
                    .HasColumnName("keyword")
                    .HasColumnType("varchar(63)")
                    .HasComment("搜索关键字")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .HasComment("用户表的用户ID");
            });

            modelBuilder.Entity<LitemallStorage>(entity =>
            {
                entity.ToTable("litemall_storage");

                entity.HasComment("文件存储表");

                entity.HasIndex(e => e.Key)
                    .HasName("key");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnName("key")
                    .HasColumnType("varchar(63)")
                    .HasComment("文件的唯一索引")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasComment("文件名")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasColumnType("int(11)")
                    .HasComment("文件大小");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(20)")
                    .HasComment("文件类型")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)")
                    .HasComment("文件访问链接")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<LitemallSystem>(entity =>
            {
                entity.ToTable("litemall_system");

                entity.HasComment("系统配置表");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.KeyName)
                    .IsRequired()
                    .HasColumnName("key_name")
                    .HasColumnType("varchar(255)")
                    .HasComment("系统配置名")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.KeyValue)
                    .IsRequired()
                    .HasColumnName("key_value")
                    .HasColumnType("varchar(255)")
                    .HasComment("系统配置值")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallTopic>(entity =>
            {
                entity.ToTable("litemall_topic");

                entity.HasComment("专题表");

                entity.HasIndex(e => e.Id)
                    .HasName("topic_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text")
                    .HasComment("专题内容，富文本格式")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Goods)
                    .HasColumnName("goods")
                    .HasColumnType("varchar(1023)")
                    .HasDefaultValueSql("''")
                    .HasComment("专题相关商品，采用JSON数组格式")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PicUrl)
                    .HasColumnName("pic_url")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("专题图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("专题相关商品最低价");

                entity.Property(e => e.ReadCount)
                    .HasColumnName("read_count")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'1k'")
                    .HasComment("专题阅读量")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'100'")
                    .HasComment("排序");

                entity.Property(e => e.Subtitle)
                    .HasColumnName("subtitle")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''''")
                    .HasComment("专题子标题")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''''")
                    .HasComment("专题标题")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<LitemallUser>(entity =>
            {
                entity.ToTable("litemall_user");

                entity.HasComment("用户表");

                entity.HasIndex(e => e.Username)
                    .HasName("user_name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTime)
                    .HasColumnName("add_time")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasColumnName("avatar")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("用户头像图片")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date")
                    .HasComment("生日");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("'0'")
                    .HasComment("逻辑删除");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("tinyint(3)")
                    .HasComment("性别：0 未知， 1男， 1 女");

                entity.Property(e => e.LastLoginIp)
                    .IsRequired()
                    .HasColumnName("last_login_ip")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("最近一次登录IP地址")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnName("last_login_time")
                    .HasColumnType("datetime")
                    .HasComment("最近一次登录时间");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasColumnName("mobile")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("用户手机号码")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("用户昵称或网络名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("用户密码")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.SessionKey)
                    .IsRequired()
                    .HasColumnName("session_key")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasComment("微信登录会话KEY")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(3)")
                    .HasComment("0 可用, 1 禁用, 2 注销");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasComment("更新时间");

                entity.Property(e => e.UserLevel)
                    .HasColumnName("user_level")
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("0 普通用户，1 VIP用户，2 高级VIP用户");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(63)")
                    .HasComment("用户名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.WeixinOpenid)
                    .IsRequired()
                    .HasColumnName("weixin_openid")
                    .HasColumnType("varchar(63)")
                    .HasDefaultValueSql("''")
                    .HasComment("微信登录openid")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
