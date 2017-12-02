using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public enum BulletType
	{
		CHARGE,
		ICE,
		EXPLOSIVE,
		HOMING
	}

	public float Speed = 15f;
	public float Damage = 20;
	public float Range = 10f;
	public int SpecialAmount = 0;
	public List<BulletType> Bullet = new List<BulletType>();
	public Vector3 StartPos;
	Vector3 Direction;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());

		// Sets the direction of the bullet
		Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float distance = Mathf.Sqrt(Mathf.Pow(MousePos.x - transform.position.x, 2) + Mathf.Pow(MousePos.y - transform.position.y, 2));
		Direction = new Vector3(MousePos.x - transform.position.x, MousePos.y - transform.position.y) / distance;
	}

	// Update is called once per frame
	void Update() {
		// Destroys the bullet if has lived too long
		if (Vector2.Distance(StartPos, transform.position) > Range)
			Destroy(gameObject);

		// Moves the bullet
		transform.position = transform.position + (Direction * Speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Damages enemies that are hit
		if (collision.transform.tag == "Enemy")
		{
			collision.gameObject.GetComponent<EnemyScript>().DamageEnemy(Damage, Bullet);
			if (Bullet.Contains(BulletType.ICE))
			{
				collision.gameObject.GetComponent<EnemyScript>().Slow(SpecialAmount);
			}
			if (!Bullet.Contains(BulletType.CHARGE))
				Destroy(gameObject);

		}
	}

	// Sets the size
	public void setScale(float scale)
	{
		transform.localScale *= scale;
	}
}
