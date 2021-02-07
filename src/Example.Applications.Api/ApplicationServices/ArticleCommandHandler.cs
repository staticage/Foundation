using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Applications.Api.Controllers;
using Example.Applications.Domain;
using Foundation.CQRS;

namespace Example.Applications.Api.ApplicationServices
{
    public class ArticleCommandHandler : ICommandHandler<CreateArticleCommand>
    {
        private readonly ExampleDbContext _dbContext;

        public ArticleCommandHandler(ExampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task Handle(CreateArticleCommand command)
        {
            var article = new Article
            {

            };
        }
    }
}
