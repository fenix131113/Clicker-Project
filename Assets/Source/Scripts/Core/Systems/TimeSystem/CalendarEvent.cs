namespace Clicker.Core.Time
{
    public abstract class CalendarEvent
    {
        protected abstract string _eventName { get; set; }
        protected abstract string _description { get; set; }
        protected abstract int _eventPeriod { get; set; }
        protected abstract int _hour { get; set; }
        protected abstract bool _eventComplete { get; set; }

        public string EventName => _eventName;
        public string Description => _description;
        public int EventPeriod => _eventPeriod;
        public int Hour => _hour;
        public bool EventComplete { get => _eventComplete; set => _eventComplete = value; }

        public abstract void EventAction();
    }
}