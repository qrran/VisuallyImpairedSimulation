using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
	[Range(0f, 2f)]
	[SerializeField] private float waypointSize = 1f;

	private void OnDrawGizmos()
	{
		if (transform.childCount == 0)
			return;

		// Draw waypoints as 2D circles
		Gizmos.color = Color.blue;
		foreach (Transform t in transform)
		{
			// Using 2D circle (flat on X and Y, with Z = 0)
			Gizmos.DrawWireSphere(new Vector3(t.position.x, t.position.y, 0f), waypointSize);
		}

		// Draw lines connecting waypoints
		Gizmos.color = Color.red;
		for (int i = 0; i < transform.childCount - 1; i++)
		{
			Vector3 start = new Vector3(transform.GetChild(i).position.x, transform.GetChild(i).position.y, 0f);
			Vector3 end = new Vector3(transform.GetChild(i + 1).position.x, transform.GetChild(i + 1).position.y, 0f);
			Gizmos.DrawLine(start, end);
		}

		// Connect the last waypoint to the first for looping
		Vector3 lastWaypoint = new Vector3(transform.GetChild(transform.childCount - 1).position.x, transform.GetChild(transform.childCount - 1).position.y, 0f);
		Vector3 firstWaypoint = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y, 0f);
		Gizmos.DrawLine(lastWaypoint, firstWaypoint);
	}

	public Transform GetNextWaypoint(Transform currentWaypoint)
	{
		if (transform.childCount == 0)
			return null;

		if (currentWaypoint == null)
		{
			// Start with the first waypoint
			return transform.GetChild(0);
		}

		// Get the next waypoint in sequence
		int nextIndex = (currentWaypoint.GetSiblingIndex() + 1) % transform.childCount;
		return transform.GetChild(nextIndex);
	}
}
