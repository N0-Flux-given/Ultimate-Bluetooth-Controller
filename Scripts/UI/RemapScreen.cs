using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemapScreen : MonoBehaviour
{

	private Button btnBack;
	public Button btnCross, btnCircle, btnSquare, btnTriangle;
	public Button btnLeft, btnRight, btnTop, btnBottom;
	public Button btnResetDefault;

	private int resetBtnIndex;

	private Transform confirmationPopup;
	private Button btnYes, btnNo;
	private InputField resetInputFieldUp, resetInputFieldDown;

	private Transform inputPopup;
	private Button btnOk, btnOkUpDown;

	private CanvasScript canvasReference;
	private FragTest fragReference;

	private int savedPref;


	public Text[] upValues = new Text[10];
	public Text[] downValues = new Text[10];
	private int currentBtnIndex;

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

		
		inputPopup = transform.Find("ChangeValUpDown");
		btnOkUpDown = inputPopup.Find("Dialog/Set").GetComponent<Button>();

		
		btnOkUpDown.onClick.AddListener(OnSetClick);
		resetInputFieldUp = inputPopup.Find("Dialog/InputField").GetComponent<InputField>();
		resetInputFieldDown = inputPopup.Find("Dialog/InputFieldDown").GetComponent<InputField>();


		confirmationPopup = transform.Find("Confirmation");
		btnYes = confirmationPopup.Find("Dialog/Yes").GetComponent<Button>();
		btnNo = confirmationPopup.Find("Dialog/No").GetComponent<Button>();
		btnYes.onClick.AddListener(OnConfirmationYes);
		btnNo.onClick.AddListener(OnConfirmationNo);

		btnResetDefault.onClick.AddListener(OnClickResetDefault);
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
		 savedPref = PlayerPrefs.GetInt("pressmode");

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
		if (confirmationPopup.gameObject.activeSelf)
			OnConfirmationNo();
		else if (inputPopup.gameObject.activeSelf)
			inputPopup.gameObject.SetActive(false);
		else
			canvasReference.TransitionToScreen(CanvasScript.Screens.ReMapScreen, CanvasScript.Screens.SettingsScreen);
	}

	private void OnConfirmationYes()
	{
		fragReference.ResetControls();
		SetButtonValues();
		confirmationPopup.gameObject.SetActive(false);
	}
	private void OnConfirmationNo()
	{
		confirmationPopup.gameObject.SetActive(false);
	}

	private void OnClickResetDefault()
	{
		confirmationPopup.gameObject.SetActive(true);
	}
	private void OnSetClick()
	{
		inputPopup.gameObject.SetActive(false);
		fragReference.controllerValues.btnValues[currentBtnIndex].upValue = resetInputFieldUp.text;
		fragReference.controllerValues.btnValues[currentBtnIndex].downValue = resetInputFieldDown.text;
		SaveNewValue();
	}
	private void ShowRemapContPopup()
	{
		inputPopup.gameObject.SetActive(true);
		resetInputFieldUp.text = fragReference.controllerValues.btnValues[currentBtnIndex].upValue;
		resetInputFieldDown.text = fragReference.controllerValues.btnValues[currentBtnIndex].downValue;
		if(savedPref != 1)
		{
			resetInputFieldDown.gameObject.SetActive(false);
			inputPopup.Find("Dialog/txtDown").gameObject.SetActive(false);
		}
		else
		{
			resetInputFieldDown.gameObject.SetActive(true);
			inputPopup.Find("Dialog/txtDown").gameObject.SetActive(true);
		}
	}
	#region Button Click Callbacks, in order of index
	private void OnClickTriangle()
	{
		currentBtnIndex = 0;
		ShowRemapContPopup();

	}
	private void OnClickSquare()
	{
		currentBtnIndex = 1;
		ShowRemapContPopup();
	}

	private void OnClickCircle()
	{
		currentBtnIndex = 2;
		currentBtnIndex = 2;
		ShowRemapContPopup();
	}
	private void OnClickCross()
	{
		currentBtnIndex = 3;
		ShowRemapContPopup();
		//print("cross value changed to 69!");
		//fragReference.controllerValues.btnValues[1].upValue = 69.ToString();
		//SaveNewValue();
	}
	private void OnClickTop()
	{
		currentBtnIndex = 4;
		ShowRemapContPopup();
	}
	private void OnClickLeft()
	{
		currentBtnIndex = 5;
		ShowRemapContPopup();
	}
	private void OnClickRight()
	{
		currentBtnIndex = 6;
		ShowRemapContPopup();
	}
	private void OnClickBottom()
	{
		currentBtnIndex = 7;
		ShowRemapContPopup();
	}



	private void SaveNewValue()
	{
		string updatedValues = JsonUtility.ToJson(fragReference.controllerValues);
		PlayerPrefs.SetString(FragTest.prefControllerJson, updatedValues);
		SetButtonValues();
	}

	#endregion
}
