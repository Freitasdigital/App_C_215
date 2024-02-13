using Game.Scripts.Game.Factories;
using UnityEngine;

namespace Game.Scripts.Game.Bounds
{
	public class BoundBehavior : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.TryGetComponent(out PooledObject pooledObject)) return;
			
			pooledObject.Release();
		}
	}
}