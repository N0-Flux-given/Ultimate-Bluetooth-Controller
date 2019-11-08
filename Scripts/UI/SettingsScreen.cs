using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
	private Button btnBack;
	private CanvasScript canvasReference;
	private FragTest fragReference;

	private static string prefEncoding = "encoding", prefPressMode = "pressmode";

	public Color txtDark, btnYellow, txtLight, btnGrey;
	public Button btnASCII, btnUnicode, btnBinary;

	




	private void Awake()
	{
		btnBack = transform.Find("TopBar/BtnBack").GetComponent<Button>();
		btnBack.onClick.AddListener(OnBackPress);

		btnASCII.onClick.AddListener(OnASCIIClick);
		btnUnicode.onClick.AddListener(OnUnicodeClick);
		btnBinary.onClick.AddListener(OnBinaryClick);
	}



	private void SetButtonColours()
	{
		if(PlayerPrefs.GetInt(prefEncoding) == -1)//Pref does not exist
		{
			PlayerPrefs.SetInt(prefEncoding, 0);  //0 -> ASCII, 1 -> Unicode, 2 -> Binary
		}
		else
		{
			switch(PlayerPrefs.GetInt(prefEncoding))
			{
				case 0:
					
					break;
				case 1:
					break;
				case 2:
					break;
			}
		}
	}


	private void ToggleBtnColour(Button button,bool active)
	{
		if(active)
		{
			button.GetComponent<Image>().color = btnYellow;
			button.transform.GetChild(0).GetComponent<Image>().color = txtLight;
		}
	}


	private void OnASCIIClick()
	{

	}
	private void OnUnicodeClick()
	{

	}
	private void OnBinaryClick()
	{

	}

	private void OnEnable()
	{
		CanvasScript.BackEvent += OnBackPress;
	}
	private void OnDisable()
	{
		CanvasScript.BackEvent -= OnBackPress;
	}

	

	private void OnBackPress()
	{
		canvasReference.TransitionToScreen(CanvasScript.Screens.SettingsScreen, CanvasScript.Screens.MainScreen);
	}
}
