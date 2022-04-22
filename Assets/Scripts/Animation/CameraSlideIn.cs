using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlideIn : MonoBehaviour
{
    float startPosX;
    Vector3 aimedPos = NewGameSceneController.c_pos;
    float aimedSize = NewGameSceneController.c_size;
    float division = 12f;
    float posDifX;
    float dividedPosDifX;
    int timesCalled = 0;
    void Start()
    {
        Setting();
    }

    void Setting()
    {
        startPosX = -8f;
        transform.position = new Vector3(startPosX, aimedPos.y, aimedPos.z);
        gameObject.GetComponent<Camera>().orthographicSize = aimedSize;

        posDifX = aimedPos.x - startPosX;
        dividedPosDifX = posDifX / division;
    }

    void Update()
    {
        CameraIn();
    }

    void CameraIn()
    {
        //division回呼ばれたら終わり
        if (timesCalled >= division)
        {
            transform.position = aimedPos;
            GameObject.Destroy(gameObject.GetComponent<CameraSlideIn>());
            return;
        }
        transform.position += new Vector3(dividedPosDifX, 0f, 0f);

        timesCalled++;
    }
}
