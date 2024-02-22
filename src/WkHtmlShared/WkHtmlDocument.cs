using WkHtmlShared.Options;

namespace WkHtmlShared
{
    public class WkHtmlDocument
    {
        public WkHtmlDocument()
        {
            GlobalOptions = new GlobalOptions();
        }

        public WkHtmlDocument(string htmlContent) : this()
        {
            HtmlContent = htmlContent;
        }

        /// <summary>
        /// Html ou url
        /// </summary>
        public string HtmlContent { get; set; }

        public GlobalOptions GlobalOptions { get; set; }
    }
}