using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void BackPressed();

public class CanvasScript : MonoBehaviour
{
	public Transform btPrompt;
	public static event BackPressed BackEvent;
	public FragTest fragReference;
	internal bool btPromptShown;
	public enum Screens
	{
		MainScreen, TankControlsScreen, BTNotSupported, ConnectingScreen, SettingsScreen
	}
	public AnimationCurve defaultAnimationCurve;
	public float defaultTransitionTime = 6f;
	internal List<Transform> screenMap = new List<Transform>();  //A list that contains all screens in the same order as the hierarchy


	private Coroutine screenCoroutine;

	private void Awake()
	{
		LoadReferences();

	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && BackEvent != null)
		{
			BackEvent();            //Back btn presss event is fired
		}
	}



	private void LoadReferences()
	{

		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			screenMap.Add(transform.GetChild(i));
		}
		print("Added " + childCount + " screens to list!");
	}


	/// <summary>
	/// Call this to transition to a different screen with an animation 
	/// </summary>
	/// <param name="screen">The screen to transition to</param>
	/// <param name="entry">True for entring, false otherwise</param>

	internal void TransitionToScreen(Screens current, Screens destination)
	{
		if (screenCoroutine != null)
			StopCoroutine(screenCoroutine);
		screenCoroutine = StartCoroutine(DefaultTransitionAnimation(screenMap[(int)current], screenMap[(int)destination]));
	}


	/// <summary>
	/// Call this to enable/disable popups. Displays no animation and doesn't affect the navigation "previous/current" variables.
	/// </summary>
	/// <param name="self">Enum value of the popup screen</param>
	/// <param name="entry">True for enabling, false for disabling</param>
	internal void TransitionToPopup(Screens self, bool entry)
	{

		if (entry)
			screenMap[(int)self].gameObject.SetActive(true);
		else
			screenMap[(int)self].gameObject.SetActive(false);
	}


	internal void ShowBluetoothPrompt()
	{
		btPromptShown = true;
		btPrompt.gameObject.SetActive(true);
	}

	

	internal IEnumerator DefaultTransitionAnimation(Transform current, Transform dest)
	{
		float time = 1, scale;
			   		
		//Scale current screen to 0
		
		//while (time > 0f)
		//{
		//	time -= Time.deltaTime / defaultTransitionTime;
		//	scale = defaultAnimationCurve.Evaluate(time);
		//	current.localScale = new Vector3(scale, scale, scale);
		//	yield return null;
		//}
		current.gameObject.SetActive(false);
		//current.gameObject.transform.localScale = Vector3.one;    //Set scale back to 1

		time = 0f;

		//Scale destination to 1

		dest.gameObject.SetActive(true);
		//dest.localScale = Vector3.one;
		yield return null;
		//while (time < 1.0f)
		//{
		//	time += Time.deltaTime / defaultTransitionTime;
		//	scale = defaultAnimationCurve.Evaluate(time);
		//	dest.localScale = new Vector3(scale, scale, scale);
		//	yield return null;
		//}


	}

	private void OnDestroy()
	{

	}
}


