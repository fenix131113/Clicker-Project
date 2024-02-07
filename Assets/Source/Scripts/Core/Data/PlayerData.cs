using Clicker.Core.Workers;
using System.Collections.Generic;
using UnityEngine;

// All data in this script will save in database
public class PlayerData
{
	[SerializeField] private RobberyManager _robberyManager;
	public RobberyManager RobberyManager => _robberyManager;


	[SerializeField] private WorkersManager _workersManager;
	public WorkersManager WorkersManager => _workersManager;


	[SerializeField] private GeneralPassiveMoneyController _passiveMoneyController;
	public GeneralPassiveMoneyController PassiveMoneyController => _passiveMoneyController;


	[SerializeField] private MafiaManager _mafiaManager;
	public MafiaManager MafiaManager => _mafiaManager;


	private int _money;
	public int Money
	{
		get { return _money; }
		set
		{
			_money = value;
			if (value < 0)
			{
				Debug.LogWarning("Money can't be -value");
				_money = 0;
			}
		}
	}

	[SerializeField] private int _skillPoints;
	public int SkillPoints => _skillPoints;
	public void AddSkillPoints(int count) => _skillPoints += count;
	public void RemoveSkillPoints(int count) => _skillPoints -= count;


	private bool[] _unlockedFood = new bool[5] { true, false, false, false, false};
	public IReadOnlyCollection<bool> UnlockedFood => _unlockedFood;
	public void UnlockFood(int index) => _unlockedFood[index] = true;


	private int _clickPower = 1;
	public int ClickPower => _clickPower;
	public void SetClickPower(int count) => _clickPower = count;


	private int _moneyPerFood = 1;
	public int MoneyPerFood => _moneyPerFood;
	public void SetMoneyPerFood(int count) => _moneyPerFood = count;


	private int _maxProgressBarClicks = 15;
	public int MaxProgressBarClicks => _maxProgressBarClicks;
	public void SetMaxProgressBarClicks(int count) => _maxProgressBarClicks = count;


    [SerializeField] private int _currentClickerProgress = 0;
	public int CurrentClickerProgress => _currentClickerProgress;
	public void SetCurrentClickerProgress(int count) => _currentClickerProgress = count;


    public PlayerData(RobberyManager robberyManager, WorkersManager workersManager, GeneralPassiveMoneyController passiveMoneyController)
    {
        _robberyManager = robberyManager;
		robberyManager.SetData(this);

		_workersManager = workersManager;
		_workersManager.SetData(this);

		_passiveMoneyController = passiveMoneyController;
		passiveMoneyController.SetData(this);
    }
}
