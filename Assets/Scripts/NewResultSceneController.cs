using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewResultSceneController : MonoBehaviour
{
    //Setting()
    Camera mainCamera;
    int level = NewGameManager.level;
    int score = NewGameSceneController.score;
    [SerializeField] GameObject scoreTextSerialized;
    bool[] unlockedLevels = NewGameManager.unlockedLevels;
    int[] staredLevels = NewGameManager.staredLevels;
    [SerializeField] GameObject FX_CrackerUp;
    [SerializeField] GameObject FX_CrackerFall;

    //ChangeStarsColor
    public Image image;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        Setting();
    }

    void Setting()
    {
        mainCamera = Camera.main;
        mainCamera.transform.position = NewGameSceneController.c_pos;
        mainCamera.orthographicSize = NewGameSceneController.c_size;

        //scoreとアンロック変更
        if (score > 0)
        {
            unlockedLevels[level - 1] = true;
            staredLevels[level - 1] = score;
        }

        Text scoreText = scoreTextSerialized.GetComponent<Text>();
        scoreText.text = score.ToString();

        FX_CrackerUp.SetActive(false);
        FX_CrackerFall.SetActive(false);

        StartCoroutine(ChangeStarsColor());
    }

    public IEnumerator ChangeStarsColor()
    {
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
        int n = 0;
        for (int i = score; i > 0; i--)
        {
            if (n > 2)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.3f);
            image = stars[n].GetComponent<Image>();
            image.sprite = sprite;
            StarAnim(stars[n]);
            n++;
        }


        if (score == 3)
        {
            FX_CrackerUp.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            FX_CrackerFall.SetActive(true);
        }
    }

    public void StarAnim(GameObject obj)
    {
        Animator anim = obj.GetComponent<Animator>();
        anim.SetTrigger("Animated");
    }
}
