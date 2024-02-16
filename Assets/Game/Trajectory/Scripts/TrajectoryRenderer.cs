using UnityEngine;

namespace Game.Trajectory.Scripts
{
	[RequireComponent(typeof(LineRenderer))]
	public class TrajectoryRenderer : MonoBehaviour
	{
		[SerializeField, Header("Line Renderer Settings"), Min(1)] private int linePoints = 100;
		[SerializeField, Min(0f)] private float timeIntervalPoints = 0.01f;
		
		[SerializeField, Header("Bounds")] private float yBound = -10f;
		
		private LineRenderer _lineRenderer;
		private Vector3[] _trajectoryPoints;

		private void Awake()
		{
			_lineRenderer = GetComponent<LineRenderer>();
			_trajectoryPoints = new Vector3[linePoints];
		}
		
		public void SetTrajectoryInactive()
		{
			_lineRenderer.enabled = false;
		}
		
		public void DrawTrajectory(Vector3 origin, Vector3 speed)
		{
			_lineRenderer.enabled = true;

			CalculateTrajectoryPoints(origin, speed);

			_lineRenderer.positionCount = _trajectoryPoints.Length;
			_lineRenderer.SetPositions(_trajectoryPoints);
		}

		private void CalculateTrajectoryPoints(Vector3 origin, Vector3 speed)
		{
			for (int i = 0; i < _trajectoryPoints.Length; i++)
			{
				var time = i * timeIntervalPoints;

				// Draw Each Point With Gravity Prediction Formula (Physics.gravity * time * time / 2f;)
				_trajectoryPoints[i] = origin + speed * time + Physics.gravity * time * time / 2f;

				// Break If Point Position.Y Is < YBound (Underground)
				if (_trajectoryPoints[i].y < yBound)
				{
					_lineRenderer.positionCount = i;
					break;
				}
			}
		}
	}
}