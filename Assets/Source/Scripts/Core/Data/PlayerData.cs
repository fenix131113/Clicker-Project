using Clicker.Core.Earnings;
using Clicker.Core.Time;
using Clicker.Core.Tournament;
using Clicker.Core.Workers;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// All data in this script will save in database
public class PlayerData
{
    [JsonIgnore] private RobberyManager _robberyManager;
    [JsonIgnore] public RobberyManager RobberyManager => _robberyManager;


    [JsonIgnore] private WorkersManager _workersManager;
    [JsonIgnore] public WorkersManager WorkersManager => _workersManager;


    [JsonIgnore] private GeneralPassiveMoneyController _passiveMoneyController;
    [JsonIgnore] public GeneralPassiveMoneyController PassiveMoneyController => _passiveMoneyController;


    [JsonProperty][SerializeField] private MafiaManager _mafiaManager;
    [JsonIgnore] public MafiaManager MafiaManager => _mafiaManager;


    [JsonProperty][SerializeField] private TournamentManager _tournamentManager;
    [JsonIgnore] public TournamentManager TournamentManager => _tournamentManager;





    [JsonProperty][SerializeField] private int _money = 0;
    [JsonIgnore]
    public int Money
    {
        get { return _money; }
        set
        {
            int oldMoney = _money;
            _money = value;
            onGetMoney?.Invoke(value - oldMoney);
            if (value < 0)
                LooseGame("Вы обанкротились!");
        }
    }
    public void AddMoneySilently(int count)
    {
        _money += count;
        if (_money < 0)
            LooseGame("Вы обанкротились!");
    }

    [JsonProperty][SerializeField] private int _skillPoints = 0;
    [JsonIgnore] public int SkillPoints => _skillPoints;
    public void AddSkillPoints(int count) => _skillPoints += count;
    public void RemoveSkillPoints(int count) => _skillPoints -= count;


    [JsonProperty][SerializeField] private bool[] _unlockedFood = new bool[5] { true, false, false, false, false };
    [JsonIgnore] public IReadOnlyCollection<bool> UnlockedFood => _unlockedFood;
    public void UnlockFood(int index) => _unlockedFood[index] = true;


    [JsonProperty][SerializeField] private int _currentClickerProgress = 0;
    [JsonIgnore] public int CurrentClickerProgress => _currentClickerProgress;
    public void SetCurrentClickerProgress(int count) => _currentClickerProgress = count;


    [JsonProperty][SerializeField] private bool[] _buyedSkills = new bool[0];
    [JsonIgnore] public IReadOnlyCollection<bool> BuyedSkills => _buyedSkills;
    public void SetBuyedSkillsArray(bool[] array) => _buyedSkills = array;


    [JsonProperty][SerializeField] private int _day;


    [JsonProperty][SerializeField] private int _clicks;
    public int Clicks => _clicks;
    public void IncreaseClicks() => _clicks++;

    [JsonProperty][SerializeField] private int _cookedFood;
    public int CookedFood => _cookedFood;
    public void IncreaseCookedFood() => _cookedFood++;

    [JsonProperty][SerializeField] private int _tournamentWins;
    public int TournamentWins => _tournamentWins;
    public void IncreaseTournamentWins() => _tournamentWins++;

    [JsonProperty][SerializeField] private int _mafiaPayments;
    public int MafiaPayments => _mafiaPayments;
    public void IncreaseMafiaPayments() => _mafiaPayments++;

    [JsonProperty][SerializeField] private int _weeklyQuestsComplete;
    public int WeeklyQuestsComplete => _weeklyQuestsComplete;
    public void IncreaseWeeklyQuestsComplete() => _weeklyQuestsComplete++;


    private int _clickPower = 1;
    [JsonIgnore] public int ClickPower => _clickPower;
    public void SetClickPower(int count) => _clickPower = count;


    private int _moneyPerFood = 1;
    [JsonIgnore] public int MoneyPerFood => _moneyPerFood;
    public void SetMoneyPerFood(int count) => _moneyPerFood = count;


