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
        //var mp = Input.mousePosition;
        //float x2 = mp.x;
        //float y2 = mp.y;
        //float z2 = mp.z;
        //Debug.Log(string.Format("x2: {0}, y2: {1}, z2: {2}", x2, y2, z2));

        //var mp2 = transform.localPosition;
        //Debug.Log(string.Format("pos: x={0}, y={1}, z={2}", mp2.x, mp2.y, mp2.z));


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePosition = hit.point;
            var floor = transform.parent.gameObject.GetComponent<FloorController>();

            var vec = floor.HWToVector(
                floor.VectorToH(mousePosition / scaleRatio),
                floor.VectorToW(mousePosition / scaleRatio));

            Debug.Log(transform.localPosition);
            //transform.localPosition = vec;

            //var h = floor.VectorToH(transform.localPosition);
            //var w = floor.VectorToW(transform.localPosition);
            //var x = transform.localPosition.x;
            //var z = transform.localPosition.z;
            ////Debug.Log(string.Format("h={0}, w={1}, x={2}, z={3}", h, w, x, z));


            var fromH = floor.VectorToH(transform.localPosition);
            var fromW = floor.VectorToW(transform.localPosition);
            var toH = floor.VectorToH(mousePosition / scaleRatio);
            var toW = floor.VectorToW(mousePosition / scaleRatio);
            Debug.Log(string.Format("AttemptToMove: {0}, {1}, {2}, {3}", fromH, fromW, toH, toW));
            if (floor.AttemptToMove(fromH, fromW, toH, toW))
            {
                //Debug.Log(string.Format("AttemptToMove: {0}, {1}, {2}, {3}", fromH, fromW, toH, toW));
            };
        }
    }
}
