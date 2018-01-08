using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlacardEvier : VRTK_InteractableObject
{

    public bool Isdoorclosing = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);
        if(!Isdoorclosing)
        {
            AkSoundEngine.PostEvent("box_opening", this.gameObject);

        }
        else
        {
            AkSoundEngine.PostEvent("box_closing", this.gameObject);

        }
       
    }

    public override void Ungrabbed(GameObject previousGrabbingObject)
    {
        base.Ungrabbed(previousGrabbingObject);
    }
}
