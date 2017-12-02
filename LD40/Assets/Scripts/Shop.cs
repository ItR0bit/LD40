using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	public GameObject ShopPanel;
	
	// Debuff stuff
	enum DebuffType
	{
		MOVEMENTSPEED = 0,
		BULLETCOOLDOWN = 1,
		BULLETDAMAGE = 2,
		BULLETSIZE = 3,
		BULLETRANGE = 4,
		ENEMYDAMAGE = 5,
		ENEMYSPEED = 6
	}

	List<DebuffType> Debuffs = new List<DebuffType>();
	int DebuffCount = 0;

	public Image DebuffMask;
	public Text DebuffText;
	public Text DebuffDisplayText;

	// Player game object
	GameObject Player;
	int TotalUpgrades = 0;

	// Total Money
	int Money = 0;
	Text MoneyText;

	// Cooldown stuff
	public Image CDMask;
	public Button CDPlus;
	public Button CDMinus;
	
	// Damage stuff
	public Image DMask;
	public Button DPlus;
	public Button DMinus;

	// Shot Range stuff
	public Image SRMask;
	public Button SRPlus;
	public Button SRMinus;
	
	// Shot Size stuff
	public Image SSMask;
	public Button SSPlus;
	public Button SSMinus;

	// Shot Speed stuff
	public Image MSMask;
	public Button MSPlus;
	public Button MSMinus;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		MoneyText = GameObject.Find("Money").GetComponent<Text>();
	}

	// Adds money
	public void AddMoney(int value)
	{
		Money += value;
		MoneyText.text = "$" + Money;

		CDPlus.interactable = (Money >= 50 && CDMask.fillAmount < 1);
		DPlus.interactable = (Money >= 100 && DMask.fillAmount < 1);
		SRPlus.interactable = (Money >= 50 && SRMask.fillAmount < 1);
		SSPlus.interactable = (Money >= 50 && SSMask.fillAmount < 1);
		MSPlus.interactable = (Money >= 50 && MSMask.fillAmount < 1);

	}

	// Toggles the shop visibility
	public void ToggleShop()
	{
		if (ShopPanel.activeSelf)
			GameObject.Find("ShopButtonText").GetComponent<Text>().text = "Open Shop";
		else
			GameObject.Find("ShopButtonText").GetComponent<Text>().text = "Close Shop";
		ShopPanel.SetActive(!ShopPanel.activeSelf);
		
	}
	
	// Updates the debuffs
	public void UpdateDebuffs(bool isIncreasing)
	{
		DebuffCount += (isIncreasing ? 1 : -1);

		if (DebuffCount % 3 == 0)
		{
			if (isIncreasing)
			{
				int newDebuff = Random.Range(0, 6);

				Debuffs.Add((DebuffType)newDebuff);

				switch ((DebuffType)newDebuff)
				{
					case DebuffType.BULLETCOOLDOWN:
						Player.GetComponent<CharacterShoot>().Cooldown += 0.1f;
						break;
					case DebuffType.BULLETDAMAGE:
						if (Player.GetComponent<CharacterShoot>().DamageMultiplier == 0)
						{
							Debuffs.RemoveAt(Debuffs.Count - 1);
							DebuffCount--;
							UpdateDebuffs(isIncreasing);
							return;
						}
						Player.GetComponent<CharacterShoot>().DamageMultiplier -= 2f;
						if (Player.GetComponent<CharacterShoot>().DamageMultiplier < 0)
						{
							Player.GetComponent<CharacterShoot>().DamageMultiplier = 0;
						}
						break;
					case DebuffType.BULLETRANGE:
						if (Player.GetComponent<CharacterShoot>().RangeMultiplier == 0)
						{
							Debuffs.RemoveAt(Debuffs.Count - 1);
							DebuffCount--;
							UpdateDebuffs(isIncreasing);
							return;
						}
						Player.GetComponent<CharacterShoot>().RangeMultiplier -= 2f;
						if (Player.GetComponent<CharacterShoot>().RangeMultiplier < 0)
						{
							Player.GetComponent<CharacterShoot>().RangeMultiplier = 0;
						}
						break;
					case DebuffType.BULLETSIZE:
						if (Player.GetComponent<CharacterShoot>().BulletScale == 0)
						{
							Debuffs.RemoveAt(Debuffs.Count - 1);
							DebuffCount--;
							UpdateDebuffs(isIncreasing);
							return;
						}
						Player.GetComponent<CharacterShoot>().BulletScale -= 2f;
						if (Player.GetComponent<CharacterShoot>().BulletScale < 0)
						{
							Player.GetComponent<CharacterShoot>().BulletScale = 0;
						}
						break;
					case DebuffType.ENEMYDAMAGE:
						// TODO
						break;
					case DebuffType.ENEMYSPEED:
						// TODO
						break;
					case DebuffType.MOVEMENTSPEED:
						if (Player.GetComponent<CharacterMovement>().MovementSpeed == 0)
						{
							Debuffs.RemoveAt(Debuffs.Count - 1);
							DebuffCount--;
							UpdateDebuffs(isIncreasing);
							return;
						}
						Player.GetComponent<CharacterMovement>().MovementSpeed -= 10f;
						if (Player.GetComponent<CharacterMovement>().MovementSpeed < 0)
						{
							Player.GetComponent<CharacterMovement>().MovementSpeed = 0;
						}
						break;
				}
			}
			else if (Debuffs.Count > 0)
			{
				int oldDebuff = Random.Range(0, Debuffs.Count-1);

				switch (Debuffs[oldDebuff])
				{
					case DebuffType.BULLETCOOLDOWN:
						Player.GetComponent<CharacterShoot>().Cooldown -= 0.1f;
						break;
					case DebuffType.BULLETDAMAGE:
						Player.GetComponent<CharacterShoot>().DamageMultiplier += 2f;
						if (DMask.fillAmount == 0)
							Player.GetComponent<CharacterShoot>().DamageMultiplier = 1;
						break;
					case DebuffType.BULLETRANGE:
						Player.GetComponent<CharacterShoot>().RangeMultiplier += 2f;
						if (SRMask.fillAmount == 0)
							Player.GetComponent<CharacterShoot>().RangeMultiplier = 1;
						break;
					case DebuffType.BULLETSIZE:
						Player.GetComponent<CharacterShoot>().BulletScale += 2f;
						if (SSMask.fillAmount == 0)
							Player.GetComponent<CharacterShoot>().BulletScale = 1;
						break;
					case DebuffType.ENEMYDAMAGE:
						// TODO
						break;
					case DebuffType.ENEMYSPEED:
						// TODO
						break;
					case DebuffType.MOVEMENTSPEED:
						Player.GetComponent<CharacterMovement>().MovementSpeed += 10f;
						if (MSMask.fillAmount == 0)
							Player.GetComponent<CharacterMovement>().MovementSpeed = 5;
						break;
				}

				Debuffs.RemoveAt(oldDebuff);
			}
		}

		DebuffText.text = "Debuffs: " + Mathf.FloorToInt(DebuffCount / 3);
		DebuffMask.fillAmount = (float)(DebuffCount % 3) / 2;
		DebuffDisplayText.text = "Debuffs:\n" +
			(Debuffs.Contains(DebuffType.BULLETCOOLDOWN) ? "Higher Bullet Cooldown x" + (Debuffs.FindAll(s => s.Equals(DebuffType.BULLETCOOLDOWN)).Count) + "\n": "") +
			(Debuffs.Contains(DebuffType.BULLETDAMAGE) ? "Lower Bullet Damage x" + (Debuffs.FindAll(s => s.Equals(DebuffType.BULLETDAMAGE)).Count) + "\n" : "") +
			(Debuffs.Contains(DebuffType.BULLETRANGE) ? "Lower Bullet Range x" + (Debuffs.FindAll(s => s.Equals(DebuffType.BULLETRANGE)).Count) + "\n" : "") +
			(Debuffs.Contains(DebuffType.BULLETSIZE) ? "Lower Bullet Size x" + (Debuffs.FindAll(s => s.Equals(DebuffType.BULLETSIZE)).Count) + "\n" : "") +
			(Debuffs.Contains(DebuffType.ENEMYDAMAGE) ? "Higher Enemy Damage x" + (Debuffs.FindAll(s => s.Equals(DebuffType.ENEMYDAMAGE)).Count) + "\n" : "") +
			(Debuffs.Contains(DebuffType.ENEMYSPEED) ? "Higher Enemy Speed x" + (Debuffs.FindAll(s => s.Equals(DebuffType.ENEMYSPEED)).Count) + "\n" : "") +
			(Debuffs.Contains(DebuffType.MOVEMENTSPEED) ? "Lower Movement Speed x" + (Debuffs.FindAll(s => s.Equals(DebuffType.MOVEMENTSPEED)).Count) + "\n" : "");
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
			AddMoney(-50);
		}
		else
		{
			CDMask.fillAmount -= 0.2f;
			CDMask.fillAmount = Mathf.Round(CDMask.fillAmount * 100f) / 100f;
			if (CDMask.fillAmount == 0)
				CDMinus.interactable = false;
			Player.GetComponent<CharacterShoot>().Cooldown += 0.05f;
			CDPlus.interactable = true;

			TotalUpgrades--;
			AddMoney(25);
		}
	}

	// Changes the damage of the gun
	public void ChangeDamage(bool isIncreasing)
	{
		if (isIncreasing)
		{
			DMask.fillAmount += 0.2f;
			if (DMask.fillAmount == 1)
				DPlus.interactable = false;
			Player.GetComponent<CharacterShoot>().DamageMultiplier += 1;
			DMinus.interactable = true;

			TotalUpgrades++;
			AddMoney(-100);
		}
		else
		{
			DMask.fillAmount -= 0.2f;
			DMask.fillAmount = Mathf.Round(DMask.fillAmount * 100f) / 100f;
			if (DMask.fillAmount == 0)
				DMinus.interactable = false;
			Player.GetComponent<CharacterShoot>().DamageMultiplier -= 1;
			DPlus.interactable = true;

			TotalUpgrades--;
			AddMoney(30);
		}
	}

	// Changes the damage of the gun
	public void ChangeShotRange(bool isIncreasing)
	{
		if (isIncreasing)
		{
			SRMask.fillAmount += 0.2f;
			if (SRMask.fillAmount == 1)
				SRPlus.interactable = false;
			Player.GetComponent<CharacterShoot>().RangeMultiplier += 1;
			SRMinus.interactable = true;

			TotalUpgrades++;
			AddMoney(-50);
		}
		else
		{
			SRMask.fillAmount -= 0.2f;
			SRMask.fillAmount = Mathf.Round(SRMask.fillAmount * 100f) / 100f;
			if (SRMask.fillAmount == 0)
				SRMinus.interactable = false;
			Player.GetComponent<CharacterShoot>().RangeMultiplier -= 1;
			SRPlus.interactable = true;
			
			TotalUpgrades--;
			AddMoney(25);
		}
	}

	// Changes the bullet size
	public void ChangeShotSize(bool isIncreasing)
	{
		if (isIncreasing)
		{
			SSMask.fillAmount += 0.2f;
			if (SSMask.fillAmount == 1)
				SSPlus.interactable = false;
			Player.GetComponent<CharacterShoot>().BulletScale += 1;
			SSMinus.interactable = true;

			TotalUpgrades++;
			AddMoney(-50);
		}
		else
		{
			SSMask.fillAmount -= 0.2f;
			SSMask.fillAmount = Mathf.Round(SSMask.fillAmount * 100f) / 100f;
			if (SSMask.fillAmount == 0)
				SSMinus.interactable = false;
			Player.GetComponent<CharacterShoot>().BulletScale -= 1;
			SSPlus.interactable = true;

			TotalUpgrades--;
			AddMoney(25);
		}
	}

	// Changes the movement speed of the player
	public void ChangeMovementSpeed(bool isIncreasing)
	{
		if (isIncreasing)
		{
			MSMask.fillAmount += 0.2f;
			if (MSMask.fillAmount == 1)
				MSPlus.interactable = false;
			Player.GetComponent<CharacterMovement>().MovementSpeed += 5;
			MSMinus.interactable = true;

			TotalUpgrades++;
			AddMoney(-50);
		}
		else
		{
			MSMask.fillAmount -= 0.2f;
			MSMask.fillAmount = Mathf.Round(MSMask.fillAmount * 100f) / 100f;
			if (MSMask.fillAmount == 0)
				MSMinus.interactable = false;
			Player.GetComponent<CharacterMovement>().MovementSpeed -= 5;
			MSPlus.interactable = true;

			TotalUpgrades--;
			AddMoney(25);
		}
	}
}
