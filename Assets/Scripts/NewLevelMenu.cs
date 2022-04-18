using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewLevelMenu : MonoBehaviour
{
    GameObject clickedGameObject;
    GameObject[] levelButtons;
    GameObject[] levelTargetImgs;
    //LevelChange用
    int height;
    int width;
    float c_fieldOfView;
    Vector3 additionalCPos;


    //SpawnLevelButtons()
    [SerializeField] GameObject levelButtonTemp;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject buttonSummary;
    int tTilesCounted;

    //RenderTexture用のSpawnにつかう
    private GameObject targetTiles;
    public GameObject menuStageCam;
    string tilesName;
    Vector3 tilesPos;
    Vector3 cameraPos;
    private List<RenderTexture> renderTextures = new List<RenderTexture>();
    int numberOfRT;
    int numberOfLevelTargetImgs;

    public Sprite starSprite;


    int level = NewGameSceneController.level;

    List<GameObject> stars = new List<GameObject>();

    Dictionary<int, bool> unlockedLevels = NewGameManager.unlockedLevels;
    Dictionary<int, int> staredLevels = NewGameManager.staredLevels;

    //引き継ぎ変数

    void Start()
    {
        SpawnLevelButtons();

        Setting();

        IconAppear();

        StarAppear();
    }

    void Setting()
    {
        // 初期化
        tTilesCounted = NewGameManager.tTilesCounted;
        levelTargetImgs = GameObject.FindGameObjectsWithTag("LevelTargetImage");
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        numberOfLevelTargetImgs = levelTargetImgs.Length;

        //levelButtonの数を検索することでallStageを代入
        tilesPos = new Vector3(-108.0f, -138.0f, 0f);
        cameraPos = new Vector3(-103.5f, -133.5f, -8.9f);

        //RenderTexture用にtargetTilesとcameraを生成
        for (int i = 0; i < numberOfLevelTargetImgs; i++)
        {
            //coloredplaneを一つずつ生成
            tilesName = "TargetTile-" + (i + 1).ToString();
            targetTiles = (GameObject)Resources.Load(tilesName);
            //ResourcesにTargetTilesが存在しなければ抜ける
            if (targetTiles == null)
            {
                continue;
            }
            GameObject planeInstance = Instantiate(targetTiles, tilesPos, Quaternion.identity);
            tilesPos.x += 16.0f;

            //planeの大きさに合わせてカメラの位置変更
            width = planeInstance.GetComponent<TargetTile>().width;
            height = planeInstance.GetComponent<TargetTile>().height;
            LevelChange();

            //cameraも生成
            GameObject menuStageCamInstance = Instantiate(menuStageCam, cameraPos + additionalCPos, Quaternion.identity);
            menuStageCamInstance.GetComponent<Camera>().fieldOfView = c_fieldOfView;
            menuStageCamInstance.gameObject.name = "MenuStageCam-" + (i + 1).ToString();
            cameraPos.x += 16.0f;

            //RenderTextureも生成
            var format = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf) ? RenderTextureFormat.Default : RenderTextureFormat.ARGBHalf;
            var renderTexture = new RenderTexture(256, 256, 24, format);
            menuStageCamInstance.GetComponent<Camera>().targetTexture = renderTexture;
            renderTextures.Add(renderTexture);
        }
    }

    void SpawnLevelButtons()
    {
        tTilesCounted = NewGameManager.tTilesCounted;
        for(int i = 0; i < tTilesCounted; i++)
        {
            GameObject levelButtonInst = Instantiate(levelButtonTemp);
            levelButtonInst.transform.SetParent(canvas.transform, false);
            levelButtonInst.transform.SetParent(buttonSummary.transform, false);
        }
    }

    void LevelChange()
    {
        switch (width)
        {
            case 6:
                switch (height)
                {
                    case 6:
                        additionalCPos.x = -2.0f;
                        additionalCPos.y = -2.0f;
                        c_fieldOfView = 45.0f;
                        break;
                    case 7:
                        additionalCPos.x = -2.0f;
                        additionalCPos.y = -1.0f;
                        c_fieldOfView = 45.0f;
                        break;
                }

                break;
            case 8:
                additionalCPos.x = -1.0f;
                additionalCPos.y = -1.0f;
                c_fieldOfView = 50.0f;
                break;
            case 10:
                additionalCPos = Vector3.zero;
                c_fieldOfView = 60.0f;
                break;
            default:
                additionalCPos = Vector3.zero;
                c_fieldOfView = 60.0f;
                break;
        }
    }

    void IconAppear()
    {
        //Stageごとのアイコンを表示
        //メニュー内レベルの画像の変更処理
        int n = 0;
        numberOfRT = renderTextures.Count;
        for (int i = 0; i < tTilesCounted; i++)
        {
            if (n > numberOfRT - 1)
            {
                continue;
            }

            if (unlockedLevels[i] == true)
            {
                levelTargetImgs[i].GetComponent<RawImage>().texture = renderTextures[i];
            }
            n++;
        }
    }

    void StarAppear()
    {
        //メニュー内スターの画像変更処理
        int m = 0;
        foreach (Transform childTransform in buttonSummary.transform)
        {
            // resourcesのCplanesよりもbuttonsummaryの子要素の数が多かったら抜ける
            if (m + 0 == tTilesCounted)
            {
                continue;
            }
            // まだ黄色になってないスターの数
            int remain = staredLevels[m];
            if (staredLevels[m] > 0)
            {
                foreach (Transform grandChildTransform in childTransform)
                {
                    if (grandChildTransform.gameObject.CompareTag("MenuStar"))
                    {
                        foreach (Transform grandGreatChildTransform in grandChildTransform)
                        {
                            if (remain > 0)
                            {
                                Image image = grandGreatChildTransform.gameObject.GetComponent<Image>();
                                image.sprite = starSprite;
                                remain--;
                            }
                        }
                    }
                }
            }
            m++;
        }
    }

    
}
