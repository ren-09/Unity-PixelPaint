using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSlideOut : MonoBehaviour
{
    float aimedPosX;
    Vector3 startPos;
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
        aimedPosX = -8f;
        startPos = transform.position;

        posDifX = startPos.x - aimedPosX;
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
            transform.position = new Vector3(aimedPosX, startPos.y, startPos.z);
            Invoke("OnLoadGameScene", 0.1f);
            return;
        }
        transform.position += new Vector3(dividedPosDifX, 0f, 0f);

        timesCalled++;
    }

    void OnLoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
        GameObject.Destroy(gameObject.GetComponent<CameraSlideOut>());
    }
}
