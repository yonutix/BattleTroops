using UnityEngine;
using System.Collections;

public class Machine{
	public GameObject tnk;
  public Vector3 minPoint, maxPoint;
  public bool colided;
  public int life;
	public Machine(){
    minPoint.x = -2.82f;
    minPoint.y = 1.3f;
    minPoint.z = -7.63f;
    maxPoint.x = 3.0f;
    maxPoint.y = 1.3f;
    maxPoint.z = 5.25f;
    life = 100;
	}
}


public class TouchScreen : MonoBehaviour {
	public Machine myCar;
	float duration = 0.5f;
  
	Vector3 startPoint, endPoint;
  
	float startTime;
  
	Quaternion startRot, endRot;
  
	bool moveState;
	bool rotState;
  
	bool ok;
  bool fireMode;
  
  GameObject bullet;
  GameObject airplane;
  
  Vector3 bullet_inital_pozition;
  bool unloaded;
  Vector3 bullet_start, bullet_end;
	// Use this for initialization
  float translateDuration;
  GameObject projectile_bonus;
  float projectile_bonus_time_show;
  float projectile_bonus_time_hide;
  int score;
  HitPoints h;
  Vector3 target;
  public bool choose_location;
  Vector3 airplane_start_loc, airplane_end_loc;
  bool airplane_fly;
  float start_flight, flight_duration;
  bool game_started;
  public bool t3, t4, t5, t6, t7;
	void Start () {
    game_started = false;
    airplane = GameObject.Find("Airplane").gameObject;
		myCar = new Machine();
		myCar.tnk = GameObject.Find("M1_Abrams").gameObject;
		startTime = Time.time;
		startPoint = myCar.tnk.transform.position;
		moveState = false;
		rotState = false;
		startRot = myCar.tnk.transform.rotation;
		ok = false;
    fireMode = false;
    
    bullet = myCar.tnk.transform.FindChild("bullet").gameObject;
    bullet_inital_pozition = bullet.transform.position;
    unloaded = false;
    translateDuration = 0.0f;
    target = Vector3.zero;
    
    ParticleEmitter p;
    p = GameObject.Find("InnerCore").GetComponent<ParticleEmitter>();
    p.emit = false;
    p = GameObject.Find("OuterCore").GetComponent<ParticleEmitter>();
    p.emit = false;
    p = GameObject.Find("InnerCore_factory").GetComponent<ParticleEmitter>();
    p.emit = false;
    p = GameObject.Find("OuterCore_factory").GetComponent<ParticleEmitter>();
    p.emit = false;
    p = GameObject.Find("InnerCore_factory2").GetComponent<ParticleEmitter>();
    p.emit = false;
    p = GameObject.Find("OuterCore_factory2").GetComponent<ParticleEmitter>();
    p.emit = false;
    
    projectile_bonus = GameObject.Find("projectile").gameObject;
    projectile_bonus.animation.Play("projectile_bonus");
    projectile_bonus.animation.wrapMode = WrapMode.Loop;
    
    projectile_bonus_time_show = Time.time;
    projectile_bonus_time_hide = -1.0f;
    
    //getting score;
    h=GameObject.Find("M1_Abrams").GetComponent<HitPoints>(); 
    choose_location = false;
    airplane_end_loc = airplane_start_loc = airplane.transform.position;
    airplane_fly = false;
    flight_duration = 6.0f;
    start_flight = -1.0f;
    t3 = t4 = t5 = t6 = t7 = false;
	}
  
