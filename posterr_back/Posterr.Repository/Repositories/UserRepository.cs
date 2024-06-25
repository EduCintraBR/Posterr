using Posterr.Domain.Entities;
using Posterr.Domain.Interfaces;
using System.Linq.Expressions;

namespace Posterr.Repository.Repositories {
    public class UserRepository : RepositoryBase<User>, IRepositoryBase<User>
    {

        public UserRepository(AppDataContext context) : base(context) { }

        public override User? GetFirstOrDefault(Expression<Func<User, bool>> where, bool tracking = false)
        {
            return base.GetFirstOrDefault(where, tracking);
        }
    }
}
