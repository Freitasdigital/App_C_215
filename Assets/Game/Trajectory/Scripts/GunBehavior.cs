using UnityEngine;

namespace Game.Trajectory.Scripts
{
	public class GunBehavior : MonoBehaviour
	{
		[SerializeField, Header("Links")] private BallBehavior ballPrefab;
		[SerializeField] private TrajectoryRenderer trajectory;
		
		[SerializeField, Header("Force"), Range(1f, 10f)] private float throwForce = 3f;
		
		[SerializeField, Header("Bounds")] private float yBound = -3f;

		private Camera _mainCamera;
		private Vector3 _throwDirection;

		private bool _isTrajectoryActive;

		private void OnValidate()
		{
			if (ballPrefab == null)
			{
				Debug.LogWarning($"{nameof(ballPrefab)} is null");
			}
			
			if (trajectory == null)
			{
				Debug.LogWarning($"{nameof(trajectory)} is null");
			}
		}
		
		private void Awake()
		{
			_mainCamera = Camera.main;
		}

		private void Update()
		{
			GetMousePoint();
			DrawTrajectory();
		}

		public void SetTrajectoryActive()
		{
			_isTrajectoryActive = true;
		}
		
		public void Shot()
		{
			SetTrajectoryInactive();
			
			var ball = Instantiate(ballPrefab, transform);
			ball.Throw(_throwDirection);
		}
		
		private void GetMousePoint()
		{
			if (!_isTrajectoryActive) return;
			
			var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			new Plane(-Vector3.forward, transform.position).Raycast(ray, out var enter);
			var mouseInWorld = ray.GetPoint(enter);
			
			mouseInWorld.x = Mathf.Max(mouseInWorld.x, transform.position.x);
			mouseInWorld.y = Mathf.Max(mouseInWorld.y, yBound);
			
			_throwDirection = (mouseInWorld - transform.position) * throwForce;
		}

		private void DrawTrajectory()
		{
			if (!_isTrajectoryActive) return;
			
			trajectory.DrawTrajectory(transform.position, _throwDirection);
		}
		
		private void SetTrajectoryInactive()
		{
			_isTrajectoryActive = false;
			trajectory.SetTrajectoryInactive();
		}
	}
}