  void playStartAnimation(){
    
    GameObject propeller = GameObject.Find("propeller_obj").gameObject;
    
    propeller.animation.Play("pole_propeller");
    propeller.animation.wrapMode = WrapMode.Loop;
    
    
    GameObject enemy = GameObject.Find("enemy1").gameObject;
    enemy.animation.Play("enemy1");
    enemy.animation.wrapMode = WrapMode.Loop;
    
    
    GameObject.Find("wall_steel_east").gameObject.animation.Play("wall_east_anim");
    GameObject.Find("wall_steel_east").gameObject.animation.wrapMode = WrapMode.Once;
    Debug.Log("Anim1");
  
    GameObject.Find("wall_steel_west").gameObject.animation.Play("wall_west_anim");
    GameObject.Find("wall_steel_west").gameObject.animation.wrapMode = WrapMode.Once;
    
    Debug.Log("Anim2");
    GameObject.Find("wall_steel_north").gameObject.animation.Play("wall_north_anim");
    GameObject.Find("wall_steel_north").gameObject.animation.wrapMode = WrapMode.Once;
    Debug.Log("Anim3");
    GameObject.Find("wall_steel_south").gameObject.animation.Play("wall_south_anim");
    GameObject.Find("wall_steel_south").gameObject.animation.wrapMode = WrapMode.Once;
    Debug.Log("Anim4");
    /*
    GameObject.Find("pCube1x").gameObject.animation.Play("crane_body");
    GameObject.Find("pCube1x").gameObject.animation.wrapMode = WrapMode.Once;
    */
    GameObject.Find("crane_up").gameObject.animation.Play("crane_up");
    GameObject.Find("crane_up").gameObject.animation.wrapMode = WrapMode.Once;
    Debug.Log("Anim5");
    GameObject.Find("factory_1").gameObject.animation.Play("cover");
    GameObject.Find("factory_1").gameObject.animation.wrapMode = WrapMode.Once;
    Debug.Log("Anim6");
    GameObject.Find("factory_2").gameObject.animation.Play("side_base");
    GameObject.Find("factory_2").gameObject.animation.wrapMode = WrapMode.Once;
    Debug.Log("Anim7");
    GameObject.Find("factory_4").gameObject.animation.Play("back_base");
    GameObject.Find("factory_4").gameObject.animation.wrapMode = WrapMode.Once;
    Debug.Log("Anim8");
    GameObject.Find("factory_5a").gameObject.animation.Play("base_gate-1");
    GameObject.Find("factory_5a").gameObject.animation.wrapMode = WrapMode.Once;
    Debug.Log("Anim9");
    GameObject.Find("factory_5b").gameObject.animation.Play("base_gate_2");
    GameObject.Find("factory_5b").gameObject.animation.wrapMode = WrapMode.Once;
  }
  
