namespace AutoLot.Api.Controllers;

public class OrdersController : BaseCrudController<Order, OrdersController>
{
    protected readonly IOrderRepo OrderRepo;
    public OrdersController(IAppLogging<OrdersController> logger, IOrderRepo repo)
        : base(repo, logger)
    {
        OrderRepo = repo;
    }
}