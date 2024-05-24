using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
//using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;


public class CharacterController1 : MonoBehaviour
{
	//used to manage the state of the character
	private CharacterControllerState currentState;

	//swipe cane
	private const float rotateDegreesPerSecond = 5f;
	private Quaternion targetRotation;
	private bool rotateClockwise = true;

	//move 
	private const float moveSpeed = 1f;
	private float rotationAmount;
	private Vector3 newPosition;

	//raycast
	RaycastHit2D hit;
	//distance for raycast
	private float distance = 1f;

	//final position in the scene
	Vector3 targetPosition;

	//controls the cane
	bool isSwippingCane;
	//manage movement after detected obstacle
	//bool obstacleDetected;

	Vector3 direction;

	public enum CharacterControllerState
	{
		forward, right, left, down, stop
	}

	void Start()
	{
		isSwippingCane = true;
		//obstacleDetected = false;
		rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
		currentState = CharacterControllerState.forward;

	}

	private void Update()
	{

		TargetPosition();

		if (isSwippingCane)
			SwipCane();

		switch (currentState)
		{
			case CharacterControllerState.forward:
				Forward();
				break;
			case CharacterControllerState.down:
				Down();
				break;
			case CharacterControllerState.right:
				Right();
				break;
			case CharacterControllerState.left:
				Left();
				break;
			case CharacterControllerState.stop:
				Stop();
				break;
		}

	}

	//private void FixedUpdate()
	//{
	//	// Update the ray direction based on the rotated angle
	//	Vector2 rayDirection = Quaternion.Euler(0f, 0f, rotateClockwise ? 45f : 315f) * Vector2.right;

	//	// Cast a ray in the specified direction
	//	RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, distance);

	//	// Check if the ray hits an obstacle
	//	if (hit.collider != null)
	//	{
	//		Debug.Log("Obstacle detected at angle: " + (rotateClockwise ? 45f : 315f));
	//		obstacleDetected = true;
	//	}
	//	else
	//	{
	//		obstacleDetected = false;
	//		Debug.Log("obstacles not detected");
	//		Debug.DrawRay(transform.position, rayDirection * 2f, Color.blue);
	//	}

	//	// Check if the rotation has reached the target
	//	if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
	//	{
	//		// Change rotation direction
	//		rotateClockwise = !rotateClockwise;
	//	}
	//}

	public void SwipCane()
	{
		// Calculate the target rotation based on current rotation
		targetRotation = Quaternion.Euler(0f, 0f, rotateClockwise ? 45f : 315f);

		// Rotate towards the target rotation
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationAmount);

		// Check if the rotation has reached the target
		if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
		{
			// Change rotation direction
			rotateClockwise = !rotateClockwise;
		}
	}
	public void Forward()
	{
		//Debug.Log("moving forward");

		newPosition = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
		transform.position += newPosition;
	}
	public void Down()
	{
		Debug.Log("Down");
		newPosition = new Vector3(0, -moveSpeed * Time.deltaTime, 0);
		transform.position += newPosition;
	}
	public void Right()
	{
		Debug.Log("Right");
		transform.Rotate(Vector3.back, rotationAmount);
		currentState = CharacterControllerState.forward;
	}
	public void Left()
	{
		Debug.Log("Left");

		transform.Rotate(Vector3.forward, rotationAmount);
	}
	public void Stop()
	{
		Debug.Log("Stop moving");
		newPosition = new Vector3(0, 0, 0);
		transform.position += newPosition;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		isSwippingCane = false;
		currentState = CharacterControllerState.stop;
	}
	private void OnCollisionStay2D(Collision2D collision)
	{
		currentState = CharacterControllerState.down;
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		isSwippingCane = true;
		currentState = CharacterControllerState.forward;
	}

	void TargetPosition()
	{
		GameObject target = GameObject.Find("TargetPosition");
		//Debug.Log(target.transform.position);
		direction = new Vector2(target.transform.position.x - transform.position.x,
									target.transform.position.y - transform.position.y);

		float distanceToTarget = Vector2.Distance(transform.position, targetPosition);

		float stoppingDistance = 1f;

		if (distanceToTarget <= stoppingDistance)
		{
			// Stop the character
			currentState = CharacterControllerState.stop;
		}
	}

	bool CannotMoveForward()
	{
		hit = Physics2D.Raycast(transform.position, Vector2.right, distance); // Cast a ray to the right with a distance of 1 unit

		//(Physics.Raycast(transform.position, transform.forward, out hit, 2f
		if (hit.collider != null)
		{
			Debug.Log("Cannot move forward, obstacles detected");
			// Draw a debug line from the ray's origin to the hit point
			Debug.DrawRay(transform.position, hit.point, Color.red);

			// Optionally, you can also draw a debug sphere at the hit point to visualize it
			Debug.DrawRay(hit.point, hit.normal, Color.blue, 0.1f);
			return true;
		}
		else
		{
			Debug.DrawRay(transform.position, transform.position + transform.right * distance, Color.blue);
			Debug.Log("didn't hit");
			return false;
		}
	}
}
