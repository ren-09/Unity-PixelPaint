using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AttachedPlane : MonoBehaviour
{

    Vector3 rotatePoint = Vector3.zero;

    //アニメーション
    Camera mainCamera;
    int level = GameSceneController.level;
    private float step_time;
    private float move_time;
    private float division;
    int ok;

    //blockのクリックできるか判定
    bool CanTouchBlock = GameSceneController.CanTouchBlock;
    bool MouseDown;
    //Planeを開ける状態にあるか
    public static bool CanMove;
    //Planeを開くのを開始して良いか
    bool CanStart;
    //Cameraが移動中
    bool CameraMoving;
    //
    public static bool PlaneOpened;

    Vector3 startAngle;
    Vector3 startPosition;
    Vector3 clickedPosition;


    Material ColoredBlock;

    private GameObject attachedPlane;


    //ColoredPlaneInstance用
    GameObject blockTarget;
    public static GameObject coloredPlaneInstance;
    Material themeColor;

    //OnLoad用
    bool ResultSceneLoaded = false;

    //新
    private Vector3 planePositionDif;
    private float dividedPositionDifferenceX;
    private float dividedPositionDifferenceY;
    private Vector3 planeScaleDif;
    private float dividedScaleDifferenceX;
    private float dividedScaleDifferenceY;
    private float dividedScaleDifferenceZ;
    private int functionCalled;
    // 新機能
    GameObject[] blocks;
    GameObject parentObj;
    GameObject rotatePointObj;

    // 修正
    Vector3 targetPosition;
    Vector3 targetScale;

    //答え合わせ用
    public static int score = 0;
    private List<Vector3> answerBlocks = new List<Vector3>();
    private int numberOfAnswerBlocks;

    public GameObject planeBlock;

    //
    int c_height = GameSceneController.c_height;
    int c_width = GameSceneController.c_width;
    void Awake()
    {
        // DontDestroyManager.DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        Setting();
        PlaneSetting();

        //↓初期位置を変更するので、startAngleとStartPositionより前に書く必要がある！
        transform.RotateAround(rotatePointObj.transform.position, new Vector3(0, 1, 0), 180f);

        //初期位置保存
        startAngle = transform.eulerAngles;
        startPosition = transform.position;
    }

    public void Setting()
    {
        PlaneOpened = false;
        //アニメーション
        mainCamera = Camera.main;
        step_time = Time.time;
        move_time = 0.0f;
        division = 20.00f;
        ok = 0;

        attachedPlane = GameObject.FindWithTag("AttachedPlane");
        // this.gameObject.GetComponent<Renderer>().material = ColoredBlock;
        // attachedPlane.gameObject.GetComponent<Renderer>().material = ColoredBlock;

        CameraMoving = true;

        //Blockを触れない状態に
        GameSceneController.CanTouchBlock = false;


        MouseDown = false;

        //blockをクリックできなくする
        CanTouchBlock = false;

        //移動を行える状態かのbool
        CanMove = false;

        CanStart = false;

        rotatePointObj = GameObject.FindWithTag("RotatePointObj");
        rotatePoint = rotatePointObj.transform.position;
        // DontDestroyManager.DontDestroyOnLoad(rotatePointObj);

        parentObj = GameObject.FindWithTag("ParentObject");

    }

    void Update()
    {
        // CameraMove();
        move_time = Time.time;
        PlaneMove();


        if (CanMove)
        {
            Dragging();
            if (Input.GetMouseButtonUp(0))
            {
                MouseDown = false;
            }
            if (!Input.GetMouseButton(0))
            {
                //位置がおかしいときは初期位置に戻す
                if (transform.eulerAngles.y < startAngle.y && transform.eulerAngles.y >= 30)
                {
                    transform.RotateAround(rotatePointObj.transform.position, new Vector3(0, 1, 0), 6f);
                }
                if (transform.eulerAngles.y < 30 || transform.eulerAngles.y >= 200 && transform.eulerAngles.y <= 180)
                {
                    transform.RotateAround(rotatePointObj.transform.position, new Vector3(0, 1, 0), -6f);
                    Debug.Log("transform.eulerAngles.y" + transform.eulerAngles.y);
                }
                //Planeが開き終わった状態
                if (transform.eulerAngles.y <= 0 || transform.eulerAngles.y >= 200)
                {
                    float a = 0 - transform.eulerAngles.y;
                    transform.RotateAround(rotatePointObj.transform.position, new Vector3(0, 1, 0), a);
                    CanMove = false;
                    PlaneOpened = true;
                    // Debug.Log("cantmove");


                    //blockTarget代入
                    blockTarget = TargetSpawner.blockTarget;

                    //ColoredPlaneのインスタンス化及び位置・スケールの変更
                    string coloredPlaneName = "ColoredPlane-" + level.ToString();
                    GameObject ColoredPlane = Resources.Load<GameObject>(coloredPlaneName);
                    coloredPlaneInstance = Instantiate(ColoredPlane);
                    Vector3 coloredPlanePosition = coloredPlaneInstance.transform.position;
                    coloredPlanePosition = blockTarget.transform.position;
                    themeColor = GameSceneController.themeColor;
                    foreach (Transform childTransform in coloredPlaneInstance.gameObject.transform)
                    {
                        childTransform.gameObject.GetComponent<Renderer>().material = themeColor;
                    }
                    coloredPlaneInstance.transform.position = coloredPlanePosition;
                    coloredPlaneInstance.transform.localScale = blockTarget.transform.localScale;
                    coloredPlaneInstance.AddComponent<ColoredPlaneAnim>();

                    //blockTargetの非アクティブ化
                    blockTarget.SetActive(false);
                }
            }
        }

        if (PlaneOpened && !ResultSceneLoaded)
        {
            CreateAnswer();
            CheckAnswer();
            DontDestroyManager.DontDestroyOnLoad(parentObj);
            Invoke("OnLoadResultScene", 3f);
            ResultSceneLoaded = true;
        }
    }



    void PlaneSetting()
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");

        // レベル別位置の設定
        switch (level)
        {
            case 10:
                targetPosition = new Vector3(-1.3f, 0.54f, 0f);
                targetScale = new Vector3(0.8f, 0.8f, 1f);
                break;
            case 8:
                targetPosition = new Vector3(-0.9f, 0.9f, 0f);
                targetScale = new Vector3(0.7f, 0.7f, 1f);
                break;
            case 6:
                switch (c_height)
                {
                    case 6:
                        targetPosition = new Vector3(-0.8f, 1f, 0f);
                        targetScale = new Vector3(0.7f, 0.7f, 1f);
                        break;
                    case 7:
                        targetPosition = new Vector3(-0.9f, 0.9f, 0f);
                        targetScale = new Vector3(0.8f, 0.8f, 1f);
                        break;
                }
                break;
            default:
                targetPosition = new Vector3(-1.3f, 0.54f, 0f);
                targetScale = new Vector3(0.8f, 0.8f, 1f);
                break;
                break;
        }
        targetPosition = Vector3.zero;
        targetScale = new Vector3(1.0f, 1.0f, 1.0f);

        //新
        planePositionDif = parentObj.transform.position;
        dividedPositionDifferenceX = planePositionDif.x / division;
        dividedPositionDifferenceY = planePositionDif.y / division;
        planeScaleDif = parentObj.transform.localScale - targetScale;
        dividedScaleDifferenceX = planeScaleDif.x / division;
        dividedScaleDifferenceY = planeScaleDif.y / division;
        dividedScaleDifferenceZ = planeScaleDif.z / division;
    }

    void CreateAnswer()
    {
        foreach (Transform childTransform in coloredPlaneInstance.transform)
        {
            answerBlocks.Add(childTransform.transform.localPosition);
        }

        numberOfAnswerBlocks = answerBlocks.Count;
        score = 0;
    }

    void CheckAnswer()
    {
        int numberOfCorrectBlocks = 0;
        int numberOfAllBlocks = 0;

        //成否判定
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("CountablePlane");


        for (int i = 0; i < allBlocks.Length; i++)
        {
            int s = 0;
            for (int n = 0; n < answerBlocks.Count; n++)
            {
                if (Mathf.Round(answerBlocks[n].x) == Mathf.Round(allBlocks[i].transform.position.x) && Mathf.Round(answerBlocks[n].y) == Mathf.Round(allBlocks[i].transform.position.y))
                {
                    GameObject correctInstance = Instantiate(planeBlock, allBlocks[i].transform.position, Quaternion.identity);
                    DontDestroyManager.DontDestroyOnLoad(correctInstance);
                    numberOfCorrectBlocks++;
                    s += 1;
                }
            }
            if (s == 0)
            {
                Debug.Log("はずれの座標：" + allBlocks[i].transform.position);
            }
        }
        for (int n = 0; n < answerBlocks.Count; n++)
        {
            Debug.Log(answerBlocks[n]);
        }

        numberOfAllBlocks = allBlocks.Length;

        Debug.Log("numberOfCorrectBlocks：" + numberOfCorrectBlocks);
        Debug.Log("numberOfAllBlocks：" + numberOfAllBlocks);
        Debug.Log("numberOfAnswerBlocks：" + numberOfAnswerBlocks);


        Debug.Log("正解:" + numberOfCorrectBlocks);

        if (numberOfAnswerBlocks == numberOfCorrectBlocks && numberOfAnswerBlocks == numberOfAllBlocks)
        {
            score = 3;
            Debug.Log("score:3");
        }
        else if (numberOfAnswerBlocks * 0.25 > numberOfCorrectBlocks && numberOfAnswerBlocks >= numberOfAllBlocks)
        {
            score = 1;
            Debug.Log("score:1の1");
        }
        else if (numberOfAnswerBlocks == numberOfCorrectBlocks && numberOfAnswerBlocks * 1.25 >= numberOfAllBlocks)
        {
            score = 1;
            Debug.Log("score:1の2");
        }
        else
        {
            score = 0;
            Debug.Log("score:1の2");
        }

        GameSceneController.ScoreIs(score);

        Debug.Log("score:" + GameSceneController.score);
    }

    void PlaneMove()
    {
        if (!CanStart)
        {
            if (planePositionDif.x > 0)
            {
                parentObj.transform.position -= new Vector3(dividedPositionDifferenceX, 0, 0);
                planePositionDif = parentObj.transform.position;
            }
            if (planePositionDif.x < 0)
            {
                parentObj.transform.position -= new Vector3(dividedPositionDifferenceX, 0, 0);
                planePositionDif = parentObj.transform.position;
            }
            if (planePositionDif.y < 0)
            {
                parentObj.transform.position -= new Vector3(0, dividedPositionDifferenceY, 0);
                planePositionDif = parentObj.transform.position;

            }
            if (planeScaleDif.x > 0)
            {
                parentObj.transform.localScale -= new Vector3(dividedScaleDifferenceX, 0, 0);
                planeScaleDif = parentObj.transform.localScale - targetScale;
                Debug.Log("planeScaleDif" + planeScaleDif.x);
            }
            if (planeScaleDif.y > 0)
            {
                parentObj.transform.localScale -= new Vector3(0, dividedScaleDifferenceY, 0);
                planeScaleDif = parentObj.transform.localScale - targetScale;
            }
            if (planeScaleDif.z > 0)
            {
                parentObj.transform.localScale += new Vector3(0, 0, dividedScaleDifferenceZ);
                planeScaleDif = parentObj.transform.localScale - targetScale;
            }
            if (functionCalled > (int)division || move_time - step_time >= 7.0f)
            {
                parentObj.transform.position = targetPosition;
                parentObj.transform.localScale = targetScale;

                CanMove = true;
                CanStart = true;
            }
            functionCalled++;
        }
    }

    void OnLoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    void MouseUp()
    {
        MouseDown = false;
    }

    void Dragging()
    {
        float distance;
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown = true;
            clickedPosition = Input.mousePosition;

            Debug.Log("clicked");
        }
        if (MouseDown)
        {
            if (Input.mousePosition.x < clickedPosition.x)
            {
                distance = (clickedPosition.x - Input.mousePosition.x) / 3;
                transform.RotateAround(rotatePointObj.transform.position, new Vector3(0, 1, 0), distance);
            }
            if (Input.mousePosition.x > clickedPosition.x)
            {
                distance = (Input.mousePosition.x - clickedPosition.x) / 3;
                transform.RotateAround(rotatePointObj.transform.position, new Vector3(0, 1, 0), -distance);
            }
            if (transform.eulerAngles.y >= 300 && transform.eulerAngles.y <= 360)
            {
                distance = (Input.mousePosition.x - clickedPosition.x) / 3;
                transform.RotateAround(rotatePointObj.transform.position, new Vector3(0, 1, 0), distance);
            }
            if (transform.eulerAngles.y >= 180 && transform.eulerAngles.y <= 300)
            {
                distance = (clickedPosition.x - Input.mousePosition.x) / 3;
                transform.RotateAround(rotatePointObj.transform.position, new Vector3(0, 1, 0), -distance);
            }

            clickedPosition = Input.mousePosition;
        }
    }
}
