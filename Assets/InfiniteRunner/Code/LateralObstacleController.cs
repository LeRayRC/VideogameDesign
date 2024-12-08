using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralObstacleController : MonoBehaviour
{
    Transform tr_;
    float speed_patrol;
    // Start is called before the first frame update
    void Start()
    {
        tr_ = GetComponent<Transform>();
        speed_patrol = Random.Range(2.0f, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.parent
    }
}
