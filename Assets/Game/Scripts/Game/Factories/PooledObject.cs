using Game.Scripts.Game.Interfaces;
using UnityEngine;

namespace Game.Scripts.Game.Factories
{
	public class PooledObject : MonoBehaviour, IConstructable
	{
		private ObjectPool _pool;

		public void SetPool(ObjectPool pool)
		{
			_pool = pool;
		}

		public void Construct(Vector3 position)
		{
			transform.position = position;
		}
		
		public void Release()
		{
			if (_pool != null)
			{
				_pool.ReturnToPool(this);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}