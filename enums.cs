using System;

namespace LeeConlin.Kentico12.MVC.WidgetResolver
{
    [Flags]
    public enum UnknownWidgetBehaviour
    {
        Ignore = 0,
        WriteErrorInline = 1,
        WriteErrorToLog = 2,
        ThrowException = 4
    }
}