using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;

public class FloorController : MonoBehaviour
{
    public GameObject WinText;

    private const int N = 4;
    private const float floorSize = 10.0f;
    private const float blockSize = floorSize / N;
    private const int shuffleCount = 1;

    private List<List<int>> board;

    private List<GameObject> blocks;
    private List<Vector3> positions;

    // Start is called before the first frame update
    void Start()
    {
        WinText.SetActive(false);

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
            blockNumber.text = num > 0 ? num.ToString() : "";

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


        shuffle(shuffleCount);
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
                swap(fromH, fromW, toH, toW);
                return true;
            }
        }
        return false;
    }

    private bool ok(int n)
    {
        return 0 <= n && n < N;
    }

    private int swapCount = 0;
    private void swap(int fromH, int fromW, int toH, int toW)
    {
        swapCount += 1;
        Debug.Log(string.Format("swap {4}: ({0}, {1}) -> ({2}, {3})", fromH, fromW, toH, toW, swapCount));
        var fromIndex = fromH * N + fromW;
        var toIndex = toH * N + toW;
        (board[fromH][fromW], board[toH][toW]) = (board[toH][toW], board[fromH][fromW]);
        (blocks[fromIndex].transform.position, blocks[toIndex].transform.position) = (blocks[toIndex].transform.position, blocks[fromIndex].transform.position);
        (blocks[fromIndex], blocks[toIndex]) = (blocks[toIndex], blocks[fromIndex]);
    }

    private void shuffle(int count)
    {
        var rand = new System.Random();

        int h = -1;
        int w = -1;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (board[i][j] == 0)
                {
                    h = i;
                    w = j;
                }
            }
        }
        if (h == -1 || w == -1)
        {
            throw new Exception("空白がありません");
        }

        for (int i = 0; i < count; i++)
        {
            var nextH = rand.Next(0, N);
            var nextW = rand.Next(0, N);
            // 空白を (nh, nw) に移動させる

            var diffH = Mathf.Abs(h - nextH);
            var diffW = Mathf.Abs(w - nextW);
            while (diffH > 0 || diffW > 0)
            {
                var ph = h;
                var pw = w;
                if (rand.Next(0, diffH + diffW) < diffH)
                {
                    if (nextH > h) h++; else h--;
                }
                else
                {
                    if (nextW > w) w++; else w--;
                }
                swap(ph, pw, h, w);
                diffH = Mathf.Abs(h - nextH);
                diffW = Mathf.Abs(w - nextW);
            }
        }
    }

    public bool DisplayTextIfWin()
    {
        var win = isWin();
        if (win)
        {
            WinText.SetActive(true);
        }
        return win;
    }
    private bool isWin()
    {
        int n = 0;
        bool ok = true;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                ok &= board[i][j] == (++n) % (N * N);
            }
        }
        return ok;
    }
}
