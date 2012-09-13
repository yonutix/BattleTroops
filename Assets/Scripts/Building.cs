using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
  MainBuilding b;
	// Use this for initialization
	void Start () {
	  b = GameObject.Find("factory_3").GetComponent<MainBuilding>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
  void OnTriggerEnter(Collider col) {
    b.life -= 10;
    Debug.Log("Building");
  }
}
