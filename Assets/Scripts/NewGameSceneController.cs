using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameSceneController : MonoBehaviour
{
    //TargetAnimスクリプト用


    //Setting()
    string tileName;
    GameObject targetTileRoaded;
    public static GameObject targetTileInst;
    public static int t_height;
    public static int t_width;
    public static int level;
    public static int score = 0;
    [SerializeField] GameObject newTargetSpawner;
    [SerializeField] GameObject newButtonsController;
    [SerializeField] GameObject congratsText;
    [SerializeField] GameObject fromStartButton;

    //NewButtonControllerスクリプト用
    [SerializeField] Button colorButtonTemp;
    public static Button colorButton;
    public static GameObject activeBlueImg;

    //LevelChange()
    public static int tTilesCounted;
    Camera mainCamera;
    public static Vector3 c_pos;
    public static float c_size;

    //SpawnTiles()
    public static GameObject drawableTiles;

    //Drawing()
    GameObject clickedGameObject;
    private List<GameObject> listOfTiles = new List<GameObject>();
    private GameObject lastTile;
    public static Color chosenColor;

    void Awake()
    {
        DontDestroyManager.DestroyAll();
    }
    
    void Start()
    {
        //TargetTilesのロードやオブジェクトのInstantaite
        Setting();
        //カメラ位置の変更
        LevelChange();
        //DrawableTilesをspawn
        SpawnTiles();
    }

    void Update()
    {
        Drawing();
    }

    //Start().begin
    void Setting()
    {
        //これが最初
        level = NewGameManager.level;
        tTilesCounted = NewGameManager.tTilesCounted;
        //上限
        if (level < tTilesCounted + 1)
        {
            // rotatePlane.SetActive(true);
            // stopPlane.SetActive(true);
        }
        Debug.Log("ttilesCounted"+tTilesCounted);
        if(level >= tTilesCounted + 1)
        {
            congratsText.SetActive(true);
            fromStartButton.SetActive(true);
            Debug.Log("はい行けたよ");
            return;
        }

        
        //heightとwidthの取得
        tileName = "TargetTile-" + level.ToString();
        Debug.Log("level"+level);
        targetTileRoaded = (GameObject)Resources.Load(tileName);
        targetTileInst = Instantiate(targetTileRoaded, Vector3.zero, Quaternion.identity);
        t_height = targetTileInst.GetComponent<TargetTile>().height;
        t_width = targetTileInst.GetComponent<TargetTile>().width;

        Instantiate(newTargetSpawner);
        //SerializeFieldをstaticに代入
        colorButton = colorButtonTemp;
        NewButtonsController.PublicStart();

        congratsText.SetActive(false);
        fromStartButton.SetActive(false);
    }

    void LevelChange()
    {
        
        mainCamera = Camera.main;
        
        
        switch(t_width)
        {
            case 7:
                switch(t_height)
                {
                    case 11:
                        c_pos = new Vector3(2.9f, 4.0f, -10f);
                        c_size = 14f;
                        break;
                    case 6:
                        c_pos = new Vector3(2.9f, 2.3f, -10f);
                        c_size = 12.0f;
                        break;
                    case 9:
                        c_pos = new Vector3(2.8f, 3.8f, -10f);
                        c_size = 13f;
                        break;
                    case 7:
                        c_pos = new Vector3(2.4f, 3.1f, -10f);
                        c_size = 12f;
                        break;
                    default:
                        c_pos = new Vector3(2.8f, 3.8f, -10f);
                        c_size = 13f;
                        break;
                }
                break;
            case 8:
                c_pos = new Vector3(4.3f, 4.9f, -10f);
                c_size = 14f;
                break;
            case 9:
                c_pos = new Vector3(3.8f, 3.9f, -10f);
                c_size = 15f;
                break;
            case 10:
                c_pos = new Vector3(4.4f, 3.9f, -10f);
                c_size = 16f;
                break;
            case 11:
                c_pos = new Vector3(3.6f, 4.3f, -10f);
                c_size = 16f;
                break;
            default:
                c_pos = new Vector3(3.6f, 4.3f, -10f);
                c_size = 16f;
                break;
        }

        mainCamera.transform.position = c_pos;
        mainCamera.orthographicSize = c_size;
    }

    void SpawnTiles()
    {
        drawableTiles =  Instantiate(targetTileRoaded, Vector3.zero, Quaternion.identity);
        drawableTiles.gameObject.name = "DrawableTiles";
        drawableTiles.tag = "DrawableTiles";
        //色を白に変更
        foreach(Transform childTransform in drawableTiles.transform)
        {
            if( childTransform.tag == "Tile" && childTransform.GetComponent<SpriteRenderer>().color != Color.white)
            {
                // childTransform.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    //Start().end
    //Update().begin
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
    //Update().end
}
