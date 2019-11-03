using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public enum ButtonType
{
	TRIANGLE,
	SQUARE,
	CIRCLE,
	CROSS,

	TOP,
	LEFT,
	RIGHT,
	DOWN,

	R1,
	L1
}



public class TankControlsScreen : MonoBehaviour
{
	private CanvasScript canvasReference;
	public Button testBtn;
	private FragTest fragReference;
	private LogBox logBox;
	int testInt = 0;
	private Color connectedGreen = Color.green, disconnectedRed = Color.red;
	private Image rightClusterOutline, leftClusterOutline;
	private Transform rightCluster, leftCluster;
	private Coroutine changeColorCoroutine;
	private int rightX, rightY, leftX, leftY;

	private ControllerValues controllerValues;

	private bool testBool;



	private void Awake()
	{
		TextAsset controlJsonFile = Resources.Load<TextAsset>("ControllerValues");
		controllerValues = JsonUtility.FromJson<ControllerValues>(controlJsonFile.ToString());


		canvasReference = transform.parent.GetComponent<CanvasScript>();
		testBtn.onClick.AddListener(OnTestButtonClick);
		fragReference = canvasReference.fragReference;
		logBox = transform.Find("LogBox").GetComponent<LogBox>();
		rightCluster = transform.Find("RightCluster");
		leftCluster = transform.Find("LeftCluster");

		rightClusterOutline = rightCluster.Find("Outline").GetComponent<Image>();
		leftClusterOutline = leftCluster.Find("Outline").GetComponent<Image>();


	}

	#region Input Callbacks


	internal void OnLeftJoystickChange(Vector3 inputVector)
	{
		if (inputVector.x < 0)
			leftX = (controllerValues.leftStickHorizontalMin + (int)(100f * inputVector.x * -1));
		else if (inputVector.x > 0)
			leftX = (controllerValues.leftStickHorizontalMax + (int)(100f * inputVector.x));
		else
			leftX = 0;

		if (inputVector.y < 0)
			leftY = (controllerValues.leftStickVerticalMin + (int)(100f * inputVector.x * -1));
		else if (inputVector.y > 0)
			leftY = (controllerValues.leftStickVerticalMax + (int)(100f * inputVector.x));
		else
			leftY = 0;
	}

	internal void OnRightJoystickChange(Vector3 inputVector)
	{
		if (inputVector.x < 0)
			rightX = (controllerValues.rightStickHorizontalMin + (int)(100f * inputVector.x * -1));
		else if (inputVector.x > 0)
			rightX = (controllerValues.rightStickHorizontalMax + (int)(100f * inputVector.x));
		else
			rightX = 0;

		if (inputVector.y < 0)
			rightY = (controllerValues.rightStickVerticalMin + (int)(100f * inputVector.x * -1));
		else if (inputVector.y > 0)
			rightY = (controllerValues.rightStickVerticalMax + (int)(100f * inputVector.x));
		else
			rightY = 0;
	}

	internal void OnButtonInteraction(ButtonType type, bool down)
	{
		switch(type)
		{
			case ButtonType.CIRCLE:
				if (down)
					SendCharacter(controllerValues.btnValues[(int)ButtonType.CIRCLE].downValue);
				else
					SendCharacter(controllerValues.btnValues[(int)ButtonType.CIRCLE].upValue);
				break;

			case ButtonType.CROSS:
				if (down)
					SendCharacter(controllerValues.btnValues[(int)ButtonType.CROSS].downValue);
				else
					SendCharacter(controllerValues.btnValues[(int)ButtonType.CROSS].upValue);
				break;
			case ButtonType.TRIANGLE:
				if (down)
					SendCharacter(controllerValues.btnValues[(int)ButtonType.TRIANGLE].downValue);
				else
					SendCharacter(controllerValues.btnValues[(int)ButtonType.TRIANGLE].upValue);
				break;
			case ButtonType.SQUARE:
				if (down)
					SendCharacter(controllerValues.btnValues[(int)ButtonType.SQUARE].downValue);
				else
					SendCharacter(controllerValues.btnValues[(int)ButtonType.SQUARE].upValue);
				break;
			case ButtonType.LEFT:
				if (down)
					SendCharacter(controllerValues.btnValues[(int)ButtonType.LEFT].downValue);
				else
					SendCharacter(controllerValues.btnValues[(int)ButtonType.LEFT].upValue);
				break;
			case ButtonType.RIGHT:
				if (down)
					SendCharacter(controllerValues.btnValues[(int)ButtonType.RIGHT].downValue);
				else
					SendCharacter(controllerValues.btnValues[(int)ButtonType.RIGHT].upValue);
				break;
			case ButtonType.TOP:
				if (down)
					SendCharacter(controllerValues.btnValues[(int)ButtonType.TOP].downValue);
				else
					SendCharacter(controllerValues.btnValues[(int)ButtonType.TOP].upValue);
				break;
			case ButtonType.DOWN:
				if (down)
					SendCharacter(controllerValues.btnValues[(int)ButtonType.DOWN].downValue);
				else
					SendCharacter(controllerValues.btnValues[(int)ButtonType.DOWN].upValue);
				break;
			case ButtonType.L1:
				if (down)
					SendCharacter(controllerValues.btnValues[(int)ButtonType.L1].downValue);
				else
					SendCharacter(controllerValues.btnValues[(int)ButtonType.L1].upValue);
				break;
			case ButtonType.R1:
				if (down)
					SendCharacter(controllerValues.btnValues[(int)ButtonType.R1].downValue);
				else
					SendCharacter(controllerValues.btnValues[(int)ButtonType.R1].upValue);
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
			if (changeColorCoroutine != null)
				StopCoroutine(changeColorCoroutine);
		if(fragReference.currentStatus == ConnectionStatus.STATUS_CONNECTED)
		{
			changeColorCoroutine = StartCoroutine(ChangeColour(connectedGreen));
		}
		else if(fragReference.currentStatus == ConnectionStatus.STATUS_DISCONNECTED || fragReference.currentStatus == ConnectionStatus.STATUS_FAILED)
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
		while(time <= 1.0f)
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
			tempColor = Color.Lerp(Color.white,finalColour, time);
			rightClusterOutline.color = tempColor;
			leftClusterOutline.color = tempColor;
			yield return null;
		}
	}

	private void SendCharacter(string character)
	{
		byte[] bytes = Encoding.Unicode.GetBytes(character);		
		fragReference.SendByte(bytes);
		print(bytes[0].ToString());
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
