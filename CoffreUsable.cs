using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CoffreUsable : VRTK_InteractableObject
{

    bool playing = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (playing && !AkSoundEngine.GetIsGameObjectActive(this.gameObject))

        {
            playing = false;
        
        }

    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
     


        if(!playing)
        {
            AkSoundEngine.PostEvent("box_closed", this.gameObject);
            playing = true;

        }
   
        }


    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);
      

    }




}
