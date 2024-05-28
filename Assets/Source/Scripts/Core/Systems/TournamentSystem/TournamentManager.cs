using Clicker.Core.Time;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Clicker.Core.Tournament
{
    public class TournamentManager
    {
        #region Save Data
        [JsonProperty][SerializeField] private bool _isTournamentProccess;
        [JsonIgnore] public bool IsTournamentProcess => _isTournamentProccess;


        [JsonProperty][SerializeField] private int _currentTournamentProgress;
        [JsonIgnore] public int CurrentTournamentProgress => _currentTournamentProgress;


        [JsonProperty][SerializeField] private int _needProgressToWin;
        [JsonIgnore] public int NeedProgressToWin => _needProgressToWin;


        [JsonProperty][SerializeField] private int _remainingHours;
        [JsonIgnore] public int RemainingHours => _remainingHours;
        #endregion

        [JsonIgnore] private readonly int _tournamentStartNeedProgressToWin = 20;
        [JsonIgnore] private readonly int _tournamentHoursTime = 12;
        [JsonIgnore] private readonly int _tournamentPeriod = 3;
        [JsonIgnore] private GlobalObjectsContainer _objectsContainer;
        [JsonIgnore] private TimeManager _timeManager;
        [JsonIgnore] private PlayerData _data;
        [JsonIgnore] private NoticeSystem _notifications;
        public delegate void TournamentNoParameterEvent();
        public event TournamentNoParameterEvent onTournamentWin;

        [JsonIgnore] public int TournamentHoursTime => _tournamentHoursTime;
        [JsonIgnore] public int TournamentPeriod => _tournamentPeriod;

        public void LoadSavedData(TournamentManager tournamentManager)
        {
            _isTournamentProccess = tournamentManager.IsTournamentProcess;
            _currentTournamentProgress = tournamentManager.CurrentTournamentProgress;
            _needProgressToWin = tournamentManager.NeedProgressToWin;
            _remainingHours = tournamentManager.RemainingHours;
        }
        public void SetData(PlayerData data) => _data = data;

        [Inject]
        private void Init(GlobalObjectsContainer objectsContainer, TimeManager timeManager, NoticeSystem notifications)
        {
            _objectsContainer = objectsContainer;
            _timeManager = timeManager;
            _notifications = notifications;
        }

        public void AskForTournament()
        {
            _timeManager.IsTimePaused = true;
            _objectsContainer.AskForTournamentPanel.SetActive(true);
            _objectsContainer.AcceptTournamentButton.onHoldComplete += StartTournament;
            _objectsContainer.DenyTournamentButton.onHoldComplete += DenyTournament;
        }
        private void StartTournament()
        {
            _objectsContainer.ClickerScript.onFoodCooked -= NewFoodCookedAction;
            TimeManager.onNewHour -= NewHourCheck;
            _currentTournamentProgress = 0;
            _remainingHours = _tournamentHoursTime;
            _needProgressToWin = _tournamentStartNeedProgressToWin;
            _objectsContainer.TournamentProgressFiller.fillAmount = 0;
            _objectsContainer.TournamentProgressText.text = $"0/{NeedProgressToWin}   {RemainingHours}ч. осталось";
            _timeManager.IsTimePaused = false;
            _objectsContainer.AskForTournamentPanel.SetActive(false);
            TimeManager.onNewHour += NewHourCheck;
            _objectsContainer.ClickerScript.onFoodCooked += NewFoodCookedAction;

            _objectsContainer.TournamentProgressFiller.transform.parent.parent.gameObject.SetActive(true);
        }

        private void DenyTournament()
        {
            _timeManager.IsTimePaused = false;
            _objectsContainer.AskForTournamentPanel.SetActive(false);
        }

        private void LooseTournament()
        {
            _notifications.CreateNewNotification("Вы проиграли в турнире");
            TimeManager.onNewHour -= NewHourCheck;
            _objectsContainer.ClickerScript.onFoodCooked -= NewFoodCookedAction;
            _objectsContainer.TournamentProgressFiller.transform.parent.parent.gameObject.SetActive(false);
        }
        private void NewHourCheck(int hour)
        {
            _remainingHours--;
            _objectsContainer.TournamentProgressText.text = $"{_currentTournamentProgress}/{NeedProgressToWin}  {RemainingHours}ч. осталось";
            if (RemainingHours <= 0)
                LooseTournament();
        }
        private void WinTournament()
        {
            onTournamentWin?.Invoke();
            _notifications.CreateNewNotification("Вы выиграли в турнире и получили 1 очко навыков");
            _data.AddSkillPoints(1);

            TimeManager.onNewHour -= NewHourCheck;
            _objectsContainer.ClickerScript.onFoodCooked -= NewFoodCookedAction;
            _objectsContainer.TournamentProgressFiller.transform.parent.parent.gameObject.SetActive(false);
        }
        private void NewFoodCookedAction(int earnedMoney)
        {
            _currentTournamentProgress++;
            _objectsContainer.TournamentProgressFiller.DOFillAmount((float)_currentTournamentProgress / NeedProgressToWin, 0.5f);
            _objectsContainer.TournamentProgressText.text = $"{_currentTournamentProgress}/{NeedProgressToWin}  {RemainingHours}ч. осталось";

            if (CurrentTournamentProgress >= NeedProgressToWin)
                WinTournament();
        }
    }
}