using B.BlazorPro.Linkify.Common;

namespace B.BlazorPro.Linkify.Events
{
    public class ClickEventArgs
    {
        public LinkType Type { get; set; }
        public string Value { get; set; }
    }
}