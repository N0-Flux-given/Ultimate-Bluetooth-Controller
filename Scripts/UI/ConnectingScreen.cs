using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectingScreen : MonoBehaviour
{

	public Text txtConnecting;
	private bool hasFailed;
	private CanvasScript canvasReference;
	private LogBox logBox;
	internal int index;
	private FragTest fragReference;

	private void Awake()
	{
		canvasReference = transform.parent.GetComponent<CanvasScript>();
		logBox = transform.Find("LogBox").GetComponent<LogBox>();
		
		fragReference = transform.parent.GetComponent<CanvasScript>().fragReference;
	}

	private void OnEnable()
	{
		logBox.ClearLogs();
		txtConnecting.text = "Connecting...";
		txtConnecting.color = new Color(209f / 255f, 209f / 255f, 209f / 255f, 1);
		CanvasScript.BackEvent += OnBackPress;
		FragTest.ConnectionStatusEvent += OnConnectionStatus;
		FragTest.NativeExceptionEvent += OnNativeException;
		ConnectTo();
	}

	private void ConnectTo()
	{
		fragReference.ConnectTo(index);
	}

	private void OnDisable()
	{
		CanvasScript.BackEvent -= OnBackPress;
		FragTest.ConnectionStatusEvent -= OnConnectionStatus;
		FragTest.NativeExceptionEvent -= OnNativeException;

	}


	private void OnNativeException(string log, LogMessageColours colour)
	{
		logBox.AddLog(log, colour);
	}

	private void OnConnectionStatus(ConnectionStatus status)
	{
		print("In onCOnnection status");
		switch(status)
		{
			case ConnectionStatus.STATUS_CONNECTING:

				break;

			case ConnectionStatus.STATUS_CONNECTED:
				txtConnecting.text = "Connected!";
				txtConnecting.color = Color.green;
				StartCoroutine(ChangeScreen());
				break;

			case ConnectionStatus.STATUS_FAILED:
				txtConnecting.text = "Failed to connect :(";
				print("In failed brnach");
				txtConnecting.color = Color.red;
				hasFailed = true;
				break;
		}
	}


	private IEnumerator ChangeScreen()
	{
		yield return new WaitForSeconds(1f);
		canvasReference.TransitionToScreen(CanvasScript.Screens.ConnectingScreen, CanvasScript.Screens.TankControlsScreen);

	}

	private void OnBackPress()
	{
		if(hasFailed)
		{
			canvasReference.TransitionToScreen(CanvasScript.Screens.ConnectingScreen, CanvasScript.Screens.MainScreen);
		}
	}


}
