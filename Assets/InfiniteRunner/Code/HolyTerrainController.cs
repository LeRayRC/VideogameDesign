using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyTerrainController : MonoBehaviour
{
    [HideInInspector]
    public bool initTimer;
    public float timeToDisappear;
    [SerializeField]
    float timeElapsed;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("ObstacleDestroyer")){
            initTimer=true;
        }
    }

    public void Start(){
        Init();
    }

    public void Update(){
        if(initTimer){
            timeElapsed += Time.deltaTime;
            if(timeElapsed >= timeToDisappear){
                GetComponent<Transform>().position = new Vector3(10000.0f, 10000.0f, 10000.0f);
                initTimer = false;
            }
        }
    }

    public void Init(){
        initTimer = false;
        timeElapsed = 0.0f;
    }
}
