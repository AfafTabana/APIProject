using APIProject.Models;
namespace APIProject.Repository
{
    public class GenericRepository<TEntity>  where TEntity :  class
    {
        SystemMangmentContext context;
        public GenericRepository(SystemMangmentContext  _context )
        {
            this.context  = _context;

        }

        //get all func 
        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public  TEntity ? GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }
        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Edit(TEntity entity, int id)
        {
            var trackedEntity = context.Set<TEntity>().Find(id);
            if (trackedEntity != null)
            {
                context.Entry(trackedEntity).CurrentValues.SetValues(entity);
            }
        }

        public void Remove(int id)
        {
            TEntity entity = GetById(id);
            if (entity != null)
            {
                context.Set<TEntity>().Remove(entity);
            }
           

        }

       

    }
}
