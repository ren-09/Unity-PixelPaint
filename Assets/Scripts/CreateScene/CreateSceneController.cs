using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateSceneController : MonoBehaviour
{
    GameObject clickedGameObject;
    public GameObject block;

    //Material
    public Material Transparent;

    //クリックの処理
    private List<GameObject> listOfBlocks = new List<GameObject>();
    private GameObject lastBlock;

    //Spawnの処理と成否判定の処理

    public int spawnHeight;
    public int spawnWidth;
    private List<Vector2> fieldBlocks = new List<Vector2>();
    private List<Vector3> answerBlocks = new List<Vector3>();
    private int numberOfAnswerBlocks;

    //インスペクタから指定
    [SerializeField] GameObject attachedPlane;
    [SerializeField] GameObject blockPlane;

    public Material ColoredBlock;

    //引き継ぐ変数
    public static int score = 0;
    public static int level = 5;


    void Awake()
    {

    }


    void Start()
    {
        Spawn();
        score = 0;

    }

    void Update()
    {
        // if(!stopPlane.activeSelf)
        // {
        //     CanTouchBlock = true;
        // }

        if (Input.GetMouseButtonDown(0))
        {

            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
                Debug.Log("clicled:" + clickedGameObject.name);
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

    void Spawn()
    {
        int i = 0;
        for (int x = 0; x < spawnWidth; x++)
        {
            for (int y = 0; y < spawnHeight; y++)
            {
                GameObject piece = Instantiate(block);
                piece.transform.position = new Vector3(x, y, 0);
                fieldBlocks.Add(new Vector2(x, y));
                i++;
            }
        }
    }

    public void OnDeleteButton()
    {
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
        List<Vector3> coloredBlocks = new List<Vector3>();
        List<GameObject> BlocksName = new List<GameObject>();
        int numberOfAllBlocks = 0;

        for (int i = 0; i < allBlocks.Length; i++)
        {
            if (allBlocks[i].GetComponent<GrayBlock>().isTransparent())
            {
                Destroy(allBlocks[i]);
                // Debug.Log(coloredBlocks[i]);
                numberOfAllBlocks++;
            }
        }
    }

    public GameObject SpawnPlane(float x, float y, GameObject parentPlane)
    {
        GameObject piece = Instantiate(blockPlane);
        piece.transform.position = new Vector3(x, y, 0);
        // piece.transform.SetParent(parentPlane.transform, true);
        piece.gameObject.GetComponent<Renderer>().material = ColoredBlock;
        return piece;
    }

    public void OnBlockToPlane()
    {
        GameObject[] attachedBlocks = GameObject.FindGameObjectsWithTag("Block");


        // Vector3 attachedPlanePosition = attachedPlane.transform.position;
        // attachedPlanePosition = new Vector3((spawnWidth - 1) / 2, (spawnHeight - 1) / 2, 0);
        // attachedPlane.transform.position = attachedPlanePosition;
        GameObject parentPlaneInst = Instantiate(attachedPlane);
        parentPlaneInst.transform.position = Vector3.zero;
        parentPlaneInst.AddComponent<ColoredPlane>();
        parentPlaneInst.GetComponent<ColoredPlane>().height = spawnHeight;
        parentPlaneInst.GetComponent<ColoredPlane>().width = spawnWidth;
        parentPlaneInst.GetComponent<ColoredPlane>().themeColor = ColoredBlock;

        for (int i = 0; i < attachedBlocks.Length; i++)
        {
            if (!attachedBlocks[i].GetComponent<GrayBlock>().isTransparent())
            {
                //allblockを複製し、positionを反転
                GameObject planeInstace = SpawnPlane(attachedBlocks[i].transform.position.x, attachedBlocks[i].transform.position.y, attachedPlane);
                planeInstace.transform.SetParent(parentPlaneInst.transform, true);
                parentPlaneInst.gameObject.name = "ColoredPlane-";

                // Vector3 copyPosition = copy.transform.position;
                // copyPosition.x = spawnWidth + spawnWidth - copy.transform.position.x - 1.0f;
                // copy.transform.position = copyPosition;
            }
        }

    }

    public void OnColorChangeButton()
    {
        
    }

    void FirstBlock(GameObject clickedGameObject)
    {
        if (clickedGameObject.gameObject.CompareTag("Block"))
        {
            var thisBlock = clickedGameObject;
            listOfBlocks.Add(thisBlock);
            // clickedGameObject.GetComponent<Block>().ColorSwitch();
            if (clickedGameObject.GetComponent<GrayBlock>().isTransparent())
            {
                clickedGameObject.GetComponent<GrayBlock>().toColored();
            }
            else
            {
                clickedGameObject.GetComponent<GrayBlock>().toTransparent();
            }
            lastBlock = thisBlock;
            // Debug.Log("BlockClicked");
        }
    }

    void Dragging(GameObject clickedGameObject)
    {
        if (clickedGameObject.gameObject.CompareTag("Block"))
        {
            var thisBlock = clickedGameObject;
            // Vector2 distance = thisBlock.transform.position - lastBlock.transform.position;

            //&& distance.magnitude <= 1.1f
            if (!listOfBlocks.Contains(thisBlock))
            {
                listOfBlocks.Add(thisBlock);
                listOfBlocks.Add(thisBlock);
                // clickedGameObject.GetComponent<Block>().ColorSwitch();
                if (clickedGameObject.GetComponent<GrayBlock>().isTransparent())
                {
                    clickedGameObject.GetComponent<GrayBlock>().toColored();
                }
                else
                {
                    clickedGameObject.GetComponent<GrayBlock>().toTransparent();
                }
                lastBlock = thisBlock;
            }
        }
    }

    void ClearList()
    {
        if (listOfBlocks.Count > 0)
        {
            listOfBlocks.Clear();
        }
    }
}
