namespace Clicker.Core.Time
{
    public class MafiaCalendarEvent : CalendarEvent
    {
        protected override string _eventName { get; set; } = "Mafia visit";
        protected override string _description { get; set; } = "Mafia_Event_Description";
        protected override int _eventPeriod { get; set; }
        protected override int _hour { get; set; }
        protected override bool _eventComplete { get; set; }

        private MafiaManager _mafiaManager;

        public MafiaCalendarEvent(int eventPeriod, int hour, MafiaManager mafiaManager)
        {
            _eventPeriod = eventPeriod;
            _hour = hour;
            _mafiaManager = mafiaManager;
            _description = $"The mafia will come to take money from you, are you ready?\nThey will take:\n<color=\"red\"><size=40>{mafiaManager.TakeMoneyCount}$";
        }
        public override void EventAction()
        {
            _eventComplete = true;
            _mafiaManager.InitMafiaVisit();
        }
    }
}