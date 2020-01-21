using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public enum ButtonType
{
	TRIANGLE,  //0
	SQUARE,    //1
	CIRCLE,   //2
	CROSS,    //3

	TOP,     //4
	LEFT,    //5
	RIGHT,   //6
	DOWN,   //7

	R1,    //8, not used
	L1    //9, not used
}



public class TankControlsScreen : MonoBehaviour
{
	private CanvasScript canvasReference;
	public Button testBtn;
	private FragTest fragReference;
	private LogBox logBox;
	int testInt = 0;
	public Color connectedGreen, disconnectedRed;
	private Image rightClusterOutline, leftClusterOutline;
	private Transform rightCluster, leftCluster;
	private Coroutine changeColorCoroutine, contSendCoroutine;
	private int rightX, rightY, leftX, leftY;

	private bool isCoroutineRunning, continousMode;

	public StickConstrain leftConstrain, rightConstrain;

	  

	private bool testBool;   

	private void Awake()
	{    



		canvasReference = transform.parent.GetComponent<CanvasScript>();
		testBtn.onClick.AddListener(OnTestButtonClick);
		fragReference = canvasReference.fragReference;
		logBox = transform.Find("Root/LogBox").GetComponent<LogBox>();
		rightCluster = transform.Find("Root/RightCluster");
		leftCluster = transform.Find("Root/LeftCluster");

		rightClusterOutline = rightCluster.Find("Outline").GetComponent<Image>();
		leftClusterOutline = leftCluster.Find("Outline").GetComponent<Image>();


	}



	#region Input Callbacks


	internal void OnLeftJoystickChange(Vector3 inputVector)
	{
		if (inputVector.x < 0)
			leftX = (fragReference.controllerValues.leftStickHorizontalMin + (int)(100f * inputVector.x * -1));
		else if (inputVector.x > 0)
			leftX = (fragReference.controllerValues.leftStickHorizontalMax + (int)(100f * inputVector.x));
		else
			leftX = 0;

		if (inputVector.y < 0)
			leftY = (fragReference.controllerValues.leftStickVerticalMin + (int)(100f * inputVector.y * -1));
		else if (inputVector.y > 0)
			leftY = (fragReference.controllerValues.leftStickVerticalMax + (int)(100f * inputVector.y));
		else
			leftY = 0;

		switch(leftConstrain.leftConstrain)
		{
			case 0:
				SendJoystickValue(leftX);
				SendJoystickValue(leftY);
				break;
			case 1:   ///Vertical constrain
				SendJoystickValue(leftY);
				break;
			case 2:
				SendJoystickValue(leftX);
				break;
		}
		//SendJoystickValue(leftX);
		//SendJoystickValue(leftY);
		

	}

	internal void OnRightJoystickChange(Vector3 inputVector)
	{
		if (inputVector.x < 0)
			rightX = (fragReference.controllerValues.rightStickHorizontalMin + (int)(100f * inputVector.x * -1));
		else if (inputVector.x > 0)
			rightX = (fragReference.controllerValues.rightStickHorizontalMax + (int)(100f * inputVector.x));
		else
			rightX = 0;

		if (inputVector.y < 0)
			rightY = (fragReference.controllerValues.rightStickVerticalMin + (int)(100f * inputVector.y * -1));
		else if (inputVector.y > 0)
			rightY = (fragReference.controllerValues.rightStickVerticalMax + (int)(100f * inputVector.y));
		else
			rightY = 0;
		switch (rightConstrain.rightConstrain)
		{
			case 0:
				SendJoystickValue(rightX);
				SendJoystickValue(rightY);
				break;
			case 1:   ///Vertical constrain
				SendJoystickValue(rightY);
				break;
			case 2:
				SendJoystickValue(rightX);
				break;
		}
		//SendJoystickValue(rightX);
		//SendJoystickValue(rightY);
	}

