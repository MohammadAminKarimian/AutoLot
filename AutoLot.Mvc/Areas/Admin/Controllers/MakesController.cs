namespace AutoLot.Mvc.Areas.Admin.Controllers;

[Area("admin")]
[Route("admin/[controller]/[action]")]
public class MakesController : BaseCrudController<Make, MakesController>
{
    public MakesController(IAppLogging<MakesController> logger, IMakeDataService makeDataService)
        : base(logger, makeDataService) { }

    protected override async Task<SelectList> GetLookupValuesAsync()
    {
        // Prefered way to return null from async methods.
        return await Task.FromResult<SelectList>(null);
    }

    [Route("/admin")]
    [Route("/admin/[controller]")]
    [Route("/admin/[controller]/[action]")]
    public override async Task<IActionResult> IndexAsync()
    {
        return await base.IndexAsync();
    }

}
