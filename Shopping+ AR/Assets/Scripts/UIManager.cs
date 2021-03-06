﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //======================== [ Button methods ] ========================



    public void TestButton()
    {
        ButtonHapticFeedback();
    }

    //enable/disable flash
    public void ToggleFlashButton()
    {
        Button button = GameObject.Find("FlashIconButton").GetComponent<Button>();

        if (button.colors.selectedColor.Equals(Color.white))
        {
            Vuforia.CameraDevice.Instance.SetFlashTorchMode(true);
            button = ChangeButtonColor(button, Color.yellow);
        }
        else
        {
            Vuforia.CameraDevice.Instance.SetFlashTorchMode(false);
            button = ChangeButtonColor(button, Color.white);
        }
        ButtonHapticFeedback();
    }






    //======================== [ Helper methods ] ========================


    //Change selectedColor of button
    public static Button ChangeButtonColor(Button button, Color newColor)
    {
        ColorBlock newColors = button.colors;
        newColors.selectedColor = newColor;
        button.colors = newColors;
        return button;
    }

    //This will peform an Android haptic feedback
    void ButtonHapticFeedback()
    {
       HapticFeedback();
    }

    private class HapticFeedbackManager
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        private int HapticFeedbackConstantsKey;
        private AndroidJavaObject UnityPlayer;
#endif

        public HapticFeedbackManager()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            HapticFeedbackConstantsKey=new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("VIRTUAL_KEY");
            UnityPlayer=new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
            //Alternative way to get the UnityPlayer:
            //int content=new AndroidJavaClass("android.R$id").GetStatic<int>("content");
            //new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("findViewById",content).Call<AndroidJavaObject>("getChildAt",0);
#endif
        }

        public bool Execute()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return UnityPlayer.Call<bool> ("performHapticFeedback",HapticFeedbackConstantsKey);
#endif
            return false;
        }
    }

    //Cache the Manager for performance
    private static HapticFeedbackManager mHapticFeedbackManager;

    public static bool HapticFeedback()
    {
        if (mHapticFeedbackManager == null)
        {
            mHapticFeedbackManager = new HapticFeedbackManager();
        }
        return mHapticFeedbackManager.Execute();
    }
}
