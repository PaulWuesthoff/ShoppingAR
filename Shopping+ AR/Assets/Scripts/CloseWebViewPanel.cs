using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWebViewPanel : MonoBehaviour
{
    public GameObject webviewpanel;

    public void closePanel()
    { 
        webviewpanel.SetActive(false);
    }
}
