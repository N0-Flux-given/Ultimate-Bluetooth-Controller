using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
	AndroidJavaClass pluginClass;
	AndroidJavaObject pluginObject;

	AndroidJavaClass unityPlayer;
	AndroidJavaObject activity;

	AndroidJavaObject context;

	


	private void Start()
	{

		//InitializePlugin();				
		//InitializeBluetooth();
		//pluginObject.Call("showToast", "this shit works!!!");


	}

	#region Private Functions

	

	
	
	private void OnDestroy()
	{
		if(pluginObject != null)
		{
			pluginObject.Call("destroyLeftoversOnExit");
		}

	}

	#endregion

	#region Internal Functions

	
		


	

	

	

	
	#endregion

	#region Public Functions

	

	#endregion
}
