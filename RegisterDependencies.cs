using System.Web.Mvc;
using Autofac;

namespace Distinction.Kentico12.MVC.WidgetResolver
{
    public static class RegisterDependencies
    {
        public static void RegisterWidgetResolver(this ContainerBuilder builder)
        {
            builder.RegisterType<RichTextResolver>()
                .AsSelf()
                .As<IRichTextResolver>()
                .SingleInstance();

            builder.RegisterType<WidgetResolver>()
                .AsSelf()
                .As<IWidgetResolver>()
                .SingleInstance();

            builder.RegisterType<WidgetRegistry>()
                .AsSelf()
                .As<IWidgetRegistry>()
                .SingleInstance();
        }
    }
}
