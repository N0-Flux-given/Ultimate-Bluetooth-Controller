using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCutout : MonoBehaviour
{

	RectTransform panel;

	private void Awake()
	{
		panel = GetComponent<RectTransform>();
		Rect screenSafeArea = Screen.safeArea;
		Vector2 anchorMin = screenSafeArea.position;
		Vector2 anchorMax = screenSafeArea.position + screenSafeArea.size;
		anchorMin.x /= Screen.width;
		anchorMin.y /= Screen.height;
		anchorMax.x /= Screen.width;
		anchorMax.y /= Screen.height;

		panel.anchorMin = anchorMin;
		panel.anchorMax = anchorMax;
	}
}
