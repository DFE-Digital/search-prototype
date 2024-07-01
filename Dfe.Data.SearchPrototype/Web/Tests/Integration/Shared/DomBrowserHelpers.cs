using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace DfE.Data.SearchPrototype.Test.Shared;

public static class DomBrowserHelpers
{
    public static IHtmlAnchorElement GetHeaderLink(this IDocument response, string linkName)
    {
        var header = response.GetElementsByTagName("header").Single();
        var anchorTags = header.GetElementsByTagName("a");
        return (IHtmlAnchorElement)anchorTags.Single(anchorTags => anchorTags.TextContent.Contains(linkName));
    }
}
