using DG.Tweening;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private int _score = 0;
	public int Score => _score;

	private int _clickPower = 1;
	public int ClickPower => _clickPower;

	private float _autoClickRate = 2;
	public float AutoClickRate => _autoClickRate;

	private int _autoClickPower = 1;
	public int AutoClickPower => _autoClickPower;

	private Tweener cameraBackPosTweener;

	public AudioClip buySound;
	public AudioClip[] cookieClickSounds;

	public Image endGameBlackImage;
	public Transform infoPanel, infoPanelBlock, background;
	public GameObject fadeCookiePrefab, cookieClickSoundPrefab;
	public TMP_Text scoreText, clickPowerText, autoClickPowerText;


	private void Update()
	{
		scoreText.text = $"Score: {Score}";
		if (Camera.main.transform.position.x == 0 && Camera.main.transform.position.y == 0)
		{
			cameraBackPosTweener.Kill();
			cameraBackPosTweener = null;
		}
		if (cameraBackPosTweener != null && (Camera.main.transform.position.x != 0 || Camera.main.transform.position.y != 0))
		{
			cameraBackPosTweener = Camera.main.transform.DOMove(new Vector3(0, 0, 0), 1f);
			cameraBackPosTweener.onComplete += () => cameraBackPosTweener = null;
		}
    }

    public void AddAutoClickRate(float time)
	{
		_autoClickRate += time;
	}
	public void RemoveAutoClickRate(float time)
	{
		_autoClickRate = Mathf.Clamp(_autoClickRate - time, 1f, float.MaxValue);
    }
	public void AddAutoClickPower(int count)
	{
		_autoClickPower += count;
        autoClickPowerText.text = $"PPAC: {AutoClickPower}";
    }
	public void RemoveAutoClickPower(int count)
	{
		_autoClickPower = Mathf.Clamp(_autoClickPower - count, 1, int.MaxValue);
        autoClickPowerText.text = $"PPAC: {AutoClickPower}";
    }
	public void AddClickPower(int count)
	{
		_clickPower += count;
        clickPowerText.text = $"PPC: {ClickPower}";
    }
	public void RemoveClickPower(int count)
	{
		_clickPower = Mathf.Clamp(_clickPower - count, 1, int.MaxValue);
        clickPowerText.text = $"PPC: {ClickPower}";
    }
	public void AddScore(int count)
	{
		_score += count;
	}
	public void RemoveScore(int count)
	{
		_score = Mathf.Clamp(_score - count, 0, int.MaxValue);
	}

	public void SetButtonInteractable(Button button)
	{
		if (button.GetComponent<ShopItem>() != null && button.GetComponent<ShopItem>().BuyedCount < button.GetComponent<ShopItem>().MaxBuyCount)
			button.interactable = true;
	}
    public void SetButtonNonInteractable(Button button)
    {
        button.interactable = false;
    }

	public void ActivateInfoPanel()
	{
		const float timeDuration = 0.5f;
		infoPanel.DOMoveY(0, timeDuration);
		infoPanelBlock.gameObject.SetActive(true);
        FindObjectOfType<MainCookie>().canClick = false;
        infoPanelBlock.GetComponent<Image>().DOFade(0.45f, timeDuration);
	}
	public void DeactivateInfoPanel()
    {
        const float timeDuration = 0.5f;
        infoPanel.DOLocalMoveY(-930, timeDuration);
        Tweener infoBlockFade = infoPanelBlock.GetComponent<Image>().DOFade(0, timeDuration);
		infoBlockFade.onComplete += () => { infoPanelBlock.gameObject.SetActive(false); FindObjectOfType<MainCookie>().canClick = true; };
    }
	public void CreateAudio(AudioClip clip)
	{
        Instantiate(cookieClickSoundPrefab, Vector3.zero, Quaternion.identity).GetComponent<CoockieClickSoundSource>().Init(clip);
    }
	public void Win()
	{
		endGameBlackImage.gameObject.SetActive(true);
		background.DOScale(new Vector3(2.1f, 2.1f, 2.1f), 2f);
		Tweener endBlackTween = endGameBlackImage.DOFade(1f, 2f);
		endBlackTween.onComplete += () => SceneManager.LoadScene("WinScene");
	}
}
