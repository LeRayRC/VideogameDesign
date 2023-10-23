using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class ObstaclesController : MonoBehaviour
{
    public enum Difficulty{
        Difficulty_Easy,
        Difficulty_Medium,
        Difficulty_Hard,
    }
    public List<GameObject> obstaclesList_;
    public List<GameObject> obstaclesToDestroyList_;
     
    public List<ObstacleData> obstaclesPrefabList_;
    public List<ObstacleData> obstaclesPrefabList1;
    public List<ObstacleData> obstaclesPrefabList2;
    public List<ObstacleData> obstaclesPrefabList3;

    public Difficulty difficulty_;

    [HideInInspector]
    public GameObject lastGeneratedObstacle;
    public GameObject hero_;
    public GameObject holyTerrain_;

    public Vector3 holyTerrainInitPos;
    public float moveTerrainOffset;
    public float spaceBetweenObstacles;
    public int minObstaclesBeforeChangeDirection;

    [HideInInspector]
    public int obstaclesGenerated;
    [HideInInspector]
    GameObject obstacleGo_;
    [HideInInspector]
    public bool changeDirection;

    public float timerToDestroyObstacle;
    public float timeElapsed;
    public int initialObstacles;

    // Start is called before the first frame update
    void Start()
    {
        switch(difficulty_){
            case Difficulty.Difficulty_Easy:
                obstaclesPrefabList_ = obstaclesPrefabList1;
                break;
            case Difficulty.Difficulty_Medium:
                obstaclesPrefabList_ = obstaclesPrefabList2;
                break;
            case Difficulty.Difficulty_Hard:
                obstaclesPrefabList_ = obstaclesPrefabList3;
                break;
        }


        InitObstacles();
        holyTerrainInitPos = holyTerrain_.transform.position;

    }

    // public void EmptyGameObjectList(List<GameObject> list){
    //     for(int i=list.Count-1;i>=0;i--){
    //         list.RemoveAt(i);
    //     }
    // }

    // public void SetGameObjectList(List<GameObject> list_src,  List<GameObject> list_dst){
    //     for(int i=0;i<obstaclesPrefabList1.Count-1;i++){
    //         list_dst.Add(list_src[i].obstaclePrefab_);
    //     }
    // }

    // public void UpdatePrefabList(Difficulty difficulty){
    //     EmptyGameObjectList(obstaclesList_);
    //     switch(difficulty){
    //         case Difficulty.Difficulty_Easy:
    //             for(int i=0;i<obstaclesPrefabList1.Count-1;i++){
    //                 obstaclesList_.Add(obstaclesPrefabList1[i].obstaclePrefab_);
    //             }
    //             break;
    //         case Difficulty.Difficulty_Medium:
    //             for(int i=0;i<obstaclesPrefabList2.Count-1;i++){
    //                 obstaclesList_.Add(obstaclesPrefabList2[i].obstaclePrefab_);
    //             }
    //             break;
    //         case Difficulty.Difficulty_Hard:
    //             for(int i=0;i<obstaclesPrefabList3.Count-1;i++){
    //                 obstaclesList_.Add(obstaclesPrefabList3[i].obstaclePrefab_);
    //             }
    //             break;
    //         default:
    //             break;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        // timeElapsed += Time.deltaTime;
        // if(timeElapsed >= timerToDestroyObstacle){
        //     GameObject goDestroy = obstaclesList_[1];
        //     obstaclesList_.RemoveAt(1);
        //     Destroy(goDestroy,0.0f);
        //     InitObstacleInstance();
        //     timeElapsed = 0.0f;
        // }
        //Iterate over list and check if one terrain is behind the
        //to teleport it towards

        // foreach (GameObject go in obstaclesList_){
        //     if(go.transform.position.z + go.transform.localScale.z + moveTerrainOffset < hero_.transform.position.z){
        //         //Destroy gameobject and instantiate another
        //         Vector3 new_position = GetFurthestTerrain().position;
        //         new_position.z += spaceBetweenObstacles;
        //         go.transform.position = new_position;
        //     }
        // }
    }

   

    

    public Transform GetFurthestTerrain(){
        Vector3 furthest_position = Vector3.zero;
        int furthest_index = 0;
        for(int i=0;i<obstaclesList_.Count;i++){
            if(obstaclesList_[i].transform.position.z > furthest_position.z){
                furthest_position = obstaclesList_[i].transform.position;
                furthest_index = i;
            }
        }
        return obstaclesList_[furthest_index].transform;
    }


    public void ResetTerrainPosition(){
        for(int i=obstaclesList_.Count-1;i>0;i--){
            Destroy(obstaclesList_[i],0.0f);
        }
        obstaclesList_ = new List<GameObject>();
        obstaclesList_.Add(holyTerrain_);

        for(int i=0;i<obstaclesToDestroyList_.Count;i++){
            Destroy(obstaclesToDestroyList_[i].gameObject,0.0f);
        }
        obstaclesToDestroyList_ = new List<GameObject>();
        InitObstacles();
    }

    void InitObstacles(){
        changeDirection = false;
        lastGeneratedObstacle = obstaclesList_[0];
        obstaclesGenerated = 0;
        for(int i=0;i<initialObstacles;i++){
            InitObstacleInstance();
        }
    }

    public void InitObstacleInstance(){
        int prefab_selected = Random.Range(0,obstaclesPrefabList_.Count);
            if(obstaclesGenerated < minObstaclesBeforeChangeDirection){

                //Generate normal obstacle
                if(obstaclesGenerated == minObstaclesBeforeChangeDirection-1){
                    obstacleGo_ = obstaclesPrefabList_[0].InstantiateOnGame(this);
                }else{
                    obstacleGo_ = obstaclesPrefabList_[prefab_selected].InstantiateOnGame(this);
                }
                obstaclesGenerated++;
            }else{
                obstaclesGenerated = 0;
                changeDirection = true;
                //Generate plain direction with change right or left
                obstacleGo_ = obstaclesPrefabList_[0].InstantiateOnGame(this);
                changeDirection = false; 
            }
            
            if( obstacleGo_ != null){
                obstaclesList_.Add(obstacleGo_);
                lastGeneratedObstacle = obstacleGo_;
            }else{
                Debug.Log("Obstacle generated is null");
            }
    }
}
