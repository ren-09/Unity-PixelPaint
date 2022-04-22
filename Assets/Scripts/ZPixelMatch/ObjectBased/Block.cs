using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Material Transparent;
    static public Material themeColor;
    public Material Gray;
    private int Value = 1;
    bool CanTouchBlock;
    static public bool copied = false;
    private Animator anim;

    Vector3 startPosition;
    Vector3 startAngle;

    void Start()
    {
        //themeColorマテリアルを取得
        themeColor = GameSceneController.themeColor;
        anim = this.GetComponent<Animator>();
        startPosition = transform.position;
        startAngle = transform.eulerAngles;
    }

    public bool isCopiedBlock()
    {
        if (copied == true)
        {
            return true;
        }
        return false;
    }

    public void ToCopied()
    {
        //falseはtrueに。trueはfalseに。
        copied = !copied;
    }

    //GamwManagementで色を確認する
    public bool isTransparent()
    {
        if (Value == 1)
        {
            return true;
        }
        return false;
    }

    //透明なマテリアルを付与
    public void toTransparent()
    {
        CanTouchBlock = GameSceneController.CanTouchBlock;
        if (CanTouchBlock)
        {
            this.gameObject.GetComponent<Renderer>().material = Transparent;
            Value = 1;
            return;
        }
    }

    //青のマテリアルを付与
    public void toColored()
    {
        CanTouchBlock = GameSceneController.CanTouchBlock;
        if (CanTouchBlock)
        {
            this.gameObject.GetComponent<Renderer>().material = themeColor;
            Value = 0;
            return;
        }
    }

    //グレーのマテリアルを付与
    public void toGray()
    {
        this.gameObject.GetComponent<Renderer>().material = Gray;
        Value = 2;
        return;
    }

    public void Animated()
    {
        // anim.SetBool ("falling", true);
        if (Value == 0)
        {
            anim = this.GetComponent<Animator>();
            anim.SetTrigger("falling");
            //タグを外すことで採点に含まれないようにする
            this.gameObject.tag = "null";
        }
        else if (Value == 1)
        {
            anim = this.GetComponent<Animator>();
            anim.SetTrigger("zooming");
        }
    }

    public void BlockDestroyed()
    {
        Destroy(this.gameObject);
    }

}


