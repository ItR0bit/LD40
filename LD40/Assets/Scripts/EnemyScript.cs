using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	
	// Enemies health
	public float Health = 100;
	public float Speed = 5f;
	public float Damage = 1f;

	// Enemies Hidden stats
	Vector3 Direction;
	float AttackCooldown = 0.5f;
	float LastAttack = 0f;

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

	// Removes health if this hits the player
	private void OnTriggerStay2D(Collider2D collision)
	{
		//Debug.Log(LastAttack + "  -  " + Time.time);
		if (collision.transform.tag == "Player" && LastAttack + AttackCooldown <= Time.time)
		{
			LastAttack = Time.time;
			Player.GetComponent<CharacterHealth>().RemoveHealth(1);
		}
	}

	// Damages the enemy
	public void DamageEnemy(float value)
	{
		Health -= value;
		if (Health <= 0)
		{
			Destroy(this.gameObject);
			GameObject.FindGameObjectWithTag("Player").GetComponent<Shop>().AddMoney(10);
		}
	}
}
