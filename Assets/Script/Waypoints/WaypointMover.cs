using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
	[SerializeField] private Waypoints waypoints;
	[SerializeField] private float moveSpeed = 1f;
	[SerializeField] private float distanceThreshold = 0.1f;

	private Transform currentWaypoint;

	// Start is called before the first frame update
	void Start()
	{
		// Initialize the current waypoint
		currentWaypoint = waypoints.GetNextWaypoint(null);
		transform.position = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, 0f);

		// Set the next waypoint
		currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
		RotateTowards(currentWaypoint.position);
	}

	// Update is called once per frame
	void Update()
	{
		// Move towards the current waypoint
		Vector3 targetPosition = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, 0f);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

		// Check if close enough to the waypoint
		if (Vector3.Distance(transform.position, targetPosition) < distanceThreshold)
		{
			// Get the next waypoint
			currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
			RotateTowards(currentWaypoint.position);
		}
	}

	// Rotate towards the target position in 2D
	private void RotateTowards(Vector3 targetPosition)
	{
		Vector3 direction = (targetPosition - transform.position).normalized;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, angle);
	}
}



















//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// for the blind character
//public class WaypointMover : MonoBehaviour
//{
//	[SerializeField] private Waypoints waypoints;
//	[SerializeField] private float moveSpeed = 5f;
//	[SerializeField] private float distanceThreshold = 0.1f;

//	private Transform currentWaypoint;

//	[SerializeField] private float avoidanceRadius = 0.1f;
//	[SerializeField] private float avoidanceSpeed = 2f;
//	[SerializeField] private float caneLength = 0.5f; // Length of the cane for obstacle detection
//	private bool isStopped = false;
//	private Transform cane;

//	// Start is called before the first frame update
//	void Start()
//	{
//		// Find the cane (child object)
//		//cane = transform.Find("Cane");

//		//if (cane == null)
//		//{
//		//	Debug.LogError("Cane not found! Make sure you have a child object named 'Cane'.");
//		//	return;
//		//}

//		// Initialize the current waypoint
//		currentWaypoint = waypoints.GetNextWaypoint(null);
//		transform.position = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, 0f);

//		// Set the next waypoint
//		currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
//		RotateTowards(currentWaypoint.position);
//	}

//	// Update is called once per frame
//	void Update()
//	{
//		//if (isStopped)
//		//	return;

//		//Vector3 targetPosition = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, 0f);

//		//// Check for obstacles using a "cane" (raycast)
//		//if (IsObstacleAhead())
//		//{
//		//	isStopped = true;
//		//	StartCoroutine(HandleObstacleAvoidance());
//		//	return;
//		//}

//		//transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

//		//if (Vector3.Distance(transform.position, targetPosition) < distanceThreshold)
//		//{
//		//	currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
//		//	if (currentWaypoint != null)
//		//	{
//		//		RotateTowards(currentWaypoint.position);
//		//	}
//		//	else
//		//	{
//		//		isStopped = true; // No more waypoints
//		//	}
//		//}
//		// Move towards the current waypoint
//		Vector3 targetPosition = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, 0f);
//		transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

//		// Check if close enough to the waypoint
//		if (Vector3.Distance(transform.position, targetPosition) < distanceThreshold)
//		{
//			// Get the next waypoint
//			currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
//			//RotateTowards(currentWaypoint.position);
//			if (currentWaypoint != null)
//			{
//				RotateTowards(currentWaypoint.position);
//			}
//			else
//			{
//				// No more waypoints, stop movement
//				isStopped = true;
//			}
//		}
//	}

//	void OnCollisionEnter2D(Collision2D collision)
//	{
//		// If the collision is with an obstacle, stop and reroute
//		//if (collision.gameObject.tag == "Obstacle")
//		//{
//			isStopped = true; // Stop movement
//			//StartCoroutine(HandleObstacleAvoidance());
//		//}
//	}

//	//private IEnumerator HandleObstacleAvoidance()
//	//{
//	//	// Perform simple obstacle avoidance logic
//	//	// Example: rotate the object and move in a different direction to avoid the obstacle
//	//	// Note: this is a basic approach; for complex cases, use Unity's NavMesh or other pathfinding methods

//	//	// Rotate the mover to avoid the obstacle
//	//	float avoidanceAngle = 45f; // Example avoidance angle
//	//	transform.Rotate(0f, 0f, avoidanceAngle);

//	//	// Move a little to avoid the obstacle
//	//	Vector3 newDirection = transform.right * avoidanceRadius;
//	//	transform.position += newDirection;

//	//	yield return new WaitForSeconds(0.5f); // Pause to simulate avoidance

//	//	// Resume movement towards the current waypoint
//	//	isStopped = false;
//	//}
//	//private IEnumerator HandleObstacleAvoidance()
//	//{
//	//	// Reduce speed to simulate careful movement when avoiding obstacles
//	//	float originalSpeed = moveSpeed;
//	//	moveSpeed = avoidanceSpeed;

//	//	// Rotate gently to avoid the obstacle
//	//	float avoidanceAngle = 45f; // Example angle for avoidance
//	//	transform.Rotate(0f, 0f, avoidanceAngle);

//	//	// Move a bit to the side to avoid the obstacle
//	//	Vector3 newDirection = transform.right * avoidanceRadius;
//	//	transform.position += newDirection;

//	//	yield return new WaitForSeconds(1f); // Longer wait to simulate careful navigation

//	//	// Resume normal speed
//	//	moveSpeed = originalSpeed;

//	//	isStopped = false; // Continue to next waypoint
//	//}

//	//private bool IsObstacleAhead()
//	//{
//	//	// Raycast from the character to detect obstacles ahead
//	//	RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, caneLength);
//	//	return hit.collider != null;
//	//}

//	// Rotate towards the target position in 2D
//	private void RotateTowards(Vector3 targetPosition)
//	{
//		Vector3 direction = (targetPosition - transform.position).normalized;
//		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//		transform.rotation = Quaternion.Euler(0f, 0f, angle);
//	}
//}
