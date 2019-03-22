using System;
using Newtonsoft.Json.Linq;

namespace LeeConlin.Kentico12.MVC.WidgetResolver
{
    public interface IWidgetResolver
    {
        TWidgetModel Resolve<TWidgetModel>(JObject parsedWidgetData) where TWidgetModel : IWidgetModel;
        IWidgetModel Resolve(JObject parsedWidgetData, Type type);

        TWidgetModel Resolve<TWidgetModel>(JObject parsedWidgetData, Func<JObject, TWidgetModel> outFunc) where TWidgetModel : IWidgetModel;
        IWidgetModel Resolve(JObject parsedWidgetData, Func<JObject, IWidgetModel> outFunc);
    }
}