    [JsonIgnore] private int _maxProgressBarClicks = 15;
    [JsonIgnore] public int MaxProgressBarClicks => _maxProgressBarClicks;
    public void SetMaxProgressBarClicks(int count) => _maxProgressBarClicks = count;

    //[JsonProperty] private WeeklyQuestContainer[] _savedWeeklyQuests;
    //[JsonIgnore] public WeeklyQuestContainer[] SavedWeeklyQuests => _savedWeeklyQuests;


    private SkillSaveManager _skillSaveManager;
    private GlobalObjectsContainer _objectsContainer;
    private TimeManager _timeManager;
    private CalendarManager _calendarManager;
    private WeeklyQuestsController _weeklyQuestsController;

    public delegate void PlayerDataIntParameterEvent(int parameter1);
    public event PlayerDataIntParameterEvent onGetMoney;

    [Inject]
    public void Init(RobberyManager robberyManager, WorkersManager workersManager,
        GeneralPassiveMoneyController passiveMoneyController, TournamentManager tournamentManager,
        SkillSaveManager skillSaveManager, MafiaManager mafiaManager, GlobalObjectsContainer objectsContainer,
        TimeManager timeManager, CalendarManager calendarManager, EarningsManager earnings, WeeklyQuestsController weeklyQuestsController)
    {
        _robberyManager = robberyManager;
        robberyManager.SetData(this, calendarManager);

        _workersManager = workersManager;
        _workersManager.SetData(this);

        _passiveMoneyController = passiveMoneyController;
        passiveMoneyController.SetData(this, calendarManager);

        _tournamentManager = tournamentManager;
        _tournamentManager.SetData(this);

        _mafiaManager = mafiaManager;
        _mafiaManager.SetData(this);

        _skillSaveManager = skillSaveManager;
        skillSaveManager.SetData(this);

        _objectsContainer = objectsContainer;

        _calendarManager = calendarManager;
        earnings.SetCalendar(_calendarManager);

        _weeklyQuestsController = weeklyQuestsController;
        _timeManager = timeManager;

        _calendarManager.onNewDay += (int day, DayType dayType) =>
        {
            if (day % 7 == 0)
                SaveData();
        };

        if (PlayerPrefs.HasKey("data"))
            LoadData();
    }

    public void SaveData()
    {
        _skillSaveManager.SaveSkillsData();
        //_savedWeeklyQuests = _weeklyQuestsController.CurrentWeeklyQuests;
        _day = _calendarManager.Day;
        string dataSave = JsonConvert.SerializeObject(this);
        PlayerPrefs.SetString("data", dataSave);
    }

    public void LoadData()
    {
        string dataKey = "data";

        if (!PlayerPrefs.HasKey(dataKey))
            return;

        PlayerData loadedData = JsonConvert.DeserializeObject<PlayerData>(PlayerPrefs.GetString(dataKey));
        _mafiaManager.LoadSavedData(loadedData._mafiaManager);
        _tournamentManager.LoadSavedData(loadedData._tournamentManager);
        //_savedWeeklyQuests = loadedData._savedWeeklyQuests;

        _calendarManager.SetDay(loadedData._day);
        _money = loadedData._money;
        _skillPoints = loadedData._skillPoints;
        _unlockedFood = loadedData._unlockedFood;
        _currentClickerProgress = loadedData._currentClickerProgress;
        _buyedSkills = loadedData._buyedSkills;
        _clicks = loadedData._clicks;
        _cookedFood = loadedData._cookedFood;
        _tournamentWins = loadedData._tournamentWins;
        _mafiaPayments = loadedData._mafiaPayments;
        _weeklyQuestsComplete = loadedData._weeklyQuestsComplete;
        _skillSaveManager.LoadSkillsData(_buyedSkills);
    }

    public void LooseGame(string looseMessage)
    {
        _timeManager.IsTimePaused = true;
        _objectsContainer.LoosePanText.text = looseMessage;
        _objectsContainer.LoosePanBlocker.SetActive(true);
        if (PlayerPrefs.HasKey("data"))
            PlayerPrefs.DeleteKey("data");
    }
}