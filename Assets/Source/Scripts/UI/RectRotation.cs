using UnityEngine;

public class RectRotation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private RectTransform _rect;

    private void Awake() => _rect = GetComponent<RectTransform>();
    void Update()
    {
        _rect.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }
}
