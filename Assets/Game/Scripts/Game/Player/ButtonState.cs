using UnityEngine;

namespace Game.Scripts.Game.Player
{
	public class ButtonState : MonoBehaviour
	{
		private void OnDestroy()
		{
			UnsubscribeFromEvent();
		}

		private void Start()
		{
			SubscribeToEvent();
		}

		private void SubscribeToEvent()
		{
			Player.Instance.OnControlActive += SetState;
		}
		
		private void UnsubscribeFromEvent()
		{
			Player.Instance.OnControlActive -= SetState;
		}

		private void SetState(bool state)
		{
			gameObject.SetActive(state);
		}
	}
}