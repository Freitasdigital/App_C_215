using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Audio
{
	public class DestroySound : MonoBehaviour
	{
		[SerializeField] private AudioClips audioClip;

		private void OnDestroy()
		{
			Play();
		}

		private void Play()
		{
			AudioEffectsManager.PlaySound(audioClip.ToString());
		}
	}
}