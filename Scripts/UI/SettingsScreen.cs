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
	public Button btnASCII, btnUnicode, btnBinary, btnCont, btnPressRel;

	




	private void Awake()
	{
		btnBack = transform.Find("TopBar/BtnBack").GetComponent<Button>();
		btnBack.onClick.AddListener(OnBackPress);

		btnASCII.onClick.AddListener(OnASCIIClick);
		btnUnicode.onClick.AddListener(OnUnicodeClick);
		btnBinary.onClick.AddListener(OnBinaryClick);

		btnCont.onClick.AddListener(OnContClick);
		btnPressRel.onClick.AddListener(OnPressRelCkick);
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
			button.transform.GetChild(0).GetComponent<Text>().color = txtDark;
		}
		else
		{
			button.GetComponent<Image>().color = btnGrey;
			button.transform.GetChild(0).GetComponent<Text>().color = txtLight;
		}
	}


	private void OnASCIIClick()
	{
		ToggleBtnColour(btnASCII, true);
		ToggleBtnColour(btnUnicode, false);
		ToggleBtnColour(btnBinary, false);

	}
	private void OnUnicodeClick()
	{
		ToggleBtnColour(btnASCII, false);
		ToggleBtnColour(btnUnicode, true);
		ToggleBtnColour(btnBinary, false);
	}
	private void OnBinaryClick()
	{
		ToggleBtnColour(btnASCII, false);
		ToggleBtnColour(btnUnicode, false);
		ToggleBtnColour(btnBinary, true);
	}
	private void OnContClick()
	{
		ToggleBtnColour(btnCont, true);
		ToggleBtnColour(btnPressRel, false);

	}
	private void OnPressRelCkick()
	{
		ToggleBtnColour(btnCont, false);
		ToggleBtnColour(btnPressRel, true);
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
