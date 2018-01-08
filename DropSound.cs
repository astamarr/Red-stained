using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DropSound : MonoBehaviour {
    VRTK_InteractableObject vr;
    public string FallSound;
    private bool  cooldown = false;
    private bool started = false;

    // Use this for initialization
    void Start () {

        vr = GetComponent<VRTK_InteractableObject>();
        StartCoroutine(starting());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (!vr.IsGrabbed() && !cooldown && started)
        {
            AkSoundEngine.SetSwitch("Falling_objects", FallSound, this.gameObject);
            AkSoundEngine.PostEvent("objects_falling", this.gameObject);
            cooldown = true;
            StartCoroutine(cdd());



        }
    }


        public IEnumerator cdd()
        {


            yield return new WaitForSeconds(1.5f);
             cooldown = false;





    }

    public IEnumerator starting()
    {


        yield return new WaitForSeconds(3f);
        started = true;





    }



}

