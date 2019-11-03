using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewObject : MonoBehaviour
{
	private Text txtDeviceName, txtDeviceMAC;
	private Button btn;
	private MainMenuScript mainMenuScriptReference;
	internal int index;

	private void Awake()
	{
		txtDeviceName = transform.Find("Name").GetComponent<Text>();
		txtDeviceMAC = transform.Find("MACAddress").GetComponent<Text>();
		btn = transform.Find("Button").GetComponent<Button>();
		btn.onClick.AddListener(BtnClicked);
		mainMenuScriptReference = GameObject.Find("Canvas/MainScreen").GetComponent<MainMenuScript>();
	}

	private void BtnClicked()
	{
		mainMenuScriptReference.ScrollBtnClicked(txtDeviceName.text, txtDeviceMAC.text, index);
	}
		

	internal void SetNameAndMac(string name, string macAdress, int index)
	{
		txtDeviceName.text = name;
		txtDeviceMAC.text = macAdress;
		this.index = index;
	}

	internal void DeactivateClicks()
	{
		//0.576
		btn.interactable = false;
		txtDeviceName.color = new Color(0.576f, 0.576f, 0.576f, 1);
		txtDeviceMAC.color = new Color(0.576f, 0.576f, 0.576f, 1);
	}

	internal void ActivateClicks()
	{
		btn.interactable = true;
		//txtDeviceName.color = new Color(0.1288f, 0.1288f, 0.1288f, 1);
		//txtDeviceMAC.color = new Color(0.1288f, 0.1288f, 0.1288f, 1);
		txtDeviceName.color = Color.white;
		txtDeviceMAC.color = Color.white;

	}
}
