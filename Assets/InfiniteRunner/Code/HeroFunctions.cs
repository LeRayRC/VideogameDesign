using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFunctions : MonoBehaviour
{
    private Vector3 spawnPonint_;
    public int score;
    // Start is called before the first frame update
    public Vector3 GetSpawnPoint(){
        return spawnPonint_;
    }
    void Start()
    {
        score = 0;
        spawnPonint_ = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToSpawn()
    {
        score = 0;
        transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
        transform.position = spawnPonint_;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
