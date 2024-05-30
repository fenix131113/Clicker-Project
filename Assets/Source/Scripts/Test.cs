using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    private NoticeSystem _notices;
    [Inject]
    private void Init(NoticeSystem notices)
    {
        _notices = notices;
    }
    [ContextMenu("Create notice rnd")]
    private void CreateNotificationRandom()
    {
        string message = Random.Range(100, 100000).ToString();
        _notices.CreateNewNotification(message);
    }
}
