using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlockController : MonoBehaviour
{
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
        // transform.position = new Vector3(transform.position.x, transform.position.y * 5, transform.position.z);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        // transform.position = new Vector3(transform.position.x, transform.position.y / 5, transform.position.z);
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


        var floor = transform.parent.gameObject.GetComponent<FloorController>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePosition = hit.point;
            transform.localPosition = new Vector3(mousePosition.x / scaleRatio, transform.position.y, mousePosition.z / scaleRatio);
        }
    }
}
