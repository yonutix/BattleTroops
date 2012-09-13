using UnityEngine;
using System.Collections;

public class MainBuilding : MonoBehaviour {
  public int life;
  public GameObject life_obj;
  public GameObject building;
  public bool showLabel=false;
  public float  timeUntilHide;
	// Use this for initialization
	
  void Start () {
    building = GameObject.Find("factory_3").gameObject;
    life_obj = building.transform.FindChild("life_1").gameObject;
	  life = 100;
    
	}
	
	// Update is called once per frame
	void Update () {
    Vector3 a;
   if (life>=0){ a.x = (float)life/100.0f;
    a.y = 1.0f;
    a.z = 1.0f;
	  life_obj.transform.localScale = a;
    }
    
    if (life<0) {
      
      if ( showLabel==false )
      { StartCoroutine("DisableAfterTime");
           
      }
    
    }
   else if ( showLabel ==true)
   {
     StopCoroutine("DisableAfterTime");
     showLabel = false;
      
   }
    
     
    
  }
  
  void OnTriggerEnter(Collider col) {
    life -= 10;
    if(life <= 50){
      ParticleEmitter p;
      p = GameObject.Find("InnerCore_factory").GetComponent<ParticleEmitter>();
      p.emit = true;
      p = GameObject.Find("OuterCore_factory").GetComponent<ParticleEmitter>();
      p.emit = true;
      p = GameObject.Find("InnerCore_factory2").GetComponent<ParticleEmitter>();
      p.emit = true;
      p = GameObject.Find("OuterCore_factory2").GetComponent<ParticleEmitter>();
      p.emit = true;
    }
           
  }

  
  
  void OnGUI () {
    if ( showLabel==true )
      GUI.Label(new Rect(0,200,250,40),"You demolished your building! Restarting now" );
       }
  
  
  
  IEnumerator DisableAfterTime()
{
  timeUntilHide = 3;

  showLabel = true;

  
  yield return new WaitForSeconds(timeUntilHide);

  showLabel = false;
  life=100;   
   
  }
  
  /*IEnumerator DisableAfterTime()
     {    
          yield return new WaitForSeconds(DisableTime);
          showLabel=false;
          life=100;
     }*/
  
  
  
}
