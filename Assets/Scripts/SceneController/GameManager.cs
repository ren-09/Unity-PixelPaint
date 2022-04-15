using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    static public Dictionary<int, bool> unlockedLevels = new Dictionary<int, bool>();
    static public Dictionary<int, int> staredLevels = new Dictionary<int, int>();

    string planeName;
    private GameObject coloredPlane;
    public static GameObject[] coloredPlanes;
    public static int CPlaneCounted;

    void Awake()
    {
        bool unlocked = false;
        int numberOfStars = 0;

        // Resources内のColoredPlaneの枚数を数えて、Levelbuttonの数を決定する。
        // LevelMenuスクリプト等で受け取る
        // ColoredPlane-1があったら1
        CPlaneCounted = 1;
        while (true)
        {
            planeName = "ColoredPlane-" + CPlaneCounted.ToString();
            coloredPlane = (GameObject)Resources.Load(planeName);

            if (coloredPlane == null)
            {
                CPlaneCounted -= 1;
                break;
            }

            CPlaneCounted++;
        }

        for (int i = 0; i < CPlaneCounted; i++)
        {
            unlockedLevels[i] = unlocked;
            staredLevels[i] = numberOfStars;
        }
    }

    void Update()
    {

    }
}
