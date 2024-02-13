using Game.Scripts.Wallet;
using UnityEngine;
using UnityEngine.Events;

namespace Game.DailyReward.Scripts
{
	public class DailyReward : MonoBehaviour
	{
		[SerializeField, Header("Events")] private UnityEvent<int> onRewardGet;
		
		[SerializeField, Header("Reward Values")] private int[] rewardAmounts = { 10, 20, 30, 50, 60, 80, 100 };

		private int _dayCounter;
		
		private void Start()
		{
			GetReward();
		}

		public void SetDayCounter(int amount)
		{
			_dayCounter = amount;
			_dayCounter = Mathf.Clamp(_dayCounter, 0, rewardAmounts.Length - 1);
		}
		
		private void GetReward()
		{
			var coins = rewardAmounts[_dayCounter];

			Wallet.AddMoney(coins);
			onRewardGet?.Invoke(coins);
			
			Debug.Log($"Received {coins} coins as daily reward.");
		}
	}
}