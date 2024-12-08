using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class HeroController : MonoBehaviour
{
    // Start is called before the first frame update
    public enum ChangeToDir{
        Left,
        Right,
        None,
    }
    public GameObject hero_;
    public GameObject deathPlane_;
    public Camera mainCamera_;

    private float initFOV;
    public float onAirFOV;

    public Vector3 dir_ = new Vector3();
    private Vector3 spawnPoint_ = new Vector3();

    public float speed_;
    [HideInInspector]
    public float speedForward_;
    public float onGroundSpeedForward_;
    public float onAirSpeedForward_;
    public float jumpForce_;
    public float gravityScale_;

    // ChangeDirection
    public ChangeToDir changedTo;

    [HideInInspector]
    public bool canJump_;

    private Rigidbody heroRb_;
    private CapsuleCollider heroCollider_;
    private Animator anim_;

    //Collider opts
    private float onRunningColliderHeight;
    private Vector3 onRunningColliderCenter;
    

    public float onRollingColliderHeight;
    public Vector3 onRollingColliderCenter;
    RaycastHit hit;

    public GameObject speedParticles_;
    public GameObject speedParticlesFatherPosition;
    public GameObject speedParticlesPrefab_;

    public float distanceDone_;
    void Start()
    {   
        changedTo = ChangeToDir.None;
        spawnPoint_ = hero_.transform.position;

        heroRb_ = hero_.GetComponent<Rigidbody>();
        heroCollider_ = hero_.GetComponent<CapsuleCollider>();
        anim_ = hero_.GetComponent<Animator>();
        initFOV = mainCamera_.fieldOfView;
        // Debug.Log(initFOV + " " + onAirFOV);
        onRunningColliderHeight = heroCollider_.height;
        onRunningColliderCenter = heroCollider_.center;
    }

    // Update is called once per frame
    void Update()
    {
        // RaycastHit hit;
        if(Physics.Raycast(hero_.transform.position + (Vector3.up * heroCollider_.height * 0.5f),Vector3.down, ((heroCollider_.height * 0.5f) + 0.1f), LayerMask.GetMask("ObstaclePart"))){
            canJump_ = true;
            anim_.SetBool("OnAir",false);
            
            
            //Debug.Log("On Ground!");
            mainCamera_.fieldOfView = Mathf.Lerp(mainCamera_.fieldOfView, initFOV, 0.01f);
            speedForward_ = onGroundSpeedForward_;
        }
        else{
            canJump_ = false;
            anim_.SetBool("OnAir",true);
            // Debug.Log("On Air!");
            mainCamera_.fieldOfView = Mathf.Lerp(mainCamera_.fieldOfView, onAirFOV, 0.01f);
            speedForward_ = onAirSpeedForward_;
        }
        Debug.DrawRay(hero_.transform.position + (Vector3.up * heroCollider_.height * 0.5f),Vector3.down*((heroCollider_.height * 0.5f) + 0.01f));
        


        if(Input.GetKey(KeyCode.W)){
            // Debug.Log("Moving forward");
        }
        if((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && anim_.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            // anim_.ResetTrigger("RollActivated");
            // Debug.Log("Moving backwards");
            anim_.SetTrigger("RollActivated");
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            hero_.transform.position += hero_.transform.right * -1.0f * speed_ * Time.deltaTime;
            //Debug.Log("Moving Left");
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
        {
            hero_.transform.position += hero_.transform.right * speed_ * Time.deltaTime;
            //Debug.Log("Moving Right");
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Moving forward");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Moving backwards");
        }
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     hero_.transform.position += hero_.transform.right * -1.0f * speed_ * Time.deltaTime;
        //     // Debug.Log("Moving Left");
        // }
        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     hero_.transform.position += hero_.transform.right * speed_ * Time.deltaTime;
        //     // Debug.Log("Moving Right");
        // }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Return");
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump_)
        {
            
            heroRb_.AddForce(Vector3.up * jumpForce_, ForceMode.Impulse);
            // Debug.Log("Jumping");
                        
            anim_.ResetTrigger("RollActivated");

            if (speedParticles_ != null)
            {
                Destroy(speedParticles_, 0.0f);
            }
            GameObject go_ = Instantiate<GameObject>(speedParticlesPrefab_,
                                                     speedParticlesFatherPosition.transform.position,
                                                     speedParticlesFatherPosition.transform.rotation);
            go_.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
            Destroy(go_, go_.GetComponent<ParticleSystem>().main.duration);
            go_.transform.SetParent(hero_.transform);
            speedParticles_ = go_;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Turn left
            hero_.transform.Rotate(new Vector3(0.0f,-90.0f,0.0f));
            changedTo = ChangeToDir.Left;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            hero_.transform.Rotate(new Vector3(0.0f,90.0f,0.0f));
            changedTo = ChangeToDir.Right;
        }

        if (anim_.GetCurrentAnimatorStateInfo(0).IsName("Rolling")){
            heroCollider_.height = onRollingColliderHeight;
            heroCollider_.center = onRollingColliderCenter;
        }else{
            heroCollider_.height = onRunningColliderHeight;
            heroCollider_.center = onRunningColliderCenter;
        }

        deathPlane_.transform.position = Vector3.Lerp(deathPlane_.transform.position,
                                                      new Vector3(hero_.transform.position.x,
                                                                  deathPlane_.transform.position.y,
                                                                  hero_.transform.position.z),1.0f);

        distanceDone_ += speedForward_*Time.deltaTime;
    }

    void FixedUpdate(){

        if(anim_.GetCurrentAnimatorStateInfo(0).IsName("OnAir")){

            Physics.CapsuleCast(hero_.transform.position + Vector3.up * heroCollider_.height*0.3f,
                               hero_.transform.position + Vector3.up * heroCollider_.height*0.65f,
                               heroCollider_.radius * 0.9f, hero_.transform.forward,out hit, 0.1f, LayerMask.GetMask("Terrain"));

            if(hit.collider != null){
                // Debug.Log("Colliding with " + hit.collider.gameObject.name);
                // heroRb_.velocity = new Vector3(0.0f, heroRb_.velocity.y, 0.0f);
                heroRb_.velocity = new Vector3(0.0f, heroRb_.velocity.y, 0.0f);
            }else{
                heroRb_.velocity = new Vector3(hero_.transform.forward.x * speedForward_, heroRb_.velocity.y, hero_.transform.forward.z * speedForward_);
            }
        }else{
            heroRb_.velocity = new Vector3(hero_.transform.forward.x * speedForward_, heroRb_.velocity.y, hero_.transform.forward.z * speedForward_);

        }
                            //    }
        // hero_.transform.position += Vector3.forward * speedForward_ * Time.deltaTime;
        heroRb_.AddForce(Vector3.down*9.8f*gravityScale_,ForceMode.Acceleration);

        // if(heroRb_.velocity.z <= 0.1f){
        //     Debug.Log("Stucked!");
        //     timeStucked += Time.deltaTime;
        //     if(timeStucked >= limitTimeStucked){
        //         gravityScale_ = 100;
        //         timeStucked = 0.0f;
        //     }
        // }else{
        //     Debug.Log(heroRb_.velocity.z);
        //         // Debug.Log("Magnitude" + heroRb_.velocity.x + ", " + heroRb_.velocity.y + " , " + heroRb_.velocity.y);
        //     }
    }
}
