using Game.Scripts.Audio;
using Game.Scripts.Enums;
using Game.Scripts.Game.Animations;
using Game.Scripts.Game.Interfaces;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
	[RequireComponent(typeof(CharacterAnimation))]
	public class EnemyBehavior : MonoBehaviour, IDamageable
	{
		private CharacterAnimation _characterAnimation;
		private Collider2D _collider2D;

		private void Awake()
		{
			_characterAnimation = GetComponent<CharacterAnimation>();
			_collider2D = GetComponent<Collider2D>();
		}

		public void TakeDamage()
		{
			_collider2D.enabled = false;
			AudioEffectsManager.PlaySound(AudioClips.EnemyHit.ToString());
			SetAnimation(State.Death);

			if (GameLogic.GameLogic.Instance != null)
			{
				GameLogic.GameLogic.Instance.AddDefeatedEnemy();
			}
			else
			{
				Debug.LogWarning($"{nameof(GameLogic)} is null");
			}
		}
		
		private void SetAnimation(State state)
		{
			_characterAnimation.SetAnimation(state);
		}
	}
}