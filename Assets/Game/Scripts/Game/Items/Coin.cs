using Game.Scripts.Audio;
using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Game.Items
{
	public class Coin : Item
	{
		protected override void Collect()
		{
			if (GameLogic.GameLogic.Instance != null)
			{
				GameLogic.GameLogic.Instance.AddScore(Reward);
			}
			else
			{
				Debug.LogWarning($"{nameof(GameLogic)} is null");
			}
			
			AudioEffectsManager.PlaySound(AudioClips.Coin.ToString());
			base.Collect();
		}
	}
}