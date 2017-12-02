using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnScript : MonoBehaviour {

	// Width of playing area
	public float Width = 100f;
	float Cooldown = 5f;
	int Wave = 0;

	public GameObject Enemy;
	
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

				var e = Instantiate(Enemy);
				e.transform.position = spawnPos;
			}

			// Resets cooldown
			Cooldown = Mathf.Max(Random.Range(2, 5) * enemycount/4, 1f);
		}
	}
}
