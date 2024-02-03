namespace AutoLot.Web.TagHelpers;

public class ItemListTagHelper : ItemLinkTagHelperBase
{
    public ItemListTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory helperFactory) : base(contextAccessor, helperFactory)
    {
        ActionName = "IndexAsync";
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        /*<a asp-page="Cars/Index" class="text-default”>Back to list <i class="fas fa-list"></i></a>*/
        BuildContent(output, "text-default", "Back to List", "list");
        /*<item-list></item-list>*/
    }
}
