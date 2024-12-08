using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medium_PlainJumpController : MonoBehaviour
{
    // Start is called before the first frame update

    public float patrolSpeed;

    Transform tr_;

    int movingRight;
    void Start()
    {
        tr_ = GetComponent<Transform>();
    }

    public void Init(float minSpeed, float maxSpeed){
        patrolSpeed = Random.Range(minSpeed,maxSpeed);
        movingRight = Random.Range(0,2);
    }

    // Update is called once per frame
    void Update()
    {
        switch(movingRight){
            case 0:
                tr_.Translate(tr_.transform.right * patrolSpeed * Time.deltaTime, Space.World);
                break;
            case 1:
                tr_.Translate(tr_.transform.right * patrolSpeed * Time.deltaTime * -1.0f, Space.World);
                break;
        }
    }


    void OnCollisionEnter(Collision other){
        // Debug.Log("Collision!");
        if(other.gameObject.layer == LayerMask.NameToLayer("ObstaclePart")){
            movingRight++;
            movingRight%=2;
        }else{
            // Debug.Log(other.gameObject.name + " " + other.gameObject.layer + " " +  LayerMask.NameToLayer("ObstaclePart"));
        }
    }
}
