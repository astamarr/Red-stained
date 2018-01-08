using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinklight : MonoBehaviour {

    bool used = false;
   public Material mat;
    Renderer rend;
    float floor = 0.3f;
    float ceiling = 1.0f;
    public bool crazy = false;
    private bool startedcrazy = false;
    Color baseColorcrazy;

    // Use this for initialization
    void Start () {
        
         rend = GetComponent<Renderer>();
        mat = rend.material;

    }
	
	// Update is called once per frame
	void Update () {
        if (!used && !crazy)
        {
            float emission = floor + Mathf.PingPong(Time.time, ceiling - floor);
            Color baseColor = Color.red; //Replace this with whatever you want for your base color at emission level '1'

            Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

            mat.SetColor("_EmissionColor", finalColor);



        }
        else if (used && !crazy)
        {
            float emission = 0;
            Color baseColor = Color.black; //Replace this with whatever you want for your base color at emission level '1'

            Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

            mat.SetColor("_EmissionColor", finalColor);
        }
        else if (crazy)
        { 
            if(!startedcrazy)
            {
                startedcrazy = true;
                 baseColorcrazy = mat.GetColor("_EmissionColor");

            }
            floor = 0.6f;
            ceiling = 1.0f;
            float emission = floor + Mathf.PingPong(Time.time * 4, ceiling - floor);
           

            Color finalColor = baseColorcrazy * Mathf.LinearToGammaSpace(emission);

            mat.SetColor("_EmissionColor", finalColor);

        }
    }

    public void usage(bool a)
    {
        used = a;  
    }




}
