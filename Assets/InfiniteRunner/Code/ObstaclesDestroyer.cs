using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    public ObstaclesController ob_;

    void OnTriggerEnter(Collider other){
        // Debug.Log(other.gameObject.layer);
        if(other.gameObject.layer == LayerMask.NameToLayer("Terrain")){
            if(other.gameObject.name != "HollyTerrain"){
                // Debug.Log("Collision!");
                ob_.InitObstacleInstance();
                //Fin gameobject on list
                for(int i=1; i<ob_.obstaclesList_.Count;i++){
                    if(ob_.obstaclesList_[i] == other.gameObject){
                        ob_.obstaclesList_.RemoveAt(i);
                        Destroy(other.gameObject,8.0f);
                        ob_.obstaclesToDestroyList_.Add(other.gameObject);
                    }
                }
            }else{
                
            }
        }else{
            // Debug.Log("Collision with " + other.gameObject.name);
        }
    }
}
