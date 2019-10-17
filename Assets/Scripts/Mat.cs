using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat : MonoBehaviour
{
    public float top;
    public float right;
    public float bottom;
    public float left;
    private Meeple meeple;
    private Vector3 meepleSize;

    // Start is called before the first frame update
    void Start()
    {
        meeple = FindObjectOfType<Meeple>();
        meepleSize = meeple.size;

        Vector3 pos = transform.position;
        Vector3 size = GetComponent<Renderer>().bounds.size;

        left = (pos.x - size.x / 2) + meepleSize.x;
        right = (pos.x + size.x / 2) - meepleSize.x;
        top = (pos.y + size.y / 2) - meepleSize.y;
        bottom = (pos.y - size.y / 2) + meepleSize.y;
        //Debug.Log("Left:" + left + " / Right:" + right + " / Top:" + top + " / Bottom:" + bottom + " / Meeple Width:" + meepleSize.x + " / Meeple Height:" + meepleSize.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
