using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinController : MonoBehaviour
{
    public static List<GameObject> coinList = new List<GameObject>();
    // public List<GameObject> coinList_;
    // Start is called before the first frame update
    public float rotateSpeed_;
    Transform tr_;
    Transform parentTr_;
    // Start is called before the first frame update
    void Start()
    {
        tr_ = GetComponent<Transform>();
        parentTr_ = GetComponentInParent<Transform>();
        coinList.Add(this.gameObject);
        Debug.Log(coinList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        tr_.SetParent(null);
        tr_.Rotate(new Vector3(0.0f, rotateSpeed_ * Time.deltaTime, 0.0f), Space.World);
        tr_.SetParent(parentTr_);

        
    }
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("hero")){

            other.gameObject.GetComponent<HeroFunctions>().score++;
            Destroy(gameObject,0.0f);
            // Debug.Log("Collision with destroy " + other.gameObject.name);
        }

        // Debug.Log("Collision with " + other.gameObject.name);
    }

    public static void EmptyCoinList(){
        for(int i=coinList.Count-1;i>0;i--){
            if(coinList[i] != null){
                Destroy(coinList[i]);
            }
            coinList.RemoveAt(i);
        }
    }
}
