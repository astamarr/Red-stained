using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LastRoomEvents : MonoBehaviour {
    public CloseDoorAndLock door;
    public GameObject EnigmeManequins;
    public MannequinManager GoodManequin;
    bool AlreadyTriggered = false;
    public GameObject LampeMagique;

    public RevealLight[] stabher;

    public Lightnin eclairs;
    public GameObject DesactivateHand;
    public GameObject Herminette;

    public GameObject Player;

    public VRTK_BasicTeleport TPdesactivate;
    public GameObject [] Desactivate;

    public GeneriqueHandler gen;

    public bool IsEnding = false;
    private bool IsEndingStarted = false;






    public Canvas SubtitleCanvas;
    // Use this for initialization
    void Start () {
        StartCoroutine(debut());

        AkSoundEngine.PostEvent("soundscape_2_begin", Player.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if(GoodManequin.EnigmaCompleted && !AlreadyTriggered)
        {

            
            StartCoroutine(fin());

        }

        if(IsEnding && !IsEndingStarted)
        {

            IsEndingStarted = true;
            StartCoroutine(Generique());

        }

		
	}
    public IEnumerator debut()
    {
        EnigmeManequins.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        door.MustOpen = true;
        yield return new WaitForSeconds(2);

        eclairs.secondeclair();


    }

    public IEnumerator fin()
    {
        yield return new WaitForSeconds(3.0f);
        AlreadyTriggered = true;
        LampeMagique.SetActive(true);
        LampeMagique.GetComponent<ProgressiveLight>().Switchon = true;
        DesactivateHand.SetActive(false);
        Herminette.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        foreach (RevealLight a in stabher)

        {

            if (a)
            {
                a.enabled = true;

            }
        }

    }



        public IEnumerator Generique()
    {

        foreach(GameObject a in Desactivate)
        {

            if(a)
            {
                a.SetActive(false);

            }
        }
        TPdesactivate.enabled = false;
            yield return new WaitForSeconds(3.5f);
        gen.enabled = true;
        gen.StartCoroutine(gen.Gen());

    }

    }