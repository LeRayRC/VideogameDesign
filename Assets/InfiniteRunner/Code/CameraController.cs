using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    // Start is called before the first frame update
    public GameObject gc_;
    private GameObject hero_;
    private HeroController hc_;
    private Transform tr_;
    public Transform followCameraTr_;
    private Vector3 crossProduct_;
    public float crossProductOffset;
    public float rotateSpeed;
    public Vector3 spawnPoint = new Vector3();
    public Quaternion spawnRotation = new Quaternion();

    private Rigidbody rb_; 
    private Rigidbody heroRb_;

    void Start()
    {   
        hc_ = gc_.GetComponent<HeroController>();
        hero_ = hc_.hero_;
        tr_ = GetComponent<Transform>();
        spawnPoint = tr_.transform.position;
        spawnRotation = tr_.transform.rotation;
        rb_ = GetComponent<Rigidbody>();
        heroRb_ = hero_.GetComponent<Rigidbody>();
        // tr_.LookAt(hero_.transform);
    }

    // Update is called once per frame
    void Update()
    {
        crossProduct_ = Vector3.Cross(new Vector3(tr_.forward.x,hero_.transform.forward.y,tr_.forward.z),hero_.transform.forward);

        if(hc_.changedTo == HeroController.ChangeToDir.Left && crossProduct_.sqrMagnitude >= crossProductOffset){

            tr_.RotateAround(hero_.transform.position,Vector3.up, Time.deltaTime * rotateSpeed * -1.0f);

        }else if(hc_.changedTo == HeroController.ChangeToDir.Right && crossProduct_.sqrMagnitude >= crossProductOffset){
            
            tr_.RotateAround(hero_.transform.position,Vector3.up, Time.deltaTime * rotateSpeed);
        }
        
        // Debug.Log(Vector3.Cross(tr_.forward, new Vector3(hero_.transform.position.x,
        //                                                 tr_.transform.position.y,
        //                                                 hero_.transform.position.z)));
        // Debug.Log("Cross" + Vector3.Cross(new Vector3(tr_.forward.x,hero_.transform.forward.y,tr_.forward.z),hero_.transform.forward));
        // Debug.Log("Cross Magnitude" + Vector3.Cross(new Vector3(tr_.forward.x,hero_.transform.forward.y,tr_.forward.z),hero_.transform.forward).sqrMagnitude);

        // Debug.Log(tr_.forward.x + "," + hero_.transform.forward.y +","+ tr_.forward.z);
        // Debug.Log("Forward camera" + tr_.forward);
        // Debug.Log("Forward hero" + hero_.transform.forward);
        // Debug.Log("Cross" + Vector3.Cross(tr_.forward,hero_.transform.forward));
    }

    void FixedUpdate(){
        crossProduct_ = Vector3.Cross(new Vector3(tr_.forward.x,hero_.transform.forward.y,tr_.forward.z),hero_.transform.forward);

        if(crossProduct_.sqrMagnitude <= crossProductOffset && Vector3.Distance(tr_.transform.position, followCameraTr_.transform.position) > 0.1f){
            tr_.transform.position = Vector3.Lerp(tr_.transform.position, followCameraTr_.position, 0.01f);
        }
        
        rb_.velocity = new Vector3(heroRb_.velocity.x, 0.0f,heroRb_.velocity.z);
    }

    public void MoveToSpawn(){
        tr_.position = spawnPoint;
        tr_.rotation = spawnRotation;
        // tr_.LookAt(hero_.transform);
    }
}
