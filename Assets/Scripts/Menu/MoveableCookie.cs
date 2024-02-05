using DG.Tweening;
using UnityEngine;

public class MoveableCookie : MonoBehaviour
{
    private Tweener moveTween;

    private void Update()
    {
        if(moveTween == null)
        {
            moveTween = transform.DOLocalMove(new Vector3(Random.Range(-862f, 862f), Random.Range(-440f, 440f), 0), Random.Range(1f, 3f));
            moveTween.onComplete += () => moveTween = null;
        }
    }
}
