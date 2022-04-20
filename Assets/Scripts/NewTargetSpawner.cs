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
    Vector3 t_pos;
    Vector3 t_scale;

    //NewButtonControllerスクリプトで使うやつ
    static public Dictionary<Vector3, Color> answerDict;

    void Start()
    {
        SpawnTile();
        LevelChange();
    }

    void SpawnTile()
    {
        targetTileInst = NewGameSceneController.targetTileInst;
        targetTileInst.gameObject.name = "TargetTile";

        //
        answerDict = new Dictionary<Vector3, Color>();
        foreach(Transform childTransform in targetTileInst.transform)
        {
            answerDict.Add(childTransform.localPosition, childTransform.GetComponent<SpriteRenderer>().color);

            if(childTransform.gameObject.tag != "WallTile")
            {
                NewButtonsController.numOfAnswers++;
            }
        }
    }

    void LevelChange()
    {
        t_height = NewGameSceneController.t_height;
        t_width = NewGameSceneController.t_width;
        // レベル別位置の設定
        switch(t_width)
        {
            case 7:
                switch(t_height)
                {
                    case 11:
                        t_pos = new Vector3(1.8f, 10.8f, 0f);
                        t_scale = new Vector3(0.4f, 0.4f, 1.00f);
                        break;
                    case 6:
                        t_pos = new Vector3(1.75f, 8.38f, 0f);
                        t_scale = new Vector3(0.4f, 0.4f, 1.00f);
                        break;
                    case 9:
                        t_pos = new Vector3(2.0f, 10.0f, 0f);
                        t_scale = new Vector3(0.4f, 0.4f, 1.00f);
                        break;
                    case 7:
                        t_pos = new Vector3(1.8f, 8.7f, 0f);
                        t_scale = new Vector3(0.5f, 0.5f, 1.00f);
                        break;
                    default:
                        t_pos = new Vector3(2.0f, 10.0f, 0f);
                        t_scale = new Vector3(0.4f, 0.4f, 1.00f);
                        break;
                }
                break;
            case 8:
                t_pos = new Vector3(2.4f, 10.8f, 0f);
                t_scale = new Vector3(0.5f, 0.5f, 1.00f);
                break;
            case 9:
                t_pos = new Vector3(2.55f, 11.25f, 0f);
                t_scale = new Vector3(0.4f, 0.4f, 1.00f);
                break;
            case 10:
                t_pos = new Vector3(2.7f, 11.6f, 0f);
                t_scale = new Vector3(0.4f, 0.4f, 1.00f);
                break;
            case 11:
                t_pos = new Vector3(2.1f, 11.6f, 0f);
                t_scale = new Vector3(0.4f, 0.4f, 1.00f);
                break;
            default:
                t_pos = new Vector3(2.7f, 11.6f, 0f);
                t_scale = new Vector3(0.4f, 0.4f, 1.00f);
                break;
        }

        targetTileInst.transform.position = t_pos;
        targetTileInst.transform.localScale = t_scale;
    }

    void Update()
    {
        
    }
}
