namespace AutoLot.Api.Controllers;

public class DriversController : BaseCrudController<Driver, DriversController>
{
    protected readonly IDriverRepo DriverRepo;
    public DriversController(IAppLogging<DriversController> logger, IDriverRepo repo)
        : base(repo, logger)
    {
        DriverRepo = repo;
    }
}