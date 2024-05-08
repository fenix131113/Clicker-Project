namespace Clicker.Core.Time
{
    public class ConsumablePaymentEvent : CalendarEvent
    {
        protected override string _eventName { get; set; } = "�������";
        protected override string _description { get; set; } = "Consumables_Pay_Event_Description";
        protected override int _eventPeriod { get; set; }
        protected override int _hour { get; set; }
        protected override bool _eventComplete { get; set; }

        private PlayerData _data;

        public ConsumablePaymentEvent(int eventPeriod, int hour, PlayerData data)
        {
            _eventPeriod = eventPeriod;
            _hour = hour;
            _data = data;
            _description = $"������ �� ����������";
        }
        public override void EventAction()
        {
            _eventComplete = true;
            _data.PassiveMoneyController.ConsumablePayment();
        }
    }
}