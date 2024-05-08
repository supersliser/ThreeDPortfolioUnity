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
    public bool active;

    public void Generate(int item)
    {
        var data = JsonUtility.FromJson<WebsitePortfolioItemData[]>(AssetDatabase.LoadAssetAtPath<Object>("Assets/Scenes/WebsitesData/WebsiteData.json").ToString())[item];
        name = data.Name;
        url = data.Url;
        image = AssetDatabase.LoadAssetAtPath<Texture>(data.Image);
        logo = AssetDatabase.LoadAssetAtPath<Texture>(data.Logo);

        gameObject.transform.Find("TitleObject").GetComponent<Text>().text = name;
        gameObject.transform.Find("Logo").GetComponent<RawImage>().texture = logo;
        gameObject.transform.Find("MainImage").GetComponent<RawImage>().texture = image;
    }

    public void Update()
    {
        if (Input.GetMouseButton(0)) // Check for left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Application.OpenURL(url);
                }
            }
        }
    }
}

public struct WebsitePortfolioItemData
{
    public string Name;
    public string Image;
    public string Url;
    public string Logo;
}
