using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class BTPrompt : MonoBehaviour
{



	public Button btnYes, btnNo;	
	CanvasScript canvasReference;
	public FragTest fragTest;


	private void Awake()
	{
		btnYes.onClick.AddListener(OnClickYes);
		btnNo.onClick.AddListener(OnClickNo);
		canvasReference = transform.parent.GetComponent<CanvasScript>();
	}

	private void OnEnable()
	{
		CanvasScript.BackEvent += OnBackPress;
	}

	private void OnDisable()
	{
		CanvasScript.BackEvent -= OnBackPress;
	}

	private void OnClickYes()
	{
		fragTest.ToggleBluetooth(true);
		gameObject.SetActive(false);
	
	}
	private void OnClickNo()
	{

		gameObject.SetActive(false);
	}

	private void OnBackPress()
	{
		OnClickNo();
	}


}

