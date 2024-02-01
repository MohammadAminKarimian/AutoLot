using AutoLot.Dal.EfStructures;
using AutoLot.Models.Entities.Base;

namespace AutoLot.Dal.Repos.Base;

public abstract class BaseRepo<T> : BaseViewRepo<T>, IBaseRepo<T> where T : BaseEntity, new()
{
    protected BaseRepo(ApplicationDbContext context) : base(context) { }

    protected BaseRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options)) { }

    // Common Read Methods.

    public virtual T Find(int? id)
    {
        return Table.Find(id);
    }

    public virtual T FindAsNoTracking(int id)
    {
        return Table.AsNoTrackingWithIdentityResolution().FirstOrDefault(x => x.Id == id);
    }

    public virtual T FindIgnoreQueryFilters(int id)
    {
        return Table.IgnoreQueryFilters().FirstOrDefault(x => x.Id == id);
    }

    public void ExecuteParameterizedQuery(string sql, object[] sqlParametersObjects)
    {
        Context.Database.ExecuteSqlRaw(sql, sqlParametersObjects);
    }

    // Add, Update, Delete Methods.

    public int Add(T entity, bool persist = true)
    {
        Table.Add(entity);
        return persist ? SaveChanges() : 0;
    }

    public int AddRange(IEnumerable<T> entities, bool persist = true)
    {
        Table.AddRange(entities);
        return persist ? SaveChanges() : 0;
    }

    public int Update(T entity, bool persist = true)
    {
        Table.Update(entity);
        return persist ? SaveChanges() : 0;
    }

    public int UpdateRange(IEnumerable<T> entities, bool persist = true)
    {
        Table.AddRange(entities);
        return persist ? SaveChanges() : 0;
    }

    public int Delete(int id, byte[] timeStamp, bool persist = true)
    {
        var entity = new T { Id = id, TimeStamp = timeStamp };
        Context.Entry(entity).State = EntityState.Deleted;
        return persist ? SaveChanges() : 0;
    }

    public int Delete(T entity, bool persist = true)
    {
        Table.Remove(entity);
        return persist ? SaveChanges() : 0;
    }

    public int DeleteRange(IEnumerable<T> entities, bool persist = true)
    {
        Table.RemoveRange(entities);
        return persist ? SaveChanges() : 0;
    }

    public int SaveChanges()
    {
        try
        {
            return Context.SaveChanges();
        }
        catch (CustomException)
        {
            //Should handle intelligently - already logged
            throw;
        }
        catch (Exception ex)
        {
            //Should log and handle intelligently
            throw new CustomException("An error occurred updating the database", ex);
        }
    }
}
