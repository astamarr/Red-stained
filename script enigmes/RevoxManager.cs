using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevoxManager : MonoBehaviour
{
    public bool Wrong = false;
    public bool Isplaying = false;
    public bool GoodSongIsTheSecond;

    public bool CurrentSongIsGoodSong;





    public bandesmagnetiques[] Bandes;
    public string Piste;



    private uint EventID;
    public float temps = 0;

    // Use this for initialization
    void Start()
    {
        EventID =  AkSoundEngine.PostEvent("revox_launch", this.gameObject);
        AkSoundEngine.SetRTPCValue("Volume_drums", 0);
        AkSoundEngine.SetRTPCValue("Volume_bass", 0);
        AkSoundEngine.SetRTPCValue("Volume_gratte", 0);
        AkSoundEngine.SetRTPCValue("Volume_organ", 0);







        if (Isplaying)
        {

            if (Wrong == true)
            {

                AkSoundEngine.SetRTPCValue(Piste, 3, this.gameObject);
                

                if (GoodSongIsTheSecond)
                {
                    CurrentSongIsGoodSong = true;

                }
                else
                {
                    CurrentSongIsGoodSong = false;

                }


            }
            else
            {


                AkSoundEngine.SetRTPCValue(Piste, 1.2f, this.gameObject);

                if (!GoodSongIsTheSecond)
                {
                    CurrentSongIsGoodSong = true;
                }
                else
                {
                    CurrentSongIsGoodSong = false;
                }




            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach (bandesmagnetiques bande in Bandes)
        {
            Debug.Log(bande.gameObject);
            bande.Backward = Wrong;

        }
        */
        if (Isplaying)
        {
            /*
            if (Wrong == true)
            {

                AkSoundEngine.SetRTPCValue(Piste, 3);

            }
            else
            {

                AkSoundEngine.SetRTPCValue(Piste, 1.2f);


            }
            */
        }

    }


    public void ChangerPiste()
    {
        Wrong = !Wrong;

        AkSoundEngine.PostEvent("revox_switch", this.gameObject);
        if (Isplaying)
        {
            
            if (Wrong == true)
            {

                AkSoundEngine.SetRTPCValue(Piste, 3, this.gameObject);

                if (GoodSongIsTheSecond)
                {
                    CurrentSongIsGoodSong = true;

                }
                else
                {
                    CurrentSongIsGoodSong = false;

                }



            }
            else
            {

                AkSoundEngine.SetRTPCValue(Piste, 1.2f, this.gameObject);


                if (!GoodSongIsTheSecond)
                {
                    CurrentSongIsGoodSong = true;
                }
                else
                {
                    CurrentSongIsGoodSong = false;
                }



            }

        }
    }


    public void MarcheArret()
    {


        Isplaying = !Isplaying;
      
        /*
        foreach (bandesmagnetiques bande in Bandes)
        {
            bande.IsPlaying = Isplaying;


        }
        */
        if (Isplaying)
        {

            if (Wrong == true)
            {

                AkSoundEngine.SetRTPCValue(Piste, 3, this.gameObject);

                if (GoodSongIsTheSecond)
                {
                    CurrentSongIsGoodSong = true;

                }
                else
                {
                    CurrentSongIsGoodSong = false;

                }




            }
            else
            {

                AkSoundEngine.SetRTPCValue(Piste, 1.2f,this.gameObject);



                if (!GoodSongIsTheSecond)
                {
                    CurrentSongIsGoodSong = true;
                }
                else
                {
                    CurrentSongIsGoodSong = false;
                }



            }




        }
        else
        {
            AkSoundEngine.SetRTPCValue(Piste, 0, this.gameObject);
            CurrentSongIsGoodSong = false;

        }

    }



    public void fin()
    {




        StartCoroutine(Finishing());





    }


    public IEnumerator Finishing()
    {
        /*
        // A DELETE QUAND LE SON SARRETERA
        yield return new WaitForSeconds(3f);
        AkSoundEngine.StopPlayingID(EventID,2);
        */
        //
        yield return new WaitForSeconds(2f);

        AkSoundEngine.PostEvent("revox_end", this.gameObject);
        foreach (bandesmagnetiques bande in Bandes)
        {

            bande.Rewind = true;

        }

        yield return new WaitForSeconds(30.606f);

        foreach (bandesmagnetiques bande in Bandes)
        {

            bande.enabled = false;

        }
      
        this.enabled = false;

    }
}
