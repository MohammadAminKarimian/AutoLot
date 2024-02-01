namespace AutoLot.Mvc.TagHelpers;

public class ItemListByMakeTagHelper : ItemLinkTagHelperBase
{
    private readonly string _controllerName;
    public int? MakeId { get; set; }
    public string MakeName { get; set; }
    public ItemListByMakeTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory)
    {
        ActionName = nameof(CarsController.ByMakeAsync).RemoveAsyncSuffix();
        _controllerName = contextAccessor.ActionContext.ActionDescriptor.RouteValues["controller"];
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        var target = UrlHelper.Action(ActionName, _controllerName, new { makeId = MakeId , makeName = MakeName});
        output.Attributes.SetAttribute("href", target);
        output.Attributes.Add("class", "text-default");
        output.Content.AppendHtml($@"{MakeName}s List <i class=""fas fa-list""></i>");
    }
}
