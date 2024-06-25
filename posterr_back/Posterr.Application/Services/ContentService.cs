using Posterr.Application.Models;
using Posterr.Application.Models.Requests;
using Posterr.Domain.Entities;
using Posterr.Domain.Interfaces;
using Posterr.Repository.Repositories;
using System.Linq.Expressions;

namespace Posterr.Application.Services {
    public class ContentService : ServiceBase<Content>, IServiceBase<Content> {

        private readonly ContentRepository _contentRepository;
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepository;

        public ContentService(
            ContentRepository contentRepository, 
            UserRepository userRepository, 
            PostRepository postRepository) 
            : base(contentRepository) { 
            _contentRepository = contentRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public GenericResult AddPost(PostAddRequest request) {

            var user = _userRepository.GetFirstOrDefault(x => x.Identifier == request.UserID);

            if (user == null)
                return new GenericResult(false, "Informed user doesn't exists.");

            if (request.Content.Trim().Length == 0)
                return new GenericResult(false, "Post content can not be blank.");

            var postByUsersInDay = this._contentRepository.Get(x => x.UserID == request.UserID && x.Date.Date == DateTime.Today).Count();
            if (postByUsersInDay >= 5)
                return new GenericResult(false, "User has reached the limit of 5 daily posts.");

            var post = new Post() {
                Identifier = Guid.NewGuid(),
                UserID = user.Identifier,
                CreatedAt = DateTime.Now,

                Content = request.Content
            };

            var content = new Content() {
                Identifier = Guid.NewGuid(),
                ContentPostID = post.Identifier,
                Action = Domain.Enums.EContentAction.Post,
                Date = post.CreatedAt,
                UserID = user.Identifier
            };

            _postRepository.Add(post);
            _postRepository.Commit();

            this.Add(content);
            this.Commit();

            return new GenericResult(true);

        }

        public GenericResult Repost(PostRepostRequest request) {

            var user = _userRepository.GetFirstOrDefault(x => x.Identifier == request.UserID);
            if (user == null)
                return new GenericResult(false, "Informed user doesn't exists.");

            var post = _postRepository.GetFirstOrDefault(x => x.Identifier == request.PostID, true);
            if (post == null)
                return new GenericResult(false, "Informed post doesn't exists.");

            if (post.UserID == user.Identifier)
                return new GenericResult(false, "You cannot repost a post created by yourself.");

            var userRepost = this._contentRepository.GetFirstOrDefault(x => x.UserID == request.UserID && x.ContentPostID == request.PostID);
            if (userRepost != null)
                return new GenericResult(false, $"User already reposted at {userRepost.Date.ToString()}.");

            var postByUsersInDay = this._contentRepository.Get(x => x.UserID == request.UserID && x.Date.Date == DateTime.Today).Count();
            if (postByUsersInDay >= 5)
                return new GenericResult(false, "User has reached the limit of 5 daily posts.");

            var content = new Content() {
                Identifier = Guid.NewGuid(),
                ContentPostID = post.Identifier,
                Action = Domain.Enums.EContentAction.Repost,
                Date = DateTime.Now,
                UserID = user.Identifier
            };

            this.Add(content);
            this.Commit();

            post.RepostCount += 1;
            _postRepository.Commit();

            return new GenericResult(true);

        }

        public IEnumerable<Content> ListByDateDescending(int offset, int limit) 
            => _contentRepository.ListByDateDescending(offset, limit);
        public IEnumerable<Content> ListByRepostsDescending(int offset, int limit) 
            => _contentRepository.ListByRepostsDescending(offset, limit);

        public IEnumerable<Content> ListSearchByKeyword(int offset, int limit, string keyword) 
            => _contentRepository.ListSearchByKeyword(offset, limit, keyword);
    }
}
