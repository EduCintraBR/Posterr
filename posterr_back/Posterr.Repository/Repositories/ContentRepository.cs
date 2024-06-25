using Microsoft.EntityFrameworkCore;
using Posterr.Domain.Entities;
using Posterr.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Posterr.Repository.Repositories {
    public class ContentRepository : RepositoryBase<Content>, IRepositoryBase<Content> {

        public ContentRepository(AppDataContext context) : base(context) { }

        public IEnumerable<Content> ListByDateDescending(int offset, int limit) {

            var result = this.Context
                .Content
                .Include(x => x.User)
                .Include(x => x.ContentPost)
                .Include(x => x.ContentPost.User)
                .OrderByDescending(x => x.Date)
                .Skip(offset)
                .Take(limit);

            return result;

        }

        public IEnumerable<Content> ListByRepostsDescending(int offset, int limit) {

            var result = this.Context
                .Content
                .Include(x => x.User)
                .Include(x => x.ContentPost)
                .Include(x => x.ContentPost.User)
                .Where(x => x.Action == Domain.Enums.EContentAction.Post)
                .OrderByDescending(x => x.ContentPost.RepostCount)
                .Skip(offset)
                .Take(limit);

            return result;


        }

        public IEnumerable<Content> ListSearchByKeyword(int offset, int limit, string keyword) {
            var result = this.Context
                .Content
                .Include(x => x.User)
                .Include(x => x.ContentPost)
                .Include(x => x.ContentPost.User)
                .Where(x => x.Action == Domain.Enums.EContentAction.Post)
                .OrderByDescending(x => x.Date)
                .Skip(offset)
                .Take(limit)
                .ToList();

            var regex = new Regex($@"\b{Regex.Escape(keyword)}\b", RegexOptions.IgnoreCase);

            var filteredResult = result.Where(x => regex.IsMatch(x.ContentPost.Content));

            return filteredResult;

        }

        public override IEnumerable<Content> Get(Expression<Func<Content, bool>> where, bool tracking = false)
        {
            return base.Get(where, tracking);
        }

        public override Content? GetFirstOrDefault(Expression<Func<Content, bool>> where, bool tracking = false)
        {
            return base.GetFirstOrDefault(where, tracking);
        }
    }
}
