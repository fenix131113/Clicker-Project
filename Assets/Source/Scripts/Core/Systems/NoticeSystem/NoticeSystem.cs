using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class NoticeSystem : ITickable
{
    private GlobalObjectsContainer _objectsContainer;
    private Queue _notificationsQueue = new Queue();
    private bool _messageCreated;

    [Inject]
    private void Init(GlobalObjectsContainer objectsContainer) => _objectsContainer = objectsContainer;

    public void CreateNewNotification(string message)
    {
        _notificationsQueue.Enqueue(message);
    }

    public void Tick()
    {
        if (_notificationsQueue.Count > 0 && !_messageCreated)
        {
            _messageCreated = true;
            CreateMessage(_notificationsQueue.Dequeue() as string);
        }
    }

    private void CreateMessage(string message)
    {
        GameObject notification = Object.Instantiate(_objectsContainer.NoticePanelPrefab, _objectsContainer.NotificationsContainer);
        notification.transform.GetChild(0).GetComponent<TMP_Text>().text = message;
        notification.GetComponent<RectTransform>().DOLocalMoveY(notification.GetComponent<RectTransform>().localPosition.y + notification.GetComponent<RectTransform>().sizeDelta.y, 0.5f).onComplete += () =>
        {
            Sequence deleteNotificationAnim = DOTween.Sequence();
            deleteNotificationAnim.Insert(2.5f, notification.GetComponent<RectTransform>().DOLocalMoveY(notification.GetComponent<RectTransform>().localPosition.y - notification.GetComponent<RectTransform>().sizeDelta.y - 1, 0.5f));
            deleteNotificationAnim.onComplete += () => { Object.Destroy(notification); _messageCreated = false; };
        };
    }
}