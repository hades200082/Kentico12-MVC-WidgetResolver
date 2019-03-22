using System;
using Newtonsoft.Json.Linq;

namespace LeeConlin.Kentico12.MVC.WidgetResolver
{
    public class WidgetRegistration
    {
        public WidgetRegistration(string codename, Type type, Func<JObject, JObject> beforeAction = null, Func<IWidgetModel, IWidgetModel> afterAction = null)
        {
            Codename = codename;
            Type = type;
            AfterAction = afterAction;
            BeforeAction = beforeAction;
        }

        public string Codename { get; }

        public Type Type { get; }

        public Func<IWidgetModel, IWidgetModel> AfterAction { get; set; }
        public Func<JObject, JObject> BeforeAction { get; set; }
    }
}