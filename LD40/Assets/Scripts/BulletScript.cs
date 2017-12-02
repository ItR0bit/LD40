using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public float Speed = 15f;
	public float ArmourPierce = 1f;
	float StartTime;
	Vector3 Direction;
	

	// Use this for initialization
	void Start () {
		// Sets the direction of the bullet
		Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float distance = Mathf.Sqrt(Mathf.Pow(MousePos.x - transform.position.x, 2) + Mathf.Pow(MousePos.y - transform.position.y, 2));
		Direction = new Vector3(MousePos.x - transform.position.x, MousePos.y - transform.position.y) / distance;

		// Gets the time the bullet was created
		StartTime = Time.time;
	}

	// Update is called once per frame
	void Update() {
		// Destroys the bullet if has lived too long
		if (StartTime + 0.5f <= Time.time)
		{
			Destroy(gameObject);
		}

		// Moves the bullet
		transform.position = transform.position + (Direction * Speed * Time.deltaTime);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.transform.tag);
		if (collision.transform.tag == "Enemy")
		{
			Destroy(collision.gameObject);
			GameObject.Find("ShopPanel").GetComponent<Shop>().AddMoney(10);
			Destroy(gameObject);
		}
	}
}
