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

    public static IElement Header(this IDocument response) =>
        response.GetElementsByTagName("header").Single();

    public static IElement Heading(this IDocument response) =>
        response.GetElementsByTagName("h1").Single();

    public static IHtmlAnchorElement AnchorTagWithName(this IElement element, string linkName) =>
        (IHtmlAnchorElement)element
            .GetElementsByTagName("a")
            .Single(anchorTags => anchorTags.TextContent.Contains(linkName));
}
