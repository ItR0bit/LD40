using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {


	Vector3 Direction;
	float Speed = 5f;

	GameObject Player;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// Sets the direction of the enemy
		float distance = Mathf.Sqrt(Mathf.Pow(Player.transform.position.x - transform.position.x, 2) + Mathf.Pow(Player.transform.position.y - transform.position.y, 2));
		Direction = new Vector3(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y) / distance;

		transform.position = transform.position + (Direction * Speed * Time.deltaTime);
	}
}
