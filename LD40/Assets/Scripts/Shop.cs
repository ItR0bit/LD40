using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	public GameObject ShopPanel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void ToggleShop()
	{
		if (ShopPanel.activeSelf)
			GetComponentInChildren<Text>().text = "Open Shop";
		else
			GetComponentInChildren<Text>().text = "Close Shop";
		ShopPanel.SetActive(!ShopPanel.activeSelf);
		
	}
}
