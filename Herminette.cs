using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Herminette : VRTK_InteractableObject
{

    public bool HasbeenGrabbed = false;

    public GameObject Player;
    public GameObject ariane;

    public LastRoomEvents Generique;
    private bool end = false;


    public VRTK_ControllerActions controllerActions;
    public VRTK_HeadsetFade fading;
    private float impactMagnifier = 120f;
    private float collisionForce = 0f;
    private float maxCollisionForce = 4000f;
    // Use this for initialization
    void Start()
    {

        AkSoundEngine.PostEvent("drop_herminette", this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Awake()
    {
        base.Awake();
        interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);

        if (!HasbeenGrabbed)
        {
            AkSoundEngine.PostEvent("soundscape_2_end", Player);
            HasbeenGrabbed = true;



        }
        controllerActions = currentGrabbingObject.GetComponent<VRTK_ControllerActions>();
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(" FORCE start   " + collisionForce);


        if (other.tag == "Ariana")
        {
            if (controllerActions && IsGrabbed())
            {
                collisionForce = VRTK_DeviceFinder.GetControllerVelocity(controllerActions.gameObject).magnitude * impactMagnifier;


                if (collisionForce > 235 && !end)
                {
                    end = true;
                    StartCoroutine(endgame());
                }

            }



        }


    }



    public IEnumerator endgame()
    {


        StartCoroutine(vibrations());


        fading.Fade(Color.black, 0.1f);
        AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent("stab", ariane);
        StartCoroutine(Generique.Generique());
        Generique.IsEnding = true;
        yield return new WaitForSeconds(2);

        while (this.enabled == true)
        {
            yield return new WaitForSeconds(0.5f);
            fading.Fade(Color.black, 0.1f);
        }



    }

    public IEnumerator vibrations()
    {
        yield return new WaitForSeconds(0.1f);
        controllerActions.TriggerHapticPulse(2f,0.4f,0.01f);
        yield return new WaitForSeconds(1f);
        

    }
}