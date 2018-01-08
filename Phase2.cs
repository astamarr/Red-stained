using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : MonoBehaviour {

    public GameObject[] SwitchObjectState;
   public plaquelumi plaque;


	// Use this for initialization
	void Start () {


        foreach(GameObject a in SwitchObjectState)
        {
            if(a)
            {
                a.SetActive(!a.activeSelf);

            }
           

        }
        plaque.enabled = true;




    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
