namespace AutoLot.Web.TagHelpers;

public class ItemEditTagHelper : ItemLinkTagHelperBase
{
    public ItemEditTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory helperFactory) : base(contextAccessor, helperFactory)
    {
        ActionName = "Edit";
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        /*<a asp-page="Edit" asp-route-id="5" class="text-warning">Edit <i class="fas fa-edit"></i></a>*/
        BuildContent(output, "text-warning", "Edit", "edit");
        /*<item-edit item-id="@item.Id"></item-edit>*/
    }
}
