using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bandesmagnetiques : MonoBehaviour {

   public bool Backward = false;
    public bool IsPlaying = true;
    public bool Rewind = false;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



        if (Rewind == true)
        {

            this.gameObject.transform.Rotate(new Vector3(800 * Time.deltaTime, 0, 0));

        }
        else
        {

            IsPlaying = this.gameObject.GetComponentInParent<RevoxManager>().Isplaying;

            if (IsPlaying && Backward == this.gameObject.GetComponentInParent<RevoxManager>().Wrong)
            {

                this.gameObject.transform.Rotate(new Vector3(-250 * Time.deltaTime, 0, 0));

            }


        }
        
    }
}
