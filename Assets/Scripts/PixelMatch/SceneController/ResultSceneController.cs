using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneController : MonoBehaviour
{
    public Image image;
    public Sprite sprite;
    int score = GameSceneController.score;
    int level = GameSceneController.level;
    int c_height = GameSceneController.c_height;
    int c_width = GameSceneController.c_width;

    Dictionary<int, bool> unlockedLevels = GameManager.unlockedLevels;
    Dictionary<int, int> staredLevels = GameManager.staredLevels;

    //カメラ位置
    Camera mainCamera;
    Vector3 cameraPosition;
    float cameraSize;



    //インスペクタから指定
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject FX_CrackerUp;
    [SerializeField] GameObject FX_CrackerFall;

    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.transform.position = GameSceneController.changedCameraPosition;
        mainCamera.orthographicSize = GameSceneController.changedCameraSize;
        //scoreとアンロック変更
        if (score > 0)
        {
            unlockedLevels[level - 1] = true;
            staredLevels[level - 1] = score;
        }
        // LevelChange();

        GameObject textObject = GameObject.Find("ScoreText");
        Text scoreText = textObject.GetComponent<Text>();

        scoreText.text = score.ToString();

        textObject = GameObject.Find("LevelText");
        Text levelText = textObject.GetComponent<Text>();
        levelText.text = "Level " + level.ToString();
        // ChangeStarsColor();

        FX_CrackerUp.SetActive(false);
        FX_CrackerFall.SetActive(false);

        StartCoroutine(ChangeStarsColor());
    }

    void LevelChange()
    {
        switch (c_width)
        {
            case 6:
                switch (c_height)
                {
                    case 6:
                        cameraPosition = new Vector3(2.5f, 3.5f, -10f);
                        cameraSize = 11f;
                        break;
                    case 7:
                        cameraPosition = new Vector3(2.5f, 4f, -10f);
                        cameraSize = 10f;
                        break;
                }
                break;
            case 8:
                cameraPosition = new Vector3(3.45f, 4f, -10f);
                cameraSize = 14f;
                break;
            case 10:
                cameraPosition = new Vector3(4.4f, 5.7f, -10f);
                cameraSize = 14f;
                break;
            default:
                cameraPosition = new Vector3(4.4f, 5.7f, -10f);
                cameraSize = 14f;
                break;
        }

        mainCamera.transform.position = cameraPosition;
        mainCamera.orthographicSize = cameraSize;
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

        // GameObject star = GameObject.Find("Star");
        // image = star.GetComponent<Image>();
        // image.sprite = sprite;
    }

    public void OnNextButton()
    {
        if (score > 0)
        {
            GameSceneController.level++;
        }
        SceneManager.LoadScene("GameScene");
    }

    public void StarAnim(GameObject obj)
    {
        Animator anim = obj.GetComponent<Animator>();
        anim.SetTrigger("Animated");
    }
}
