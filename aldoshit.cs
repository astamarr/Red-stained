using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aldoshit : MonoBehaviour {

    Transform Origin;
    Random a;

    // Use this for initialization
    void Start () {
        Origin = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
       
      
            transform.position = new Vector3(transform.position.x, transform.position.y, 6f + Mathf.PingPong(Time.time, 4));



    }
}
