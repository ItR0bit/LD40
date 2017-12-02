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
	public int IceAmount = 0;
	public int ChargeAmount = 0;
	float TimeLastShot = 0;
	float ChargeTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && TimeLastShot + Cooldown <= Time.time)
		{
			if (ChargeAmount > 0)
			{
				ChargeTime += Time.deltaTime;
				if (ChargeTime >= 2.5f / ChargeAmount)
					GetComponent<SpriteRenderer>().color = Color.gray;
			}
			else
			{
				// Creates bullet and sets its position/rotation
				var bullet = Instantiate(BulletPrefab);
				bullet.transform.position = transform.position;
				bullet.GetComponent<BulletScript>().StartPos = transform.position;
				bullet.GetComponent<BulletScript>().Damage *= DamageMultiplier;
				bullet.GetComponent<BulletScript>().Range *= RangeMultiplier;
				bullet.GetComponent<BulletScript>().setScale(BulletScale);

				if (IceAmount > 0)
				{
					bullet.GetComponent<BulletScript>().SpecialAmount = IceAmount;
					bullet.GetComponent<BulletScript>().Bullet.Add(BulletScript.BulletType.ICE);
				}

				TimeLastShot = Time.time;
			}
		}

		if (ChargeAmount > 0 && Input.GetMouseButtonUp(0) && ChargeTime >= 2.5f / ChargeAmount)
		{
			// Creates bullet and sets its position/rotation
			var bullet = Instantiate(BulletPrefab);
			bullet.transform.position = transform.position;
			bullet.GetComponent<BulletScript>().StartPos = transform.position;
			bullet.GetComponent<BulletScript>().Damage *= DamageMultiplier;
			bullet.GetComponent<BulletScript>().Range *= RangeMultiplier;
			bullet.GetComponent<BulletScript>().setScale(BulletScale);
			bullet.GetComponent<BulletScript>().Bullet.Add(BulletScript.BulletType.CHARGE);

			if (IceAmount > 0)
			{
				bullet.GetComponent<BulletScript>().SpecialAmount = IceAmount;
				bullet.GetComponent<BulletScript>().Bullet.Add(BulletScript.BulletType.ICE);
			}

			TimeLastShot = Time.time;
		}

		if (!Input.GetMouseButton(0))
		{
			ChargeTime = 0;
			gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		}
	}
}
