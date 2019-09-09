using OrchardCore.ContentManagement;

namespace OrchardCore.Menu.Models
{
    public class LinkMenuItemPart : ContentPart
    {
        /// <summary>
        /// The name of the link
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If the Property IsCustomUrl is true, the url of the link to create.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// if the url is customized.
        /// </summary>
        public bool IsCustomUrl { get; set; }

        /// <summary>
        /// if the Property IsCustomUrl is false, the Attached ContentItem will be created.
        /// </summary>
        public ContentItem AttachedContentItem { get; set; }
    }
}
