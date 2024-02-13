using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Game.Factories
{
	public class ObjectPool : MonoBehaviour
	{
		[SerializeField, Header("Pooled Objects Prefabs")] private List<PooledObject> prefabsList;

		[SerializeField, Header("Pool Size"), Min(0)] private int poolSize = 10;
		
		public event Action OnItemReturnToPool;
		
		private List<PooledObject> _pool;

		private int _lastActiveIndex;
		private int _currentIndex = -1;

		private void OnValidate()
		{
			if (prefabsList.Count <= 0)
			{
				Debug.LogWarning($"{nameof(prefabsList)} is empty");
			}
		}
		
		private void OnDestroy()
		{
			DestroyChildObjects();
		}

		public void Init()
		{
			_pool = new List<PooledObject>();
			
			for (int i = 0; i < poolSize; i++)
			{
				InstantiateObject();
			}
		}
		
		public PooledObject GetObjectFromPool()
		{
			var inactiveObjects = _pool.Where(obj => !obj.gameObject.activeInHierarchy).ToList();

			var newObject = inactiveObjects.Count > 0 
				? prefabsList.Count <= 1 ? inactiveObjects.First() : inactiveObjects[RandomActiveIndex(inactiveObjects.Count)]
				: InstantiateObject();
			
			newObject.gameObject.SetActive(true);
			
			return newObject;
		}
		
		public void ReturnToPool(PooledObject obj)
		{
			if (!_pool.Contains(obj)) return;
			
			obj.gameObject.SetActive(false);
			OnItemReturnToPool?.Invoke();
		}
		
		protected virtual PooledObject InstantiateObject()
		{
			var pooledObject = Instantiate(GetObject(), transform);
			pooledObject.gameObject.SetActive(false);
			pooledObject.SetPool(this);
			_pool.Add(pooledObject);

			return pooledObject;
		}
		
		private PooledObject GetObject()
		{
			return prefabsList.Count <= 1 ? prefabsList.First() : prefabsList[IndexBounds()];
		}
		
		private void DestroyChildObjects()
		{
			transform.DestroyChildObjects();
		}
		
		private int RandomActiveIndex(int count)
		{
			var index = Random.Range(0, count);

			if (index == _lastActiveIndex)
			{
				return RandomActiveIndex(count);
			}
            
			_lastActiveIndex = index;
			return _lastActiveIndex;
		}
		
		private int IndexBounds()
		{
			if (_currentIndex >= prefabsList.Count - 1)
			{
				_currentIndex = 0;
			}
			else
			{
				_currentIndex++;
			}
			
			return _currentIndex;
		}
	}
}