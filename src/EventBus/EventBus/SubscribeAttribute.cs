namespace EventBus
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SubscribeAttribute : Attribute
    {
        public string EventName { get; }
        public bool IsDynamic { get; }
        public string NameSpace { get; }
        public SubscribeAttribute(string eventName, bool isDynamic = false, string nameSpace = "")
        {
            EventName = eventName;
            IsDynamic = isDynamic;
            NameSpace = nameSpace;
        }
    }
}
