﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StickConstrain : MonoBehaviour
{

	public Transform imgVertical, imgHorizontal, imgOff;
	public Button btn;
	public Joystick leftStick, rightStick;
	public bool isLeft;
	private int state;
	public LogBox logBox;

	internal int leftConstrain, rightConstrain;

	private void Awake()
	{
		btn.onClick.AddListener(OnButtonClicked);
	}

	private void OnButtonClicked()
	{
		state++;
		if (state >= 3)
			state = 0;
		switch(state)
		{
			case 0:
				imgVertical.gameObject.SetActive(false);
				imgHorizontal.gameObject.SetActive(false);
				imgOff.gameObject.SetActive(true);
				if(isLeft)
				{
					leftStick.xFactor = 1;
					leftStick.yFactor = 1;
					logBox.AddLog("Left Joystick constrains removed", LogMessageColours.GREEN);
					leftConstrain = 0;
				}
				else
				{
					rightStick.xFactor = 1;
					rightStick.yFactor = 1;
					logBox.AddLog("Right Joystick constrains removed", LogMessageColours.GREEN);
					rightConstrain = 0;
				}

				break;
			case 1:				
				imgVertical.gameObject.SetActive(true);
				imgHorizontal.gameObject.SetActive(false);
				imgOff.gameObject.SetActive(false);
				if (isLeft)
				{
					leftStick.xFactor = 0;
					leftStick.yFactor = 1;
					logBox.AddLog("Left Joystick constrained vertically", LogMessageColours.GREEN);
					leftConstrain = 1;
				}
				else
				{
					rightStick.xFactor = 0;
					rightStick.yFactor = 1;
					logBox.AddLog("Right Joystick constrained vertically", LogMessageColours.GREEN);
					rightConstrain = 1;
				}
				break;
			case 2:
				imgVertical.gameObject.SetActive(false);
				imgHorizontal.gameObject.SetActive(true);
				imgOff.gameObject.SetActive(false);
				if (isLeft)
				{
					leftStick.xFactor = 1;
					leftStick.yFactor = 0;
					logBox.AddLog("Left Joystick constrained horizontally", LogMessageColours.GREEN);
					leftConstrain = 2;
				}
				else
				{
					rightStick.xFactor = 1;
					rightStick.yFactor = 0;
					logBox.AddLog("Right Joystick constrained horizontally", LogMessageColours.GREEN);
					rightConstrain = 2;
				}
				break;

		}
	}
}
