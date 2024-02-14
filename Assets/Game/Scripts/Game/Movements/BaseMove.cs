using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Game.Movements
{
	public abstract class BaseMove : MonoBehaviour
	{
		protected Tween _tween;

		private void OnDestroy()
		{
			StopTween();
		}

		public abstract void Move(float endDestination, float durationTime);
		
		private void StopTween()
		{
			if (_tween != null && _tween.IsActive())
			{
				_tween.Kill();
			}
		}
	}
}