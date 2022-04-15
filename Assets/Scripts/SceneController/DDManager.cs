using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameObject dontDestroyObject;

    void Start()
    {
        DontDestroyOnLoad(gameManagerObject);
        DontDestroyOnLoad(dontDestroyObject);
    }
}
