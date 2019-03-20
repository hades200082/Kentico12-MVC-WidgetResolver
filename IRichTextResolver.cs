namespace Distinction.Kentico12.MVC.WidgetResolver
{
    public interface IRichTextResolver
    {
        IRichTextData Resolve(string richText, UnknownWidgetBehaviour unknownWidgetBehaviour = UnknownWidgetBehaviour.WriteErrorInline);
    }
}