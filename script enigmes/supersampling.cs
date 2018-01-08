using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class supersampling : MonoBehaviour {

    public float renderscale ;
	// Use this for initialization
	void Start () {

     


    }
	
	// Update is called once per frame
	void Update () {

        VRSettings.renderScale = renderscale;


    }
}
