using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class BlockController : MonoBehaviour
{
    public int blockIndex = -1;

    private bool isDragging = false;
    private TextMeshPro blockNumber;

    /// <summary>
    /// localPosition = position / scaleRatio
    /// position = localPosition * scaleRatio
    /// </summary>
    private float scaleRatio;


    // Start is called before the first frame update
    void Start()
    {
        blockNumber = transform.Find("Block Number").GetComponent<TextMeshPro>();

        var num = Vector3.Magnitude(transform.position);
        var den = Vector3.Magnitude(transform.localPosition);
        scaleRatio = den != 0 ? num / den : 1;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        isDragging = true;
        transform.position = new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z);

        var floor = transform.parent.gameObject.GetComponent<FloorController>();
        var h = blockIndex / 4;
        var w = blockIndex % 4;
        Debug.Log(floor.HWToVector(h, w));
    }

    private void OnMouseUp()
    {
        isDragging = false;
        transform.position = new Vector3(transform.position.x, transform.position.y - .1f, transform.position.z);
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePosition = hit.point;
            var floor = transform.parent.gameObject.GetComponent<FloorController>();

            var fromH = floor.VectorToH(transform.localPosition);
            var fromW = floor.VectorToW(transform.localPosition);
            var toH = floor.VectorToH(mousePosition / scaleRatio);
            var toW = floor.VectorToW(mousePosition / scaleRatio);

            Debug.Log(string.Format("AttemptToMove: {0}, {1}, {2}, {3}", fromH, fromW, toH, toW));
            var res = floor.AttemptToMove(fromH, fromW, toH, toW);
            if (res)
            {
                floor.DisplayTextIfWin();
            }
        }
    }
}
