using UnityEngine;
using System.Collections;

public class WebViewTestScene : MonoBehaviour 
{
	public string m_url = "www.google.com";

	public void OnButtonClick(string buttonName)
	{	
		if(buttonName == "ViewWebsite")
		{	
			if(!Application.isEditor)
			{	WebViewHandler.Instance.openURL(m_url);
			} else
			{	Debug.Log("WebViewTestScene:: Cannot view WebView in Editor.");
			}
		}
	}
}
