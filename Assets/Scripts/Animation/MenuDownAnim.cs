using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDownAnim : MonoBehaviour
{
    float left;
    float right;

    Vector2 startPos;
    Vector2 aimedPos;
    float posDif;
    float dividedPosDif;
    // CurrentPos
    Vector2 crtPos;
    float division = 4f;
    int timesCalled = 0;
    void Start()
    {
        Setting();
    }

    void Setting()
    {
        left = gameObject.GetComponent<RectTransform>().offsetMax.x;
        right = gameObject.GetComponent<RectTransform>().offsetMin.x;

        startPos = new Vector2(2905f, -2079f);
        aimedPos = new Vector2(537f, 289f);

        posDif = startPos.x - aimedPos.x;
        dividedPosDif = posDif / division;

        gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(left, -aimedPos.x);
        gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(right, aimedPos.y);
    }
    void Update()
    {
        MenuMove();
    }

    void MenuMove()
    {
        //division回呼ばれたら終わり
        if (timesCalled >= division)
        {
            //なぜか不明だがTopがマイナスになるのでマイナス
            gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(left, -startPos.x);
            gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(right, startPos.y);
            this.gameObject.SetActive(false);
            GameObject.Destroy(gameObject.GetComponent<MenuDownAnim>());
            return;
        }
        gameObject.GetComponent<RectTransform>().offsetMax -= new Vector2(0f, dividedPosDif);
        gameObject.GetComponent<RectTransform>().offsetMin -= new Vector2(0f, dividedPosDif);

        timesCalled++;
    }
}
