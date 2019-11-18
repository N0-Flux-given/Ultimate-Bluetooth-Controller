using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


public enum BluetoothStates
{
	TURNING_ON,
	ON,
	TURNING_OFF,
	OFF
}
public enum ConnectionStatus
{
	STATUS_CONNECTING,
	STATUS_CONNECTED,
	STATUS_FAILED,
	STATUS_DISCONNECTED
}

public enum LogMessageColours
{
	RED,
	GREEN,
	DARKGREEN
}



public delegate void BluetoothStat(BluetoothStates status);
public delegate void ConnectionStat(ConnectionStatus status);
public delegate void NativeException(string whatHappened, LogMessageColours colour);


public delegate void OnResume();

public class FragTest : MonoBehaviour
{
	AndroidJavaClass javaClass;
	AndroidJavaObject pluginInstance;
	internal bool hasBluetoothAdapter;
	public static event BluetoothStat BluetoothStateUpdate;
	public static event ConnectionStat ConnectionStatusEvent;
	public static event NativeException NativeExceptionEvent;
	public static event OnResume OnResumeEvent;

	internal ControllerValues controllerValues;

	internal ConnectionStatus currentStatus;
	internal static string prefControllerJson = "controllerJson";

	public CanvasScript canvasReference;
	//{
	//get
	//{
	//	return javaClass.GetStatic<AndroidJavaObject>("instance");
	//}
	//	}




	#region Unity Callbacks
	private void Awake()
	{
		GenerateBtnJSON();
		InitializePlugin();
		TextAsset controlJsonFile = Resources.Load<TextAsset>("ControllerValues");
		if (PlayerPrefs.GetString(prefControllerJson) == string.Empty)
			PlayerPrefs.SetString(prefControllerJson, controlJsonFile.ToString());
		controllerValues = JsonUtility.FromJson<ControllerValues>(PlayerPrefs.GetString(prefControllerJson));
	}



	#endregion

	internal void ResetControls()
	{
		TextAsset controlJsonFile = Resources.Load<TextAsset>("ControllerValues");
		PlayerPrefs.SetString(prefControllerJson, controlJsonFile.ToString());
	}


	public void OnBluetoothStateChange(string state)
	{
		BluetoothStates currentState = BluetoothStates.OFF;
		switch (state)
		{
			case "0":
				currentState = BluetoothStates.OFF;
				break;
			case "1":
				currentState = BluetoothStates.TURNING_OFF;
				break;
			case "2":
				currentState = BluetoothStates.ON;
				break;
			case "3":
				currentState = BluetoothStates.TURNING_ON;
				break;
		}

		BluetoothStateUpdate?.Invoke(currentState);
	}

	void InitializePlugin()
	{
		javaClass = new AndroidJavaClass("com.btaurdino.m2studios.bluetoothplugin.FragTest");
		javaClass.CallStatic("start", "FragTest");
		pluginInstance = javaClass.GetStatic<AndroidJavaObject>("instance");

		InitializeBluetooth();


		pluginInstance.Call("callMeFromUnity", "lolol");
	}

	public void CallbackFromJava(string arg)
	{
		print("Callback from java!! arg : " + arg);
	}





	#region Private Functions

	/// <summary>
	/// Gets the dafault bluetooth adapter and sets a bool to true if it succedes, false otherwise and in case of no bluetooth hardware available
	/// </summary>
	private void InitializeBluetooth()
	{
		hasBluetoothAdapter = pluginInstance.Call<bool>("getBluetoothAdapter");
		print("hasBTStatus : " + hasBluetoothAdapter);

	}


	#endregion

	#region Internal Functions


	internal void SendByte(byte[] byteArray)
	{
		pluginInstance.Call("writeByte", byteArray);
	}


	/// <summary>
	/// Returns status of bluetooth, either on or off
	/// </summary>
	/// <returns></returns>
	internal bool BluetoothStatus()
	{
		return pluginInstance.Call<bool>("bluetoothStatus");
	}

	internal void CloseSocket()
	{
		pluginInstance.Call("cancelConnection");
	}


