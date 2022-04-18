using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewButtonsController : MonoBehaviour
{
    //Setting()
    int level;

    //ColorButtonsControl()
    public Button colorButton;
    // public static Color[] paintColors;
    public static List<Color> paintColors;
    int numOfColorButtons;
    GameObject targetTileRoaded;

    void Start()
    {
        Setting();
    }

    void Setting()
    {
        level = NewGameSceneController.level;
    }

    void ColorButtonsControl()
    {
        switch(level)
        {
            case 3:
            case 6:
            case 8:
            case 10:
            case 11:
            case 12:
                numOfColorButtons = 3;
                break;
            default:
                numOfColorButtons = 2;
                break;
        }


        targetTileRoaded = NewGameSceneController.targetTileInst;
        for(int i = 0; i < numOfColorButtons; i++)
        {
            foreach(Transform childTransform in targetTileRoaded.transform)
            {
                if( childTransform.tag == "Tile" && !paintColors.Contains(childTransform.GetComponent<SpriteRenderer>().color))
                {
                    paintColors[i] = childTransform.GetComponent<SpriteRenderer>().color;
                }
            }
            
        }

        //colorName
        // string[] colNameArr;
        // string colName1;
        // string colName2;
        // string colName3;
        // switch(level)
        // {
        //     case 1:
        //         colo
        //         string[] colNameArr = {"pink","white"};
        //         break;
        //     case 2:
        //         string[] colNameArr = {"lightBrown","white"};
        //         break;
        //     case 3:
        //         paintColors[0] = lightOrange;
        //         paintColors[1] = yellow;
        //         paintColors[2] = white;
        //         break;
        //     case 4:
        //         paintColors[0] = lightRed;
        //         paintColors[1] = blue;
        //         break;
        //     case 5:
        //         paintColors[0] = yellow;
        //         paintColors[1] = darkYellow;
        //         break;
        //     case 6:
        //         paintColors[0] = lightBlack;
        //         paintColors[1] = pinkOrange;
        //         paintColors[2] = white;
        //         break;
        //     case 7:
        //         paintColors[0] = white;
        //         paintColors[1] = yellow;
        //         break;
        //     case 8:
        //         paintColors[0] = pinkOrange;
        //         paintColors[1] = lightBlack;
        //         paintColors[2] = white;
        //         break;
        //     case 9:
        //         paintColors[0] = lightBlue;
        //         paintColors[1] = darkBlue;
        //         break;
        //     case 10:
        //         paintColors[0] = brown;
        //         paintColors[1] = red;
        //         paintColors[2] = lightBlack;
        //         break;
        //     case 11:
        //         paintColors[0] = lightYellow;
        //         paintColors[1] = darkRed;
        //         paintColors[2] = white;
        //         break;
        //     case 12:
        //         paintColors[0] = red;
        //         paintColors[1] = darkYellow;
        //         paintColors[2] = lightBlack;
        //         break;
        // }
    }

    static public Dictionary<string, Color> colorDict = new Dictionary<string, Color>()
    {
        {"red",Color.red},
        {"white",Color.white},
        {"pink",new Color32(254, 125, 244, 255)},
        {"lightBrown",new Color32(230, 145, 55, 255)},
        {"lightOrange",new Color32(253, 153, 5, 255)},
        {"yellow",new Color32(253, 216, 102, 255)},
        {"lightRed",new Color32(254, 2, 0, 255)},
        {"blue",new Color32(159, 197, 233, 255)},
        {"lightBlack",new Color32(67, 67, 67, 255)},
        {"pinkOrange",new Color32(234, 152,154, 255)},
        {"lightBlue",new Color32(207, 226, 242, 255)},
        {"darkBlue",new Color32(111, 167, 221, 255)},
        {"brown",new Color32(180, 94, 5, 255)},
        {"lightYellow",new Color32(254, 222, 145, 255)},
        {"darkRed",new Color32(253, 0, 1, 255)},
        {"darkYellow",new Color32(243, 192, 51, 255)}
    };
}
