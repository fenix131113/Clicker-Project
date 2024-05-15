using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalObjectsContainer : MonoBehaviour
{
    [SerializeField] private GameObject mafiaTakeMoneyAskPanel;
    public GameObject MafiaTakeMoneyAskPanel => mafiaTakeMoneyAskPanel;


    [SerializeField] private RectTransform skillInfoPanel;
    public RectTransform SkillInfoPanelRectTransform => skillInfoPanel;


    [SerializeField] private HoldButton acceptPaymentToMafiaButton;
    public HoldButton AcceptPaymentToMafiaButton => acceptPaymentToMafiaButton;


    [SerializeField] private HoldButton denyPaymentToMafiaButton;
    public HoldButton DenyPaymentToMafiaButton => denyPaymentToMafiaButton;


    [SerializeField] private GameObject noticePanelPrefab;
    public GameObject NoticePanelPrefab => noticePanelPrefab;


    [SerializeField] private Transform notificationsContainer;
    public Transform NotificationsContainer => notificationsContainer;


    [SerializeField] private MainClicker clickerScript;
    public MainClicker ClickerScript => clickerScript;


    [SerializeField] private GameObject askForTournamentPanel;
    public GameObject AskForTournamentPanel => askForTournamentPanel;


    [SerializeField] private HoldButton acceptTournamentButton;
    public HoldButton AcceptTournamentButton => acceptTournamentButton;


    [SerializeField] private HoldButton denyTournamentButton;
    public HoldButton DenyTournamentButton => denyTournamentButton;


    [SerializeField] private Image tournamentProgressFiller;
    public Image TournamentProgressFiller => tournamentProgressFiller;


    [SerializeField] private TMP_Text tournamentProgressText;
    public TMP_Text TournamentProgressText => tournamentProgressText;


    [SerializeField] private Image holdButtonFiller;
    public Image HoldButtonFiller => holdButtonFiller;


    [SerializeField] private TMP_Text loosePanText;
    public TMP_Text LoosePanText => loosePanText;


    [SerializeField] private GameObject loosePanBlocker;
    public GameObject LoosePanBlocker => loosePanBlocker;

    [SerializeField] private GameInitializer gameInitializer;
    public GameInitializer GameInitializer => gameInitializer;
}