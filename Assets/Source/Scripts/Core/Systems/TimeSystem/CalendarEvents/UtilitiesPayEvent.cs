namespace Clicker.Core.Time
{
    public class UtilitiesPayEvent : CalendarEvent
    {
        protected override string _eventName { get; set; } = "Utilities";
        protected override string _description { get; set; } = "Utilities_Pay_Event_Description";
        protected override int _eventPeriod { get; set; }
        protected override int _hour { get; set; }
        protected override bool _eventComplete { get; set; }

        private PlayerData _data;

        public UtilitiesPayEvent(int eventPeriod, int hour, PlayerData data, GeneralPassiveMoneyController passiveController)
        {
            _eventPeriod = eventPeriod;
            _hour = hour;
            _data = data;
            _description = $"Payment for utilities\n\n<color=\"red\"><size=40>{passiveController.UtilityServiceCost}$";
        }
        public override void EventAction()
        {
            _eventComplete = true;
            _data.PassiveMoneyController.UtilitiesPayment();
        }
    }
}