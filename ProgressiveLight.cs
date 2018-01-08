using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressiveLight : MonoBehaviour {

    float baseintensity;
    float Emissiveintensity;
    Light Lights;
    public bool Activatescript = true;
    public bool Switchon = false;
    public bool stop = false;
    public bool HaveEmissive = false;
    Material emissive;
    Color baseEmissiveColor;
    float baseemissivevalue = 0.9f;
    public float Step = 0.5f ;
    float emission;

    public bool alreadyon;


    // Use this for initialization
    void Start () {
        Step = 0.3f;
        Lights = this.GetComponentInChildren<Light>();
        baseintensity = Lights.intensity;
        if (Activatescript && !alreadyon)
        {
           
           

            
            Lights.intensity = 0;

        }
        if (HaveEmissive)
        {
            
            emissive = this.gameObject.GetComponentInChildren<hasemissive>().gameObject.GetComponent<Renderer>().material;
            if (!alreadyon)
            {



           
             emission = 0;
            baseEmissiveColor = emissive.GetColor("_EmissionColor"); //Replace this with whatever you want for your base color at emission level '1'
            
     

            Color finalColor = new Color(0,0,0) * Mathf.LinearToGammaSpace(0);
       

            emissive.SetColor("_EmissionColor", finalColor);
            }







        }
        
        


    }
	
	// Update is called once per frame
	void Update () {

        if (stop || !Activatescript) { return; }

        if(Switchon)
        {

            if(Lights.intensity >= baseintensity)
            {
                Lights.intensity = baseintensity;
                stop = true;
                if (HaveEmissive)
                {

                    Color baseColor = baseEmissiveColor; //Replace this with whatever you want for your base color at emission level '1'

                    Color finalColor = baseColor * Mathf.LinearToGammaSpace(0.9f);

                    emissive.SetColor("_EmissionColor", finalColor);
                }


            }
            else
            {

                Lights.intensity += Step * baseintensity * ( Time.deltaTime / 4 );

                
                if (HaveEmissive && Lights.intensity > baseintensity / 2)
                { 

                if (emission < baseemissivevalue)
                    {
                        emission += Time.deltaTime;

                    }
                else
                    {

                        emission = baseemissivevalue;
                    }
                emission +=  0.0000000000000001f ;
                Color baseColor = baseEmissiveColor; //Replace this with whatever you want for your base color at emission level '1'

                Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

                emissive.SetColor("_EmissionColor", finalColor);
                }

            }



        }
        else if (!Switchon && alreadyon)
        {

            if (Lights.intensity <= 0)
            {
                Lights.intensity = 0;
                stop = true;
                if (HaveEmissive)
                {

                    Color baseColor = baseEmissiveColor; //Replace this with whatever you want for your base color at emission level '1'

                    Color finalColor = baseColor * Mathf.LinearToGammaSpace(0);

                    emissive.SetColor("_EmissionColor", finalColor);
                }


            }
            else
            {

                Lights.intensity -= Step * baseintensity * (Time.deltaTime / 3);


                if (HaveEmissive && Lights.intensity < baseintensity / 2)
                {

                     

                   
              
                    Color baseColor = baseEmissiveColor; //Replace this with whatever you want for your base color at emission level '1'

                    Color finalColor = baseColor * Mathf.LinearToGammaSpace(0);

                    emissive.SetColor("_EmissionColor", finalColor);
                }

            }






        }
		
	}

    public void Switchonoff(bool a)
    {

        Switchon = a;
        stop = false;

    }







}
