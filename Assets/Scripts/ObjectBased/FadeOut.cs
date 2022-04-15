using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    float fadeSpeed = 0.05f;
    float red, green, blue, alpha;

    Image fadeImage;

    private float step_time;
    
    void Start()
    {
        step_time = 0.0f;

        fadeImage = GetComponent<Image>();
        alpha = fadeImage.color.a;
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
    }

    // Update is called once per frame
    void Update()
    {
        step_time += Time.deltaTime;

        if(step_time >= 2.0f)
        {
            alpha -= fadeSpeed;
            fadeImage.color = new Color(red, green, blue, alpha);
        }
    }
}
