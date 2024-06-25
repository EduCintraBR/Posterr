using System.Linq.Expressions;

namespace Posterr.Domain.Interfaces {
    public interface IRepositoryBase<Entity> {

        int Commit();

        void Add(Entity entity);
        void Update(Entity entity);
        void Delete(Entity entity);
        IEnumerable<Entity> GetAll();

        IEnumerable<Entity> Get(Expression<Func<Entity, bool>> where, bool tracking = false);
        Entity? GetFirstOrDefault(Expression<Func<Entity, bool>> where, bool tracking = false);

    }
}
