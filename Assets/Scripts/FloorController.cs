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
            var num = (i + 1) % (N * N);

            var block = blocks[i];
            var blockNumber = block.transform.Find("Block Number").GetComponent<TextMeshPro>();
            blockNumber.text = num.ToString();

            positions.Add(new Vector3(block.transform.localPosition.x, block.transform.localPosition.y, block.transform.localPosition.z));

            block.GetComponent<BlockController>().blockIndex = i;

            if (num == 0)
            {
                var renderer = block.GetComponent<Renderer>();
                foreach (var material in renderer.materials)
                {
                    // 背景と同じ色にしておく
                    material.color = new Color(255, 255, 255);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 HWToVector(int h, int w)
    {
        var x = w * blockSize + blockSize / 2 - floorSize / 2;
        var z = h * blockSize + blockSize / 2 - floorSize / 2;
        z *= -1;
        var y = .2f;
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

    public bool AttemptToMove(int fromH, int fromW, int toH, int toW)
    {
        string s = "";
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                s += board[i][j].ToString() + ",";
            }
            s += "\n";
        }

        if (!ok(fromH) || !ok(fromW) || !ok(toH) || !ok(toW)) return false;
        int absH = Mathf.Abs(fromH - toH);
        int absW = Mathf.Abs(fromW - toW);
        if (absH == 1 && absW == 0 || absH == 0 && absW == 1)
        {
            if (board[fromH][fromW] == 0 || board[toH][toW] == 0)
            {
                var fromIndex = fromH * N + fromW;
                var toIndex = toH * N + toW;
                (board[fromH][fromW], board[toH][toW]) = (board[toH][toW], board[fromH][fromW]);
                (blocks[fromIndex].transform.position, blocks[toIndex].transform.position) = (blocks[toIndex].transform.position, blocks[fromIndex].transform.position);
                (blocks[fromIndex], blocks[toIndex]) = (blocks[toIndex], blocks[fromIndex]);
                return true;
            }
        }
        return false;
    }

    private bool ok(int n)
    {
        return 0 <= n && n < N;
    }
}
