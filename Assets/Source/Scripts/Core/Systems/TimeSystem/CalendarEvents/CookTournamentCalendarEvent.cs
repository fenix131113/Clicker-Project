using Clicker.Core.Tournament;

namespace Clicker.Core.Time
{
    public class CookTournamentCalendarEvent : CalendarEvent
    {
        protected override string _eventName { get; set; } = "Турнир";
        protected override string _description { get; set; } = "Соревнование, за победу в котором вы получите 1 очко навыков";
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