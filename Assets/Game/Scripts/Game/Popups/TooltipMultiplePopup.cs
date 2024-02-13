using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Game.Scripts.Game.Popups
{
	public class TooltipMultiplePopup : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField, Header("Tooltips List")] private List<GameObject> tooltipsList;
		
		[SerializeField, Header("Click Settings")] private bool isClickActive = true;
		
		[SerializeField, Header("Timer Settings")] private bool isTimerActive;
		[SerializeField, Min(0)] private float displayTime = 3f;
		
		private int _index;

		private void OnValidate()
		{
			if (tooltipsList.Count <= 0)
			{
				Debug.LogWarning("Tooltips List is empty");
			}
		}
		
		public void OnPointerClick(PointerEventData eventData)
		{
			if (!isClickActive) return;

			SetNextTooltip();
		}
		
		public void SetActive()
		{
			gameObject.SetActive(true);
			
			if (isTimerActive)
			{
				StartCoroutine(SwitchTooltip());
			}
		}
		
		private void SetNextTooltip()
		{
			if (_index < tooltipsList.Count - 1)
			{
				_index++;
				SetTooltipActive();
			}
			else
			{
				SetFirstPlayDone();
				DisableTooltip();
			}
		}

		private void SetTooltipActive()
		{
			for (int i = 0; i < tooltipsList.Count; i++)
			{
				tooltipsList[i].SetActive(i == _index);
			}
		}
		
		private IEnumerator SwitchTooltip()
		{
			for (int i = 0; i < tooltipsList.Count; i++)
			{
				yield return new WaitForSecondsRealtime(displayTime);
				
				SetNextTooltip();
			}

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
				Debug.LogWarning("GameLogic.Instance is null");
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