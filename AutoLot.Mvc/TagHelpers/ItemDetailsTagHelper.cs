namespace AutoLot.Mvc.TagHelpers;

public class ItemDetailsTagHelper : ItemLinkTagHelperBase
{
    public ItemDetailsTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory)
    {
        ActionName = nameof(CarsController.DetailAsync).RemoveAsyncSuffix();    // ActionName = Detail
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //<a asp-action="Detail" asp-route-id="@item.Id" class="text-info">Details <i class="fas fa-info-circle"></i></a>
        BuildContent(output, "text-info", "Detail", "info-circle");
        //<item-details item-id="@item.Id"></item-details>
    }
}
