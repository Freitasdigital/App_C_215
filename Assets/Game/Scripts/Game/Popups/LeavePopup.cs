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
				ScenesManager.Instance.LoadScene(Scenes.MenuScene.ToString());
			}
			else
			{
				Debug.LogWarning($"{nameof(ScenesManager)} is null");
			}
		}
		
		public void No()
		{
			gameObject.SetActive(false);
		}
	}
}