using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    Vector3 rotatePoint = Vector3.zero;
    Vector3 rotateAxis = Vector3.zero;

    float planeSizeHalf;

    //アニメーション
    Camera mainCamera;
    int level = GameSceneController.level;
    private float step_time;
    private float move_time;
    private float positionDifference;
    private float dividedPositionDifference;
    private float sizeDifference;
    private float dividedSizeDifference;
    private float division;
    //新
    private Vector3 planePositionDif;
    private float dividedPositionDifferenceX;
    private float dividedPositionDifferenceY;
    private Vector3 planeScaleDif;
    private float dividedScaleDifferenceX;
    private float dividedScaleDifferenceY;
    private float dividedScaleDifferenceZ;
    private int functionCalled;

    //blockのクリックできるか判定
    bool CanTouchBlock;

    //移動を行える状態かのbool
    bool CanMove;

    bool MouseDown;

    Vector3 startAngle;
    Vector3 startPosition;
    Vector3 clickedPosition;

    Vector3 startCameraPosition;
    float startCameraSize;

    Material themeColor;

    // 新機能
    GameObject stopPlane;
    GameObject rotatePlane;
    GameObject[] blocks;
    public static GameObject parentObj;
    GameObject rotatePointObj;
    Vector3 blocksPos;
    Vector3 blocksScale;

    //levelCange()で使う
    int c_height = GameSceneController.c_height;
    int c_width = GameSceneController.c_width;
    void Start()
    {
        Setting();
        PlaneSetting();
        rotatePoint = rotatePointObj.transform.position;
        float blockPlaneScale = parentObj.transform.localScale.x / 10f;

        //↓初期位置を変更するので、startAngleとStartPositionより前に書く必要がある！
        transform.RotateAround(rotatePoint, new Vector3(0, 1, 0), 6f);

        startAngle = transform.eulerAngles;
        startPosition = transform.position;
    }

    void Setting()
    {
        themeColor = GameSceneController.themeColor;
        //アニメーション
        mainCamera = Camera.main;
        step_time = 0.0f;
        move_time = 0.0f;
        division = 20f;

        stopPlane = GameObject.FindWithTag("StopPlane");

        //Blockをクリックできなくする
        GameSceneController.CanTouchBlock = false;

        startCameraPosition = mainCamera.transform.position;
        startCameraSize = mainCamera.orthographicSize;
        CanMove = true;

        // rotatePoint = new Vector3(stopPlane.transform.position.x * 2 + 0.5f , transform.position.y, transform.position.z);
        rotatePoint = new Vector3((stopPlane.transform.position.x + this.gameObject.transform.position.x) / 2.0f, transform.position.y, transform.position.z);
        rotatePointObj = new GameObject("RotatePointObj");
        rotatePointObj.tag = "RotatePointObj";
        rotatePointObj.transform.position = rotatePoint;
    }

    void Update()
    {
        if (CanMove)
        {
            Dragging();
            if (Input.GetMouseButtonUp(0))
            {
                MouseUp();
            }
            if (!Input.GetMouseButton(0))
            {
                if (transform.eulerAngles.y > startAngle.y && transform.eulerAngles.y <= 180)
                {
                    transform.RotateAround(rotatePoint, new Vector3(0, 1, 0), -6f);
                }
            }
        }
        else
        {
            move_time = Time.time;

            //カメラの移動アニメーション
            // CameraMove();
            PlaneMoveBack();
        }
    }

    void PlaneSetting()
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");

        //親オブジェクト設定
        parentObj = new GameObject("ParentObject");
        parentObj.gameObject.tag = "ParentObject";
        parentObj.transform.position = Vector3.zero;
        parentObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        // 親オブジェクトに設定
        rotatePointObj.transform.SetParent(parentObj.transform, true);
        stopPlane.transform.SetParent(parentObj.transform, true);
        this.transform.SetParent(parentObj.transform, true);
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].transform.SetParent(parentObj.transform, true);
        }

        //planeの場所を決定
        parentObj.transform.position = Vector3.zero;
        parentObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    void PlaneMoveBack()
    {
        //planeがblockになる位置
        LevelChange();

        if (move_time - step_time >= 0.7f)
        {
            if (planePositionDif.x > 0)
            {
                parentObj.transform.position += new Vector3(dividedPositionDifferenceX, 0, 0);
                planePositionDif = blocksPos - parentObj.transform.position;

            }
            if (planePositionDif.x < 0)
            {
                parentObj.transform.position -= new Vector3(dividedPositionDifferenceX, 0, 0);
                planePositionDif = blocksPos - parentObj.transform.position;
            }
            if (planePositionDif.y < 0)
            {
                parentObj.transform.position += new Vector3(0, dividedPositionDifferenceY, 0);
                planePositionDif = blocksPos - parentObj.transform.position;

            }
            if (planeScaleDif.x > 0)
            {
                parentObj.transform.localScale += new Vector3(dividedScaleDifferenceX, 0, 0);
                planeScaleDif = blocksScale - parentObj.transform.localScale;
                Debug.Log("planeScaleDif" + planeScaleDif.x);
            }
            if (planeScaleDif.y > 0)
            {
                parentObj.transform.localScale += new Vector3(0, dividedScaleDifferenceY, 0);
                planeScaleDif = blocksScale - parentObj.transform.localScale;
            }
            if (planeScaleDif.z > 0)
            {
                parentObj.transform.localScale += new Vector3(0, 0, dividedScaleDifferenceZ);
                planeScaleDif = blocksScale - parentObj.transform.localScale;
            }
            else if (functionCalled > (int)division || move_time - step_time >= 7.0f)
            {
                GameSceneController.CanTouchBlock = true;
                GameSceneController.AllToColored();

                parentObj.transform.position = blocksPos;
                parentObj.transform.localScale = blocksScale;

                Destroy(stopPlane);
                Destroy(this.gameObject);
            }
            functionCalled++;
        }
    }

    // planeがブロックになる時の位置
    void LevelChange()
    {
        switch (c_width)
        {
            case 6:
                switch (c_height)
                {
                    case 6:
                        blocksPos = new Vector3(1.15f, 0.14f, 0f);
                        blocksScale = new Vector3(1.3f, 1.3f, 1f);
                        break;
                    case 7:
                        blocksPos = new Vector3(1.16f, -0.9f, 0f);
                        blocksScale = new Vector3(1.3f, 1.3f, 1f);
                        break;
                }
                break;
            case 8:
                blocksPos = new Vector3(1.5f, -1.3f, 0f);
                blocksScale = new Vector3(1.4f, 1.4f, 1f);
                break;
            case 10:
                blocksPos = new Vector3(2.0f, -1.0f, 0f);
                blocksScale = new Vector3(1.2f, 1.2f, 1f);
                break;
            default:
                blocksPos = new Vector3(2.0f, -1.0f, 0f);
                blocksScale = new Vector3(1.2f, 1.2f, 1f);
                break;
        }
        //planeの場所と目標位置の差を算出
        planePositionDif = blocksPos;
        dividedPositionDifferenceX = planePositionDif.x / division;
        dividedPositionDifferenceY = planePositionDif.y / division;
        // planeScaleDif = parentObj.transform.localScale - new Vector3(1.0f, 1.0f, 1.0f);
        planeScaleDif = blocksScale - parentObj.transform.localScale;
        dividedScaleDifferenceX = planeScaleDif.x / division;
        dividedScaleDifferenceY = planeScaleDif.y / division;
        dividedScaleDifferenceZ = planeScaleDif.z / division;

        // Debug.Log("posdif:" + planePositionDif);
        // Debug.Log("scaledif:" + planeScaleDif);
    }

    void MouseUp()
    {
        MouseDown = false;
        float speed = 0.05f;

        if (transform.eulerAngles.y >= 170 && transform.eulerAngles.y <= 330)
        {
            CanMove = false;
            step_time = Time.time;
        }
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
                transform.RotateAround(rotatePoint, new Vector3(0, 1, 0), distance);
            }
            if (Input.mousePosition.x > clickedPosition.x)
            {
                distance = (Input.mousePosition.x - clickedPosition.x) / 3;
                transform.RotateAround(rotatePoint, new Vector3(0, 1, 0), -distance);
            }
            if (transform.eulerAngles.y >= 300 && transform.eulerAngles.y <= 360)
            {
                distance = (Input.mousePosition.x - clickedPosition.x) / 3;
                transform.RotateAround(rotatePoint, new Vector3(0, 1, 0), distance);
            }
            if (transform.eulerAngles.y >= 180 && transform.eulerAngles.y <= 300)
            {
                distance = (clickedPosition.x - Input.mousePosition.x) / 3;
                transform.RotateAround(rotatePoint, new Vector3(0, 1, 0), -distance);
            }

            clickedPosition = Input.mousePosition;
        }
    }

    void CameraMove()
    {
        if (move_time - step_time >= 0.7f)
        {
            if (positionDifference > 0)
            {
                mainCamera.transform.position += new Vector3(-dividedPositionDifference, 0, 0);
                positionDifference = mainCamera.transform.position.x - startCameraPosition.x;
            }
            if (sizeDifference > 0)
            {
                mainCamera.orthographicSize += -dividedSizeDifference;
                sizeDifference = mainCamera.orthographicSize - startCameraSize;
            }
            else if (mainCamera.transform.position.x <= startCameraPosition.x && mainCamera.orthographicSize <= startCameraSize)
            {
                // Debug.Log("positionDif" + positionDifference);
                // Debug.Log("sizeDif" + sizeDifference);
                GameSceneController.CanTouchBlock = true;
                GameSceneController.AllToColored();
                stopPlane.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
