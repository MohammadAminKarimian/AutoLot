namespace AutoLot.Mvc.Controllers;

public class CarsController : BaseCrudController<Car, CarsController>
{
    private readonly IMakeDataService _makeDataService;
    public CarsController(IAppLogging<CarsController> logger, ICarDataService carDataService,
        IMakeDataService makeDataService) : base(logger, carDataService)
    {
        _makeDataService = makeDataService;
    }

    protected override async Task<SelectList> GetLookupValuesAsync()
        => new SelectList(await _makeDataService.GetAllAsync(), nameof(Make.Id), nameof(Make.Name));

    [HttpGet("{makeId}/{makeName}")]
    public async Task<IActionResult> ByMakeAsync(int makeId, string makeName)
    {
        ViewBag.MakeName = makeName;
        return View(await ((ICarDataService)MainDataService).GetAllByMakeIdAsync(makeId));
    }

    public IActionResult Index()
    {
        return View();
    }
}
