using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    public Sprite[] numsTemp; 
    [SerializeField] Sprite levelTemp;
    [SerializeField] GameObject levelLevel;
    [SerializeField] Image levelNum1;
    [SerializeField] Image levelNum2;
    int level;
    
    void Start()
    {
        LevelTextChange();
    }

    void LevelTextChange()
    {
        level = NewGameManager.level;
        

        string s = level.ToString();
        int count = 0;

        Debug.Log("level:"+level);

        if(level < 10)
        {
            levelNum2.gameObject.SetActive(false);
            levelNum1.GetComponent<Image>().sprite = numsTemp[level];
        }
        else
        {
            //FirstDigit, SecondDigit
            int fDgt = level % 10;
            int sDgt = (level - fDgt) / 10;

            levelNum2.GetComponent<Image>().sprite = numsTemp[fDgt];
            levelNum1.GetComponent<Image>().sprite = numsTemp[sDgt];
        }
    }
}
