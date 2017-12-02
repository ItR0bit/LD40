using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemyScript : EnemyScript {
	
	public override void DamageEnemy(float value, List<BulletScript.BulletType> bulletTypes)
	{
		if (bulletTypes.Contains(BulletScript.BulletType.CHARGE))
		{
			Health -= value;
			if (Health <= 0)
			{
				Destroy(this.gameObject);
				GameObject.FindGameObjectWithTag("Player").GetComponent<Shop>().AddMoney(100);
			}
		}
	}
}
