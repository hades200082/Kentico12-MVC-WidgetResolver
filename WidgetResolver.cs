using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeeConlin.Kentico12.MVC.WidgetResolver
{
    public class WidgetResolver : IWidgetResolver
    {
        private IWidgetRegistry WidgetRegistry { get; }

        public WidgetResolver(IWidgetRegistry widgetRegistry)
        {
            WidgetRegistry = widgetRegistry;
        }

        /// <inheritdoc />
        public TWidgetModel Resolve<TWidgetModel>(JObject parsedWidgetData) where TWidgetModel : IWidgetModel
        {
            var obj = parsedWidgetData
                .ToObject<TWidgetModel>(JsonSerializer.Create(new JsonSerializerSettings { ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor }));

            var codename = parsedWidgetData.Property("name").Value.ToString();
            var action = WidgetRegistry.GetAfterAction(codename);
            action.DynamicInvoke(obj);

            return obj;
        }
        /// <inheritdoc />
        public IWidgetModel Resolve(JObject parsedWidgetData, Type type)
        {
            var codename = parsedWidgetData.Property("name").Value.ToString();

            var beforeAction = WidgetRegistry.GetBeforeAction(codename);
            if(beforeAction != null) parsedWidgetData = (JObject)beforeAction.DynamicInvoke(parsedWidgetData);

            var obj = parsedWidgetData?.ToObject(type, JsonSerializer.Create(new JsonSerializerSettings { ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor }));
            
            var afterAction = WidgetRegistry.GetAfterAction(codename);
            if (afterAction != null) obj = afterAction.DynamicInvoke(obj);
            
            return (IWidgetModel) obj;
        }
        /// <inheritdoc />
        public TWidgetModel Resolve<TWidgetModel>(JObject parsedWidgetData, Func<JObject, TWidgetModel> outFunc) where TWidgetModel : IWidgetModel
        {
            var obj = outFunc.Invoke(parsedWidgetData);

            var codename = parsedWidgetData.Property("name").Value.ToString();
            var action = WidgetRegistry.GetAfterAction(codename);
            action.DynamicInvoke(obj);

            return obj;
        }

        /// <inheritdoc />
        public IWidgetModel Resolve(JObject parsedWidgetData, Func<JObject, IWidgetModel> outFunc)
        {
            var obj = outFunc.Invoke(parsedWidgetData);

            var codename = parsedWidgetData.Property("name").Value.ToString();
            var action = WidgetRegistry.GetAfterAction(codename);
            action.DynamicInvoke(obj);

            return (IWidgetModel)obj;
        }
    }
}