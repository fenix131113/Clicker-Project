using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class NoticeSystem
{
    private GlobalObjectsContainer objectsContainer;

    [Inject]
    private void Init(GlobalObjectsContainer objectsContainer) => this.objectsContainer = objectsContainer;

    public void CreatNewNotification(string message)
    {
        GameObject notification = Object.Instantiate(objectsContainer.NoticePanelPrefab, objectsContainer.NotificationsContainer);
        notification.transform.GetChild(0).GetComponent<TMP_Text>().text = message;
        notification.GetComponent<RectTransform>().DOMoveY(notification.GetComponent<RectTransform>().position.y + notification.GetComponent<RectTransform>().sizeDelta.y, 0.5f).onComplete += () =>
        {
            Sequence deleteNotificationAnim = DOTween.Sequence();
            deleteNotificationAnim.Insert(2.5f, notification.GetComponent<RectTransform>().DOMoveY(notification.GetComponent<RectTransform>().position.y - notification.GetComponent<RectTransform>().sizeDelta.y - 1, 0.5f));
            deleteNotificationAnim.onComplete += () => Object.Destroy(notification);
        };
    }
}
