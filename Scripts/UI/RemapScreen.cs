using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemapScreen : MonoBehaviour
{

	private Button btnBack;
	public Button btnCross, btnCircle, btnSquare, btnTriangle;
	public Button btnLeft, btnRight, btnTop, btnBottom;

	private CanvasScript canvasReference;
	private FragTest fragReference;


	public Text[] upValues = new Text[10];
	public Text[] downValues = new Text[10];

	private void Awake()
	{
		LoadReferences();
	}
	private void LoadReferences()
	{
		canvasReference = transform.parent.GetComponent<CanvasScript>();
		fragReference = canvasReference.fragReference;

		btnBack = transform.Find("TopBar/BtnBack").GetComponent<Button>();
		btnBack.onClick.AddListener(OnBackPress);
		btnCircle.onClick.AddListener(OnClickCircle);
		btnCross.onClick.AddListener(OnClickCross);
		btnSquare.onClick.AddListener(OnClickSquare);
		btnTriangle.onClick.AddListener(OnClickTriangle);

		btnLeft.onClick.AddListener(OnClickLeft);
		btnRight.onClick.AddListener(OnClickRight);
		btnTop.onClick.AddListener(OnClickTop);
		btnBottom.onClick.AddListener(OnClickBottom);
	}

	private void SetButtonValues()
	{
		int savedPref = PlayerPrefs.GetInt("pressmode");

		if (savedPref == 1)     //Pref valus 1 -> Up/Down
			for (int i = 0; i < 8; i++)
			{
				upValues[i].text = "Up: " + fragReference.controllerValues.btnValues[i].upValue;
				downValues[i].text = "Down " + fragReference.controllerValues.btnValues[i].downValue;
			}
		else if (savedPref == 0 || savedPref == -1)   //Pref does not exist or is 0 -> continous mode
			for (int i = 0; i < 8; i++)
			{
				upValues[i].text = fragReference.controllerValues.btnValues[i].upValue;
				downValues[i].text = string.Empty;
			}
	}

	private void OnEnable()
	{
		SetButtonValues();
		CanvasScript.BackEvent += OnBackPress;
	}
	private void OnDisable()
	{
		CanvasScript.BackEvent -= OnBackPress;
	}
	private void OnBackPress()
	{
		canvasReference.TransitionToScreen(CanvasScript.Screens.ReMapScreen, CanvasScript.Screens.SettingsScreen);
	}

	#region Button Click Callbacks

	private void OnClickCircle()
	{

	}
	private void OnClickCross()
	{
		print("cross value changed to 69!");
		fragReference.controllerValues.btnValues[1].upValue = 69.ToString();
		SaveNewValue();

	}
	private void OnClickSquare()
	{

	}
	private void OnClickTriangle()
	{

	}
	private void OnClickLeft()
	{

	}
	private void OnClickRight()
	{

	}
	private void OnClickTop()
	{

	}
	private void OnClickBottom()
	{

	}


	private void SaveNewValue()
	{
		string updatedValues = JsonUtility.ToJson(fragReference.controllerValues);
		PlayerPrefs.SetString(FragTest.prefControllerJson, updatedValues);
		SetButtonValues();
	}

	#endregion
}
