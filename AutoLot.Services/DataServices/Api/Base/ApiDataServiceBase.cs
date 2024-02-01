namespace AutoLot.Services.DataServices.Api.Base;

public abstract class ApiDataServiceBase<TEntity, TDataService> : IDataServiceBase<TEntity>
    where TEntity : BaseEntity, new()
    where TDataService : IDataServiceBase<TEntity>
{
    protected readonly IApiServiceWrapperBase<TEntity> ServiceWrapper;
    protected readonly IAppLogging<TDataService> AppLoggingInstance;
    protected ApiDataServiceBase(IAppLogging<TDataService> loggingInstance, IApiServiceWrapperBase<TEntity> serviceWrapperBase)
    {
        ServiceWrapper = serviceWrapperBase;
        AppLoggingInstance = loggingInstance;
    }

    public async Task<TEntity> AddAsync(TEntity entity, bool persist = true)
    {
        return await ServiceWrapper.AddEntityAsync(entity);
    }

    public async Task DeleteAsync(TEntity entity, bool persist = true)
    {
        await ServiceWrapper.DeleteEntityAsync(entity);
    }

    public async Task<TEntity> FindAsync(int id)
    {
        return await ServiceWrapper.GetEntityAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await ServiceWrapper.GetAllEntitiesAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, bool persist = true)
    {
        return await ServiceWrapper.UpdateEntityAsync(entity);
    }
}
