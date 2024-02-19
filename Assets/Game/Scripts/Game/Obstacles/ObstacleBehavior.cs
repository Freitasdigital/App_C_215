using Game.Scripts.Game.Interfaces;
using UnityEngine;

namespace Game.Scripts.Game.Obstacles
{
	public class ObstacleBehavior : MonoBehaviour
	{
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (!other.gameObject.TryGetComponent(out IDamageable unit)) return;

			unit.TakeDamage();
		}
	}
}