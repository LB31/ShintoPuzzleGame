using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plattformcontroller : MonoBehaviour
{
	public GameObject player;
	public Vector3 pos = Vector3.zero;
	public float speed = 0.25f;
	public Transform start;
	public Transform end;
	private bool playerEnter = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			Debug.Log("Druff");
			playerEnter = true;
		}
	}

	//private void OnTriggerStay(Collider other)
	//{
	//	if (other.gameObject == player)
	//	{
	//		pos = transform.position - pos;
	//		Debug.Log("Druff");
	//		player.GetComponent<CharacterController>().Move(pos * Time.deltaTime);
	//	}
	//}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
		{
			playerEnter = false;
			player.GetComponent<CharacterController>().GetComponent<FPSController>().additionalMovementSpeed = Vector3.zero;
		}
	}

	private void FixedUpdate()
	{
		float pingPong = Mathf.PingPong(Time.time * speed, 1);
		pos = Vector3.Lerp(start.position, end.position, pingPong);
		transform.position = pos;
		if (playerEnter) {
			player.GetComponent<CharacterController>().GetComponent<FPSController>().additionalMovementSpeed = pos;
		}

	}
}