	internal string GetPairedDeviceListJson()
	{
		return pluginInstance.Call<string>("getPairedDeviceListJson");
	}
	internal void ShowToast(string message)
	{
		pluginInstance.Call("showToast", message);
	}
	internal string GetUUID(int index)
	{
		return pluginInstance.Call<string>("getUUID", index);
	}
	internal void ConnectTo(int index)
	{
		print("COnnect to called index :" + index);
		pluginInstance.Call("connectTo", index);
	}
	internal void CancelConnection()
	{
		print("Cancel connection called!");
		pluginInstance.Call("cancelConnection");
	}

	internal void ToggleBluetooth(bool on)
	{
		pluginInstance.Call("toggleBluetooth", on);
	}

	#endregion


	#region Public Functions

	public void CallbackFromAndroid(string message)
	{
		print(message);
	}

	public void BTrequestCallback(string result)
	{
		if (result.Equals("y"))
			canvasReference.transform.Find("MainScreen").GetComponent<MainMenuScript>().PopulateScrollView();
		else
			canvasReference.transform.Find("MainScreen").GetComponent<MainMenuScript>().SetInfoText("Bluetooth request was denied :/");
	}

	public void LogNativeCaughtException(string whatHappened)
	{
		LogMessageJson obj = JsonUtility.FromJson<LogMessageJson>(whatHappened);
		NativeExceptionEvent?.Invoke(obj.message, (LogMessageColours)obj.colour);
		print("caught exception colour : " + (LogMessageColours)obj.colour);
	}

	public void OnResumeCallback(string args)
	{
		OnResumeEvent?.Invoke();
	}

	public void OnConnectionStatusReport(string status)
	{

		switch (status)
		{
			case "con":
				currentStatus = ConnectionStatus.STATUS_CONNECTING;
				ConnectionStatusEvent?.Invoke(ConnectionStatus.STATUS_CONNECTING);
				break;
			case "ctd":
				currentStatus = ConnectionStatus.STATUS_CONNECTED;
				ConnectionStatusEvent?.Invoke(ConnectionStatus.STATUS_CONNECTED);
				break;

			case "err":   //GOt an exception while trying to connect
				currentStatus = ConnectionStatus.STATUS_FAILED;
				ConnectionStatusEvent?.Invoke(ConnectionStatus.STATUS_FAILED);
				break;
			case "dis":  //Got an exception in socket or stream after establising a successful connection
				currentStatus = ConnectionStatus.STATUS_DISCONNECTED;
				ConnectionStatusEvent?.Invoke(ConnectionStatus.STATUS_DISCONNECTED);
				break;

		}
	}

	private void GenerateBtnJSON()
	{

		ControllerValues controllerValues = new ControllerValues
		{
			btnValues = new ButtonValues[10]
		};

		controllerValues.btnValues[0] = new ButtonValues { upValue = "900", downValue = "901" };
		controllerValues.btnValues[1] = new ButtonValues { upValue = "902", downValue = "903" };
		controllerValues.btnValues[2] = new ButtonValues { upValue = "904", downValue = "905" };
		controllerValues.btnValues[3] = new ButtonValues { upValue = "906", downValue = "907" };
		controllerValues.btnValues[4] = new ButtonValues { upValue = "908", downValue = "909" };
		controllerValues.btnValues[5] = new ButtonValues { upValue = "910", downValue = "911" };
		controllerValues.btnValues[6] = new ButtonValues { upValue = "912", downValue = "913" };
		controllerValues.btnValues[7] = new ButtonValues { upValue = "914", downValue = "915" };
		controllerValues.btnValues[8] = new ButtonValues { upValue = "916", downValue = "917" };
		controllerValues.btnValues[9] = new ButtonValues { upValue = "918", downValue = "919" };

		print(JsonUtility.ToJson(controllerValues, true));
	}

	#endregion


}

[System.Serializable]
public class LogMessageJson
{
	public string message;
	public int colour;
}

[System.Serializable]
public class ButtonValues
{
	public string upValue;
	public string downValue;
}

[System.Serializable]
public class ControllerValues
{
	public int leftStickVerticalMax = 100, leftStickVerticalMin = 200, leftStickHorizontalMax = 300, leftStickHorizontalMin = 400;
	public int rightStickVerticalMax = 500, rightStickVerticalMin = 600, rightStickHorizontalMax = 700, rightStickHorizontalMin = 800;
	public ButtonValues[] btnValues;
}