using UnityEngine;
using DG.Tweening;

public class MainCookie : MonoBehaviour
{
	private Tweener scaleAnim;
	private GameManager gm;

	public bool canClick = true;
	private void Start()
	{
		gm = FindObjectOfType<GameManager>();
	}
	private void Update()
	{
		if (scaleAnim != null && !scaleAnim.active)
			scaleAnim = transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f);
	}
	private void ClickAnimation()
	{
		if (transform.lossyScale.x <= 1.09f)
			scaleAnim = transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.15f);
	}
	public void CreateFadedCookie(Vector3 pos = default(Vector3))
	{
        Instantiate(gm.fadeCookiePrefab, pos, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-4f, 4f), Random.Range(1, 11)), ForceMode2D.Impulse);
	}
	public void RandomShakeCam()
	{
		Camera.main.DOShakePosition(0.15f, 0.1f, 10, 90, false).onComplete += () => { Camera.main.transform.position = new Vector3(0, 0, -10); };
	}
	private void OnMouseDown()
	{
		if (canClick)
		{
			gm.CreateAudio(gm.cookieClickSounds[Random.Range(0, gm.cookieClickSounds.Length)]);
			gm.AddScore(gm.ClickPower);
			RandomShakeCam();
			CreateFadedCookie();
			ClickAnimation();
		}
	}
}
