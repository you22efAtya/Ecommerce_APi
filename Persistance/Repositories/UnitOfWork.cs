namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();

        }

        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey> => 
            (IGenericRepository<TEntity,Tkey>)_repositories.GetOrAdd(typeof(TEntity).Name,(_)=> new GenericRepository<TEntity,Tkey>(_dbContext));

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
