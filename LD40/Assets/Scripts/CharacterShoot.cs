using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterShoot : MonoBehaviour {

	public GameObject BulletPrefab;
	public float DamageMultiplier = 1;
	public float RangeMultiplier = 1;
	public float BulletScale = 1;
	public float Cooldown = 0.50f;
	float TimeLastShot = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && TimeLastShot + Cooldown <= Time.time)
		{
			// Creates bullet and sets its position/rotation
			var bullet = Instantiate(BulletPrefab);
			bullet.transform.position = transform.position;
			bullet.GetComponent<BulletScript>().StartPos = transform.position;
			bullet.GetComponent<BulletScript>().Damage *= DamageMultiplier;
			bullet.GetComponent<BulletScript>().Range *= RangeMultiplier;
			bullet.GetComponent<BulletScript>().setScale(BulletScale);
			TimeLastShot = Time.time;
		}
	}
}
