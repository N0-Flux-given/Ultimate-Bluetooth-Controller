using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
	private Button btnBack;
	private CanvasScript canvasReference;
	private FragTest fragReference;

	public Button btnCross, btnCircle, btnSquare, btnTriangle;
	public Button btnLeft, btnRight, btnTop, btnBottom;

	public Text[] upValues = new Text[10];
	public Text[] downValues = new Text[10];




	private void Awake()
	{
		LoadReferences();
		

	}


	#region Button Click Callbacks

	private void OnClickCircle()
	{

	}
	private void OnClickCross()
	{

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


	#endregion

	private void OnEnable()
	{
		CanvasScript.BackEvent += OnBackPress;
	}
	private void OnDisable()
	{
		CanvasScript.BackEvent -= OnBackPress;
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


		for (int i = 0; i < 8; i++)
		{
			upValues[i].text = "Up: " + fragReference.controllerValues.btnValues[i].upValue;
			downValues[i].text = "Down " + fragReference.controllerValues.btnValues[i].downValue;

		}

	}

	private void OnBackPress()
	{
		canvasReference.TransitionToScreen(CanvasScript.Screens.SettingsScreen, CanvasScript.Screens.MainScreen);
	}
}
