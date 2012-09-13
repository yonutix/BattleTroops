using UnityEngine;
using System.Collections;

public class TimedGUI : MonoBehaviour {
 
  public int life;
  public float DisableTime=100.0f;
  public bool showLabel=false;
  public float  interval;
  public float timer=1.0f;
  public int ok=0;
	// Use this for initialization
	void Start () {
	 life = GameObject.Find("factory_3").GetComponent<MainBuilding>().life;
	 timer = Time.time + interval;
  }
	
	// Update is called once per frame
	void Update () {
	if (life<=0) 
    { if(Time.time >= timer)
      {timer = Time.time + interval;
      ok=1;
      }
      if (ok==1) showLabel =true;
      else {
        showLabel=false;
        //life=100;
        GameObject.Find("factory_3").GetComponent<MainBuilding>().life=100;  
      }
    }
      
     /* if ( !showLabel )
      { StartCoroutine("DisableAfterTime");
           
      }
    }
   
   else if ( showLabel )
   {
     StopCoroutine("DisableAfterTime");
     showLabel = false;
      
   }*/
	}
   
  





  
  void OnGUI () {
    if ( showLabel )
      GUI.Label(new Rect(50,100,250,40),"You demolished your building! Restarting now" );
       }
  
  
  /*IEnumerator DisableAfterTime()
     {    
          yield return new WaitForSeconds(DisableTime);
          showLabel=false;
          life=100;
     }
  
  */
}
