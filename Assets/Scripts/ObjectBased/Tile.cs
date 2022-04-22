using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static Color white;
    public static Color themeColor;
    public static Color transparent;
    public Material None;
    private int Value = 0;


    void Awake()
    {
        white = Color.white;
        themeColor = ForCreateSceneController.themeColor;
        transparent = ForCreateSceneController.transparent;
    }

    //GamwManagementで色を確認する
    public bool isWhite()
    {
        if (Value == 1)
        {
            return true;
        }
        return false;
    }

    //透明なマテリアルを付与
    public void toWhite()
    {
        GetComponent<SpriteRenderer>().color = white;
        return;
    }

    //青のマテリアルを付与
    public void toColored()
    {
        GetComponent<SpriteRenderer>().color = themeColor;
        return;
    }

    public void toTransparent()
    {
        GetComponent<SpriteRenderer>().color = transparent;
        return;
    }
}
