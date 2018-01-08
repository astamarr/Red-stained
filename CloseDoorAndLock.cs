using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CloseDoorAndLock : MonoBehaviour {

   public bool MustClose = false;
    public bool MustOpen = false;
    public bool Isclosed = false;
    public Vector3 target;
    public Vector3 targetOpen;
    public float speed;
    doorUsable door;

   public GameObject Player;

    private bool startopen = false;
    private bool startclose = false;
    // Use this for initialization
    void Start () {
        door = GetComponent<doorUsable>();
        target = transform.position;
        targetOpen = new Vector3(this.transform.position.x, this.transform.position.y, 10.223f);

    }
	
	// Update is called once per frame
	void Update () {
   
        if (!Isclosed)
        {
            if (MustClose == true && !MustOpen)
            {

                if(!startclose)
                {
                    AkSoundEngine.PostEvent("door_opening", this.gameObject);
                    startclose = true;
                }

               

                if ( transform.position.z > target.z + 0.1 )
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target, step);
                    Debug.Log("MOVING");

                }
                else
                {
                    AkSoundEngine.PostEvent("door_opening", this.gameObject);
                    transform.position = target;
                    AkSoundEngine.PostEvent("door_locked", this.gameObject);
                    Isclosed = true;
                    door.isGrabbable = false;
                    door.isUsable = true;




                }



            }
         


        }
        else if (MustOpen)
        {
            if(!startopen)
            {
                AkSoundEngine.PostEvent("door_opening_2", this.gameObject);
                door.isGrabbable = true;
                door.isUsable = false;
                startopen = true;
                AkSoundEngine.PostEvent("soundscape_2_begin", Player);

            }
            

            if (transform.position.z < targetOpen.z - 0.1)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetOpen, step);



            }
            else
            {
                MustOpen = false;
                transform.position = targetOpen;
              
                door.isGrabbable = false;
                door.isUsable = true;
                AkSoundEngine.PostEvent("door_opening", this.gameObject);




            }



        }



    }
}
