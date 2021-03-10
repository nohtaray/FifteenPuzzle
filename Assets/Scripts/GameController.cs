using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {



        //blocks = new List<GameObject>();
        //positions = new List<Vector3>();

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    var childObject = transform.GetChild(i).gameObject;
        //    if (childObject.CompareTag("Block"))
        //    {
        //        blocks.Add(childObject);
        //    }
        //}

        //for (int i = 0; i < blocks.Count(); i++)
        //{
        //    var num = (i + 1) % (N * N);

        //    var block = blocks[i];
        //    var blockNumber = block.transform.Find("Block Number").GetComponent<TextMeshPro>();
        //    blockNumber.text = num > 0 ? num.ToString() : "";

        //    positions.Add(new Vector3(block.transform.localPosition.x, block.transform.localPosition.y, block.transform.localPosition.z));

        //    block.GetComponent<BlockController>().blockIndex = i;

        //    if (num == 0)
        //    {
        //        var renderer = block.GetComponent<Renderer>();
        //        foreach (var material in renderer.materials)
        //        {
        //            // 背景と同じ色にしておく
        //            material.color = new Color(255, 255, 255);
        //        }
        //    }
        //}

    }

    // Update is called once per frame
    void Update()
    {

    }
}
