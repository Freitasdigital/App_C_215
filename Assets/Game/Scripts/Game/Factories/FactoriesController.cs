using UnityEngine;

namespace Game.Scripts.Game.Factories
{
	public class FactoriesController : MonoBehaviour
	{
		[SerializeField, Header("Factories")] private ObjectPool coinsFactory;
		[SerializeField] private ObjectPool livesFactory;
		
		[SerializeField, Header("Spawn Settings")] private Vector3 startPosition;
		[SerializeField] private float offsetY;
		[SerializeField] private float xBounds;
		[SerializeField, Min(1)] private int startItemsCount = 10;
		[SerializeField, Range(0f, 1f)] private float coinsProbability = 0.2f;

		private Vector3 _currentPosition;

		private void OnValidate()
		{
			if (coinsFactory == null)
			{
				Debug.LogWarning($"{nameof(coinsFactory)} is null");
			}
			
			if (livesFactory == null)
			{
				Debug.LogWarning($"{nameof(livesFactory)} is null");
			}
		}

		private void OnDestroy()
		{
			SubscribeFromEvents();
		}

		private void Start()
		{
			Init();
			SubscribeToEvents();
		}

		private void SubscribeToEvents()
		{
			coinsFactory.OnItemReturnToPool += GetItem;
			livesFactory.OnItemReturnToPool += GetItem;
		}
		
		private void SubscribeFromEvents()
		{
			coinsFactory.OnItemReturnToPool -= GetItem;
			livesFactory.OnItemReturnToPool -= GetItem;
		}

		private void GetItem()
		{
			var item = Random.value > coinsProbability ? GetLive() : GetCoin();
			item.Construct(_currentPosition);

			_currentPosition = GetNextPosition();
		}

		private void Init()
		{
			_currentPosition = startPosition;
			
			coinsFactory.Init();
			livesFactory.Init();

			for (int i = 0; i < startItemsCount; i++)
			{
				GetItem();
			}
		}

		private PooledObject GetCoin()
		{
			return coinsFactory.GetObjectFromPool();
		}
		
		private PooledObject GetLive()
		{
			return livesFactory.GetObjectFromPool();
		}

		private Vector3 GetNextPosition()
		{
			return new Vector3(GetRandomPositionX(), _currentPosition.y + offsetY, 0f);
		}

		private float GetRandomPositionX()
		{
			return Random.Range(-xBounds, xBounds);
		}
	}
}