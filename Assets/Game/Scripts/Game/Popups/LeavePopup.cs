using Game.Scripts.Enums;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Game.Popups
{
	public class LeavePopup : MonoBehaviour
	{
		public void Yes()
		{
			if (ScenesManager.Instance != null)
			{
				ScenesManager.Instance.LoadScene(Scenes.MenuScene);
			}
			else
			{
				Debug.LogWarning("ScenesManager.Instance is null");
			}
		}
		
		public void No()
		{
			gameObject.SetActive(false);
		}
	}
}