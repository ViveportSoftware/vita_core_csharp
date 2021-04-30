using System;
using System.Threading;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public class EventBusTest
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var eventBus = EventBus.GetInstance();
            Assert.NotNull(eventBus);
        }

        [Fact]
        public static void Default_1_RegisterListener()
        {
            var eventBus = EventBus.GetInstance();
            Assert.NotNull(eventBus);
            var myEventListener = new MyEventListener();
            Assert.True(eventBus.RegisterListener<MyEventData>(myEventListener));
        }

        [Fact]
        public static void Default_2_UnregisterListener()
        {
            var eventBus = EventBus.GetInstance();
            Assert.NotNull(eventBus);
            var myEventListener = new MyEventListener();
            Assert.True(eventBus.RegisterListener<MyEventData>(myEventListener));
            Assert.True(eventBus.UnregisterListener<MyEventData>(myEventListener));
        }

        [Fact]
        public static void Default_3_Trigger()
        {
            var eventBus = EventBus.GetInstance();
            Assert.NotNull(eventBus);
            var myEventListener0 = new MyEventListener();
            var myEventListener1 = new MyEventListener();
            Assert.True(eventBus.RegisterListener<MyEventData>(myEventListener0));
            Assert.True(eventBus.RegisterListener<MyEventData>(myEventListener1));
            var myEventData = new MyEventData();
            Assert.NotNull(eventBus.Trigger<MyEventData>(myEventData));

            SpinWait.SpinUntil(() => false, TimeSpan.FromSeconds(1));

            Assert.True(myEventListener0.IsEventProcessed());
            Assert.True(myEventListener1.IsEventProcessed());
            Assert.True(eventBus.UnregisterListener<MyEventData>(myEventListener0));
            Assert.True(eventBus.UnregisterListener<MyEventData>(myEventListener1));
        }

        [Fact]
        public static void Default_3_Trigger_WithOtherType()
        {
            var eventBus = EventBus.GetInstance();
            Assert.NotNull(eventBus);
            var myEventListener0 = new MyEventListener();
            var myEventListener1 = new MyEventListener();
            Assert.True(eventBus.RegisterListener<MyEventData>(myEventListener0));
            Assert.True(eventBus.RegisterListener<MyEventData>(myEventListener1));
            var otherEventData = new OtherEventData();
            Assert.NotNull(eventBus.Trigger<MyEventData>(otherEventData));

            SpinWait.SpinUntil(() => false, TimeSpan.FromSeconds(1));

            Assert.False(myEventListener0.IsEventProcessed());
            Assert.False(myEventListener1.IsEventProcessed());
            Assert.True(eventBus.UnregisterListener<MyEventData>(myEventListener0));
            Assert.True(eventBus.UnregisterListener<MyEventData>(myEventListener1));
        }

        [Fact]
        public static void Default_3_Trigger_WithSubType()
        {
            var eventBus = EventBus.GetInstance();
            Assert.NotNull(eventBus);
            var myEventListener0 = new MyEventListener();
            var myEventListener1 = new MyEventListener();
            Assert.True(eventBus.RegisterListener<MyEventData>(myEventListener0));
            Assert.True(eventBus.RegisterListener<MyEventData>(myEventListener1));
            var mySubEventData = new MySubEventData();
            Assert.NotNull(eventBus.Trigger<MyEventData>(mySubEventData));

            SpinWait.SpinUntil(() => false, TimeSpan.FromSeconds(1));

            Assert.True(myEventListener0.IsEventProcessed());
            Assert.True(myEventListener1.IsEventProcessed());
            Assert.True(eventBus.UnregisterListener<MyEventData>(myEventListener0));
            Assert.True(eventBus.UnregisterListener<MyEventData>(myEventListener1));
        }

        [Fact]
        public static void Default_3_Trigger_AfterUnregisterListener()
        {
            var eventBus = EventBus.GetInstance();
            Assert.NotNull(eventBus);
            var myEventListener0 = new MyEventListener();
            var myEventListener1 = new MyEventListener();
            Assert.True(eventBus.RegisterListener<MyEventData>(myEventListener0));
            Assert.True(eventBus.RegisterListener<MyEventData>(myEventListener1));
            Assert.True(eventBus.UnregisterListener<MyEventData>(myEventListener0));
            Assert.True(eventBus.UnregisterListener<MyEventData>(myEventListener1));
            var myEventData = new MyEventData();
            Assert.NotNull(eventBus.Trigger<MyEventData>(myEventData));

            SpinWait.SpinUntil(() => false, TimeSpan.FromSeconds(1));

            Assert.False(myEventListener0.IsEventProcessed());
            Assert.False(myEventListener1.IsEventProcessed());
        }

        public class MyEventData : EventBus.EventData
        {
        }

        public class MyEventListener : EventBus.IEventListener<MyEventData>
        {
            private bool eventProcessed;

            public bool IsEventProcessed()
            {
                return eventProcessed;
            }

            public void ProcessEvent(MyEventData eventData)
            {
                Logger.GetInstance(typeof(MyEventListener)).Info("eventData.Source: " + eventData.Source);
                Logger.GetInstance(typeof(MyEventListener)).Info("eventData.TimeInUtc: " + eventData.TimeInUtc);
                eventProcessed = true;
            }
        }

        public class MySubEventData : MyEventData
        {
        }

        public class OtherEventData : EventBus.EventData
        {
        }
    }
}
