using UnityEngine;
using System.Collections;

public class HitPoints : MonoBehaviour {
 
  public int score=100;
  TouchScreen m;
  
  void Start () {
    m = GameObject.Find("map").GetComponent<TouchScreen>();
  }
   
 
  public void AdjustCurrentScore(int adj)
    {
        score-= adj;
   
   if (score < 0)
     score = 0;
 }
  
  public void AddScore(int adj) {
    score+=adj;
    
  }
 
  void OnTriggerEnter(Collider col) {
    Debug.Log ("Praz");
    if (col.gameObject.tag == "wall"){
      AdjustCurrentScore(15);  
      m.myCar.colided = true;
   }
    Debug.Log ("Praz2");
   if (col.gameObject.tag == "Tree01"){
      AdjustCurrentScore(10);
      switch(col.gameObject.name)
      {
      case "tree5":{
        if(!m.t5){
          col.gameObject.animation.Play("tree1_fall");
          col.gameObject.animation.wrapMode = WrapMode.Once;
          m.t5 = true;
        }
      }
        break;
      case "tree3":{
        if(!m.t3){
        col.gameObject.animation.Play("tree1_fall");
        col.gameObject.animation.wrapMode = WrapMode.Once;
          m.t3 = true;
        }
      }
        break;
      case "tree4":{
        if(!m.t4){
        col.gameObject.animation.Play("tree1_fall");
        col.gameObject.animation.wrapMode = WrapMode.Once;
          m.t4 = true;
        }
      }
        break;
      case "tree6":{
        if(!m.t6){
        col.gameObject.animation.Play("tree1_fall");
        col.gameObject.animation.wrapMode = WrapMode.Once;
          m.t6 = true;
        }
      }
        break;
      case "tree7":{
        if(!m.t7){
        col.gameObject.animation.Play("tree1_fall");
        col.gameObject.animation.wrapMode = WrapMode.Once;
          m.t7 = true;
        }
      }
        break;
        
      }
      m.myCar.colided = true;
    }
    
    if (col.gameObject.tag=="enemy") {
       AddScore(50);
       Destroy(col.gameObject);
        m.myCar.colided = true;
    }
    if(col.gameObject.name == "projectile"){
      Vector3 hided;
      hided.x = col.gameObject.transform.position.x;
      hided.y = -80.0f;
      hided.z = col.gameObject.transform.position.z;
      col.gameObject.transform.position = hided;
      m.choose_location = true;
    }
  }
}
