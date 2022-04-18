using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewButtonsController : MonoBehaviour
{
    //Setting()
    int level;

    //ColorButtonsControl()
    Button colorButton;
    public static List<Color> paintColors;
    int numOfColorButtons;
    GameObject targetTileInst;
    GameObject canvas;
    GameObject colorButtons;
    GameObject[] blueImgs;
    GameObject activeBlueImg;

    //各種ボタンスクリプト
    GameObject menuPanel;
    GameObject menuBackgroundButton;
    Color chosenColor;

    //LevelChangeButton()
    GameObject[] levelButtons;

    //OnOkButton()
    static public Dictionary<Vector3, Color> answerDict = new Dictionary<Vector3, Color>();
    GameObject drawableTiles;
    Vector3 keyPos;
    int numOfTiles = 0;
    int numOfCorrects = 0;

    void Start()
    {
        Setting();
        ColorButtonsControl();
    }

    void Setting()
    {
        level = NewGameSceneController.level;
        
        //ColoredButtonsControl()
        paintColors = new List<Color>();
        canvas = GameObject.FindWithTag("Canvas");
        colorButtons = GameObject.FindWithTag("ColorButtons");
        colorButton = NewGameSceneController.colorButton;
        activeBlueImg = NewGameSceneController.activeBlueImg;

        //各種ボタンスクリプト
        menuPanel = GameObject.FindWithTag("MenuPanel");
        menuBackgroundButton = GameObject.FindWithTag("MenuBackgroundButton");
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
    }

    void ColorButtonsControl()
    {
        //GameSceneControllerのtargetTileInstから色を取得
        targetTileInst = NewGameSceneController.targetTileInst;
        
        numOfColorButtons = 0;
        foreach(Transform childTransform in targetTileInst.transform)
        {
            if( childTransform.tag == "Tile" && !paintColors.Contains(childTransform.GetComponent<SpriteRenderer>().color))
            {
                paintColors.Add(childTransform.GetComponent<SpriteRenderer>().color);
                numOfColorButtons++;
            }
        }

        //ColorButtonの数と色を変更.親を設定
        for(int i = 0; i < numOfColorButtons; i++)
        {
            Button colorButtonInst = Instantiate(colorButton);
            colorButtonInst.transform.SetParent(canvas.transform, false);
            colorButtonInst.transform.SetParent(colorButtons.transform, false);
            Transform coloredImg = colorButtonInst.transform.Find("ColoredImg");
            coloredImg.gameObject.GetComponent<Image>().color = paintColors[i];
            if(i == 0)
            {
                OnColorButton(colorButtonInst.gameObject);
            }
        }
    }

    public void OnColorButton(GameObject clickedButton)
    {
        Transform coloredImg = clickedButton.transform.Find("ColoredImg");
        chosenColor = coloredImg.gameObject.GetComponent<Image>().color;
        NewGameSceneController.chosenColor = chosenColor;
        blueImgs = GameObject.FindGameObjectsWithTag("BlueImg");
        for(int i = 0; i < blueImgs.Length; i++)
        {
            blueImgs[i].gameObject.SetActive(false);
        }
        clickedButton.transform.Find("BlueImg").gameObject.SetActive(true);
    }

    public void OnLevelChangeButton(GameObject clickedGameObject)
    {

        int clickedLevel = Array.IndexOf(levelButtons, clickedGameObject, 0) + 1;
        Debug.Log(clickedLevel);
        NewGameSceneController.level = clickedLevel;

        SceneManager.LoadScene("GameScene");
    }

    public void OnMenuButton()
    {
        if (menuPanel.activeSelf)
        {
            menuBackgroundButton.SetActive(false);
            menuPanel.SetActive(false);
        }
        else
        {
            menuBackgroundButton.SetActive(true);
            menuPanel.SetActive(true);
        }
    }

    public void OnmenuBackgroundButton()
    {
        menuBackgroundButton.SetActive(false);
        menuPanel.SetActive(false);
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnXButton()
    {
        menuPanel = GameObject.FindWithTag("MenuPanel");
        menuBackgroundButton = GameObject.FindWithTag("MenuBackgroundButton");
        menuPanel.SetActive(false);
        menuBackgroundButton.SetActive(false);
    }

    public void OnFromStartButton()
    {
        GameSceneController.level = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void OnOkButton()
    {
        drawableTiles = GameObject.FindWithTag("DrawableTiles");
        answerDict = NewTargetSpawner.answerDict;
        
        numOfTiles = 0;
        numOfCorrects = 0;
        foreach(Transform childTransform in drawableTiles.transform)
        {
            keyPos = childTransform.localPosition;
            if(childTransform.gameObject.tag == "Tile" && answerDict[keyPos] == childTransform.GetComponent<SpriteRenderer>().color)
            {
                numOfCorrects++;
            }
            numOfTiles++;
        }
        Debug.Log(numOfCorrects);
        Debug.Log(numOfTiles);
    }

    //使ってないから消してもいい
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
