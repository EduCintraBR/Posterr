using Posterr.Domain.Interfaces;
using System.Linq.Expressions;

namespace Posterr.Application.Services {
    public abstract class ServiceBase<Entity> : IServiceBase<Entity> {

        internal IRepositoryBase<Entity> Repository { get; set; }

        internal ServiceBase(IRepositoryBase<Entity> repositoryBase) {
            Repository = repositoryBase;
        }

        public int Commit() => Repository.Commit();
        public void Add(Entity entity) => Repository.Add(entity);
        public void Update(Entity entity) => Repository.Update(entity);
        public void Delete(Entity entity) => Repository.Delete(entity);
        public IEnumerable<Entity> GetAll() => Repository.GetAll();
        public IEnumerable<Entity> Get(Expression<Func<Entity, bool>> where, bool tracking = false) => Repository.Get(where, tracking);
        public virtual Entity? GetFirstOrDefault(Expression<Func<Entity, bool>> where, bool tracking = false) => Repository.GetFirstOrDefault(where, tracking);

    }
}
