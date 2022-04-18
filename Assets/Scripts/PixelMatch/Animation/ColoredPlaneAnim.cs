using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredPlaneAnim : MonoBehaviour
{
    //アニメーション
    private float step_time;
    private float move_time;
    private Vector3 positionDifference;
    private Vector3 dividedPositionDifference;
    private Vector3 sizeDifference;
    private Vector3 dividedSizeDifference;
    private float division;
    bool setStepTime;
    Vector3 targetSize = new Vector3(1f, 1f, 1f);

    //参照系



    void Start()
    {
        //参照

        //アニメーション
        step_time = 0.0f;
        move_time = 0.0f;
        division = 20f;

        //アニメーション
        step_time = Time.time;
        positionDifference.x = transform.position.x;
        positionDifference.y = transform.position.y;
        dividedPositionDifference = positionDifference / division;
        sizeDifference = targetSize - transform.localScale;
        dividedSizeDifference = sizeDifference / division;
        // Debug.Log("DivSizeDif:" + sizeDifference);
        DontDestroyManager.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        //アニメーション
        //一回だけstep_timeを設定する
        if (!setStepTime)
        {
            step_time = Time.time;
            setStepTime = true;
        }
        CameraMove();
    }

    public void CameraMove()
    {
        move_time = Time.time;
        //カメラの移動アニメーション
        if (move_time - step_time >= 0.7f)
        {
            if (positionDifference.x > 0)
            {
                transform.position += new Vector3(-dividedPositionDifference.x, 0, 0);
                positionDifference = transform.position;
            }
            if (positionDifference.y > 0)
            {
                transform.position += new Vector3(0, -dividedPositionDifference.y, 0);
                positionDifference = transform.position;
            }
            if (sizeDifference.x > 0)
            {
                transform.localScale += new Vector3(dividedSizeDifference.x, 0, 0);
                sizeDifference = transform.localScale;
            }
            if (sizeDifference.y > 0)
            {
                transform.localScale += new Vector3(0, dividedSizeDifference.y, 0);
                sizeDifference = transform.localScale;
            }
            if (targetSize.x < transform.localScale.x)
            {
                transform.position = Vector3.zero;
                transform.localScale = targetSize;
            }
        }
    }
}
