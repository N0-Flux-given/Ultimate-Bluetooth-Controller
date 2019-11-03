using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
	private Button btnBack;
	private CanvasScript canvasReference;


	private void Awake()
	{
		LoadReferences();

	}

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
