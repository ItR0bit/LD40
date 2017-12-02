using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour {

	public GameObject BulletPrefab;

	public float Cooldown = 0.50f;
	float TimeLastShot = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) && TimeLastShot + Cooldown <= Time.time)
		{
			// Creates bullet and sets its position/rotation
			var bullet = Instantiate(BulletPrefab);
			bullet.transform.position = transform.position;
			TimeLastShot = Time.time;
		}
	}
}
