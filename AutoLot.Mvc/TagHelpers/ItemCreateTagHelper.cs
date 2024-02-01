using System;

namespace AutoLot.Mvc.TagHelpers;

public class ItemCreateTagHelper : ItemLinkTagHelperBase
{
    public ItemCreateTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory) 
        : base(contextAccessor, urlHelperFactory)
    {
        ActionName = nameof(CarsController.CreateAsync).RemoveAsyncSuffix();
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //< a asp - action = "Create" class="text-warning">Create New<I class="fas fa-plus"></i></a>
        BuildContent(output, "text-success", "Create New", "plus");
        //<item-create></item-create>
    }
}