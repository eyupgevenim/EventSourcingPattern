using System;
using System.Reflection;

namespace EventBus
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        public class SubscriptionInfo
        {
            public bool IsDynamic { get; }
            public Type HandlerType{ get; }
            public MethodInfo MethodInfo { get; }

            private SubscriptionInfo(bool isDynamic, Type handlerType)
            {
                IsDynamic = isDynamic;
                HandlerType = handlerType;
            }

            private SubscriptionInfo(bool isDynamic, MethodInfo methodInfo)
            {
                IsDynamic = isDynamic;
                HandlerType = methodInfo.DeclaringType;
                MethodInfo = methodInfo;
            }

            public static SubscriptionInfo Dynamic(Type handlerType)
            {
                return new SubscriptionInfo(true, handlerType);
            }
            public static SubscriptionInfo Typed(Type handlerType)
            {
                return new SubscriptionInfo(false, handlerType);
            }

            public static SubscriptionInfo Dynamic(MethodInfo methodInfo)
            {
                return new SubscriptionInfo(true, methodInfo);
            }
            public static SubscriptionInfo Typed(MethodInfo methodInfo)
            {
                return new SubscriptionInfo(false, methodInfo);
            }
        }

    }
}