	internal void OnButtonInteraction(ButtonType type, bool down)
	{
		switch (type)
		{
			case ButtonType.CIRCLE:
				if (down)
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.CIRCLE].downValue, true);
				else
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.CIRCLE].upValue, false);
				break;

			case ButtonType.CROSS:
				if (down)
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.CROSS].downValue, true);
				else
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.CROSS].upValue, false);
				break;
			case ButtonType.TRIANGLE:
				if (down)
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.TRIANGLE].downValue, true);
				else
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.TRIANGLE].upValue, false);
				break;
			case ButtonType.SQUARE:
				if (down)
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.SQUARE].downValue, true);
				else
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.SQUARE].upValue, false);
				break;
			case ButtonType.LEFT:
				if (down)
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.LEFT].downValue, true);
				else
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.LEFT].upValue, false);
				break;
			case ButtonType.RIGHT:
				if (down)
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.RIGHT].downValue, true);
				else
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.RIGHT].upValue, false);
				break;
			case ButtonType.TOP:
				if (down)
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.TOP].downValue, true);
				else
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.TOP].upValue, false);
				break;
			case ButtonType.DOWN:
				if (down)
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.DOWN].downValue, true);
				else
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.DOWN].upValue, false);
				break;
			case ButtonType.L1:
				if (down)
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.L1].downValue, true);
				else
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.L1].upValue, false);
				break;
			case ButtonType.R1:
				if (down)
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.R1].downValue, true);
				else
					SendCharacter(fragReference.controllerValues.btnValues[(int)ButtonType.R1].upValue, false);
				break;
		}
	}

	#endregion

	private void OnConnectionStatusChange(ConnectionStatus status)
	{
		SetOutlineColour();
	}

	private void SetOutlineColour()
	{
		if (isCoroutineRunning)
			return;
		//StopCoroutine(changeColorCoroutine);

		if (fragReference.currentStatus == ConnectionStatus.STATUS_CONNECTED)
		{
			changeColorCoroutine = StartCoroutine(ChangeColour(connectedGreen));
		}
		else if (fragReference.currentStatus == ConnectionStatus.STATUS_DISCONNECTED || fragReference.currentStatus == ConnectionStatus.STATUS_FAILED)
		{
			changeColorCoroutine = StartCoroutine(ChangeColour(disconnectedRed));
		}
	}

	private void OnNativeException(string log, LogMessageColours colour)
	{
		logBox.AddLog(log, colour);
	}

	private IEnumerator ChangeColour(Color finalColour)
	{
		float time = 0.0f;
		Color tempColor;
		isCoroutineRunning = true;
		while (time <= 1.0f)
		{
			time += Time.deltaTime / 0.5f;
			tempColor = Color.Lerp(rightClusterOutline.color, Color.white, time);
			rightClusterOutline.color = tempColor;
			leftClusterOutline.color = tempColor;
			yield return null;
		}
		time = 0.0f;
		while (time <= 1.0f)
		{
			time += Time.deltaTime / 0.5f;
			tempColor = Color.Lerp(Color.white, finalColour, time);
			rightClusterOutline.color = tempColor;
			leftClusterOutline.color = tempColor;
			yield return null;
		}
		isCoroutineRunning = false;
	}

	private void SendJoystickValue(int value)
	{
		logBox.AddLog(value.ToString(), LogMessageColours.GREEN);
		fragReference.SendByte(Encoding.ASCII.GetBytes(value.ToString()));
	}

	private void SendCharacter(string character, bool isDown)
	{
		byte[] bytes;
		switch (PlayerPrefs.GetInt(SettingsScreen.prefEncoding))
		{
			case -1:
			case 0:
				bytes = Encoding.ASCII.GetBytes(character);
				break;
			case 1:
				bytes = Encoding.Unicode.GetBytes(character);
				break;
			case 2:
				bytes = Encoding.ASCII.GetBytes(character);
				break;
			default:
				bytes = Encoding.ASCII.GetBytes(character);
				break;
		}
		//	byte[] bytes = Encoding.Unicode.GetBytes(character);
		if (!continousMode)
		{
			fragReference.SendByte(bytes);
			logBox.AddLog(character, LogMessageColours.GREEN);
		}
		else
		{
			if (isDown)			
				contSendCoroutine = StartCoroutine(SendContinously(bytes, character));			
			else
				if (contSendCoroutine != null)
				StopCoroutine(contSendCoroutine);
		}
	}
	private IEnumerator SendContinously(byte[] bytes, string character)
	{
		WaitForSeconds delay = new WaitForSeconds(0.25f);
		while (true)
		{
			fragReference.SendByte(bytes);
		    logBox.AddLog(character, LogMessageColours.GREEN);
			yield return delay;
		}
	}

	private void OnTestButtonClick()
	{
		if (!testBool)
		{

			byte[] bytes = Encoding.ASCII.GetBytes("1");
			fragReference.SendByte(bytes);
		}
		else
		{
			byte[] bytes = Encoding.ASCII.GetBytes("0");
			fragReference.SendByte(bytes);
		}
		testBool = !testBool;

		logBox.AddLog((++testInt).ToString(), LogMessageColours.GREEN);
	}

	private void OnEnable()
	{
		if (PlayerPrefs.GetInt(SettingsScreen.prefPressMode) == -1 || PlayerPrefs.GetInt(SettingsScreen.prefPressMode) == 0)
			continousMode = true;
		else
			continousMode = false;
		SetOutlineColour();
		logBox.ClearLogs();
		CanvasScript.BackEvent += OnBackPress;
		FragTest.ConnectionStatusEvent += OnConnectionStatusChange;
		FragTest.NativeExceptionEvent += OnNativeException;

	}

	private void OnDisable()
	{
		CanvasScript.BackEvent -= OnBackPress;
		FragTest.ConnectionStatusEvent -= OnConnectionStatusChange;
		FragTest.NativeExceptionEvent -= OnNativeException;
	}

	private void OnBackPress()
	{
		if (fragReference.currentStatus == ConnectionStatus.STATUS_DISCONNECTED)
			canvasReference.TransitionToScreen(CanvasScript.Screens.TankControlsScreen, CanvasScript.Screens.MainScreen);
		else
		{
			fragReference.CloseSocket();
			Application.Quit();
		}
	}

}