	// Update is called once per frame
	void Update () {
    if(Time.time >= projectile_bonus_time_show + 20 && projectile_bonus_time_show != -1){
      projectile_bonus_time_show = -1.0f;
      projectile_bonus_time_hide = Time.time;
      Vector3 temp;
      temp.x = projectile_bonus.transform.position.x;
      temp.y = -80.0f;
      temp.z = projectile_bonus.transform.position.z;
      projectile_bonus.transform.position = temp;
    }
    
    if(projectile_bonus_time_hide + 100 <= Time.time && projectile_bonus_time_hide != -1){
      projectile_bonus_time_show = Time.time;
      projectile_bonus_time_hide = -1.0f;
      Vector3 temp;
      temp.x = projectile_bonus.transform.position.x;
      temp.y = 0.0f;
      temp.z = projectile_bonus.transform.position.z;
      projectile_bonus.transform.position = temp;
    }
    
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				// Construct a ray from the current touch coordinates
        if(touch.position.x > 150 || touch.position.y < Screen.height - 100){
          
  				var ray = Camera.main.ScreenPointToRay (touch.position);
  				Plane hPlane = new Plane(Vector3.up, Vector3.zero);
  				float distance = 0;
  				if (hPlane.Raycast(ray, out distance)){
            
  				  var temp = ray.GetPoint(distance);
  					temp.y = 1.3f;
           if(game_started){
             if(!choose_location){
      					startTime = Time.time;
      					startPoint = myCar.tnk.transform.position;
      					endPoint = temp;
                translateDuration = Vector3.Distance(startPoint, endPoint)/50;
                Vector3 a1;
                a1.x = 0.0f;
                a1.y = 0.0f;
                a1.z = 1.0f;
                Vector3 direction = endPoint-startPoint;
                startRot = myCar.tnk.transform.rotation;
                if(direction.x < 0){
                  endRot = Quaternion.AngleAxis(360-Vector3.Angle(direction, a1), Vector3.up);
                }
                else{
                  endRot = Quaternion.AngleAxis(Vector3.Angle(direction, a1), Vector3.up);
                }
                rotState = true;
                myCar.colided = false;
      					ok = false;
      					moveState = false;
                if(fireMode && !unloaded){
                  bullet_start = bullet.transform.position;
                  bullet_end = temp;
                  unloaded = true;
                  bullet_inital_pozition = bullet.transform.position;
                  endPoint = myCar.tnk.transform.position;
                }
              }
              else{
                target = temp;
                airplane_fly = true;
                start_flight = Time.time;
                choose_location = false;
              }
            }
            else{
              game_started = true;
              playStartAnimation();
            }
  				}
  			}
      }
		}
    if(airplane_fly){
      GameObject.Find("Airplane").gameObject.animation.Play("airplane");
      airplane_fly = false;
    }
    if(Time.time - start_flight >= 2.0f && start_flight != -1){
      Debug.Log ("Nu inca");
      Vector3 target_poz;
      target_poz = target;
      target_poz.y = 1.3f;
      GameObject.Find("Flame3").gameObject.transform.position = target_poz;
      ParticleEmitter p;
      p = GameObject.Find("InnerCore3").GetComponent<ParticleEmitter>();
      p.emit = true;
      p = GameObject.Find("OuterCore3").GetComponent<ParticleEmitter>();
      p.emit = true;
      Debug.Log ("Nu inca1");
    }
    if(Time.time - start_flight >= 2.9f && start_flight != -1){
      Debug.Log("Acum am zis");
      Vector3 target_poz;
      target_poz = target;
      target_poz.y = -80f;
      GameObject.Find("Flame3").gameObject.transform.position = target_poz;
      start_flight = -1.0f;
      ParticleEmitter p;
      p = GameObject.Find("InnerCore3").GetComponent<ParticleEmitter>();
      p.emit = false;
      p = GameObject.Find("OuterCore3").GetComponent<ParticleEmitter>();
      p.emit = false;
      Debug.Log("Acum am zis2");
    }
		if(rotState){
			Quaternion rot = Quaternion.Lerp(startRot, endRot, (Time.time - startTime) / duration);
			if((rot == myCar.tnk.transform.rotation) && ok){
				rotState = false;
			}
      if(!ok && Time.time - startTime >= duration){
        rotState = false;
        startTime = Time.time;
      }
			myCar.tnk.transform.rotation = rot;
			ok = !fireMode;
      bullet_inital_pozition = bullet.transform.position;
		}
		else{
  			if(ok){
  				rotState = false;
  				moveState = true;
  				startTime = Time.time;
  				ok = false;
          
  			}
  				
  			Vector3 poz = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime ) / translateDuration);
        Vector3 undo_poz = myCar.tnk.transform.position;
  			if(!myCar.colided && moveState){
  				myCar.tnk.transform.position = poz;
  			}
        if(myCar.colided && moveState){
          myCar.tnk.transform.position = undo_poz;
          moveState = false;
          myCar.colided  = false;
        }
      if(unloaded){
        ParticleEmitter p;
        p = GameObject.Find("InnerCore").GetComponent<ParticleEmitter>();
        p.emit = true;
        p = GameObject.Find("OuterCore").GetComponent<ParticleEmitter>();
        p.emit = true;
        Vector3 b_poz = Vector3.Lerp(bullet_start, bullet_end, (Time.time - startTime ) / duration);
        bullet.transform.position = b_poz;
        if(b_poz == bullet_end){
          unloaded = false;
          bullet.transform.position = bullet_inital_pozition;
          p = GameObject.Find("InnerCore").GetComponent<ParticleEmitter>();
          p.emit = false;
          p = GameObject.Find("OuterCore").GetComponent<ParticleEmitter>();
          p.emit = false;
        }
      }
      
		}
    
    //getting updated score
    score=h.score;
	}
    void OnGUI () {
     if (GUI.Button (new Rect (0,0,150,100), "Fire mode on/off")) {
      fireMode = !fireMode;
   }
    if(!game_started){
      GUI.Label(new Rect(200,200,300,40),"Touch to start game");
    }
    if(choose_location){
      GUI.Label(new Rect(200,200,300,40),"Select target");
    }
    GUI.Label(new Rect(0,100,250,40),"Your score is: " + score.ToString());
 }
}