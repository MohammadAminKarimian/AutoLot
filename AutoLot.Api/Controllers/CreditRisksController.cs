namespace AutoLot.Api.Controllers;

public class CreditRisksController : BaseCrudController<CreditRisk, CreditRisksController>
{
    protected readonly ICreditRiskRepo CreditRiskRepo;
    public CreditRisksController(IAppLogging<CreditRisksController> logger, ICreditRiskRepo repo) : base(repo, logger)
    {
        CreditRiskRepo = repo;
    }
}
