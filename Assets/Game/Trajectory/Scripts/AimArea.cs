using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Trajectory.Scripts
{
	[RequireComponent(typeof(Image))]
	public class AimArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField, Header("Gun")] private GunBehavior gun;
		
		private Image _image;

		private void OnValidate()
		{
			if (gun == null)
			{
				Debug.LogWarning($"{nameof(gun)} is null");
			}
		}

		private void Awake()
		{
			_image = GetComponent<Image>();
		}
		
		public void OnPointerDown(PointerEventData eventData)
		{
			gun.SetTrajectoryActive();
		}
		
		public void OnPointerUp(PointerEventData eventData)
		{
			gun.Shot();
			//SetInactive();
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