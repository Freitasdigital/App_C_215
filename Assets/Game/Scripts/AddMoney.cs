using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
	[RequireComponent(typeof(Button))]
	public class AddMoney : MonoBehaviour
	{
		[SerializeField, Header("Reward")] private TextMeshProUGUI rewardText;
		
		[SerializeField, Header("Reward")] private int reward = 100;
	
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
			
			_button.onClick.AddListener(Add);
			DisplayRewardAmount(reward);
		}

		private void DisplayRewardAmount(int amount)
		{
			rewardText.text = $"+{amount} Coins";
		}

		private void Add()
		{
			Wallet.Wallet.AddMoney(reward);
		}
	}
}