using UnityEngine;

namespace Game.Scripts.Game.Obstacles
{
	public class Obstacle : MonoBehaviour
	{
		[SerializeField, Header("Damage"), Min(1)] protected int Damage = 1;
		
		private void OnTriggerEnter2D(Collider2D col)
		{
			if (!col.TryGetComponent(out Player.Player player)) return;

			player.TakeDamage(Damage);
		}
	}
}