using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CylinderObstaclesController : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    float speedRotation_;
    Transform tr_;
    void Start()
    {
        tr_ = GetComponent<Transform>();
    }

    public void Init(float min_speed, float max_speed){
        speedRotation_ = Random.Range(min_speed, max_speed);
    }

    // Update is called once per frame
    void Update()
    {
        tr_.Rotate(new Vector3(0.0f, speedRotation_ * Time.deltaTime, 0.0f), Space.World);
    }
}
