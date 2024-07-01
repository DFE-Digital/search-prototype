using AngleSharp.Dom;

namespace DfE.Data.SearchPrototype.Test.PageModels;

public class Link : HtmlElement
{
    public Link(IElement element) : base(element)
    {
    }

    public string? Href { get => _element.GetAttribute("href"); }
    public string? TextContent { get => _element.TextContent; }
}
