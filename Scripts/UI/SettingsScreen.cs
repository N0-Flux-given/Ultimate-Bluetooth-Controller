using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
	private Button btnBack, btnRemapControls;
	private CanvasScript canvasReference;
	private FragTest fragReference;

	internal static string prefEncoding = "encoding", prefPressMode = "pressmode";

	public Color txtDark, btnYellow, txtLight, btnGrey;
	public Button btnASCII, btnUnicode, btnBinary, btnCont, btnPressRel;
	   	 


	private void Awake()
	{
		canvasReference = transform.parent.GetComponent<CanvasScript>();
		btnBack = transform.Find("TopBar/BtnBack").GetComponent<Button>();
		btnBack.onClick.AddListener(OnBackPress);
		btnRemapControls = transform.Find("TopBar/Root/RemapControls/btnRemapControls").GetComponent<Button>();
		btnRemapControls.onClick.AddListener(OnRemapControlsClick);

		btnASCII.onClick.AddListener(OnASCIIClick);
		btnUnicode.onClick.AddListener(OnUnicodeClick);
		btnBinary.onClick.AddListener(OnBinaryClick);
		  
		btnCont.onClick.AddListener(OnContClick);
		btnPressRel.onClick.AddListener(OnPressRelCkick);

		SetButtonColours();
	}


	private void OnRemapControlsClick()
	{
		canvasReference.TransitionToScreen(CanvasScript.Screens.SettingsScreen, CanvasScript.Screens.ReMapScreen);

	}

	private void SetButtonColours()
	{
		if (PlayerPrefs.GetInt(prefEncoding) == -1)//Pref does not exist
		{
			PlayerPrefs.SetInt(prefEncoding, 0);  //0 -> ASCII, 1 -> Unicode, 2 -> Binary
			ToggleBtnColour(btnASCII, true);
			ToggleBtnColour(btnUnicode, false);
			ToggleBtnColour(btnBinary, false);
		}
		else
		{
			switch (PlayerPrefs.GetInt(prefEncoding))
			{
				case 0:
					ToggleBtnColour(btnASCII, true);
					ToggleBtnColour(btnUnicode, false);
					ToggleBtnColour(btnBinary, false);

					break;
				case 1:
					ToggleBtnColour(btnASCII, false);
					ToggleBtnColour(btnUnicode, true);
					ToggleBtnColour(btnBinary, false);
					break;
				case 2:
					ToggleBtnColour(btnASCII, false);
					ToggleBtnColour(btnUnicode, false);
					ToggleBtnColour(btnBinary, true);
					break;
			}
		}
		if(PlayerPrefs.GetInt(prefPressMode) == -1) // -1 -> Pref does not exist
		{
			PlayerPrefs.SetInt(prefPressMode, 0);   //0 -> continous, 1-> Up/Down
			ToggleBtnColour(btnCont, true);
			ToggleBtnColour(btnPressRel, false);
		}
		else
		{
			switch(PlayerPrefs.GetInt(prefPressMode))
			{
				case 0:
					ToggleBtnColour(btnCont, true);
					ToggleBtnColour(btnPressRel, false);
					break;
				case 1:
					ToggleBtnColour(btnCont, false);
					ToggleBtnColour(btnPressRel, true);
					break;
			}
		}

	}


	private void ToggleBtnColour(Button button, bool active)
	{
		if (active)
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
		PlayerPrefs.SetInt(prefEncoding, 0);

	}
	private void OnUnicodeClick()
	{
		PlayerPrefs.SetInt(prefEncoding, 1);
		ToggleBtnColour(btnASCII, false);
		ToggleBtnColour(btnUnicode, true);
		ToggleBtnColour(btnBinary, false);
	}
	private void OnBinaryClick()
	{
		PlayerPrefs.SetInt(prefEncoding, 2);
		ToggleBtnColour(btnASCII, false);
		ToggleBtnColour(btnUnicode, false);
		ToggleBtnColour(btnBinary, true);
	}
	private void OnContClick()
	{
		PlayerPrefs.SetInt(prefPressMode, 0);
		ToggleBtnColour(btnCont, true);
		ToggleBtnColour(btnPressRel, false);

	}
	private void OnPressRelCkick()
	{
		PlayerPrefs.SetInt(prefPressMode, 1);
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
