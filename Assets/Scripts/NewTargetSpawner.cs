using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTargetSpawner : MonoBehaviour
{
    //SpawnTile()
    string tileName;
    GameObject targetTileRoaded;
    GameObject targetTileInst;

    //LevelChange();
    int level;
    int t_height;
    int t_width;
    Vector3 targetPos;
    Vector3 targetScale;

    void Start()
    {
        SpawnTile();
        LevelChange();
    }

    void SpawnTile()
    {
        targetTileInst = NewGameSceneController.targetTileInst;
        targetTileInst.gameObject.name = "TargetTile";
    }

    void LevelChange()
    {
        t_height = NewGameSceneController.t_height;
        t_width = NewGameSceneController.t_width;
        // レベル別位置の設定
        switch (t_width)
        {
            case 10:
                targetPos = new Vector3(2.6f, 12.3f, 0.3f);
                targetScale = new Vector3(0.4f, 0.4f, 1f);
                break;
            case 8:
                targetPos = new Vector3(1.9f, 9.4f, 0.3f);
                targetScale = new Vector3(0.45f, 0.45f, 1f);
                break;
            case 6:
                switch (t_height)
                {
                    case 6:
                        targetPos = new Vector3(1.2f, 8.4f, 0.3f);
                        targetScale = new Vector3(0.5f, 0.5f, 1f);
                        break;
                    case 7:
                        targetPos = new Vector3(1.34f, 8.8f, 0.3f);
                        targetScale = new Vector3(0.45f, 0.45f, 1f);
                        break;
                }
                break;
            default:
                targetPos = new Vector3(2.6f, 10.0f, 0.3f);
                targetScale = new Vector3(0.4f, 0.4f, 1f);
                break;
        }

        targetTileInst.transform.position = targetPos;
        targetTileInst.transform.localScale = targetScale;
    }

    void Update()
    {
        
    }
}
