using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LineController : MonoBehaviour
{


    private void FixedUpdate()
    {
        Transform endPoint = GameObject.FindGameObjectWithTag("mainHome").transform; // Get "mainHome" tag object as aend point
        Vector3 endPos = new Vector3(endPoint.position.x, endPoint.position.y, endPoint.position.z); // Splited a vector3 incase any control need to do 

        #region Line renderer

        /* ..............................Line renderer in this block............................*/

        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, endPos);
        GetComponent<LineRenderer>().startWidth = 0.01f;
        GetComponent<LineRenderer>().endWidth = 0f;

        /*.....................................line rendering...................................*/

        #endregion

    }


}