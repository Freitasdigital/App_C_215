using UnityEngine;

namespace Game.Scripts.Game.Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField, Header("Move Settings"), Min(0)] private float speed = 10f;
		
		private Rigidbody2D _rigidbody2D;

		private Vector2 _direction = Vector2.zero;
		
		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}
		
		private void FixedUpdate()
		{
			_rigidbody2D.velocity = _direction * (speed * Time.fixedDeltaTime);
		}

		public void SetDirection(Vector2 direction)
		{
			_direction = direction;
		}
	}
}