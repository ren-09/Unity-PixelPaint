using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredPlane : MonoBehaviour
{
    public int height;
    public int width;
    public Material themeColor;


    public int Height()
    {
        return height;
    }

    public int Width()
    {
        return width;
    }

    void Start()
    {
        // foreach (Transform childTransform in this.gameObject.transform)
        // {
        //     childTransform.gameObject.GetComponent<Renderer>().material = themeColor;
        // }
    }
}
