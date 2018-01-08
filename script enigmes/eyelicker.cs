using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyelicker : MonoBehaviour {

    public GameObject ey;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        ey.transform.position = new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5));
		
	}
}
