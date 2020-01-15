using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWebViewPanel : MonoBehaviour
{
    public GameObject webviewpanel;

    public void openPanel() {
        webviewpanel.SetActive(true);
    }
}
