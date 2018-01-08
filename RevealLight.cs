using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealLight : MonoBehaviour {

    public Transform tfLight;
    public bool ActivatedByLuminol = true;

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {

        if (tfLight)
        {
            this.GetComponent<Renderer>().material.SetVector("_LightPos", tfLight.position);
            this.GetComponent<Renderer>().material.SetVector("_LightDir", tfLight.forward);


       
        }
    }
    
 
 




}
