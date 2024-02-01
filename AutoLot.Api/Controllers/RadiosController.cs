namespace AutoLot.Api.Controllers;

public class RadiosController : BaseCrudController<Radio, RadiosController>
{
    protected readonly IRadioRepo RadioRepo;
    public RadiosController(IAppLogging<RadiosController> logger, IRadioRepo repo)
        : base(repo, logger)
    {
        RadioRepo = repo;
    }
}