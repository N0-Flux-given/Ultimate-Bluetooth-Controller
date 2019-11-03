using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogBox : MonoBehaviour
{
	internal Text[] logText = new Text[10];
	int currentLogIndex = 0;


	private void Awake()
	{

		Transform textParent = transform.Find("TextHolder");
		for (int i = 0; i < 10; i++)
		{
			logText[i] = textParent.GetChild(i).GetComponent<Text>();
		}
	}

	internal void AddLog(string logMessage, LogMessageColours colour)
	{
		Color textColour = Color.white;
		switch (colour)
		{
			case LogMessageColours.RED:
				textColour = Color.red;
				break;
			case LogMessageColours.GREEN:
				textColour = Color.green;
				break;

			case LogMessageColours.DARKGREEN:
				textColour = Color.yellow;
				break;
		}

		for (int i = 9; i > 0; i--)
		{
			logText[i].text = logText[i - 1].text;
			logText[i].color = logText[i - 1].color;

		}
		logText[0].text = logMessage;
		logText[0].color = textColour;


	}

	internal void ClearLogs()
	{
		for(int i = 0; i < 10; i ++)
		{
			logText[i].text = "";
		}
	}

}
