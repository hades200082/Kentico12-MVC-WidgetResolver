using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Distinction.Kentico12.MVC.WidgetResolver
{
    public class WidgetRegistry : IWidgetRegistry
    {
        private List<WidgetRegistration> Registry { get; set; }

        public WidgetRegistry()
        {
            Registry = new List<WidgetRegistration>();
        }

        /// <inheritdoc />
        public IWidgetRegistry RegisterWidget<TWidgetModel>(string codename, Func<JObject, JObject> beforeAction = null, Func<IWidgetModel, IWidgetModel> afterAction = null)
            where TWidgetModel : IWidgetModel
        {
            if (Registry.Any(x => x.Codename == codename))
            {
                var item = Registry.First(x => x.Codename == codename);
                Registry.Remove(item);
            }

            Registry.Add(new WidgetRegistration(codename, typeof(TWidgetModel), beforeAction, afterAction));

            return this;
        }

        /// <inheritdoc />
        public Type GetWidgetType(string codename)
        {
            return Registry.FirstOrDefault(x => x.Codename == codename)?.Type;
        }

        /// <inheritdoc />
        public Delegate GetBeforeAction(string codename)
        {
            return Registry.FirstOrDefault(x => x.Codename == codename)?.BeforeAction;
        }

        /// <inheritdoc />
        public Delegate GetAfterAction(string codename)
        {
            return Registry.FirstOrDefault(x => x.Codename == codename)?.AfterAction;
        }
    }
}