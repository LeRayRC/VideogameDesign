using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plain Obstacle", menuName = "MyAutorun/Obstacles/New Plain", order = 101)]
public class PlainObstacle : ObstacleData
{
    

    // Update is called once per frame
    public override GameObject InstantiateOnGame(ObstaclesController oc_)
    {
        // Transform furthestTr_ = oc_.GetFurthestTerrain();
        Vector3 new_position = new Vector3();
        Vector3 edge_position;
        GameObject obstacleGo_ = null;
        if(!oc_.changeDirection){
            //Forward obstacle
            if(oc_.lastGeneratedObstacle.transform.eulerAngles.y % 180 == 0 || oc_.lastGeneratedObstacle.transform.eulerAngles.y == 0){
                edge_position = new Vector3(oc_.lastGeneratedObstacle.transform.forward.x * oc_.lastGeneratedObstacle.transform.localScale.x * 0.5f,
                                                oc_.lastGeneratedObstacle.transform.forward.y * oc_.lastGeneratedObstacle.transform.localScale.y * 0.5f,
                                                oc_.lastGeneratedObstacle.transform.forward.z * oc_.lastGeneratedObstacle.transform.localScale.z * 0.5f);
                new_position = oc_.lastGeneratedObstacle.transform.position + edge_position + oc_.lastGeneratedObstacle.transform.forward * oc_.spaceBetweenObstacles;


            }else{
                edge_position = new Vector3(oc_.lastGeneratedObstacle.transform.forward.x * oc_.lastGeneratedObstacle.transform.localScale.z * 0.5f,
                                                oc_.lastGeneratedObstacle.transform.forward.y * oc_.lastGeneratedObstacle.transform.localScale.y * 0.5f,
                                                oc_.lastGeneratedObstacle.transform.forward.z * oc_.lastGeneratedObstacle.transform.localScale.x * 0.5f);
                new_position = oc_.lastGeneratedObstacle.transform.position + edge_position + oc_.lastGeneratedObstacle.transform.forward * oc_.spaceBetweenObstacles;

                // Debug.Log("new pos: " + new_position);
            }
            
            
            obstacleGo_ = Instantiate<GameObject>(obstaclePrefab_,
                                                         new_position,
                                                         oc_.lastGeneratedObstacle.transform.rotation);
            
            obstacleGo_.transform.Translate(TranslateHalfScale(obstacleGo_.transform, oc_.lastGeneratedObstacle.transform, oc_.changeDirection), Space.World);
        }else{
            //Turn obstacle left or right
            // Debug.Log("Change Direction edge!");
            switch(Random.Range(0,2)){
                case 0: {
                    // Debug.Log("Right change!");
                    //Right
                    if(oc_.lastGeneratedObstacle.transform.eulerAngles.y % 180 == 0 || oc_.lastGeneratedObstacle.transform.eulerAngles.y == 0){
                        edge_position = new Vector3(oc_.lastGeneratedObstacle.transform.right.x * oc_.lastGeneratedObstacle.transform.localScale.x * 0.5f,
                                                        oc_.lastGeneratedObstacle.transform.right.y * oc_.lastGeneratedObstacle.transform.localScale.y * 0.5f,
                                                        oc_.lastGeneratedObstacle.transform.right.z * oc_.lastGeneratedObstacle.transform.localScale.z * 0.5f);
                        new_position = oc_.lastGeneratedObstacle.transform.position + edge_position; //+ oc_.lastGeneratedObstacle.transform.right * oc_.spaceBetweenObstacles;
                    }else{
                        edge_position = new Vector3(oc_.lastGeneratedObstacle.transform.right.x * oc_.lastGeneratedObstacle.transform.localScale.z * 0.5f,
                                                        oc_.lastGeneratedObstacle.transform.right.y * oc_.lastGeneratedObstacle.transform.localScale.y * 0.5f,
                                                        oc_.lastGeneratedObstacle.transform.right.z * oc_.lastGeneratedObstacle.transform.localScale.x * 0.5f);
                        new_position = oc_.lastGeneratedObstacle.transform.position + edge_position;
                        // Debug.Log("Forward " + oc_.lastGeneratedObstacle.transform.right);
                    }

                    obstacleGo_ = Instantiate<GameObject>(obstaclePrefab_,
                                                  new_position,
                                                  oc_.lastGeneratedObstacle.transform.rotation);
                    obstacleGo_.transform.Rotate(new Vector3(0.0f,90.0f,0.0f));
                    break;
                }
                case 1: {
                    // Debug.Log("Left change!");
                    //Turn left
                    if(oc_.lastGeneratedObstacle.transform.eulerAngles.y % 180 == 0 || oc_.lastGeneratedObstacle.transform.eulerAngles.y == 0){
                        edge_position = new Vector3(oc_.lastGeneratedObstacle.transform.right.x * -1.0f * oc_.lastGeneratedObstacle.transform.localScale.x * 0.5f,
                                                        oc_.lastGeneratedObstacle.transform.right.y * -1.0f * oc_.lastGeneratedObstacle.transform.localScale.y * 0.5f,
                                                        oc_.lastGeneratedObstacle.transform.right.z * -1.0f * oc_.lastGeneratedObstacle.transform.localScale.z * 0.5f);
                        new_position = oc_.lastGeneratedObstacle.transform.position + edge_position; //+ oc_.lastGeneratedObstacle.transform.right * oc_.spaceBetweenObstacles;
                    }else{
                        edge_position = new Vector3(oc_.lastGeneratedObstacle.transform.right.x * -1.0f * oc_.lastGeneratedObstacle.transform.localScale.z * 0.5f,
                                                        oc_.lastGeneratedObstacle.transform.right.y * -1.0f * oc_.lastGeneratedObstacle.transform.localScale.y * 0.5f,
                                                        oc_.lastGeneratedObstacle.transform.right.z * -1.0f * oc_.lastGeneratedObstacle.transform.localScale.x * 0.5f);
                        new_position = oc_.lastGeneratedObstacle.transform.position + edge_position;
                        // Debug.Log("Forward " + oc_.lastGeneratedObstacle.transform.right);
                    }

                    obstacleGo_ = Instantiate<GameObject>(obstaclePrefab_,
                                                  new_position,
                                                  oc_.lastGeneratedObstacle.transform.rotation);
                    obstacleGo_.transform.Rotate(new Vector3(0.0f,-90.0f,0.0f));
                    break;
                }
            }
            
            // Debug.Log("Edge pos: " + edge_position);
            
            obstacleGo_.transform.Translate(TranslateHalfScale(obstacleGo_.transform, oc_.lastGeneratedObstacle.transform, oc_.changeDirection), Space.World); 

        }
        
        // Debug.Log("New position" + new_position);
        // Debug.Log("Edge position" + edge_position);
        
        
        return obstacleGo_;
    }

