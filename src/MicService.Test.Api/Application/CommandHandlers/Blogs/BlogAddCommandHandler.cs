using Core.Data.Domain.CommandHandlers;
using Core.Data.Domain.Interfaces;
using MediatR;
using MicService.Test.Api.Application.Commands.Blogs;
using MicService.Test.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicService.Test.Api.Application.CommandHandlers.Blogs
{
    public class BlogAddCommandHandler : CommandHandler, IRequestHandler<BlogAddCommand, bool>
    {
        private readonly IRepository<Blog> _blogRepository;
        public BlogAddCommandHandler(IUnitOfWork uow, IRepository<Blog> blogRepository) : base(uow)
        {
            _blogRepository = blogRepository;
        }
        public async Task<bool> Handle(BlogAddCommand request, CancellationToken cancellationToken)
        {
            Blog model = new Blog();
            model.Name = request.Name;
            model.Url = request.Url;
            _blogRepository.Add(model);
            return await CommitAsync();
        }
    }
}
