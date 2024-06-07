using Clicker.Core.Earnings;
using Clicker.Core.Time;
using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PanelsController : MonoBehaviour
{
    [Header("Skills Panel")]
    [SerializeField] private Image skillPanelBlockerFade;
    [SerializeField] private RectTransform skillPanelRect;

    [Header("Earnings History")]
    [SerializeField] private Image earningHistoryPanelBlockerFade;
    [SerializeField] private RectTransform earningHistoryPanelRect;
    [SerializeField] private GameObject allLabelBlock;
    [SerializeField] private GameObject categoryBlock;
    [SerializeField] private GameObject dayBlock;
    [SerializeField] private GameObject lineBlock;
    [SerializeField] private Transform earningsContainerTransform;

    [Header("Calendar")]
    [SerializeField] private Image calendarBlocker;
    [SerializeField] private RectTransform calendarPanel;
    [SerializeField] private CalendarEventsController calendarEventController;

    [Header("Settings")]
    [SerializeField] private Image settingsBlocker;
    [SerializeField] private RectTransform settingsPanel;

    private TimeManager timeManager;
    private EarningsManager earningsManager;
    private GlobalObjectsContainer objectsContainer;

    [Inject]
    private void Init(TimeManager timeManager, EarningsManager earningsManager, GlobalObjectsContainer objectsContainer)
    {
        this.timeManager = timeManager;
        this.earningsManager = earningsManager;
        this.objectsContainer = objectsContainer;
    }

    public void OpenSettings()
    {
        timeManager.IsTimePaused = true;

        settingsBlocker.gameObject.SetActive(true);
        settingsBlocker.DOFade(0.7f, 0.4f);


        settingsPanel.DOMoveY(Screen.height / 2, 0.4f);
    }

    public void CloseSettings()
    {
        timeManager.IsTimePaused = false;

        settingsBlocker.DOFade(0f, 0.4f).onComplete += () => settingsBlocker.gameObject.SetActive(false);

        settingsPanel.DOMoveY(Screen.height * -1.5f, 0.4f);
    }

    public void OpenCalendar()
    {
        calendarEventController.SelectDay(0);
        calendarEventController.LightCurrentDay();
        calendarEventController.ReloadCalendar();

        timeManager.IsTimePaused = true;

        calendarBlocker.gameObject.SetActive(true);
        calendarBlocker.DOFade(0.7f, 0.4f);


        calendarPanel.DOMoveY(Screen.height / 2, 0.4f);
    }

    public void CloseCalendar()
    {
        calendarEventController.DeactivateCurrentCalendarSelection();
        calendarEventController.SelectedDayIndex = -1;

        timeManager.IsTimePaused = false;

        calendarBlocker.DOFade(0f, 0.4f).onComplete += () => calendarBlocker.gameObject.SetActive(false);

        calendarPanel.DOMoveY(Screen.height * 1.5f, 0.4f);
    }
    public void OpenSkillPanel()
    {
        timeManager.IsTimePaused = true;
        skillPanelBlockerFade.gameObject.SetActive(true);
        skillPanelBlockerFade.DOFade(0.7f, 0.4f);
        objectsContainer.SkillInfoPanel.SelectFirstSkill();

        skillPanelRect.DOMove(new Vector2(Screen.width / 2, Screen.height / 2), 0.4f);
    }

    public void CloseSkillPanel()
    {
        timeManager.IsTimePaused = false;
        skillPanelBlockerFade.DOFade(0, 0.4f).onComplete += () => skillPanelBlockerFade.gameObject.SetActive(false);

        skillPanelRect.DOMove(new Vector2(Screen.width + Screen.width / 2, Screen.height / 2), 0.4f);
    }

    public void OpenEarningHistoryPanel()
    {
        if (!earningHistoryPanelBlockerFade.gameObject.activeSelf)
            UpdateVisualEarning();
        timeManager.IsTimePaused = true;
        earningHistoryPanelBlockerFade.gameObject.SetActive(true);
        earningHistoryPanelBlockerFade.DOFade(0.7f, 0.4f);

        earningHistoryPanelRect.DOMove(new Vector2(Screen.width / 2, Screen.height / 2), 0.4f);
    }

    public void CloseEarningHistoryPanel()
    {
        timeManager.IsTimePaused = false;
        earningHistoryPanelBlockerFade.DOFade(0, 0.4f).onComplete += () => earningHistoryPanelBlockerFade.gameObject.SetActive(false);

        earningHistoryPanelRect.DOMove(new Vector2(-Screen.width - Screen.width / 2, Screen.height / 2), 0.4f);
        ClearVisualEarning();
    }

    public void UpdateVisualEarning()
    {
        foreach (EarningDayHistory historyDay in earningsManager.EarningsList.Reverse())
        {

            TMP_Text dayBlockText = Instantiate(dayBlock, earningsContainerTransform).transform.GetChild(0).GetComponent<TMP_Text>();
            dayBlockText.text = $"Day {historyDay.Day}";
            foreach (EarningsHistoryCategory category in historyDay.Catigories)
            {
                Transform categoryCell = Instantiate(categoryBlock, earningsContainerTransform).transform;
                TMP_Text categoryText = categoryCell.GetChild(0).GetComponent<TMP_Text>();
                TMP_Text expensesText = categoryCell.GetChild(1).GetComponent<TMP_Text>();
                TMP_Text profitText = categoryCell.GetChild(2).GetComponent<TMP_Text>();
                categoryText.text = $"- {category.CategoryName} -";
                expensesText.text = $"{category.ExpensesMoney}$";
                profitText.text = $"{category.EarningMoney}$";
            }

            Transform allLabelTransform = Instantiate(allLabelBlock, earningsContainerTransform).transform;
            TMP_Text dayExpensesText = allLabelTransform.GetChild(1).GetComponent<TMP_Text>();
            TMP_Text dayEarningsText = allLabelTransform.GetChild(2).GetComponent<TMP_Text>();

            int earningsSum = 0;
            int expensesSum = 0;
            foreach (EarningsHistoryCategory earn in historyDay.Catigories)
            {
                earningsSum += earn.EarningMoney;
                expensesSum -= earn.ExpensesMoney;
            }

            dayExpensesText.text = expensesSum.ToString().Replace("-", "") + "$";
            dayEarningsText.text = earningsSum.ToString() + "$";

            if (historyDay != earningsManager.EarningsList.ElementAt(0))
                Instantiate(lineBlock, earningsContainerTransform).GetComponent<TMP_Text>();
        }
    }
    public void ClearVisualEarning()
    {
        for (int i = 0; i < earningsContainerTransform.childCount; i++)
            Destroy(earningsContainerTransform.GetChild(i).gameObject);
    }
}