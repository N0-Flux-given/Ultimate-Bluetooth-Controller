using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
	public ButtonType type;
	public TankControlsScreen controlScreenReference;



	public void OnPointerDown(PointerEventData eventData)
	{
		controlScreenReference.OnButtonInteraction(type, true);

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		controlScreenReference.OnButtonInteraction(type, false);
	}

}
