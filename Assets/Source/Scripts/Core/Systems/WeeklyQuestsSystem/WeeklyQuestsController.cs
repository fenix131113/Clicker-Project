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

    private WeeklyQuestContainer[] _currentWeeklyQuests = new WeeklyQuestContainer[2];
    private GameObject[] _currentWeeklyObjects = new GameObject[2];
    private PlayerData _data;
    private GlobalObjectsContainer _objectsContainer;
    private NoticeSystem _notices;

    [Inject]
    public void Init(PlayerData data, CalendarManager calendarManager, GlobalObjectsContainer objectsContainer, NoticeSystem notices)
    {
        _data = data;
        _objectsContainer = objectsContainer;
        _notices = notices;
        calendarManager.onNewDay += OnNewDay;
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
        }
    }

    // Call this method FIRST OF ALL
    private void GenerateAllQuests()
    {
        _allQuests = new()
        {
            new ClickerWeeklyQuest("Кликов", this, 100, 1000, _objectsContainer.ClickerScript),
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
            _currentWeeklyObjects[i].transform.GetChild(0).GetComponent<TMP_Text>().text = _allQuests[_currentWeeklyQuests[0].QuestIndex].Description;
            _currentWeeklyObjects[i].transform.GetChild(2).GetComponent<TMP_Text>().text = _currentWeeklyQuests[i].DaysLeft.ToString();
            _currentWeeklyObjects[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = (float)_currentWeeklyQuests[i].Progress / _currentWeeklyQuests[i].NeedProgress;

            if (!_currentWeeklyQuests[i].Complete && _currentWeeklyQuests[i].IsLoose)
                _currentWeeklyObjects[i].transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = "X";
            else
                _currentWeeklyObjects[i].transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = $"{_currentWeeklyQuests[i].Progress} / {_currentWeeklyQuests[i].NeedProgress}";
        }
    }

    private void CheckConditions()
    {
        for (int i = 0; i < 2; i++)
            if (!_currentWeeklyQuests[i].Complete && !_currentWeeklyQuests[i].IsLoose && _currentWeeklyQuests[i].Progress == _currentWeeklyQuests[i].NeedProgress)
            {
                string noticeMessage = "Вы выполнили задание и получили:";
                if (_currentWeeklyQuests[i].MoneyReward > 0)
                    noticeMessage += "\n" + _currentWeeklyQuests[i].MoneyReward.ToString() + "$";
                if (_currentWeeklyQuests[i].SkillPointsReward > 0)
                    noticeMessage += "\n" + _currentWeeklyQuests[i].SkillPointsReward.ToString() + "$";
                _notices.CreateNewNotification(noticeMessage);
                _currentWeeklyQuests[i].SetCompleted();
            }

        UpdateVisual();
    }

    private void GenerateNewRandomQuests()
    {
        ClearQuestsObjectsAndData();

        for (int i = 0; i < 2; i++)
        {
            int questIndex = Random.Range(0, _allQuests.Count);

            while (i == 1 && questIndex == _currentWeeklyQuests[0].QuestIndex)
                questIndex = Random.Range(0, _allQuests.Count);

            _currentWeeklyQuests[i] = new WeeklyQuestContainer(questIndex, Random.Range(_allQuests[i].MinNeedProgress, _allQuests[i].MaxNeedProgress), Random.Range(1, 8), Random.Range(0, 1000), Random.Range(0, 2));
            _allQuests[i].onProgressIncreased += _currentWeeklyQuests[i].IncreaseProgress;
            _allQuests[i].onProgressIncreased += CheckConditions;



            _currentWeeklyObjects[i] = Instantiate(_weeklyQuestCellPrefab, _weeklyQuestsContainerTransform);
        }
        UpdateVisual();
    }

    private void ClearQuestsObjectsAndData()
    {
        foreach (var current in _allQuests)
            current.onProgressIncreased = null;

        _currentWeeklyObjects = new GameObject[2];

        for (int i = 0; i < _weeklyQuestsContainerTransform.childCount; i++)
            Destroy(_weeklyQuestsContainerTransform.GetChild(i).gameObject);

        _currentWeeklyQuests = new WeeklyQuestContainer[2];
    }
}