namespace AutoLot.Api.Controllers;

public class CarDriversController : BaseCrudController<CarDriver, CarDriversController>
{
    protected readonly ICarDriverRepo CarDriverRepo;
    public CarDriversController(IAppLogging<CarDriversController> logger, ICarDriverRepo repo) : base(repo, logger) 
    {
        CarDriverRepo = repo;
    }
}
