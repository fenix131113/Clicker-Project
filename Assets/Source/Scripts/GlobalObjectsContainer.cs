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
}