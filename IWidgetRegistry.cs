using System;
using Newtonsoft.Json.Linq;

namespace Distinction.Kentico12.MVC.WidgetResolver
{
    public interface IWidgetRegistry
    {
        IWidgetRegistry RegisterWidget<TWidgetModel>(string codename, Func<JObject, JObject> beforeAction = null, Func<IWidgetModel, IWidgetModel> afterAction = null) where TWidgetModel : IWidgetModel;
        Type GetWidgetType(string codename);
        Delegate GetBeforeAction(string codename);
        Delegate GetAfterAction(string codename);

    }
}