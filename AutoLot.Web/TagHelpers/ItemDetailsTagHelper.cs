namespace AutoLot.Web.TagHelpers;

public class ItemDetailsTagHelper : ItemLinkTagHelperBase
{
    public ItemDetailsTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory helperFactory) : base(contextAccessor, helperFactory)
    {
        ActionName = "Details";
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //< a asp-page = "Cars/Details" asp-route-id = "5" class="text-info">Details<i class="fas fa-info-circle"></i></a>
        BuildContent(output, "text-info", "Details", "info-circle");
        //< item-details item-id = "@item.Id" ></ item-details >
    }

}
