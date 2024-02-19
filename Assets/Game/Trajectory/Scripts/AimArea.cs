using Game.Scripts.Game.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Trajectory.Scripts
{
	[RequireComponent(typeof(Image))]
	public class AimArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private Image _image;

		private void Awake()
		{
			_image = GetComponent<Image>();
		}
		
		public void OnPointerDown(PointerEventData eventData)
		{
			if (Player.Instance != null)
			{
				Player.Instance.SetTrajectoryActive();
			}
			else
			{
				Debug.LogWarning($"{nameof(Player)} is null");
			}
		}
		
		public void OnPointerUp(PointerEventData eventData)
		{
			if (Player.Instance != null)
			{
				Player.Instance.ThrowBall();
				SetInactive();
			}
			else
			{
				Debug.LogWarning($"{nameof(Player)} is null");
			}
		}
		
		public void SetActive()
		{
			_image.enabled = true;
		}

		private void SetInactive()
		{
			_image.enabled = false;
		}
	}
}