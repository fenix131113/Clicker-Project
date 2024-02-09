using Clicker.Core.Workers;
using Clicker.Core.Tournament;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Zenject;
using Clicker.Core.Time;
using UnityEngine.SceneManagement;

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
            _money = value;
            if (value < 0)
                LooseGame("Вы обанкротились!");
        }
    }

    [JsonProperty][SerializeField] private int _skillPoints = 1;
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


    private int _clickPower = 1;
    [JsonIgnore] public int ClickPower => _clickPower;
    public void SetClickPower(int count) => _clickPower = count;


    private int _moneyPerFood = 1;
    [JsonIgnore] public int MoneyPerFood => _moneyPerFood;
    public void SetMoneyPerFood(int count) => _moneyPerFood = count;


    private int _maxProgressBarClicks = 15;
    [JsonIgnore] public int MaxProgressBarClicks => _maxProgressBarClicks;
    public void SetMaxProgressBarClicks(int count) => _maxProgressBarClicks = count;


    private SkillSaveManager _skillSaveManager;

    [Inject]
    public void Init(RobberyManager robberyManager, WorkersManager workersManager,
        GeneralPassiveMoneyController passiveMoneyController, TournamentManager tournamentManager,
        SkillSaveManager skillSaveManager, MafiaManager mafiaManager)
    {
        _robberyManager = robberyManager;
        robberyManager.SetData(this);

        _workersManager = workersManager;
        _workersManager.SetData(this);

        _passiveMoneyController = passiveMoneyController;
        passiveMoneyController.SetData(this);

        _tournamentManager = tournamentManager;
        _tournamentManager.SetData(this);

        _mafiaManager = mafiaManager;
        _mafiaManager.SetData(this);

        _skillSaveManager = skillSaveManager;
        skillSaveManager.SetData(this);

        CalendarManager.onNewDay += (int day, DayType dayType) =>
        {
            if (day % 7 == 0)
                SaveData();
        };
        if (PlayerPrefs.HasKey("data"))
            LoadData();
    }

    public void SaveData()
    {
        if (PlayerPrefs.HasKey("data"))
            PlayerPrefs.SetString("backup", PlayerPrefs.GetString("data"));

        _skillSaveManager.SaveSkillsData();
        _day = CalendarManager.Day;
        string dataSave = JsonConvert.SerializeObject(this);
        PlayerPrefs.SetString("data", dataSave);
    }

    public void LoadData()
    {
        string dataKey = "data";

        if (PlayerPrefs.HasKey("died") && PlayerPrefs.GetInt("died") == 1 && PlayerPrefs.HasKey("data") && !PlayerPrefs.HasKey("backup"))
            return;

        if (PlayerPrefs.HasKey("died") && PlayerPrefs.GetInt("died") == 1 && !PlayerPrefs.HasKey("backup"))
            return;
        else if (PlayerPrefs.HasKey("died") && PlayerPrefs.GetInt("died") == 1)
        {
            dataKey = "backup";
            PlayerPrefs.SetString("data", PlayerPrefs.GetString("backup"));
            PlayerPrefs.SetInt("died", 0);
        }

        PlayerData loadedData = JsonConvert.DeserializeObject<PlayerData>(PlayerPrefs.GetString(dataKey));
        _mafiaManager.LoadSavedData(loadedData._mafiaManager);
        _tournamentManager.LoadSavedData(loadedData._tournamentManager);

        CalendarManager.SetDay(loadedData._day);
        _money = loadedData._money;
        _skillPoints = loadedData._skillPoints;
        _unlockedFood = loadedData._unlockedFood;
        _currentClickerProgress = loadedData._currentClickerProgress;
        _buyedSkills = loadedData._buyedSkills;
        _skillSaveManager.LoadSkillsData(_buyedSkills);
    }

    public void LooseGame(string looseMessage)
    {
        PlayerPrefs.SetInt("died", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}