using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    int level;
    Material bgColorLoaded;
    [SerializeField] Material bgColorTemp;
    string bgColorName;
    void Start()
    {
        level = NewGameManager.level;
        LevelChange();
    }

    void LevelChange()
    {
        switch (level)
        {
            case 1:
                bgColorName = "Pink";
                break;
            case 2:
            case 9:
                bgColorName = "Blue";
                break;
            case 3:
            case 7:
            case 12:
                bgColorName = "Yellow";
                break;
            case 4:
            case 5:
                bgColorName = "Orange";
                break;
            case 6:
            case 10:
                bgColorName = "Green";
                break;
            case 11:
                bgColorName = "Red";
                break;
            case 8:
                bgColorName = "Gray";
                break;
            default:
                bgColorName = "Yellow";
                break;
        }
        // Debug.Log("Level:" + level);

        bgColorLoaded = (Material)Resources.Load("metatex/materials/" + bgColorName);
        Debug.Log(bgColorName);

        gameObject.GetComponent<Renderer>().material = bgColorLoaded;
    }
}
