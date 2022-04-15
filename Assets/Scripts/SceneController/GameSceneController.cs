using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    GameObject clickedGameObject;
    //ColoredPlaneで参照
    public static GameObject blockTarget;

    //Material

    private Sprite sprite;

    static public Material themeColor;

    //クリックの処理
    private List<GameObject> listOfBlocks = new List<GameObject>();
    private GameObject lastBlock;

    //Spawnの処理と成否判定の処理
    public static int spawnWidth = 0;
    public static int spawnHeight = 0;
    private List<Vector2> fieldBlocks = new List<Vector2>();
    public static List<Vector3> answerBlocks = new List<Vector3>();
    public static int numberOfAnswerBlocks;

    public static List<GameObject> firstBlocks;

    //CameraChange用の変数
    public static Vector3 changedCameraPosition;
    public static float changedCameraSize;

    //インスペクタから指定
    [SerializeField] GameObject block;
    [SerializeField] GameObject blockPlane;
    [SerializeField] GameObject stopPlane;
    [SerializeField] GameObject rotatePlane;
    [SerializeField] GameObject attachedPlane;
    [SerializeField] GameObject rotateAttachedPlane;
    [SerializeField] GameObject wallPlane;
    [SerializeField] GameObject okButton;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject congratsText;
    [SerializeField] GameObject fromStartButton;
    [SerializeField] GameObject targetSpawner;

    Camera mainCamera;
    // ColorSwitch用
    string themeColorName;

    //引き継ぐ変数
    public static int score = 0;
    public static int level = 1;

    //ブロックが動けるか判定
    public static bool CanTouchBlock = true;

    //planeが閉じられて、blockの色変更を行ったかどうか
    bool colorChanged;

    bool PlaneOpened = AttachedPlane.PlaneOpened;

    //ColoredPlaneで参照
    public static GameObject coloredPlaneInstance;

    GameObject[] copied;

    //blockのアニメーション用
    private Animator anim;
    //planeの呼び出し用。switchで使う
    string planeName;
    public static GameObject coloredPlane;
    public static int c_height;
    public static int c_width;
    int CPlaneCounted;
    // planeName = "ColoredPlane-" + level.ToString();
    // coloredPlane = (GameObject) Resources.Load(planeName);

    void Awake()
    {
        DontDestroyManager.DestroyAll();
    }

    void Start()
    {
        //初期化
        Setting();

        //レベルごとにカラーをBlockとPlaneで変更
        ColorSwitch(level);

        LevelChange(level);
        //上限
        if (level < CPlaneCounted + 1)
        {
            Spawn();
        }
    }

    void Setting()
    {
        colorChanged = false;
        firstBlocks = new List<GameObject>();
        okButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        //全ステージ終了
        congratsText.SetActive(false);
        fromStartButton.SetActive(false);

        CanTouchBlock = false;

        // score = 0;
        GameObject textObject = GameObject.Find("LevelText");
        Text levelText = textObject.GetComponent<Text>();
        levelText.text = "Level " + level.ToString();

        CPlaneCounted = GameManager.CPlaneCounted;

        Instantiate(targetSpawner);
    }

    void Update()
    {
        //planeのアニメーションが終わってBlockを触れる状態になるのを待つ
        if (!CanTouchBlock)
        {
            return;
        }
        if (!colorChanged)
        {
            colorChanged = true;
            okButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        MouseControl();
    }

    void MouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {

            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;

                FirstBlock(clickedGameObject);
            }
        }
        if (Input.GetMouseButton(0) && listOfBlocks.Count > 0)
        {
            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;

                Dragging(clickedGameObject);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ClearList();
        }
    }

    public void LevelChange(int level)
    {

        //上限
        if (level < CPlaneCounted + 1)
        {
            rotatePlane.SetActive(true);
            stopPlane.SetActive(true);
        }
        else
        {
            rotatePlane.SetActive(false);
            stopPlane.SetActive(false);
            congratsText.SetActive(true);
            fromStartButton.SetActive(true);
            return;
        }


        //背景色の変更
        Color Blue = new Color32(131, 218, 253, 255);
        Color Yellow = new Color32(223, 225, 188, 255);
        Color Pink = new Color32(251, 198, 241, 255);
        Color Green = new Color32(178, 255, 163, 255);
        Color SkyBlue = new Color32(162, 231, 255, 255);
        Color Orange = new Color32(255, 207, 162, 255);

        mainCamera = Camera.main;

        // 背景色変更
        string backgroundColorName = "Background" + themeColorName;
        Material backgroundColor = (Material)Resources.Load(backgroundColorName);
        wallPlane.gameObject.GetComponent<Renderer>().material = backgroundColor;

        //ColoredPlaneを呼んでwidthとheightをゲット
        planeName = "ColoredPlane-" + level.ToString();
        coloredPlane = (GameObject)Resources.Load(planeName);
        c_height = coloredPlane.GetComponent<ColoredPlane>().height;
        c_width = coloredPlane.GetComponent<ColoredPlane>().width;
        spawnHeight = c_height;
        spawnWidth = c_width / 2;
        //初期ブロックの数、カメラの位置およびスケール
        switch (c_width)
        {
            case 6:
                switch (c_height)
                {
                    case 6:
                        //カメラ位置変更
                        mainCamera.transform.position = new Vector3(2.3f, 4.3f, -10f);
                        mainCamera.orthographicSize = 11f;
                        break;
                    case 7:
                        //カメラ位置変更
                        mainCamera.transform.position = new Vector3(2.4f, 4.6f, -10f);
                        mainCamera.orthographicSize = 11f;
                        break;
                }
                break;
            case 8:
                //カメラ位置変更
                mainCamera.transform.position = new Vector3(3.31f, 4.62f, -10f);
                mainCamera.orthographicSize = 13f;
                break;
            case 10:
                //カメラ位置変更
                mainCamera.transform.position = new Vector3(4.32f, 6.7f, -10f);
                mainCamera.orthographicSize = 15f;
                break;
            default:
                break;
        }
        changedCameraPosition = mainCamera.transform.position;
        changedCameraSize = mainCamera.orthographicSize;
    }

    public void Spawn()
    {
        int i = 0;

        //動かない方のplaneの位置
        Vector3 stopPlanePosition = stopPlane.transform.position;
        stopPlanePosition = new Vector3((spawnWidth - 1.0f) / 2.0f, (spawnHeight - 1.0f) / 2.0f, 0);
        stopPlane.transform.position = stopPlanePosition;

        GameObject RotatePlaneInstance = Instantiate(rotatePlane);
        //動く方のplaneの親オブジェクトの位置
        Vector3 RotatePlanePosition = RotatePlaneInstance.transform.position;
        float rotatePlaneX = spawnWidth + (spawnWidth - 1.0f) / 2.0f;
        float rotatePlaneY = (spawnHeight - 1.0f) / 2.0f;
        RotatePlanePosition = new Vector3(rotatePlaneX, rotatePlaneY, 0);
        RotatePlaneInstance.transform.position = RotatePlanePosition;

        for (int x = 0; x < spawnWidth; x++)
        {
            for (int y = 0; y < spawnHeight; y++)
            {
                GameObject piece = Instantiate(block);
                piece.transform.position = new Vector3(x, y, 0);
                firstBlocks.Add(piece);
                // piece.GetComponent<Block>().toTransparent();
                fieldBlocks.Add(new Vector2(x, y));
                SpawnPlane(x, y, stopPlane);
                SpawnPlane(spawnWidth + spawnWidth - x - 1.0f, y, RotatePlaneInstance);
                i++;
            }
        }
    }

    public GameObject SpawnPlane(float x, float y, GameObject parentPlane)
    {
        GameObject piece = Instantiate(blockPlane);
        piece.transform.position = new Vector3(x, y, 0);
        piece.transform.SetParent(parentPlane.transform, true);
        piece.gameObject.GetComponent<Renderer>().material = themeColor;
        return piece;
    }

    public GameObject SpawnAttachedPlane(GameObject allBlocks, GameObject parentPlane)
    {
        GameObject piece = Instantiate(blockPlane);
        piece.transform.position = new Vector3(allBlocks.transform.position.x, allBlocks.transform.position.y, 0);
        piece.transform.SetParent(parentPlane.transform, true);
        return piece;
    }

    public static void ScoreIs(int s)
    {
        score = s;
    }

    public static void AllToTransparent()
    {
        int numberOfFirstBlocks = firstBlocks.Count;
        for (int i = 0; i < numberOfFirstBlocks; i++)
        {
            firstBlocks[i].GetComponent<Block>().toTransparent();
        }
    }

    public static void AllToColored()
    {
        int numberOfFirstBlocks = firstBlocks.Count;
        for (int i = 0; i < numberOfFirstBlocks; i++)
        {
            firstBlocks[i].GetComponent<Block>().toColored();
        }

    }

    public void OnOKButton()
    {
        //okbutton非アクティブ化
        okButton.gameObject.SetActive(false);
        //target非アクティブ化
        blockTarget = TargetSpawner.blockTarget;

        GameObject[] attachedBlocks = GameObject.FindGameObjectsWithTag("Block");
        GameObject parentObj = GameObject.FindWithTag("ParentObject");
        // 元々のpositionを保存しておいて後で代入
        Vector3 pastPosition = parentObj.transform.position;
        Vector3 pastScale = parentObj.transform.localScale;
        int numberOfCorrectBlocks = 0;
        int numberOfAllBlocks = 0;

        CanTouchBlock = false;

        //Height 6, Width 3
        //動かない方のplaneの親オブジェクトの位置
        GameObject atPlaneInstance = Instantiate(attachedPlane);
        atPlaneInstance.transform.position = Vector3.zero;

        //動く方のplaneの親オブジェクトの位置
        GameObject ratPlaneInstance = Instantiate(rotateAttachedPlane);
        ratPlaneInstance.transform.position = Vector3.zero;

        atPlaneInstance.transform.position = new Vector3((spawnWidth - 1.0f) / 2.0f, (spawnHeight - 1.0f) / 2.0f, 0);
        ratPlaneInstance.transform.position = new Vector3(spawnWidth + (spawnWidth - 1.0f) / 2.0f, (spawnHeight - 1.0f) / 2.0f, 0);

        for (int i = 0; i < attachedBlocks.Length; i++)
        {
            if (!attachedBlocks[i].GetComponent<Block>().isTransparent())
            {
                //allblockを複製し、positionを反転
                GameObject piece = Instantiate(blockPlane);
                piece.transform.position = new Vector3(attachedBlocks[i].transform.position.x, attachedBlocks[i].transform.position.y, 0);
                piece.gameObject.GetComponent<Renderer>().material = themeColor;
                piece.tag = "CountablePlane";
                piece.transform.SetParent(parentObj.transform, true);
            }
            Destroy(attachedBlocks[i]);
        }

        //
        parentObj.transform.position = Vector3.zero;
        parentObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);


        atPlaneInstance.transform.SetParent(parentObj.transform, true);
        ratPlaneInstance.transform.SetParent(parentObj.transform, true);

        GameObject[] caPlanes = GameObject.FindGameObjectsWithTag("CountablePlane");

        // 右側のPlaneの生成等
        for (int i = 0; i < caPlanes.Length; i++)
        {
            caPlanes[i].transform.SetParent(atPlaneInstance.transform, true);
            // setparentを色々いじったことでlocalScaleがおかしくなったのを修正
            caPlanes[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            GameObject CountableRight = SpawnPlane(spawnWidth + spawnWidth - caPlanes[i].transform.position.x - 1.0f, caPlanes[i].transform.position.y, ratPlaneInstance);
            CountableRight.tag = "CountablePlane";
        }

        parentObj.transform.position = pastPosition;
        parentObj.transform.localScale = pastScale;

        mainCamera = Camera.main;
    }


    void FirstBlock(GameObject clickedGameObject)
    {
        if (clickedGameObject.gameObject.CompareTag("Block"))
        {
            bool isCopiedBlock = clickedGameObject.GetComponent<Block>().isCopiedBlock();

            var thisBlock = clickedGameObject;
            listOfBlocks.Add(thisBlock);

            // blockを消したりアニメーションしたりする関数
            BlockClicked();
            lastBlock = thisBlock;
        }
    }

    void Dragging(GameObject clickedGameObject)
    {
        if (clickedGameObject.gameObject.CompareTag("Block"))
        {
            bool isCopiedBlock = clickedGameObject.GetComponent<Block>().isCopiedBlock();


            var thisBlock = clickedGameObject;

            // 
            if (!listOfBlocks.Contains(thisBlock))
            {
                listOfBlocks.Add(thisBlock);
                listOfBlocks.Add(thisBlock);

                // blockを消したりアニメーションしたりする関数
                BlockClicked();

                lastBlock = thisBlock;
            }
            // }
        }
    }

    void BlockClicked()
    {
        if (clickedGameObject.GetComponent<Block>().isTransparent())
        {
            clickedGameObject.transform.localScale = new Vector3(0.01f, 0.01f, 1f);
            clickedGameObject.GetComponent<Block>().Animated();
            clickedGameObject.GetComponent<Block>().toColored();
        }
        else
        {
            GameObject blockInstance = Instantiate(clickedGameObject);
            blockInstance.transform.position = clickedGameObject.transform.position;

            Destroy(blockInstance.gameObject.GetComponent<BoxCollider2D>());
            //Instanceの初期値を書き換え
            blockInstance.GetComponent<Block>().toColored();
            //instanceでanimation実行
            blockInstance.GetComponent<Block>().Animated();
            clickedGameObject.GetComponent<Block>().toTransparent();
        }
    }

    void ClearList()
    {
        if (listOfBlocks.Count > 0)
        {
            listOfBlocks.Clear();

        }
    }

    public void ColorSwitch(int level)
    {
        themeColorName = "";

        switch (level)
        {
            case 1:
            case 10:
                themeColorName += "Yellow";
                break;
            case 2:
            case 6:
                themeColorName += "Pink";
                break;
            case 3:
            case 5:
                themeColorName += "Blue";
                break;
            case 4:
                themeColorName += "Green";
                break;
            case 7:
                themeColorName += "SkyBlue";
                break;
            case 8:
            case 9:
                themeColorName += "Orange";
                break;
            default:
                themeColorName += "Yellow";
                break;
        }
        themeColor = (Material)Resources.Load("Block" + themeColorName);
    }
}


