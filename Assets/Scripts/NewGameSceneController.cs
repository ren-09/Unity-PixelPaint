using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //SpawnTiles()
    GameObject drawableTiles;
    
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
    }

    void SpawnTiles()
    {
        drawableTiles =  Instantiate(targetTileRoaded, Vector3.zero, Quaternion.identity);
        drawableTiles.gameObject.name = "DrawableTiles";
        drawableTiles.tag = "DrawAbleTiles";
    }

    void Update()
    {
        
    }
}
