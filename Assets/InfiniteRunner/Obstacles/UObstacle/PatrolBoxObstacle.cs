using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrolBoxObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    Transform tr_;
    BoxCollider bc_;

    int movingRight;

    float speedPatrol;

    Vector3 edge;
    void Start()
    {   
        movingRight = Random.Range(0,2);
        speedPatrol = Random.Range(5.0f,10.0f);
        tr_ = GetComponent<Transform>();
        bc_ = GetComponent<BoxCollider>();
    }


    // Update is called once per frame
    void Update()
    {
        if(movingRight == 1){
            tr_.transform.position += tr_.transform.right*Time.deltaTime*speedPatrol;
            if(tr_.parent.transform.rotation.eulerAngles.y % 180 == 0 || tr_.parent.transform.rotation.eulerAngles.y == 0 ){
                edge = new Vector3(tr_.transform.right.x * tr_.transform.localScale.x * tr_.parent.transform.localScale.x * 0.5f,
                                     tr_.transform.right.y * tr_.transform.localScale.y * tr_.parent.transform.localScale.y * 0.5f,
                                     tr_.transform.right.z * tr_.transform.localScale.z * tr_.parent.transform.localScale.z * 0.5f);
            }else if(tr_.parent.transform.rotation.eulerAngles.y % 90 == 0 ){
                edge = new Vector3(tr_.transform.right.x * tr_.transform.localScale.z * tr_.parent.transform.localScale.z * 0.5f,
                                     tr_.transform.right.y * tr_.transform.localScale.y * tr_.parent.transform.localScale.y * 0.5f,
                                     tr_.transform.right.z * tr_.transform.localScale.x * tr_.parent.transform.localScale.x * 0.5f);
            }
            //Check Raycast
            if(!Physics.Raycast(tr_.transform.position + edge *1.1f,Vector3.down, 2.0f)){
                movingRight = 0;
            }

            Debug.DrawRay(tr_.transform.position + edge * 1.1f,Vector3.down* 2.0f, Color.blue);
        }else if(movingRight == 0){
            tr_.transform.position -= tr_.transform.right*Time.deltaTime*speedPatrol;
            // edge = new Vector3(tr_.transform.right.x * tr_.transform.localScale.x * 0.5f * tr_.parent.parent.transform.localScale.x * -1.0f,
            //                    tr_.transform.right.y * tr_.transform.localScale.y * 0.5f * tr_.parent.parent.transform.localScale.y * -1.0f,
            //                    tr_.transform.right.z * tr_.transform.localScale.z * 0.5f * tr_.parent.parent.transform.localScale.z * -1.0f);
            if(tr_.parent.transform.rotation.eulerAngles.y % 180 == 0 || tr_.parent.transform.rotation.eulerAngles.y == 0 ){
                edge = new Vector3(tr_.transform.right.x * tr_.transform.localScale.x * tr_.parent.transform.localScale.x * 0.5f  * -1.0f,
                                     tr_.transform.right.y * tr_.transform.localScale.y * tr_.parent.transform.localScale.y * 0.5f * -1.0f,
                                     tr_.transform.right.z * tr_.transform.localScale.z * tr_.parent.transform.localScale.z * 0.5f * -1.0f);
            }else if(tr_.parent.transform.rotation.eulerAngles.y % 90 == 0 ){
                edge = new Vector3(tr_.transform.right.x * tr_.transform.localScale.z * tr_.parent.transform.localScale.z * 0.5f * -1.0f,
                                     tr_.transform.right.y * tr_.transform.localScale.y * tr_.parent.transform.localScale.y * 0.5f * -1.0f,
                                     tr_.transform.right.z * tr_.transform.localScale.x * tr_.parent.transform.localScale.x * 0.5f * -1.0f);
            }
            //Check Raycast
            if(!Physics.Raycast(tr_.transform.position + edge * 1.1f ,Vector3.down, 2.0f)){
                movingRight = 1;
            }
            Debug.DrawRay(tr_.transform.position + edge * 1.1f ,Vector3.down* 2.0f, Color.red);
        }
    }
}
