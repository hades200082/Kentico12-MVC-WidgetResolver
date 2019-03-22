using System;

namespace LeeConlin.Kentico12.MVC.WidgetResolver
{
    public class RichTextHtml : IWidgetModel, IComparable<string>, IEquatable<string>
    {
        public RichTextHtml(string data)
        {
            HtmlStringData = data;
        }

        private string HtmlStringData { get; }

        public static implicit operator RichTextHtml(string data)
        {
            return new RichTextHtml(data);
        }
        public static implicit operator string(RichTextHtml data)
        {
            return data.ToString();
        }

        /// <inheritdoc />
        public int CompareTo(string other)
        {
            return string.Compare(HtmlStringData, other, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return HtmlStringData;
        }
        

        public bool Equals(string other)
        {
            return string.Equals(HtmlStringData, other);
        }

        protected bool Equals(RichTextHtml other)
        {
            return string.Equals(HtmlStringData, other.HtmlStringData);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RichTextHtml) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (HtmlStringData != null ? HtmlStringData.GetHashCode() : 0);
        }
    }
}