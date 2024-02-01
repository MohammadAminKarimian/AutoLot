namespace AutoLot.Api.Controllers;

public class CustomersController : BaseCrudController<Customer, CustomersController>
{
    protected readonly ICustomerRepo CustomerRepo;
    public CustomersController(IAppLogging<CustomersController> logger, ICustomerRepo repo) : base(repo, logger)
    {
        CustomerRepo = repo;
    }
}
