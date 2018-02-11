using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BeginScene : MonoBehaviour 
{

	float tempsAct;
	float tempsMax;
	 bool done;
	public bool shipArrived;

	public GameObject creaP;
	public GameObject flotte;

	// Use this for initialization
	void Start () 
	{
		//PLAYER
		creaP = new GameObject ("VaisseauJoueur");
		creaP.tag = "Player";
		creaP.AddComponent <ChassisPlayer>();
		float posXplayer = (GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.x - 2);
		creaP.transform.position = new Vector3 (posXplayer, 0, -15);
		GameObject.FindWithTag ("Player").GetComponent<ChassisPlayer> ().invulnerable = false;

		GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().checkFenetre ();

		tempsAct = 0;
		tempsMax = 0;
		done = false;
		shipArrived = false;

		if( GameObject.Find ("patternToLaunch").GetComponent<PatternToLaunch> ().flotte )
		{
			Camera.main.orthographicSize = 7.5f;
				
				
			//FLOTTE
			flotte = new GameObject ("Flotte");
			flotte.tag = "flotte";

			GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().checkFenetre ();
			flotte.transform.position = new Vector3(GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.x - 2, 0, flotte.transform.position.z);
			GameObject.FindWithTag ("Player").transform.parent = flotte.transform;
			flotte.AddComponent <FlotteController>();
			flotte.GetComponent<FlotteController>().deplacementOK = false;
			flotte.GetComponent<FlotteController> ().listAllie = new List<Transform> ();
			GameObject.FindWithTag ("Player").transform.localPosition = new Vector3 (0, 0, -15);
			//FLOTTE
				
			flotte.GetComponent<FlotteController>().player = GameObject.FindWithTag("Player").transform;
				
			//ALLIE
			//1
			GameObject creaA = new GameObject ("Allie1");
			creaA.tag = "Allie";
			creaA.AddComponent <ChassisAllie>();
			creaA.GetComponent<ChassisAllie>().chassisCree.parent = creaA.transform;
			creaA.GetComponent<ChassisAllie>().chassisCree.transform.localPosition = new Vector3 (0, 0, 0);
			creaA.GetComponent<ChassisAllie> ().nom = "Allie1";
			creaA.transform.position = new Vector3 (GameObject.FindWithTag ("Player").transform.position.x - 1f, -1f, -15);
			creaA.transform.parent = flotte.transform;
			creaA.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			flotte.GetComponent<FlotteController> ().listAllie.Add (creaA.transform);

			//2
			creaA = new GameObject ("Allie2");
			creaA.tag = "Allie";
			creaA.AddComponent <ChassisAllie>();
			creaA.GetComponent<ChassisAllie>().chassisCree.parent = creaA.transform;
			creaA.GetComponent<ChassisAllie>().chassisCree.transform.localPosition = new Vector3 (0, 0, 0);
			creaA.GetComponent<ChassisAllie> ().nom = "Allie2";
			creaA.transform.position = new Vector3 (GameObject.FindWithTag ("Player").transform.position.x - 1f, 1f, -15);
			creaA.transform.parent = flotte.transform;
			creaA.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			flotte.GetComponent<FlotteController> ().listAllie.Add (creaA.transform);
			//ALLIE
		}
		else
		{
			creaP.AddComponent <characterController>();
			creaP.GetComponent<characterController>().deplacementOK = false;
		}

		GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().checkFenetre ();
		

	}

	
	// Update is called once per frame
	void Update () 
	{
		//tant que le vaisseau n'est pas en position
		if(!shipArrived)
		{
			//si on a la flotte
			if( GameObject.Find ("patternToLaunch").GetComponent<PatternToLaunch> ().flotte )
			{
				//On le déplace a droite
				if(flotte.transform.position.x < (GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerDownLeft.x /2) )
				{
					flotte.transform.position = Vector3.Lerp (flotte.transform.position, new Vector3(1f, 0, 0), 0.5f*Time.deltaTime);
				}
				else // il est arrivé a droite, on le met controllable
				{
					flotte.transform.position = new Vector3((GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerDownLeft.x /2), 0, 0);
					flotte.GetComponent<FlotteController>().deplacementOK = true;
					flotte.GetComponent<FlotteController>().shootOK = true;
					shipArrived = true;
				}
			}
			else // on ne controle que le joueur
			{
				//On le déplace a droite
				if(creaP.transform.position.x < (GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerDownLeft.x /2))
				{
					creaP.transform.position = Vector3.Lerp (creaP.transform.position, new Vector3(1f, 0, 0), 0.5f*Time.deltaTime);
				}
				else // il est arrivé a droite, on le met controllable
				{
					creaP.transform.position = new Vector3((GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerDownLeft.x /2), 0, 0);
					//creaP.AddComponent ("characterController");
					creaP.GetComponent<characterController>().deplacementOK = true;
					creaP.GetComponent<characterController>().shootOK = true;
					shipArrived = true;
				}
			}
		}
		else //Vaisseau en position
		{
			if(!done)
			{
				if(tempsAct > tempsMax)
				{
                    //On lance le Pattern
                    //UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent (GameObject.FindWithTag ("Pattern"), "Assets/Script/BeginScene.cs (138,6)", GameObject.Find ("patternToLaunch").GetComponent<PatternToLaunch> ().nomPattern);

                    //IPattern pattern = GameObject.Find("patternToLaunch").GetComponent<PatternToLaunch>().nomPattern;
                    //System.Type patternType = pattern.GetType();

                    System.Type UserDefinedType = Type.GetType(GameObject.Find("patternToLaunch").GetComponent<PatternToLaunch>().nomPattern);

                    GameObject.FindWithTag("Pattern").AddComponent(UserDefinedType);


                    //On met a jour la fenetre
                    GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().checkFenetre ();
					
					//On met le booleen a true pour ne pas retourenr dans le if
					done = true;
					Destroy(this.GetComponent<BeginScene>());
				}
				else
				{
					tempsAct += Time.deltaTime;
				}
			}
		}
	}
}




