using UnityEngine;
using UnityEngine.UI;

namespace Game.DailyReward.Scripts
{
	[RequireComponent(typeof(Button))]
	public class DailyButtonState : MonoBehaviour
	{
		private Button _button => GetComponent<Button>();
		
		private void Awake()
		{
			_button.onClick.AddListener(SetInactive);
		}

		public void SetState(bool state)
		{
			gameObject.SetActive(state);
		}

		private void SetInactive()
		{
			SetState(false);
		}
	}
}