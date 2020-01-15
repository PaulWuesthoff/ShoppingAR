using System;
using System.Collections;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using TMPro;
using OpenFoodFactsAPIData;

public class OpenFoodFactsAPIReader : MonoBehaviour
{
    //Barcode of Product
    public TextMeshProUGUI barcode;
    //Json Gui output on screen
    public TextMeshProUGUI productAPIData;

    private string productJson;

    public ProductDataClass productObject;
    private bool jsonIsDone = false;

    public QRScanner QRScanner;

    private String productName; 
    public void GetJsonData()
    {
        barcode.text = productName;  //hardcoded product barcode
        StartCoroutine(RequestWebService());
    }

    IEnumerator RequestWebService()
    {
        string getDataUrl = "https://world-de.openfoodfacts.org/api/v0/product/" + QRScanner.textUI.text + ".json";
        Debug.Log(getDataUrl);

        using (UnityWebRequest webData = UnityWebRequest.Get(getDataUrl))
        {

            yield return webData.SendWebRequest();
            if (webData.isNetworkError || webData.isHttpError)
            {
                print("---------------- ERROR ----------------");
                print(webData.error);
            }
            else
            {
                if (webData.isDone)
                {
                    JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(webData.downloadHandler.data));

                    if (jsonData == null)
                    {
                        print("---------------- NO DATA ----------------");
                    }
                    else
                    {
                        print("---------------- JSON DATA ----------------");
                        print("jsonData.Count:" + jsonData.Count);
                        print(jsonData.ToString());
                        productAPIData.text = jsonData.ToString();
                        productJson = jsonData.ToString();
                    }
                }
            }
        }

        jsonIsDone = true;
    }

    //Update is called once per frame
    void Update()
    {
        if (jsonIsDone)
        {
            //convert json string to class
            productObject = ProductDataClass.FromJson(productJson);
            String name = productObject.Product.ProductName;
            print(name);

            if (productObject != null)
            {
                try
                {
                   
                    Debug.Log("Hier: " + productObject.Product.ProductName + " " + productObject.Product.Brands);
                }
                catch (NullReferenceException ex)
                {
                    Debug.Log("NullRefrenceExeption" + ex.ToString());
                }
            }
            else
            {
                Debug.Log("ERROR NULL OBJECT");
            }
            jsonIsDone = false;
        }

    }

}