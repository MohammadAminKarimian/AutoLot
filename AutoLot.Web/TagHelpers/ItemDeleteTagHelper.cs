namespace AutoLot.Web.TagHelpers;

public class ItemDeleteTagHelper : ItemLinkTagHelperBase
{
    public ItemDeleteTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory helperFactory) : base(contextAccessor, helperFactory)
    {
        ActionName = "Delete";
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //<a asp-page="Cars/Delete" asp-route-id="5" class="text-danger">Delete <i class="fas fa-trash"></i></a>
        BuildContent(output, "text-danger", "Delete", "trash");
        //<item-delete item-id="@item.Id"></item-delete>
    }
}
