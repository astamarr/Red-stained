using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightnin : MonoBehaviour {

   public float offMin = 20.0f; // Minimum wait time between each lightning/thunder
public float offMax = 30.0f; // Maximum wait time between each lightning/thunder
    public int range1 = 0;
    public int range2 = 360;
    public GameObject Emmissionsound;
    public bool Startthestorm = false;
    public Light arianalight;
    public Light Eclair2;
    public GameObject Torescale;



    // a DELETE
    private bool hasstared = false;



    private float onMin = 0.25f; // Minimum duration of lightning bolt flash
private float onMax = 2.0f; // Maximum duration of lightning bolt flash
   public GameObject Eclair;

	// Use this for initialization
	void Start () {

       
     
    }
	
	// Update is called once per frame
	void Update () {

        if (Startthestorm && !hasstared)
        {
            hasstared = true;
            StartCoroutine(lightz());

        }

    }

   public IEnumerator lightz()
    {
        
        Eclair.SetActive(true); // Show the lighning bolt particle effect 



        AkSoundEngine.PostEvent("thunder_hard", Emmissionsound);

        yield return new WaitForSeconds(0.3f);
        arianalight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        arianalight.enabled = false;
        yield return new WaitForSeconds(0.15f);
        arianalight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        arianalight.enabled = false;
        yield return new WaitForSeconds(0.15f);
        arianalight.enabled = true;
        yield return new WaitForSeconds(0.7f);
        arianalight.enabled = false;




        yield return new WaitForSeconds(0.5f);
        Torescale.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Eclair.SetActive(false);

     



        while (true)
        {
            yield return new WaitForSeconds(Random.Range(offMin, offMax));
            float duration = Random.Range(0.25f, 2);





            Eclair.SetActive(true); // Show the lighning bolt particle effect 


            

            yield return new WaitForSeconds(duration);

            Eclair.SetActive(false);

           
         AkSoundEngine.PostEvent("thunder_gentle", Emmissionsound);

         

        }
    }

    public IEnumerator EclairSurprise()
    {

        Eclair.SetActive(true); // Show the lighning bolt particle effect 



        AkSoundEngine.PostEvent("thunder_hard", Emmissionsound);

        yield return new WaitForSeconds(0.3f);
        Eclair2.enabled = true;
        yield return new WaitForSeconds(0.1f);
        Eclair2.enabled = false;
        yield return new WaitForSeconds(0.15f);
        Eclair2.enabled = true;
        yield return new WaitForSeconds(0.1f);
        Eclair2.enabled = false;
        yield return new WaitForSeconds(0.15f);
        Eclair2.enabled = true;
        yield return new WaitForSeconds(0.4f);
        Eclair2.enabled = false;




        yield return new WaitForSeconds(0.5f);
        Torescale.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Eclair.SetActive(false);

    }

    public void secondeclair()
    {

        StartCoroutine(EclairSurprise());
    }
}
