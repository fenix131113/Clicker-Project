using Clicker.Core.Earnings;
using Clicker.Core.Time;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WeeklyQuestsController : MonoBehaviour
{
    [SerializeField] private GameObject _weeklyQuestCellPrefab;
    [SerializeField] private Transform _weeklyQuestsContainerTransform;

    private List<WeeklyQuestBase> _allQuests = new List<WeeklyQuestBase>();
    private float _rewardModifier = 1f;
    private float _questsDifficultModifier = 1f;

    private WeeklyQuestContainer[] _currentWeeklyQuests = new WeeklyQuestContainer[2];
    private GameObject[] _currentWeeklyObjects = new GameObject[2];
    private PlayerData _data;
    private GlobalObjectsContainer _objectsContainer;
    private NoticeSystem _notices;
    private EarningsManager _earnings;
    private CalendarManager _calendarManager;

    public PlayerData Data => _data;
    public WeeklyQuestContainer[] CurrentWeeklyQuests => _currentWeeklyQuests;
    public float RewardModifier => _rewardModifier;
    public float QuestsDifficultModifier => _questsDifficultModifier;
    public void SetQuestsDifficultModifier(float modifier) => _questsDifficultModifier = modifier;
    public void SetRewardModifier(float modifier) => _rewardModifier = modifier;

    public delegate void OnQuestComplete();
    public OnQuestComplete onQuestComplete;

    [Inject]
    public void Init(PlayerData data, CalendarManager calendarManager, GlobalObjectsContainer objectsContainer, NoticeSystem notices,
        EarningsManager earnings)
    {
        _data = data;
        _objectsContainer = objectsContainer;
        _notices = notices;
        _earnings = earnings;
        _calendarManager = calendarManager;
        calendarManager.onNewDay += OnNewDay;
        onQuestComplete += _data.IncreaseWeeklyQuestsComplete;

        GenerateAllQuests();

        if (!PlayerPrefs.HasKey("data"))
            GenerateNewRandomQuests();
        else
            LoadData(data.SavedWeeklyQuests);
    }

    private void OnNewDay(int day, DayType dayType)
    {
        if (day % 7 == 0)
        {
            GenerateNewRandomQuests();
        }
        else
        {
            for (int i = 0; i < 2; i++)
                _currentWeeklyQuests[i].DecreaseDaysLeft();
            CheckQuestsDeadline();
            UpdateVisual();
        }
    }

    public void LoadData(WeeklyQuestContainer[] weeklyQuests)
    {
        _currentWeeklyQuests = weeklyQuests;

        for (int i = 0; i < _currentWeeklyQuests.Length; i++)
        {
            _allQuests[_currentWeeklyQuests[i].QuestIndex].onProgressIncreased += _currentWeeklyQuests[i].IncreaseProgress;
            _allQuests[_currentWeeklyQuests[i].QuestIndex].onProgressIncreased += (int parametr1) => CheckConditions();



            _currentWeeklyObjects[i] = Instantiate(_weeklyQuestCellPrefab, _weeklyQuestsContainerTransform);
        }
        UpdateVisual();
    }

    // Call this method FIRST OF ALL
    private void GenerateAllQuests()
    {
        _allQuests = new()
        {
            new ClickerWeeklyQuest("Кликов", this, new WeeklyQuestDifficultItem[]{ new(1, 50, 1, 150),}, _objectsContainer.ClickerScript),
            new FoodCookingWeeklyQuest("Приготовить еды", this, new WeeklyQuestDifficultItem[]{ new(1, 15, 0, 150), }, _objectsContainer.ClickerScript),
            new EarnMoneyWeeklyQuest("Заработать денег", this, new WeeklyQuestDifficultItem[]{ new(1, 50, 0, 150), }),
        };
    }

    public void CheckQuestsDeadline()
    {
        for (int i = 0; i < 2; i++)
        {
            if (_currentWeeklyQuests[i].DaysLeft == 0)
                _currentWeeklyQuests[i].SetLoose();
        }
    }

    private void UpdateVisual()
    {
        for (int i = 0; i < _currentWeeklyObjects.Length; i++)
        {
            _currentWeeklyObjects[i].transform.GetChild(0).GetComponent<TMP_Text>().text = _allQuests[_currentWeeklyQuests[i].QuestIndex].Description;
            _currentWeeklyObjects[i].transform.GetChild(2).GetComponent<TMP_Text>().text = "Осталось дней: " + _currentWeeklyQuests[i].DaysLeft.ToString();

            if (!_currentWeeklyQuests[i].Complete && _currentWeeklyQuests[i].IsLoose)
                _currentWeeklyObjects[i].transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = "X";
            else if (!_currentWeeklyQuests[i].IsLoose)
            {
                _currentWeeklyObjects[i].transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = $"{_currentWeeklyQuests[i].Progress} / {_currentWeeklyQuests[i].NeedProgress}";
                _currentWeeklyObjects[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = (float)_currentWeeklyQuests[i].Progress / _currentWeeklyQuests[i].NeedProgress;
            }
        }
    }

    private void CheckConditions()
    {
        UpdateVisual();
        for (int i = 0; i < 2; i++)
            if (!_currentWeeklyQuests[i].Complete && !_currentWeeklyQuests[i].IsLoose && _currentWeeklyQuests[i].Progress == _currentWeeklyQuests[i].NeedProgress)
            {
                _allQuests[_currentWeeklyQuests[i].QuestIndex].onProgressIncreased = null;
                string noticeMessage = "Вы выполнили задание и получили:";
                if (_currentWeeklyQuests[i].MoneyReward > 0)
                {
                    noticeMessage += "\n" + _currentWeeklyQuests[i].MoneyReward.ToString() + "$";
                    _earnings.AddOrUpdateHistoryEntry(_calendarManager.Day, "Задания", _currentWeeklyQuests[i].MoneyReward);
                    _data.AddMoneySilently(_currentWeeklyQuests[i].MoneyReward);
                }
                if (_currentWeeklyQuests[i].SkillPointsReward > 0)
                {
                    noticeMessage += "\n" + "Очки навыков: " + _currentWeeklyQuests[i].SkillPointsReward.ToString();
                    _data.AddSkillPoints(_currentWeeklyQuests[i].SkillPointsReward);
                }
                _notices.CreateNewNotification(noticeMessage);
                _currentWeeklyQuests[i].SetCompleted();
            }
    }

    private void GenerateNewRandomQuests()
    {
        ClearQuestsObjectsAndData();

        for (int i = 0; i < 2; i++)
        {
            int questIndex = Random.Range(0, _allQuests.Count);

            while (i == 1 && questIndex == _currentWeeklyQuests[0].QuestIndex)
                questIndex = Random.Range(0, _allQuests.Count);

            _currentWeeklyQuests[i] = new WeeklyQuestContainer(questIndex,
                _allQuests[questIndex].WeeklyQuestDifficultItems[Random.Range(0, _allQuests[questIndex].WeeklyQuestDifficultItems.Length)],
                _questsDifficultModifier, RewardModifier);

            _allQuests[questIndex].onProgressIncreased += _currentWeeklyQuests[i].IncreaseProgress;
            _allQuests[questIndex].onProgressIncreased += (int parametr1) => CheckConditions();



            _currentWeeklyObjects[i] = Instantiate(_weeklyQuestCellPrefab, _weeklyQuestsContainerTransform);
        }
        //System.Array.Sort(_currentWeeklyQuests);
        UpdateVisual();
    }

    private void ClearQuestsObjectsAndData()
    {
        foreach (var current in _allQuests)
        {
            current.onProgressIncreased = null;
        }

        _currentWeeklyObjects = new GameObject[2];

        for (int i = 0; i < _weeklyQuestsContainerTransform.childCount; i++)
            Destroy(_weeklyQuestsContainerTransform.GetChild(i).gameObject);

        _currentWeeklyQuests = new WeeklyQuestContainer[2];
    }
}