using Microsoft.EntityFrameworkCore;
using Posterr.Domain.Interfaces;
using System.Linq.Expressions;

namespace Posterr.Repository.Repositories {
    public abstract class RepositoryBase<Entity> : IRepositoryBase<Entity> where Entity : class {

        internal AppDataContext Context { get; set; }
        internal DbSet<Entity> Entities { get; set; }

        internal RepositoryBase(AppDataContext context) {

            Context = context;

            var entities = Context.GetType().GetProperties().Where(x => x.PropertyType == typeof(DbSet<Entity>)).FirstOrDefault();
            if (entities == null)
                throw new Exception($"Entity of type '{typeof(Entity).Name}' not founded on AppDataContext.");

            var entity = (DbSet<Entity>?) entities.GetValue(Context);

            if (entity == null)
                throw new Exception($"Entity of type '{typeof(Entity).Name}' not founded on AppDataContext.");

            Entities = entity;

        }

        public virtual int Commit() => Context.SaveChanges();

        public virtual void Add(Entity entity) {
            Entities.Add(entity);
        }

        public IEnumerable<Entity> GetAll() {
            return Entities.ToList();
        }


        public void Update(Entity entity) {
            Entities.Update(entity);
        }

        public void Delete(Entity entity) {
            Entities.Remove(entity);
        }

        public virtual IEnumerable<Entity> Get(Expression<Func<Entity, bool>> where, bool tracking = false) {

            var result = Entities.Where(where);

            if (tracking)
                result.AsTracking();
            else
                result.AsNoTracking();

            return result;

        }

        public virtual Entity? GetFirstOrDefault(Expression<Func<Entity, bool>> where, bool tracking = false) => Get(where, tracking).FirstOrDefault();
    }
}
