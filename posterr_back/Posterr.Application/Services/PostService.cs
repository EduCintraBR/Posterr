using Posterr.Domain.Entities;
using Posterr.Domain.Interfaces;
using Posterr.Repository.Repositories;

namespace Posterr.Application.Services {
    public class PostService : ServiceBase<Post>, IServiceBase<Post> {

        public PostService(PostRepository postRepository) : base(postRepository) { }

    }
}
