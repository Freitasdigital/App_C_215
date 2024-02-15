using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Game.Popups
{
	public class TooltipSinglePopup : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField, Header("Click Settings")] private bool isClickActive = true;
		
		[SerializeField, Header("Timer Settings")] private bool isTimerActive;
		[SerializeField, Min(0)] private float displayTime = 3f;
		
		public void OnPointerClick(PointerEventData eventData)
		{
			if (!isClickActive) return;

			SetFirstPlayDone();
			DisableTooltip();
		}

		public void SetActive()
		{
			gameObject.SetActive(true);
			
			if (isTimerActive)
			{
				StartCoroutine(SetActiveState());
			}
		}

		private IEnumerator SetActiveState()
		{
			yield return new WaitForSecondsRealtime(displayTime);

			SetFirstPlayDone();
			DisableTooltip();
		}
		
		private void SetFirstPlayDone()
		{
			if (GameLogic.GameLogic.Instance != null)
			{
				GameLogic.GameLogic.Instance.SetFirstPlayDone();
			}
			else
			{
				Debug.LogWarning($"{nameof(GameLogic)} is null");
			}
		}
		
		private void DisableTooltip()
		{
			StopAllCoroutines();
			SetInactiveState();
		}
		
		private void SetInactiveState()
		{
			gameObject.SetActive(false);
		}
	}
}