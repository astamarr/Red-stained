using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
     public GameObject TPFirstRoom;
    public GameObject AldoMovingShit;
    public bool playerHere = false;
    bool fired = false;
    public CloseDoorAndLock door;
    public Light[] offlights;

    public hasemissive[] emissoff;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !fired)
        {

            Debug.Log("HES HERE OMG");
            playerHere = true;
            StartCoroutine(closing());

        }

    }

    private void OnTriggerLeave(Collider other)
    {

        if (other.gameObject.tag == "Player" && !fired)
        {

           
            playerHere = false;

        }

    }

    public IEnumerator closing()
    {
        TPFirstRoom.SetActive(false);
        yield return new WaitForSeconds(5);

        if (playerHere)
        {
            Debug.Log("mustlock");
            door.MustClose = true;

            fired = true;
            
            yield return new WaitForSeconds(8);
            AkSoundEngine.PostEvent("aldo_movingshit", AldoMovingShit);
           

            foreach(Light l in offlights)
            {

                if (l)
                {

                    l.enabled = false;
                }
            }

            foreach (hasemissive a in emissoff)
            {
                if(a)
                {
                    Color finalColor = new Color(0, 0, 0) * Mathf.LinearToGammaSpace(0);

                    a.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", finalColor);
    
                }




            }







            TPFirstRoom.SetActive(true);
            this.gameObject.SetActive(false);


        }



    }

    public IEnumerator opening()
    {

        yield return new WaitForSeconds(5);

        if (playerHere)
        {
            Debug.Log("mustlock");
            door.MustOpen = true;

       
            TPFirstRoom.SetActive(true);


        }



    }
}
