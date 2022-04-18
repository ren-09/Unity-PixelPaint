using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForCreateSceneController : MonoBehaviour
{
    //色変更
    public static Color themeColor;
    // public static List<Color> paintColors = new List<Color>();
    public static Color[] paintColors = new Color[4];
    public static Color transparent;
    public static Color wallColor;
    public static Color chosenColor;
    public Button toPColor1Button;
    public Button toPColor2Button;
    public Button toPColor3Button;
    public Button toColoredButton;
    public Button toTransparentButton;
    public Button toWallButton;
    GameObject coloredImg1;
    GameObject coloredImg2;
    GameObject coloredImg3;
    GameObject coloredBlueImg1;
    GameObject coloredBlueImg2;
    GameObject coloredBlueImg3;
    GameObject pColor1BlueImg;
    GameObject pColor2BlueImg;
    GameObject pColor3BlueImg;
    GameObject transparentBlueImg;
    GameObject wallBlueImg;
    // public static bool isDrawing;

    //Drawing()
    GameObject clickedGameObject;
    private List<GameObject> listOfTiles = new List<GameObject>();
    private GameObject lastTile;
    public GameObject tile;

    //LevelChange();
    public int level;

    //SpawnTiles()
    public int spawnHeight;
    public int spawnWidth;
    private List<Vector2> fieldTiles = new List<Vector2>();


    void Awake()
    {
        
    }

    void Start()
    {
        Setting();
        SpawnTiles();
    }

    void Setting()
    {
        //初期は白ボタンを押しておく
        coloredImg1 =  toPColor1Button.transform.Find("ColoredImg").gameObject;
        coloredImg2 =  toPColor2Button.transform.Find("ColoredImg").gameObject;
        coloredImg3 =  toPColor3Button.transform.Find("ColoredImg").gameObject;
        coloredBlueImg1 =  toColoredButton.transform.Find("BlueImg").gameObject;
        coloredBlueImg2 =  toColoredButton.transform.Find("BlueImg").gameObject;
        coloredBlueImg3 =  toColoredButton.transform.Find("BlueImg").gameObject;
        transparentBlueImg =  toTransparentButton.transform.Find("BlueImg").gameObject;
        wallBlueImg =  toWallButton.transform.Find("BlueImg").gameObject;
        WhiteChosen();
        OnToColoredButton1();

        LevelChange();
        transparent = Color.clear;
        wallColor = new Color32(104, 97, 97, 255);
    }

    void LevelChange()
    {
        Color red = Color.red;
        Color white = Color.white;
        Color pink = new Color32(254, 125, 244, 255);
        Color lightBrown = new Color32(230, 145, 55, 255);
        Color lightOrange = new Color32(253, 153, 5, 255);
        Color yellow = new Color32(253, 216, 102, 255);
        Color lightRed = new Color32(254, 2, 0, 255);
        Color blue = new Color32(159, 197, 233, 255);
        Color lightBlack = new Color32(67, 67, 67, 255);
        Color pinkOrange = new Color32(234, 152,154, 255);
        Color lightBlue = new Color32(207, 226, 242, 255);
        Color darkBlue = new Color32(111, 167, 221, 255);
        Color brown = new Color32(180, 94, 5, 255);
        Color lightYellow = new Color32(254, 222, 145, 255);
        Color darkRed = new Color32(253, 0, 1, 255); 
        Color darkYellow = new Color32(243, 192, 51, 255);

        switch(level)
        {
            case 1:
                paintColors[0] = pink;
                paintColors[1] = white;
                paintColors[2] = white;
                break;
            case 2:
                paintColors[0] = white;
                paintColors[1] = lightBrown;
                paintColors[2] = white;
                break;
            case 3:
                paintColors[0] = lightOrange;
                paintColors[1] = yellow;
                paintColors[2] = white;
                break;
            case 4:
                paintColors[0] = lightRed;
                paintColors[1] = blue;
                paintColors[2] = white;
                break;
            case 5:
                paintColors[0] = yellow;
                paintColors[1] = darkYellow;
                paintColors[2] = white;
                break;
            case 6:
                paintColors[0] = lightBlack;
                paintColors[1] = pinkOrange;
                paintColors[2] = white;
                break;
            case 7:
                paintColors[0] = white;
                paintColors[1] = yellow;
                paintColors[2] = white;
                break;
            case 8:
                paintColors[0] = pinkOrange;
                paintColors[1] = lightBlack;
                paintColors[2] = white;
                break;
            case 9:
                paintColors[0] = lightBlue;
                paintColors[1] = darkBlue;
                paintColors[2] = white;
                break;
            case 10:
                paintColors[0] = brown;
                paintColors[1] = red;
                paintColors[2] = lightBlack;
                break;
            case 11:
                paintColors[0] = lightYellow;
                paintColors[1] = darkRed;
                paintColors[2] = white;
                break;
            case 12:
                paintColors[0] = red;
                paintColors[1] = darkYellow;
                paintColors[2] = lightBlack;
                break;
                
        }

        Debug.Log(paintColors[0]);
        Debug.Log(paintColors[1]);
        Debug.Log(paintColors[2]);
        coloredImg1.GetComponent<Image>().color = paintColors[0];
        coloredImg2.GetComponent<Image>().color = paintColors[1];
        coloredImg3.GetComponent<Image>().color = paintColors[2];
    }

    void SpawnTiles()
    {
        int i = 0;
        for (int x = 0; x < spawnWidth; x++)
        {
            for (int y = 0; y < spawnHeight; y++)
            {
                GameObject tileInst = Instantiate(tile);
                tileInst.transform.position = new Vector3(x, y, 0);
                tileInst.GetComponent<Tile>().toWhite();
                tileInst.GetComponent<Tile>().colorIs();
                fieldTiles.Add(new Vector2(x, y));
                i++;
            }
        }
    }

    void Update()
    {
        Drawing();
    }

    void Drawing()
    {
        if (Input.GetMouseButtonDown(0))
        {

            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
                Debug.Log("clicked:" + clickedGameObject.name);
                FirstTile(clickedGameObject);
            }
        }
        if (Input.GetMouseButton(0) && listOfTiles.Count > 0)
        {
            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;

                Dragging(clickedGameObject);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ClearList();
        }
    }

    void FirstTile(GameObject clickedGameObject)
    {
        var thisTile = clickedGameObject;
        if (clickedGameObject.gameObject.CompareTag("Tile"))
        {
            listOfTiles.Add(thisTile);

            thisTile.GetComponent<SpriteRenderer>().color = chosenColor;
        }
    }

    void ClearList()
    {
        if (listOfTiles.Count > 0)
        {
            listOfTiles.Clear();
        }
    }

    void Dragging(GameObject clickedGameObject)
    {
        if (clickedGameObject.gameObject.CompareTag("Tile"))
        {
            var thisTile = clickedGameObject;
            if (!listOfTiles.Contains(thisTile))
            {
                listOfTiles.Add(thisTile);
                listOfTiles.Add(thisTile);

                clickedGameObject.GetComponent<SpriteRenderer>().color = chosenColor;
                lastTile = thisTile;
            }
        }
    }

    public void WhiteChosen()
    {
        chosenColor = Color.white;
    }

    public void OnToColoredButton1()
    {
        coloredBlueImg1.gameObject.SetActive (true);
        coloredBlueImg2.gameObject.SetActive (false);
        coloredBlueImg3.gameObject.SetActive (false);
        transparentBlueImg.gameObject.SetActive (false);
        wallBlueImg.gameObject.SetActive (false);

        chosenColor = paintColors[0];
    }

    public void OnToColoredButton2()
    {
        coloredBlueImg1.gameObject.SetActive (false);
        coloredBlueImg2.gameObject.SetActive (true);
        coloredBlueImg3.gameObject.SetActive (false);
        transparentBlueImg.gameObject.SetActive (false);
        wallBlueImg.gameObject.SetActive (false);

        chosenColor = paintColors[1];
    }

    public void OnToColoredButton3()
    {
        coloredBlueImg1.gameObject.SetActive (false);
        coloredBlueImg2.gameObject.SetActive (false);
        coloredBlueImg3.gameObject.SetActive (true);
        transparentBlueImg.gameObject.SetActive (false);
        wallBlueImg.gameObject.SetActive (false);

        chosenColor = paintColors[2];
    }

    public void OnToTransparentButton()
    {
        coloredBlueImg1.gameObject.SetActive (false);
        coloredBlueImg2.gameObject.SetActive (false);
        coloredBlueImg3.gameObject.SetActive (false);
        transparentBlueImg.gameObject.SetActive (true);
        wallBlueImg.gameObject.SetActive (false);

        chosenColor = transparent;
    }

    public void OnToWallButton()
    {
        coloredBlueImg1.gameObject.SetActive (false);
        coloredBlueImg2.gameObject.SetActive (false);
        coloredBlueImg3.gameObject.SetActive (false);
        transparentBlueImg.gameObject.SetActive (false);
        wallBlueImg.gameObject.SetActive (true);

        chosenColor = wallColor;
    }

    public void OnDeleteButton()
    {
        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");
        List<Vector3> coloredTiles = new List<Vector3>();
        List<GameObject> TilesName = new List<GameObject>();
        int numberOfAllTiles = 0;

        Debug.Log("alltiles:"+allTiles.Length);
        for (int i = 0; i < allTiles.Length; i++)
        {
            if (allTiles[i].GetComponent<SpriteRenderer>().color == transparent)
            {
                Destroy(allTiles[i]);
                // Debug.Log(coloredBlocks[i]);
                numberOfAllTiles++;
            }

            else if (allTiles[i].GetComponent<SpriteRenderer>().color == wallColor)
            {
                allTiles[i].tag = "WallTile";
            }
        }
    }
}
