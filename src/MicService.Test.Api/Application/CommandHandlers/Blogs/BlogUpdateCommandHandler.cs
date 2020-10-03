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
    /// <summary>
    /// BlogUpdateCommandHandler
    /// </summary>
    public class BlogUpdateCommandHandler : CommandHandler, IRequestHandler<BlogUpdateCommand, bool>
    {
        private readonly IRepository<Blog> _blogRepository;
        public BlogUpdateCommandHandler(IUnitOfWork uow, IRepository<Blog> blogRepository) : base(uow)
        {
            _blogRepository = blogRepository;
        }
        public async Task<bool> Handle(BlogUpdateCommand request, CancellationToken cancellationToken)
        {
            var update = await _blogRepository.GetByIdAsync(request.Id);
            update.Name = request.Name;
            update.Url = request.Url;
            _blogRepository.Update(update);
            return await CommitAsync();
        }
    }
}
