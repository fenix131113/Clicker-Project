using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
	public string _itemName;
	[SerializeField] private bool withLevels;
	[SerializeField] private int _cost, _maxBuyCount;
	[SerializeField] private float _multiplyRate;
	public UnityEvent onBuy;

	public int Cost => _cost;
	public int MaxBuyCount => _maxBuyCount;
	public float MultiplyRate => _multiplyRate;

    private int _buyedCount;
	public int BuyedCount => _buyedCount;

	private GameManager gm;

	private void Awake()
	{
		gm = FindObjectOfType<GameManager>();
        UpdateText();
	}
	public void Buy()
	{
		if (gm.Score >= _cost)
		{
			gm.RemoveScore(_cost);
			_cost = (int)(_cost * _multiplyRate);
			gm.CreateAudio(gm.buySound);
			onBuy?.Invoke();

			if (withLevels)
			{
				_buyedCount++;
				if (_buyedCount == MaxBuyCount)
					GetComponent<Button>().interactable = false;
			}
			else
				GetComponent<Button>().interactable = false;

            UpdateText();
        }
	}

	/// <summary>
	/// Use this method <b>after</b> all changes with variables 
	/// </summary>
	public void UpdateText()
	{
		if (withLevels)
			transform.GetComponentInChildren<TMP_Text>().text = $"{_itemName} ({BuyedCount}/{MaxBuyCount}) / {_cost} score";
		else
            transform.GetComponentInChildren<TMP_Text>().text = $"{_itemName} / {_cost} score";
    }
}
