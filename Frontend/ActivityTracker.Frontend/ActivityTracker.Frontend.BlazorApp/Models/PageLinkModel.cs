namespace ActivityTracker.Frontend.BlazorApp.Models
{
    public class PageLinkModel
    {
        /// <summary>
        /// The text on the button
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The page index
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// If the link is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; }
        
        public PageLinkModel(int page, bool enabled, string text)
        {
            Page = page;
            Enabled = enabled;
            Text = text;
        }
    }
}
