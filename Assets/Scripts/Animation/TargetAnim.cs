using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnim : MonoBehaviour
{
    //アニメーション
    private float step_time;
    private float move_time;
    private Vector3 aimedPos;
    private Vector3 aimedScale;
    private Vector3 positionDifference;
    private Vector3 dividedPositionDifference;
    private Vector3 sizeDifference;
    private Vector3 dividedSizeDifference;
    private float division;
    bool setStepTime;

    //LevelChange()
    int t_height = NewGameSceneController.t_height;
    int t_width = NewGameSceneController.t_width;

    int numOfCalled = 0;

    GameObject tilePlane;


    void Start()
    {
        LevelChange();

        //tileをplaneに変換
        tilePlane = (GameObject)Resources.Load("TilePlane");
        int m = 0;
        GameObject[] tilePlaneInsts = new GameObject[transform.childCount];
        foreach(Transform childTransform in transform)
        {
            tilePlaneInsts[m] = Instantiate(tilePlane, childTransform.position, Quaternion.identity);
            tilePlaneInsts[m].GetComponent<SpriteRenderer>().color = childTransform.GetComponent<SpriteRenderer>().color;
            tilePlaneInsts[m].gameObject.tag = childTransform.gameObject.tag;
            Destroy(childTransform.gameObject);
            m++;
        }

        for(int s = 0; s < tilePlaneInsts.Length; s++)
        {
            tilePlaneInsts[s].transform.SetParent(transform, true);
        }
        
        //アニメーション
        step_time = Time.time;
        move_time = 0.0f;
        division = 20f;

        
        aimedScale = new Vector3(0.6f,0.6f,1f);
        positionDifference = aimedPos - transform.position;
        dividedPositionDifference = positionDifference / division;
        sizeDifference = aimedScale - transform.localScale;
        dividedSizeDifference = sizeDifference / division;
        DontDestroyManager.DontDestroyOnLoad(this.gameObject);

        //なぜか分からないが、上のfor文でscaleを変えるとバグるのでここに書いた
        for(int s = 0; s < tilePlaneInsts.Length; s++)
        {
            tilePlaneInsts[s].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    void LevelChange()
    {
        aimedPos = new Vector3(-1.26f,1.27f,0);
        switch(t_width)
        {
            case 7:
                aimedPos = new Vector3(-1.47f, 1f, 0f);
                break;
            case 10:
                aimedPos = new Vector3(-1.76f, 1f, 0f);
                break;
            case 11:
                aimedPos = new Vector3(-1.76f, 1f, 0f);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        //一回だけstep_timeを設定する
        if (!setStepTime)
        {
            step_time = Time.time;
            setStepTime = true;
        }
        TargetMove();
    }

    public void TargetMove()
    {
        move_time = Time.time;
        //カメラの移動アニメーション
        if (move_time - step_time >= 0.7f)
        {
            if (positionDifference.x > 0 || positionDifference.x < 0)
            {
                transform.position += new Vector3(dividedPositionDifference.x, 0, 0);
            }
            if (positionDifference.y > 0 || positionDifference.y < 0)
            {
                transform.position += new Vector3(0, dividedPositionDifference.y, 0);
            }
            if (sizeDifference.x > 0 || sizeDifference.x < 0)
            {
                transform.localScale += new Vector3(dividedSizeDifference.x, 0, 0);
            }
            if (sizeDifference.y > 0 || sizeDifference.y < 0)
            {
                transform.localScale += new Vector3(0, dividedSizeDifference.y, 0);
            }
            positionDifference = aimedPos - transform.position;
            sizeDifference = aimedScale - transform.localScale;
            numOfCalled++;
        }

        if (numOfCalled >= division)
        {
            transform.position = aimedPos;
            transform.localScale = aimedScale;
        }
    }
}

