using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    public ObstaclesController oc_;
    public CameraController cc_;

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
            HeroFunctions hf_ = other.GetComponent<HeroFunctions>();
            oc_.SetDifficulty(ObstaclesController.Difficulty.Difficulty_Easy);
            hf_.MoveToSpawn();
            cc_.MoveToSpawn();
            oc_.gameObject.GetComponent<HeroController>().changedTo = HeroController.ChangeToDir.None;
            oc_.holyTerrain_.transform.position = oc_.holyTerrainInitPos;
            oc_.holyTerrain_.GetComponent<HolyTerrainController>().Init();
            oc_.ResetTerrainPosition();
            oc_.timePlaying = 0.0f;
        }
    }
}
