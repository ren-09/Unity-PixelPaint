using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAnim : MonoBehaviour
{
    GameObject drawableTiles = NewGameSceneController.drawableTiles;
    GameObject targetTile = NewGameSceneController.targetTileInst;

    float aimedPosY;
    Vector3 startPos;
    float division = 30f;
    float posDifY;
    float dividedPosDifY;
    int timesCalled = 0;
    void Start()
    {
        Setting();
    }

    void Setting()
    {
        //
        startPos = new Vector3(3.99f, 0f, 0.65f);
        transform.position = startPos;
        aimedPosY = drawableTiles.transform.position.y + 5.3f;

        posDifY = aimedPosY - startPos.y;
        dividedPosDifY = posDifY / division;

    }

    // Update is called once per frame
    void Update()
    {
        MoveLazer();
    }

    void MoveLazer()
    {
        //division回呼ばれたら終わり
        if (timesCalled >= division)
        {
            transform.position = new Vector3(startPos.y, aimedPosY, startPos.z);
            gameObject.SetActive(false);
            GameObject.Destroy(gameObject.GetComponent<LazerAnim>());
            return;
        }
        transform.position += new Vector3(0f, dividedPosDifY, 0f);

        timesCalled++;
    }
}
