using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

	private Image bgImage, stickImage;
	private Vector3 inputVector;
	private Coroutine resetJoystickCoroutine;
	public bool isLeftStick;
	public TankControlsScreen controlScreenReference;
	internal int xFactor = 1, yFactor = 1;
	private bool dampJoysticks;


	private void Awake()
	{
		bgImage = transform.GetComponent<Image>();
		stickImage = transform.GetChild(1).GetComponent<Image>();

		switch (PlayerPrefs.GetInt(SettingsScreen.prefDampJoysticks))
		{
			case 0:
				dampJoysticks = false;
				break;
			case 1:
				dampJoysticks = true;
				break;
			default:
				dampJoysticks = false;
				break;
		}

	}


	public void OnDrag(PointerEventData eventData)
	{
		Vector2 position;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform,
			eventData.position,
			eventData.pressEventCamera,
			out position))
		{
			position.x = (position.x / bgImage.rectTransform.sizeDelta.x) * xFactor;
			position.y = (position.y / bgImage.rectTransform.sizeDelta.y) * yFactor;

			inputVector = new Vector3(position.x * 2, position.y * 2, 0);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			stickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x / 3.5f),
				inputVector.y * (bgImage.rectTransform.sizeDelta.y / 3.5f));
			InputVectorChanged();
		}
	}

	public virtual void OnPointerDown(PointerEventData pointerEventData)
	{
		if (resetJoystickCoroutine != null)
			StopCoroutine(resetJoystickCoroutine);
		OnDrag(pointerEventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (resetJoystickCoroutine != null)
			StopCoroutine(resetJoystickCoroutine);
		resetJoystickCoroutine = StartCoroutine(MoveToCenter()); 			
			InputVectorChanged();
	}

	private IEnumerator MoveToCenter()
	{
		if (dampJoysticks)		//Smoothly bring joystics to the center
		{
			float time = 0.0f;
			Vector3 inputVectorCurrentPos = inputVector;
			while (time <= 1.0f)
			{
				time += Time.deltaTime / 0.08f;
				inputVector = Vector3.Lerp(inputVectorCurrentPos, Vector3.zero, time);
				stickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x / 3.5f),
					inputVector.y * (bgImage.rectTransform.sizeDelta.y / 3.5f));
				InputVectorChanged();
				yield return null;
			}
		}
		else			//Snap joysticks to the center
		{
			inputVector = Vector3.zero;
			stickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x / 3.5f),
					inputVector.y * (bgImage.rectTransform.sizeDelta.y / 3.5f));
			InputVectorChanged();
			yield break;
		}
	}

	private void InputVectorChanged()
	{
		if (isLeftStick)
			controlScreenReference.OnLeftJoystickChange(inputVector);
		else
			controlScreenReference.OnRightJoystickChange(inputVector);
	}
}
