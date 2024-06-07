using Clicker.Core.Tournament;

namespace Clicker.Core.Time
{
    public class CookTournamentCalendarEvent : CalendarEvent
    {
        protected override string _eventName { get; set; } = "Tournament";
        protected override string _description { get; set; } = "A competition where you will earn 1 skill point for winning";
        protected override int _eventPeriod { get; set; }
        protected override int _hour { get; set; }
        protected override bool _eventComplete { get; set; }

        private TournamentManager _tournaments;


        public CookTournamentCalendarEvent(int eventPeriod, int hour, TournamentManager tournaments)
        {
            _eventPeriod = eventPeriod;
            _hour = hour;
            _tournaments = tournaments;
        }
        public override void EventAction()
        {
            _eventComplete = true;
            _tournaments.AskForTournament();
        }
    }
}