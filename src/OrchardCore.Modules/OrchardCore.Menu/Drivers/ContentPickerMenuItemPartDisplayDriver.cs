using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Menu.Models;
using OrchardCore.Menu.ViewModels;

namespace OrchardCore.Menu.Drivers
{
    public class ContentPickerMenuItemPartDisplayDriver : ContentPartDisplayDriver<ContentPickerMenuItemPart>
    {
        private readonly IContentManager _contentManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public ContentPickerMenuItemPartDisplayDriver(
            IContentManager contentManager,
            IContentDefinitionManager contentDefinitionManager
            )
        {
            _contentManager = contentManager;
            _contentDefinitionManager = contentDefinitionManager;
        }

        public override IDisplayResult Display(ContentPickerMenuItemPart part, BuildPartDisplayContext context)
        {
            return Combine(
                Dynamic("ContentPickerMenuItemPart_Admin", shape =>
                {
                    shape.MenuItemPart = part;
                })
                .Location("Admin", "Content:10"),
                Dynamic("ContentPickerMenuItemPart_Thumbnail", shape =>
                {
                    shape.MenuItemPart = part;
                })
                .Location("Thumbnail", "Content:10")
            );
        }

        public override IDisplayResult Edit(ContentPickerMenuItemPart part)
        {
            return Initialize<ContentPickerMenuItemPartEditViewModel>("ContentPickerMenuItemPart_Edit", model =>
            {
                model.Name = part.Name;
                model.Url = part.Url;
                model.MenuItemPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPickerMenuItemPart part, IUpdateModel updater)
        {
            //Update Part ContentItemIds
            var contentTypePartDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType).Parts
               .FirstOrDefault(x => x.Name == nameof(ContentPickerMenuItemPart));
            var contentItemIdsPrefix = contentTypePartDefinition.Name + "." + contentTypePartDefinition.PartDefinition.Fields.FirstOrDefault().Name;
            await updater.TryUpdateModelAsync(part, contentItemIdsPrefix, x => x.ContentItemIds);

            //Update Part Name
            await updater.TryUpdateModelAsync(part, Prefix, x => x.Name);

            return Edit(part);
        }
    }
}