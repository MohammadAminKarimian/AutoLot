using AutoLot.Dal.EfStructures;

namespace AutoLot.Dal.Repos.Base;

public abstract class BaseViewRepo<T> : IBaseViewRepo<T> where T : class, new()
{
    private readonly bool _disposeContext;
    private bool _isDisposed;

    public ApplicationDbContext Context { get; }
    public DbSet<T> Table { get; }

    protected BaseViewRepo(ApplicationDbContext context)
    {
        Context = context;
        Table = Context.Set<T>();
        _disposeContext = false;
    }

    protected BaseViewRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options))
    {
        _disposeContext = true;
    }

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }
        if (disposing)
        {
            if (_disposeContext)
            {
                Context.Dispose();
            }
        }
        _isDisposed = true;
    }

    ~BaseViewRepo()
    {
        Dispose(false);
    }

    public IEnumerable<T> ExecuteSqlString(string sql)
    {
        return Table.FromSqlRaw(sql);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return Table.AsQueryable();
    }

    public virtual IEnumerable<T> GetAllIgnoreQueryFilters()
    {
        return Table.AsQueryable().IgnoreQueryFilters();
    }
}
