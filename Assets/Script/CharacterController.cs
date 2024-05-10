using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	private CharacterControllerState currentState;

	private const float rotateDegreesPerSecond = 2f;
	private const float moveSpeed = 2f;
	float rotationAmount;
	Vector3 newPosition;

	public enum CharacterControllerState
	{
		//Idle, Walk, DetectedObstacle
		forward, right, left, down
	}

	private void Update()
	{
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
		}
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
		Debug.Log("moving forward");

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

	void Start()
	{
		rotationAmount = rotateDegreesPerSecond * Time.deltaTime;

		currentState = CharacterControllerState.left;
	}
}
