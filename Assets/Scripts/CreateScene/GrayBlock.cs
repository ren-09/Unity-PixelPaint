using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayBlock : MonoBehaviour
{
    public Material Transparent;
    public Material Gray;
    public Material None;
    private int Value = 0;


    void Start()
    {
        // DontDestroyOnLoad(this);
        // ColorSwitch(level);
        //ColoredBlockマテリアルを取得
    }


    void Update()
    {
        if (Value == 0)
        {
            // Debug.Log("Blue");
            this.gameObject.GetComponent<Renderer>().material = Gray;
        }
        if (Value == 1)
        {
            // Debug.Log("Transp");
            this.gameObject.GetComponent<Renderer>().material = Transparent;

        }
        if (Value == 2)
        {
            // Debug.Log("Transp");
            this.gameObject.GetComponent<Renderer>().material = Gray;

        }
    }

    //GamwManagementで色を確認する
    public bool isTransparent()
    {
        if (Value == 1)
        {
            return true;
        }
        return false;
    }

    //透明なマテリアルを付与
    public void toTransparent()
    {
        Value = 1;
        return;
    }

    //青のマテリアルを付与
    public void toColored()
    {
        Value = 0;
        return;
    }

    //グレーのマテリアルを付与
    public void toGray()
    {
        Value = 2;
        return;
    }



}
