using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
	//Make sure to have same order as in hirearchy otherwise navigation will get fucked up

	private Transform scrollObjParent;
	private Text loadingText, infoText, descriptionText;
	public CanvasScript canvasReference;
	public ScrollViewObject scrollViewObject;
	public FragTest fragTestReference;
	public Button testButton, btnCancel, btnSettings;
	
	private string btOnDesctiption = "Select the device you want to connect to. If you don't see your device listed here, go to your system settings and pair with it first.\nThe list auto - updates every time the app comes into focus.";



	private void Awake()
	{
		btnSettings = transform.Find("btnSettings").GetComponent<Button>();
		btnSettings.onClick.AddListener(OnSettingsClick);
		scrollObjParent = transform.Find("ScrollContainer/Scroll View/Viewport/Content");
		infoText = transform.Find("txtInfo").GetComponent<Text>();
		descriptionText = transform.Find("txtInfoDescription").GetComponent<Text>();
		loadingText = transform.Find("ScrollContainer/Scroll View/Loading...").GetComponent<Text>();
		loadingText.gameObject.SetActive(false);
	
	}
	   

	private void OnEnable()
	{
		testButton.onClick.AddListener(TestButtonFunction);
		//btnCancel.onClick.AddListener(CancelConnectionBtn);
		InitializeBluetooth();
		CanvasScript.BackEvent += OnBackPress;
		FragTest.BluetoothStateUpdate += OnBluetoothStateChange;

		FragTest.OnResumeEvent += OnResumeCallback;


	}

	internal void OnBackPress()
	{
		//print("Pack btn pressed, printing from MainMenuScript");
		Application.Quit();
	}


	private void OnDisable()
	{
		CanvasScript.BackEvent -= OnBackPress;
		FragTest.BluetoothStateUpdate -= OnBluetoothStateChange;
		FragTest.OnResumeEvent -= OnResumeCallback;

	}

	private void TestButtonFunction()
	{
		//canvasReference.TransitionToPopup(CanvasScript.Screens.EnableBTPrompt, true);
		canvasReference.TransitionToScreen(CanvasScript.Screens.MainScreen, CanvasScript.Screens.TankControlsScreen);
	}

	private void OnSettingsClick()
	{
		canvasReference.TransitionToScreen(CanvasScript.Screens.MainScreen, CanvasScript.Screens.SettingsScreen);
	}

	private void OnCancelConnectionClick()
	{
		fragTestReference.CancelConnection();
	}

	internal void ScrollBtnClicked(string name, string MAC, int index)
	{

		transform.parent.Find("ConnectingScreen").GetComponent<ConnectingScreen>().index = index;
		canvasReference.TransitionToScreen(CanvasScript.Screens.MainScreen, CanvasScript.Screens.ConnectingScreen);

		//fragTestReference.ConnectTo(index);
		//testScriptReference.ShowToast("Test toast. Index : " + index);	
	}

	internal void SetInfoText(string text)
	{
		infoText.text = text;
	}


	internal void InitializeBluetooth()
	{
		if(fragTestReference.hasBluetoothAdapter)
		{
			if(fragTestReference.BluetoothStatus())   //Bluetooth is on  
			{
				descriptionText.text = btOnDesctiption;
				PopulateScrollView();
			}
			else										//Bluetooth id off
			{
				descriptionText.text = "Turn on Bluetooth to see the list of paired devices.";
				SetInfoText("Bluetooth is off");
				if(!canvasReference.btPromptShown)
				canvasReference.ShowBluetoothPrompt();
			}			

		}
		else
		{
			//SHow BT not supported
		}
	}

	
	private void BluetoothTurnedOff()  //this
	{
		SetInfoText("Bluetooth is off");
		foreach(Transform item in scrollObjParent)
		{
			item.GetComponent<ScrollViewObject>().DeactivateClicks();
		}
	}

	private void OnBluetoothStateChange(BluetoothStates currentState)
	{
		switch(currentState)
		{
			case BluetoothStates.ON:
				SetInfoText("Select your device");
				descriptionText.text = btOnDesctiption;
				PopulateScrollView();
				break;

			case BluetoothStates.TURNING_ON:
				SetInfoText("Turning on Bluetooth...");
				break;

			case BluetoothStates.OFF:
				SetInfoText("Bluetooth is off");
				descriptionText.text = "Turn on Bluetooth to see the list of paired devices.";
				foreach (Transform item in scrollObjParent)
				{
					item.GetComponent<ScrollViewObject>().DeactivateClicks();
				}
				break;
			case BluetoothStates.TURNING_OFF:

				break;
		}
	}

	private void OnResumeCallback()
	{
		InitializeBluetooth();
	}

	private void BluetoothTurningOn()  //this
	{
		SetInfoText("Turning on Bluetooth...");
	}

	private void BluetoothTurnedOn() //this
	{
		SetInfoText("Select your device");
		PopulateScrollView();
	}


	internal void OnBluetoothRequestGranted()
	{
		SetInfoText("Turning Bluetooth on...");
		Invoke("PopulateScrollView", 1.5f);
	}


	internal void PopulateScrollView()
	{
		
		foreach (Transform item in scrollObjParent)
		{
			Destroy(item.gameObject);
		}

		//ScrollArray scrollArray = JsonUtility.FromJson<ScrollArray>(CreateFAKEJson());
		
		ScrollArray scrollArray = JsonUtility.FromJson<ScrollArray>(fragTestReference.GetPairedDeviceListJson());
		int i = 0;
		foreach (ScrollObject obj in scrollArray.Devices)
		{
			ScrollViewObject spawnedObj = Instantiate(scrollViewObject, scrollObjParent);
			spawnedObj.SetNameAndMac(obj.Name, obj.MAC, i);
			spawnedObj.ActivateClicks();
			i++;
		}

	}




	private string CreateFAKEJson()
	{
		string name = "LOL";
		string mac = "sdfgsdfgsdfg";

		string name2 = "LOLOL";
		string MAC2 = "zgfzsdhgfxcvbn";


		ScrollObject obj1 = new ScrollObject();
		obj1.Name = name;
		obj1.MAC = mac;

		ScrollObject obj2 = new ScrollObject();
		obj2.Name = name2;
		obj2.MAC = MAC2;

		ScrollArray jsonArray = new ScrollArray();
		jsonArray.Devices.Add(obj1);
		jsonArray.Devices.Add(obj2);



		return JsonUtility.ToJson(jsonArray, true);

	}
}


[System.Serializable]
public class ScrollObject               //Class for inner clickable thing with name and MAC address
{
	public string Name;
	public string MAC;
}

[System.Serializable]
public class ScrollArray                //Class for the entire array of clickable name-MAC units
{
	public List<ScrollObject> Devices = new List<ScrollObject>();
}