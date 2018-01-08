using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class Mainmenu : MonoBehaviour {
    public GameObject Light;
   public  Slider Vol;
    public VRTK_UIPointer pointeurdroit;
    public VRTK_UIPointer pointeurgauche;

    bool Starting = false;
    public Animator volets;

    CanvasGroup group;
    bool fadeIn = false;
    bool fadeOut = false;
    // Use this for initialization
    void Start () {
       
     
        group = this.GetComponent<CanvasGroup>();
        AkSoundEngine.PostEvent("menu_music",this.gameObject);
        group.alpha = 0.0f;
        StartCoroutine(LaunchMenu());
    }
	
	// Update is called once per frame
	void Update () {



        if (fadeIn)
        {

            group.alpha = group.alpha + (Time.deltaTime / 8 ) ;
            if (group.alpha >= 1)
            {

                group.alpha = 1;
                fadeIn = false;
               
            }
        }
        if (fadeOut)
        {

            group.alpha = group.alpha - (Time.deltaTime / 3);
            if (group.alpha <= 0)
            {
                group.alpha = 0;
                fadeOut = false;
              
               
                pointeurdroit.gameObject.GetComponent<VRTK_Pointer>().enabled = true;
                pointeurgauche.gameObject.GetComponent<VRTK_Pointer>().enabled = true;
                this.gameObject.GetComponent<Canvas>().enabled = false;
                this.gameObject.GetComponent<VRTK_UICanvas>().enabled = false;
            }

        }



            if (Starting)
        {

        }

    }



    public void StartGame()
    {
        AkSoundEngine.PostEvent("menu_button", this.gameObject);


        StartCoroutine(vanishing());
        pointeurdroit.gameObject.GetComponent<VRTK_Pointer>().Toggle(false);
        pointeurdroit.gameObject.GetComponent<VRTK_StraightPointerRenderer>().enabled = false;
    }


    public void ChangeSoundLevel()
    {
      
        AkSoundEngine.SetRTPCValue("Volume", (int)Vol.value);

    }

    IEnumerator vanishing()
    {
        if (!Starting)
        {
           // pointeurdroit.gameObject.GetComponent<VRTK_Pointer>().Toggle(false);
            //pointeurgauche.gameObject.GetComponent<VRTK_Pointer>().Toggle(false);



            yield return new WaitForSeconds(3);
            AkSoundEngine.PostEvent("ambiance_room_tone_intro", this.gameObject);
            

            Starting = true;
        this.fadeOut = true;

  
        pointeurdroit.gameObject.GetComponent<VRTK_Pointer>().enabled = false;


        pointeurgauche.gameObject.GetComponent<VRTK_Pointer>().enabled = false;
        pointeurdroit.enabled = false;
        pointeurgauche.enabled = false;
        pointeurdroit.gameObject.GetComponent<VRTK_Pointer>().holdButtonToActivate = true;
        pointeurgauche.gameObject.GetComponent<VRTK_Pointer>().holdButtonToActivate = true;
        pointeurdroit.gameObject.GetComponent<VRTK_Pointer>().enableTeleport = true;
        pointeurgauche.gameObject.GetComponent<VRTK_Pointer>().enableTeleport = true;
        pointeurdroit.gameObject.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;



            yield return new WaitForSeconds(9);
            AkSoundEngine.PostEvent("loupiote", Light);
            Light.GetComponentInChildren<Light>().enabled = true;


        }


    }

    IEnumerator LaunchMenu()
    {

        yield return new WaitForSeconds(15);
      
        this.fadeIn = true;
        yield return new WaitForSeconds(2);
        pointeurdroit.gameObject.GetComponent<VRTK_Pointer>().Toggle(true);

    }
}
