using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    //Awake()用
    public static int level = 1;

    //levelMenu用
    static public bool[] unlockedLevels;
    static public int[] staredLevels;
    
    //Resources内検索用
    string tTilesName;
    private GameObject tTilesRoaded;
    public static GameObject[] tTilesArr;
    public static int tTilesCounted;

    void Awake()
    {
        bool unlocked = true;
        int numberOfStars = 0;

        tTilesCounted = 1;
        while (true)
        {
            tTilesName = "TargetTile-" + tTilesCounted.ToString();
            tTilesRoaded = (GameObject)Resources.Load(tTilesName);

            if (tTilesRoaded == null)
            {
                tTilesCounted -= 1;
                break;
            }

            tTilesCounted++;
        }

        unlockedLevels = new bool[tTilesCounted];
        staredLevels = new int[tTilesCounted];

        for (int i = 0; i < tTilesCounted; i++)
        {
            unlockedLevels[i] = !unlocked;
            staredLevels[i] = numberOfStars;
        }

        Debug.Log("やられてますね");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
