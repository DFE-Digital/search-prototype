using AngleSharp.Dom;

namespace DfE.Data.SearchPrototype.Test.PageModels;

public class HtmlElement
{
    protected IElement _element;

    public HtmlElement(IElement element)
    {
        _element = element;
    }

    public string? Id { get => _element.Id; }
    public string? Class { get => _element.ClassName; }
    public string InnerHtml { get => _element.InnerHtml; }
}
