using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCylinderObstacle : MonoBehaviour
{
    public float rotateSpeed_;
    Transform tr_;
    // Start is called before the first frame update
    void Start()
    {
        tr_ = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tr_.Rotate(new Vector3(0.0f, rotateSpeed_ * Time.deltaTime, 0.0f), Space.World);
    }
}
