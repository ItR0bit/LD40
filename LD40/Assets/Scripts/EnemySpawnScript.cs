using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnScript : MonoBehaviour {
	
	// Width of playing area
	public float Width = 100f;
	float Cooldown = 5f;
	int Wave = 0;

	float AddedDamage = 0;
	float AddedSpeed = 0;

	public GameObject NormalEnemy;
	public List<GameObject> SpecialEnemies = new List<GameObject>();
	public List<float> PCChance = new List<float>();

	// Update is called once per frame
	void Update () {
		Cooldown -= Time.deltaTime;

		// Spawns more enemies when cooldown = 0
		if (Cooldown <= 0)
		{
			Wave++;
			int enemycount = Random.Range(1 * Wave, 2 * Wave);
			for (int i = 0; i < enemycount; i++)
			{
				// Sets spawn to at least 5 units away from player
				Vector2 spawnPos;
				do
				{
					spawnPos = new Vector2(Random.Range(-Width / 2, Width / 2), Random.Range(-Width / 2, Width / 2));
				} while (Vector2.Distance(spawnPos, GameObject.FindGameObjectWithTag("Player").transform.position) < 5);

				float EnemyPC = Random.Range(0f, 100f);
				int EnemyIndex = -1;
				float currentval = 0;
				for (int j = 0; j < PCChance.Count; j++)
				{
					currentval += PCChance[j];
					if (currentval > EnemyPC)
					{
						EnemyIndex = j;
						break;
					}
				}

				GameObject enemy;
				if (EnemyIndex == -1)
					enemy = Instantiate(NormalEnemy);
				else
					enemy = Instantiate(SpecialEnemies[EnemyIndex]);
				enemy.transform.position = spawnPos;
				enemy.transform.parent = transform;
				enemy.GetComponent<EnemyScript>().Damage += AddedDamage;
				enemy.GetComponent<EnemyScript>().Speed += AddedSpeed;
			}

			// Resets cooldown
			Cooldown = Mathf.Max(Random.Range(2, 5) * enemycount/4, 1f);
		}
	}

	// Changes the speed of ALL enemies
	public void ChangeSpeed(int AddedValue)
	{
		AddedSpeed += AddedValue;

		for (int i = 0; i < transform.childCount; i++)
			transform.GetChild(i).GetComponent<EnemyScript>().Speed += AddedValue;
	}

	// Changes the damage of ALL enemies
	public void ChangeDamage(int AddedValue)
	{
		AddedDamage = AddedValue;

		for (int i = 0; i < transform.childCount; i++)
			transform.GetChild(i).GetComponent<EnemyScript>().Speed += AddedValue;
	}
}
