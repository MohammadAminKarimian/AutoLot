namespace AutoLot.Mvc.TagHelpers;

public class ItemListTagHelper : ItemLinkTagHelperBase
{
    public ItemListTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory) : base(contextAccessor, urlHelperFactory)
    {
        ActionName = nameof(CarsController.IndexAsync).RemoveAsyncSuffix();
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //<a asp-action="Index" class="text-default”>Back to list <i class="fas fa-list"></i></a>
        BuildContent(output, "text-default", "Back to List", "list");
        //<item-list></item-list>
    }
}