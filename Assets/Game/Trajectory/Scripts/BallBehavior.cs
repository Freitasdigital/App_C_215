using System;
using System.Collections;
using Game.Scripts.Game.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Trajectory.Scripts
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class BallBehavior : MonoBehaviour
	{
		[SerializeField, Header("Torque Settings"), Min(0f)] private float minTorque = 15f;
		[SerializeField, Min(0f)] private float maxTorque = 45f;
		
		[SerializeField, Header("Destroy Settings"), Min(0f)] private float destroyDelay = 10f;

		public event Action onBallDestroyed;
		
		private Rigidbody2D _rigidbody2D;
		private Coroutine _destroyCoroutine;

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		public void Throw(Vector2 forceDirection)
		{
			_rigidbody2D.AddForce(forceDirection, ForceMode2D.Impulse);
			_rigidbody2D.AddTorque(GetRandomTorque());
		}

		private float GetRandomTorque()
		{
			return Random.Range(minTorque, maxTorque);
		}

		private IEnumerator DestroyAfterDelay()
		{
			yield return new WaitForSeconds(destroyDelay);

			Destroy(gameObject);
			onBallDestroyed?.Invoke();
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			_destroyCoroutine ??= StartCoroutine(DestroyAfterDelay());
			
			if (!other.gameObject.TryGetComponent(out IDamageable unit)) return;

			unit.TakeDamage();
		}
	}
}