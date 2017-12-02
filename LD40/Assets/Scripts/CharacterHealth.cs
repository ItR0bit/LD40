using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour {

	int Health = 5;
	Image HealthMask;
	

	// Use this for initialization
	void Start () {
		HealthMask = GameObject.Find("HealthMask").GetComponent<Image>();
	}
	
	public void RemoveHealth(int value)
	{
		Health -= value;

		HealthMask.fillAmount = (float)Health / 5f;

		if (Health == 0)
		{
			Debug.Log("YOU ARE DEAD MATE");
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
