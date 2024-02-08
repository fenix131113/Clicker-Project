using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalObjectsContainer : MonoBehaviour
{
    [SerializeField] private GameObject mafiaTakeMoneyAskPanel;
    public GameObject MafiaTakeMoneyAskPanel => mafiaTakeMoneyAskPanel;


    [SerializeField] private RectTransform skillInfoPanel;
    public RectTransform SkillInfoPanelRectTransform => skillInfoPanel;


    [SerializeField] private Button acceptPaymentToMafiaButton;
    public Button AcceptPaymentToMafiaButton => acceptPaymentToMafiaButton;


    [SerializeField] private Button denyPaymentToMafiaButton;
    public Button DenyPaymentToMafiaButton => denyPaymentToMafiaButton;


    [SerializeField] private GameObject noticePanelPrefab;
    public GameObject NoticePanelPrefab => noticePanelPrefab;


    [SerializeField] private Transform notificationsContainer;
    public Transform NotificationsContainer => notificationsContainer;


    [SerializeField] private MainClicker clickerScript;
    public MainClicker ClickerScript => clickerScript;


    [SerializeField] private GameObject askForTournamentPanel;
    public GameObject AskForTournamentPanel => askForTournamentPanel;


    [SerializeField] private Button acceptTournamentButton;
    public Button AcceptTournamentButton => acceptTournamentButton;


    [SerializeField] private Button denyTournamentButton;
    public Button DenyTournamentButton => denyTournamentButton;


    [SerializeField] private Image tournamentProgressFiller;
    public Image TournamentProgressFiller => tournamentProgressFiller;


    [SerializeField] private TMP_Text tournamentProgressText;
    public TMP_Text TournamentProgressText => tournamentProgressText;
}