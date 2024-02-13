using Game.Scripts.Game.Factories;
using UnityEngine;

namespace Game.Scripts.Game.Items
{
	public abstract class Item : MonoBehaviour
	{
		[SerializeField, Header("Reward"), Min(0)] protected int Reward = 1;
        
		[SerializeField, Header("Delay"), Min(0)] private float collectDelay = 1f;

		private Collider2D _collider2D;
		private PooledObject _pooledObject;

		private void OnEnable()
		{
			Init();
		}
		
		private void Awake()
		{
			_collider2D = GetComponent<Collider2D>();
			_pooledObject = GetComponent<PooledObject>();
		}
		
		private void Init()
		{
			_collider2D.enabled = true;
		}

		protected virtual void Collect()
		{
			_collider2D.enabled = false;
			
			Invoke(nameof(Release), collectDelay);
		}
		
		private void Release()
		{
			_pooledObject.Release();
		}
		
		private void OnTriggerEnter2D(Collider2D col)
		{
			if (!col.TryGetComponent(out Player.Player player)) return;

			Collect();
		}
	}
}