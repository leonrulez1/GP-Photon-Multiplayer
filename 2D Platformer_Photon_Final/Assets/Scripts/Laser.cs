using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer line;
    public Transform hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = true;
        line.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localEulerAngles);
        //Debug.DrawLine(transform.position, hit.point);
        //hitPoint.position = hit.point;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hitPoint.position);
        
    }
}
