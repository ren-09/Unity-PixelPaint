using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnDeleteButton()
    {
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
        List<Vector3> coloredBlocks = new List<Vector3>();
        List<GameObject> BlocksName = new List<GameObject>();

        int numberOfAllBlocks = 0;

        for(int i = 0; i < allBlocks.Length; i++)
        {
            if(allBlocks[i].GetComponent<Block>().isTransparent())
            {
                Destroy(allBlocks[i]);
                // Debug.Log(coloredBlocks[i]);
                numberOfAllBlocks++;
            }
        }
    }
}
