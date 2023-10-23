using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    // Start is called before the first frame update
    public ObstaclesController oc_;
    public CameraController cc_;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Hero")
        {   
            
            HeroFunctions hf_ = other.GetComponent<HeroFunctions>();
            oc_.SetDifficulty(ObstaclesController.Difficulty.Difficulty_Easy);
            hf_.MoveToSpawn();
            cc_.MoveToSpawn();
            oc_.gameObject.GetComponent<HeroController>().changedTo = HeroController.ChangeToDir.None;
            oc_.holyTerrain_.transform.position = oc_.holyTerrainInitPos;
            oc_.holyTerrain_.GetComponent<HolyTerrainController>().Init();
            oc_.ResetTerrainPosition();
            
        }
    }
}
