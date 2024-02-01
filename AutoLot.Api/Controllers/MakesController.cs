namespace AutoLot.Api.Controllers;

public class MakesController : BaseCrudController<Make, MakesController>
{
    protected readonly IMakeRepo MakeRepo;
    public MakesController(IAppLogging<MakesController> logger, IMakeRepo repo)
        : base(repo, logger)
    {
        MakeRepo = repo;
    }
}
