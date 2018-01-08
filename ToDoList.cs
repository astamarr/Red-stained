using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDoList : MonoBehaviour {

    public MeshRenderer Down;
   public PhoneIntro téléphone;

  
    public Material Downtriggered;


    public bool DrawMiddle;
    public bool DrawDown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



   public  void TicketFound()
    {
        AkSoundEngine.PostEvent("checklist_tick", this.gameObject);
        Down.material = Downtriggered;
        téléphone.isGrabbable = true;


    }
}
