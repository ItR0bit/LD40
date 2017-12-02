using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	public float MovementSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Moves the player
		transform.position = transform.position + (new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * MovementSpeed * Time.deltaTime);
		
		// Deals with the camera
		Camera.main.transform.position = new Vector3(Mathf.Lerp(Camera.main.transform.position.x, transform.position.x, Time.deltaTime), Mathf.Lerp(Camera.main.transform.position.y, transform.position.y, Time.deltaTime), Camera.main.transform.position.z);
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - (Input.GetAxisRaw("Mouse ScrollWheel") * 25f), 5, 25);

	}
}
