using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    //levelMenu用
    static public Dictionary<int, bool> unlockedLevels = new Dictionary<int, bool>();
    static public Dictionary<int, int> staredLevels = new Dictionary<int, int>();
    
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

        for (int i = 0; i < tTilesCounted; i++)
        {
            unlockedLevels[i] = !unlocked;
            staredLevels[i] = numberOfStars;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
