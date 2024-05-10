using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WebsitePortfolioItem : MonoBehaviour
{
    private string name;
    private string url;
    private Texture image;
    private Texture logo;


    //REMEMBER TO UPDATE WEBSITE COUNT
    public static int WebsiteCount = 2;

    public void Generate(int item)
    {
        switch (item)
        {
            case 0:
                name = "Valentines Website";
                url = "https://valentines-website-rosy.vercel.app";
                image = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Scenes/WebsitesData/ValentinesWebsite/ValentinesWebsiteScreenshot.png");
                logo = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Scenes/WebsitesData/ValentinesWebsite/Astro.png");
                break;
            case 1:
                name = "Decision Maker";
                url = "https://decisionmakerversion3.azurewebsites.net/";
                image = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Scenes/WebsitesData/DecisionMaker/DecisionMakerImage.png");
                logo = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Scenes/WebsitesData/DecisionMaker/BlazorLogo.png");
                break;
        }

        gameObject.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
        gameObject.transform.GetChild(0).GetChild(3).GetComponent<RawImage>().texture = logo;
        gameObject.transform.GetChild(0).GetChild(2).GetComponent<RawImage>().texture = image;
    }
    private void OnMouseDown()
    {
        Application.OpenURL(url);
    }
}