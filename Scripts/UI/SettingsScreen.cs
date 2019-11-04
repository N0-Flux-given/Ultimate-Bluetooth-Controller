using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
	private Button btnBack;
	private CanvasScript canvasReference;
	public Button btnCross, btnCircle, btnSquare, btnTriangle;
	public Button btnLeft, btnRight, btnTop, btnBottom;


	private void Awake()
	{
		LoadReferences();
		btnCircle.onClick.AddListener(OnClickCircle);
		btnCross.onClick.AddListener(OnClickCross);
		btnSquare.onClick.AddListener(OnClickSquare);
		btnTriangle.onClick.AddListener(OnClickTriangle);

		btnLeft.onClick.AddListener(OnClickLeft);
		btnRight.onClick.AddListener(OnClickRight);
		btnLeft.onClick.AddListener(OnClickLeft);
		btnRight.onClick.AddListener(OnClickRight);

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
	private void OnClickBotton()
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
		btnBack = transform.Find("TopBar/BtnBack").GetComponent<Button>();
		btnBack.onClick.AddListener(OnBackPress);

	}

	private void OnBackPress()
	{
		canvasReference.TransitionToScreen(CanvasScript.Screens.SettingsScreen, CanvasScript.Screens.MainScreen);
	}
}
