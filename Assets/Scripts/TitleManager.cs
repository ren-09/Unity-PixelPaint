using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    private float step_time;
    
    void Start()
    {
        step_time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        step_time += Time.deltaTime;

        if(step_time >= 3.0f)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
