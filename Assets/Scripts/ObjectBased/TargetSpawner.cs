using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    //start()
    int CPlaneCounted = GameManager.CPlaneCounted;
    int c_height;
    int c_width;
    //LevelChange()
    int level = GameSceneController.level;
    string planeName;
    //SpawnPlane()
    Vector3 targetPos;
    Vector3 targetScale;
    GameObject planeTarget;
    public static GameObject blockTarget;
    GameObject coloredPlane;
    //PlaneToGrayBlock()
    [SerializeField] Material Gray;
    [SerializeField] GameObject block;

    void Start()
    {
        // resources内ColoredPlaneの数より高いレベルはTargetを生成しない。
        if (level < CPlaneCounted + 1)
        {
            //Plane生成
            SpawnPlane();
            //レベルごとのposとscale設定
            LevelChange();
            //PlaneをBlockに変更
            PlaneToGrayBlock();
        }
    }

    void SpawnPlane()
    {
        planeName = "ColoredPlane-" + level.ToString();
        coloredPlane = (GameObject)Resources.Load(planeName);
        planeTarget = Instantiate(coloredPlane, Vector3.zero, Quaternion.identity);
        planeTarget.gameObject.name = "PlaneTarget";
    }

    void PlaneToGrayBlock()
    {
        blockTarget = new GameObject("BlockTarget");
        blockTarget.transform.position = planeTarget.transform.position;
        blockTarget.transform.localScale = planeTarget.transform.localScale;

        foreach (Transform childTransform in planeTarget.transform)
        {
            GameObject blockInstance = Instantiate(block);
            blockInstance.transform.position = childTransform.position;
            blockInstance.gameObject.GetComponent<Renderer>().material = Gray;
            blockInstance.gameObject.tag = "null";
            blockInstance.transform.SetParent(blockTarget.transform, true);
        }
        Destroy(planeTarget);

        blockTarget.transform.position = targetPos;
        blockTarget.transform.localScale = targetScale;
    }

    void LevelChange()
    {
        c_height = GameSceneController.c_height;
        c_width = GameSceneController.c_width;
        // レベル別位置の設定
        switch (c_width)
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
                switch (c_height)
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
                targetPos = new Vector3(2.6f, 12.3f, 0.3f);
                targetScale = new Vector3(0.4f, 0.4f, 1f);
                break;
        }
    }

    void Update()
    {

    }
}
