using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemyScript : EnemyScript {



	// Damages the enemy
	public override void DamageEnemy(float value, List<BulletScript.BulletType> bulletTypes)
	{
		return;
	}

	// Slows the enemy
	public override void Slow(int amount)
	{
		Destroy(gameObject);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Shop>().AddMoney(50);
	}
}
