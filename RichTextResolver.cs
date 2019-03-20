using System;
using System.Globalization;
using System.Text.RegularExpressions;
using CMS.Base;
using CMS.Helpers.Caching.Abstractions;
using Microsoft.Web.Services3.Referral;
using Newtonsoft.Json.Linq;

namespace Distinction.Kentico12.MVC.WidgetResolver
{
    public class RichTextResolver : IRichTextResolver
    {
        public IWidgetResolver WidgetResolver { get; }
        public IWidgetRegistry WidgetRegistry { get; }
        private const string WIDGET_REGEX = @"\{\^widget\|(.+)\^\}";
        private const string WIDGET_INTERNAL_REGEX = @"^\(([a-zA-Z0-9]+)\)(.+)$";

        public RichTextResolver(IWidgetResolver widgetResolver, IWidgetRegistry widgetRegistry)
        {
            WidgetResolver = widgetResolver;
            WidgetRegistry = widgetRegistry;
        }


        // TODO Finish implementing Resolve
        // TODO Add caching
        // 

        /// <inheritdoc />
        public IRichTextData Resolve(string richText,
            UnknownWidgetBehaviour unknownWidgetBehaviour = UnknownWidgetBehaviour.WriteErrorInline)
        {
            var rt = new RichTextData();

            var lastStartIndex = 0;
            var match = Regex.Match(richText, WIDGET_REGEX);
            while (match.Success)
            {
                var widgetText = match.Groups[1].Value;
                var widgetObj = ParseWidgetText(widgetText);

                // Get any text prior to the widget
                var rawHtml = richText.Substring(lastStartIndex, richText.IndexOf(match.Value, StringComparison.Ordinal) - lastStartIndex);
                rt.Add(new RichTextHtml(rawHtml));

                var codename = widgetObj.Property("name").Value.ToString();
                var type = WidgetRegistry.GetWidgetType(codename);

                if (type != null)
                {
                    rt.Add(WidgetResolver.Resolve(widgetObj, type));
                }

                lastStartIndex = lastStartIndex + rawHtml.Length + match.Value.Length;
                match = match.NextMatch();
            }

            var lastRawHtml = richText.Substring(lastStartIndex);
            rt.Add(new RichTextHtml(lastRawHtml));

            return rt;
        }


        private JObject ParseWidgetText(string widgetText)
        {
            var obj = new JObject();
            var split = widgetText.Split('|');

            foreach (var field in split)
            {
                var match = Regex.Match(field, WIDGET_INTERNAL_REGEX);
                if (match.Success)
                {
                    if(match.Groups.Count < 3) continue;

                    var fieldName = match.Groups[1].Value;
                    var value = match.Groups[2].Value;

                    if (int.TryParse(value, NumberStyles.Integer, null, out int iVal))
                    {
                        obj.Add(fieldName,iVal);
                    }
                    else if (decimal.TryParse(value, NumberStyles.Float, null, out decimal dVal))
                    {
                        obj.Add(fieldName, dVal);
                    }
                    else if (bool.TryParse(value, out bool bVal))
                    {
                        obj.Add(fieldName, bVal);
                    }
                    else
                    {
                        // Otherwise just add it as a string
                        obj.Add(fieldName, value);
                    }
                }
            }
            return obj;
        }

    }
}