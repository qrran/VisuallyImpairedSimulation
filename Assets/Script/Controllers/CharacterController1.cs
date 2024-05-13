using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
//using System.Numerics;
using UnityEngine;

public class CharacterController1 : MonoBehaviour
{
	//1.position

	private CharacterControllerState currentState;

	private const float rotateDegreesPerSecond = 2f;
	private const float moveSpeed = 2f;
	float rotationAmount;
	Vector3 newPosition;

	bool isMoving;

	RaycastHit2D hit;

	public enum CharacterControllerState
	{
		forward, right, left, down, stop
	}

	void Start()
	{
		rotationAmount = rotateDegreesPerSecond * Time.deltaTime;

		currentState = CharacterControllerState.forward;
	}

	private void Update()
	{
		// Check if the current state is stop
		if (currentState == CharacterControllerState.stop)
		{
			// If the character is stopped, return without performing any movement actions
			return;
		}

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

		if (CannotMoveForward())
		{
			currentState = CharacterControllerState.down;
		}
		//else
		//{
		//	currentState = CharacterControllerState.forward;
		//}
		//currentState = CharacterControllerState.forward;

	}

	//public void SwipCane()
	//{
	//	float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
	//switch (currentState)
	//{
	//	case CharacterControllerState.Idle:
	//		Idle();
	//		break;
	//	case CharacterControllerState.Walk:
	//		Walk();
	//		break;
	//	case CharacterControllerState.DetectedObstacle:
	//		DetectedObstacle();
	//		break;
	//}
	//}

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
		// Set the current state to stop
		//currentState = CharacterControllerState.stop;
		//transform.position = collisionPoint;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		currentState = CharacterControllerState.down;

		//collision.gameObject.CompareTag("Obstacle");

	}

	//private void OnCollisionStay2D(Collision2D collision)
	//{
	//	//currentState = CharacterControllerState.down;
	//}

	private void OnCollisionExit2D(Collision2D collision)
	{
		//currentState = CharacterControllerState.forward;
		//Debug.Log("oncolision2d moving down");
		currentState = CharacterControllerState.forward;

	}

	bool CannotMoveForward()
	{
		//RaycastHit2D hit;
		//if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
		//{
		//	Debug.Log("cannot move forward, obstacles detected");
		//	return true;
		//}
		//return false;
		//if (Physics2D.Raycast(transform.position, Vector2.right))
		//{
		//	Debug.Log("cannot move forward, obstacles detected");
		//	return true;
		//}
		//return false;
		hit = Physics2D.Raycast(transform.position, Vector2.right, 0.1f); // Cast a ray to the right with a distance of 1 unit

		if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
		{
			Debug.Log("Cannot move forward, obstacles detected");
			return true;
		}
		return false;
	}
}
