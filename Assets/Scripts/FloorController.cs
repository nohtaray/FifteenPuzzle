using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class FloorController : MonoBehaviour
{
    private const int N = 4;
    private const float floorSize = 10.0f;
    private const float blockSize = floorSize / N;
    private List<List<int>> board;

    private List<GameObject> blocks;
    private List<Vector3> positions;

    // Start is called before the first frame update
    void Start()
    {
        board = new List<List<int>>();
        int n = 0;
        for (int h = 0; h < N; h++)
        {
            var row = new List<int>();
            for (int w = 0; w < N; w++)
            {
                row.Add(++n % (N * N));
            }
            board.Add(row);
        }

        //transform.gameObject.GetComponent<Transform>().localScale

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

            block.GetComponent<BlockController>().blockIndex = i;
        }

        Debug.Log(transform.position);
        Debug.Log(transform.right);
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

    public Vector3 HWToVector(int h, int w)
    {
        var x = w * blockSize + blockSize / 2 - floorSize / 2;
        var z = h * blockSize + blockSize / 2 - floorSize / 2;
        z *= -1;
        var y = .2f;
        Debug.Log(string.Format("h={0}, w={1}, x={2}, z={3}", h, w, x, z));
        return new Vector3(x, y, z);
    }

    public int VectorToW(Vector3 p)
    {
        var ret = (int)((p.x + floorSize / 2) / blockSize);
        return Mathf.Min(N - 1, Mathf.Max(0, ret));
    }
    public int VectorToH(Vector3 p)
    {
        var ret = (int)((-p.z + floorSize / 2) / blockSize);
        return Mathf.Min(N - 1, Mathf.Max(0, ret));
    }
}
