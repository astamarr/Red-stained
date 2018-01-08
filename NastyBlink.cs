using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NastyBlink : MonoBehaviour {

    Light Thelight;
	// Use this for initialization
	void Start () {
        Thelight = this.gameObject.GetComponent<Light>();
        StartCoroutine(debut());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator debut()
    {
        while (this.enabled)
        {
            yield return new WaitForSeconds(20);
            Thelight.enabled = false;
            AkSoundEngine.PostEvent("loupiote", this.gameObject);
            yield return new WaitForSeconds(1f);
            Thelight.enabled = true;
            AkSoundEngine.PostEvent("loupiote", this.gameObject);


        }


    }
}
