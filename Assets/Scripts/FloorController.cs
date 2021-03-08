using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class FloorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var childObject = transform.GetChild(i).gameObject;
            if (childObject.CompareTag("Block"))
            {
                var block = childObject;
                var blockNumber = block.transform.Find("Block Number").GetComponent<TextMeshPro>();
                blockNumber.text = (i + 1).ToString();
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