    public override Vector3 TranslateHalfScale(Transform tr_, Transform parentTr_, bool changeDirection){
        Vector3 translate_pos = new Vector3();
        // Debug.Log(parentTr_.gameObject.name + " rot y : " + parentTr_.eulerAngles.y);
        if(changeDirection){
            if(parentTr_.eulerAngles.y % 180 == 0 || parentTr_.eulerAngles.y == 0){
                // Debug.Log("Change Direction!");

                translate_pos.x = tr_.localScale.z * 0.5f * tr_.forward.x;
                translate_pos.y = tr_.localScale.y * 0.5f * tr_.forward.y;
                translate_pos.z = tr_.localScale.x * 0.5f * tr_.forward.z;
                // Debug.Log("Translate pos " + translate_pos);
            }else if(parentTr_.eulerAngles.y % 90 == 0){
                translate_pos.x = tr_.localScale.x * 0.5f * tr_.forward.x;
                translate_pos.y = tr_.localScale.y * 0.5f * tr_.forward.y;
                translate_pos.z = tr_.localScale.z * 0.5f * tr_.forward.z;
            }
        }else{
            if(parentTr_.eulerAngles.y % 180 == 0 || parentTr_.eulerAngles.y == 0){
                // Debug.Log("Change Direction!");
                // Debug.Log("Translate pos " + translate_pos);
                translate_pos.x = tr_.localScale.x * 0.5f * tr_.forward.x;
                translate_pos.y = tr_.localScale.y * 0.5f * tr_.forward.y;
                translate_pos.z = tr_.localScale.z * 0.5f * tr_.forward.z;

            }else if(parentTr_.eulerAngles.y % 90 == 0){
                // Debug.Log("Nothing");
                translate_pos.x = tr_.localScale.z * 0.5f * tr_.forward.x;
                translate_pos.y = tr_.localScale.y * 0.5f * tr_.forward.y;
                translate_pos.z = tr_.localScale.x * 0.5f * tr_.forward.z;
            }
        }

        return translate_pos;
    }
}
