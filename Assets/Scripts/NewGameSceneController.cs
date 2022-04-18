using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameSceneController : MonoBehaviour
{
    //Setting()
    string tileName;
    GameObject targetTileRoaded;
    public static GameObject targetTileInst;
    public static int t_height;
    public static int t_width;
    public static int level;
    [SerializeField] GameObject newTargetSpawner;
    [SerializeField] GameObject newButtonsController;

    //NewButtonControllerスクリプト用
    [SerializeField] Button colorButtonTemp;
    public static Button colorButton;
    public static GameObject activeBlueImg;

    //SpawnTiles()
    GameObject drawableTiles;

    //Drawing()
    GameObject clickedGameObject;
    private List<GameObject> listOfTiles = new List<GameObject>();
    private GameObject lastTile;
    public GameObject tile;
    public static Color chosenColor;
    
    void Start()
    {
        Setting();
        SpawnTiles();
    }

    void Setting()
    {
        level = 1;
        //heightとwidthの取得
        tileName = "TargetTile-" + level.ToString();
        targetTileRoaded = (GameObject)Resources.Load(tileName);
        targetTileInst = Instantiate(targetTileRoaded, Vector3.zero, Quaternion.identity);
        t_height = targetTileInst.GetComponent<TargetTile>().height;
        t_width = targetTileInst.GetComponent<TargetTile>().width;

        Instantiate(newTargetSpawner);
        //SerializeFieldをstaticに
        colorButton = colorButtonTemp;
        Instantiate(newButtonsController);
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
                childTransform.GetComponent<SpriteRenderer>().color = Color.white;
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
}
