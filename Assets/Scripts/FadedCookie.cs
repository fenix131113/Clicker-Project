using UnityEngine;

public class FadedCookie : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 4f);
    }
}
