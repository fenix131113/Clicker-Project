namespace Clicker.Core.Time
{
    public class UtilitiesPayEvent : CalendarEvent
    {
        protected override string _eventName { get; set; } = "Коммунальные услуги";
        protected override string _description { get; set; } = "Utilities_Pay_Event_Description";
        protected override int _eventPeriod { get; set; }
        protected override int _hour { get; set; }
        protected override bool _eventComplete { get; set; }

        private PlayerData _data;

        public UtilitiesPayEvent(int eventPeriod, int hour, PlayerData data)
        {
            _eventPeriod = eventPeriod;
            _hour = hour;
            _data = data;
            _description = $"Оплата за коммунальные услуги";
        }
        public override void EventAction()
        {
            _eventComplete = true;
            _data.PassiveMoneyController.UtilitiesPayment();
        }
    }
}