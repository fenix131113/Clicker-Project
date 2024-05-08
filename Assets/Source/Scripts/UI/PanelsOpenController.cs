using Clicker.Core.Earnings;
using Clicker.Core.Time;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PanelsOpenController : MonoBehaviour
{
    [Header("Skills Panel")]
    [SerializeField] private Image skillPanelBlockerFade;
    [SerializeField] private RectTransform skillPanelRect;

    [Header("Earnings History")]
    [SerializeField] private Image earningHistoryPanelBlockerFade;
    [SerializeField] private RectTransform earningHistoryPanelRect;
    [SerializeField] private GameObject allLabelBlock;
    [SerializeField] private GameObject allMoneyBlock;
    [SerializeField] private GameObject categoryBlock;
    [SerializeField] private GameObject dayBlock;
    [SerializeField] private Transform earningsContainerTransform;

    private TimeManager timeManager;
    private EarningsManager earningsManager;

    [Inject]
    private void Init(GlobalObjectsContainer container, TimeManager timeManager, EarningsManager earningsManager)
    {
        container.RightSideHighLighter.OnSideClicked += OpenSkillPanel;
        container.LeftSideHighLighter.OnSideClicked += OpenEarningHistoryPanel;
        this.timeManager = timeManager;
        this.earningsManager = earningsManager;
    }


    public void OpenSkillPanel()
    {
        timeManager.IsTimePaused = true;
        skillPanelBlockerFade.gameObject.SetActive(true);
        skillPanelBlockerFade.DOFade(0.7f, 0.5f);

        skillPanelRect.DOMove(new Vector2(Screen.width / 2, Screen.height / 2), 0.5f);
    }

    public void CloseSkillPanel()
    {
        timeManager.IsTimePaused = false;
        skillPanelBlockerFade.DOFade(0, 0.5f).onComplete += () => skillPanelBlockerFade.gameObject.SetActive(false);

        skillPanelRect.DOMove(new Vector2(Screen.width + Screen.width / 2, Screen.height / 2), 0.5f);
    }

    public void OpenEarningHistoryPanel()
    {
        if (!earningHistoryPanelBlockerFade.gameObject.activeSelf)
            UpdateVisualEarning();
        timeManager.IsTimePaused = true;
        earningHistoryPanelBlockerFade.gameObject.SetActive(true);
        earningHistoryPanelBlockerFade.DOFade(0.7f, 0.5f);

        earningHistoryPanelRect.DOMove(new Vector2(Screen.width / 2, Screen.height / 2), 0.5f);
    }

    public void CloseEarningHistoryPanel()
    {
        timeManager.IsTimePaused = false;
        earningHistoryPanelBlockerFade.DOFade(0, 0.5f).onComplete += () => earningHistoryPanelBlockerFade.gameObject.SetActive(false);

        earningHistoryPanelRect.DOMove(new Vector2(-Screen.width - Screen.width / 2, Screen.height / 2), 0.5f);
        ClearVisualEarning();
    }

    public void UpdateVisualEarning()
    {
        foreach (EarningDayHistory historyDay in earningsManager.EarningsList)
        {

            TMP_Text dayBlockText = Instantiate(dayBlock, earningsContainerTransform).GetComponent<TMP_Text>();
            dayBlockText.text = $"----------------------День {historyDay.Day}----------------------";
            foreach (EarningsHistoryCategory category in historyDay.Catigories)
            {
                TMP_Text categoryText = Instantiate(categoryBlock, earningsContainerTransform).GetComponent<TMP_Text>();
                categoryText.text = $"+{category.EarningMoney}$ {category.CategoryName} -{category.ExpensesMoney}$";
            }

            TMP_Text allMoneyLabelText = Instantiate(allLabelBlock, earningsContainerTransform).GetComponent<TMP_Text>();
            allMoneyLabelText.text = "----------Итог----------";

            int earningsSum = 0;
            int expensesSum = 0;
            foreach (EarningsHistoryCategory earn in historyDay.Catigories)
            {
                earningsSum += earn.EarningMoney;
                expensesSum -= earn.ExpensesMoney;
            }


            TMP_Text allMoneyText = Instantiate(allMoneyBlock, earningsContainerTransform).GetComponent<TMP_Text>();
            allMoneyText.text = $"+{earningsSum}$ {expensesSum}$";
        }
    }
    public void ClearVisualEarning()
    {
        for (int i = 0; i < earningsContainerTransform.childCount; i++)
            Destroy(earningsContainerTransform.GetChild(i).gameObject);
    }
}