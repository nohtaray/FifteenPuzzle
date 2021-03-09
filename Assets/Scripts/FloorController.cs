using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class FloorController : MonoBehaviour
{
    private List<GameObject> blocks;
    private List<Vector3> positions;

    // Start is called before the first frame update
    void Start()
    {
        blocks = new List<GameObject>();
        positions = new List<Vector3>();

        for (int i = 0; i < transform.childCount; i++)
        {
            var childObject = transform.GetChild(i).gameObject;
            if (childObject.CompareTag("Block"))
            {
                blocks.Add(childObject);
            }
        }

        for (int i = 0; i < blocks.Count(); i++)
        {
            var block = blocks[i];
            var blockNumber = block.transform.Find("Block Number").GetComponent<TextMeshPro>();
            blockNumber.text = (i + 1).ToString();

            positions.Add(new Vector3(block.transform.localPosition.x, block.transform.localPosition.y, block.transform.localPosition.z));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDrag()
    {
        //Debug.Log("abc");
        //var mp = Input.mousePosition;
        //mp.Normalize();
        //Debug.Log("1: " + mp);
        //float x = Input.mousePosition.x;
        //float y = Input.mousePosition.y;
        //Debug.Log(string.Format("x: {0}, y: {1}", x, y));
    }

    public void MoveTo(float x, float z, ref GameObject block)
    {
        block.transform.localPosition = new Vector3(x, block.transform.localPosition.y, z);
    }
}
