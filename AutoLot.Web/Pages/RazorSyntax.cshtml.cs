namespace AutoLot.Web.Pages;
public class RazorSyntaxModel : PageModel
{
    protected readonly ICarDataService _carDataService;

    [BindProperty]
    public Car Entity { get; set; }

    public RazorSyntaxModel(ICarDataService carDataService)
    {
        _carDataService = carDataService;
    }

    public async Task OnGetAsync()
    {
        Entity = await _carDataService.FindAsync(6);
    }

    //public async Task<IActionResult> OnGetAsync()
    //{
    //    Entity = await _carDataService.FindAsync(6);
    //    return Page();
    //}

    public async Task OnPostAsync()
    {

    }

    //  Using bound property (common practice)
    //public async Task<IActionResult> OnPostAsync()
    //{
    //    await _carDataService.UpdateAsync(Entity);
    //    return RedirectToPage("Index");
    //}

    //  Implicit binding
    //public async Task<IActionResult> OnPostAsync(Car entity)
    //{
    //    //do something interesting
    //    return RedirectToPage("Index");
    //}

    //  Explicit binding
    //public async Task<IActionResult> OnPostAsync()
    //{
    //    var newCar = new Car();
    //    if (await TryUpdateModelAsync(newCar, "Entity",
    //    c => c.Id,
    //    c => c.TimeStamp,
    //    c => c.PetName,
    //    c => c.Color,
    //    c => c.IsDrivable,
    //    c => c.MakeId,
    //    c => c.Price
    //    ))
    //    {
    //        //do something interesting
    //    }
    //}

}
