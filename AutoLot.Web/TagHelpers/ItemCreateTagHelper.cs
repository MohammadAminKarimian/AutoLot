namespace AutoLot.Web.TagHelpers;

public class ItemCreateTagHelper : ItemLinkTagHelperBase
{
    public ItemCreateTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory helperFactory) : base(contextAccessor, helperFactory)
    {
        ActionName = "Create";
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        /*<a asp-page="Cars/Create" class="text-warning">Create New <i class="fas fa-plus"></i></a>*/
        BuildContent(output, "text-success", "Create New", "plus");
        /*<item-create></item-create>*/
    }
}
