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
    public class BlogDeleteCommandHandler : CommandHandler, IRequestHandler<BlogDeleteCommand, bool>
    {
        private readonly IRepository<Blog> _blogRepository;
        public BlogDeleteCommandHandler(IUnitOfWork uow, IRepository<Blog> blogRepository) : base(uow)
        {
            _blogRepository = blogRepository;
        }
        public async Task<bool> Handle(BlogDeleteCommand request, CancellationToken cancellationToken)
        {
            _blogRepository.Remove(request.Id);
            return  await CommitAsync();
        }
    }
}
