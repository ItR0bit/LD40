using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	// Player game object
	GameObject Player;

	int TotalUpgrades = 0;

	// Total Money
	int Money = 0;
	Text MoneyText;

	// Cooldown stuff
	Image CDMask;
	Button CDPlus;
	Button CDMinus;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		MoneyText = GameObject.Find("Money").GetComponent<Text>();

		// Cooldown
		CDMask = GameObject.Find("CDMask").GetComponent<Image>();
		CDPlus = GameObject.Find("CDPlus").GetComponent<Button>();
		CDMinus = GameObject.Find("CDMinus").GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Adds money
	public void AddMoney(int value)
	{
		Money += value;
		MoneyText.text = "$" + Money;

		CDPlus.interactable = (Money >= 10 && CDMask.fillAmount < 1);

	}

	// Toggles the shop visibility
	public void ToggleShop()
	{
		if (gameObject.activeSelf)
			GameObject.Find("ShopButtonText").GetComponent<Text>().text = "Open Shop";
		else
			GameObject.Find("ShopButtonText").GetComponent<Text>().text = "Close Shop";
		gameObject.SetActive(!gameObject.activeSelf);
		
	}
	
	// Changes the cooldown of the gun
	public void ChangeCooldown(bool isIncreasing)
	{
		if (isIncreasing)
		{
			CDMask.fillAmount += 0.2f;
			if (CDMask.fillAmount == 1)
				CDPlus.interactable = false;
			Player.GetComponent<CharacterShoot>().Cooldown -= 0.05f;
			CDMinus.interactable = true;

			TotalUpgrades++;
			AddMoney(-10);
		}
		else
		{
			CDMask.fillAmount -= 0.2f;
			if (CDMask.fillAmount == 0)
				CDMinus.interactable = false;
			Player.GetComponent<CharacterShoot>().Cooldown += 0.05f;
			CDPlus.interactable = true;

			TotalUpgrades--;
			AddMoney(5);
		}
	}





}
