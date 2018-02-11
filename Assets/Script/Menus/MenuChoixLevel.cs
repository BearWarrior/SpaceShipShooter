using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuChoixLevel : MonoBehaviour 
{
	public List<Rect> rectButtonLevel;

	public Rect nextSolarSystem;
	public Rect prevSolarSystem;

	public int numSS;
	public int nbSS;

	public Rect rectLabelNomSS;
	

	public Rect rectButtonRetour;

	public GameObject patternToLaunch;

	public GUISkin skin;
	public GUISkin skinlabelDescr;

	private bool escapePush;

	// Use this for initialization
	void Start ()
	{
		escapePush = false;

		nbSS = 2;
		GameObject solarSystem = Instantiate (Resources.Load ("SolarSystem/SolarSystem1"), new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
		solarSystem.name = "SolarSystem";
		numSS = 1;

		rectButtonLevel = new List<Rect> ();
		for(int i =0; i < GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().nbPlanet; i++)
		{
			rectButtonLevel.Add(new Rect(Camera.main.WorldToScreenPoint (GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().listPlanet[i].transform.position).x - 150, GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().HAUTEURpx - Camera.main.WorldToScreenPoint (GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().listPlanet[i].transform.position).y + GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().listDecalage[i], 300, 40));
		}

		rectButtonRetour = new Rect (Screen.width - 325, Screen.height - 65, 300, 40);
		nextSolarSystem = new Rect (Screen.width - 225, 25, 200, 40);
		prevSolarSystem = new Rect (25, 25, 200, 40);
		rectLabelNomSS = new Rect (Screen.width/2 - 200, 25, 400, 60);

		//Si c'est le premier niveau joué, on instancie 
		if(GameObject.Find("patternToLaunch") == null)
		{
			patternToLaunch = new GameObject ("patternToLaunch");
			DontDestroyOnLoad (patternToLaunch.gameObject);
			patternToLaunch.AddComponent <PatternToLaunch>();
		}


		skin = Resources.Load ("Menu/SkinMenu") as GUISkin;
		skinlabelDescr = Resources.Load ("Menu/SkinMenuAchat") as GUISkin;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			escapePush = true;
		}
		else
		{
			escapePush = false;
		}
	}
	
	void OnGUI()
	{

		GUI.skin = skin;

		//SUIVANT PRECEDENT
		if(numSS < nbSS)
		{
			if(GUI.Button (nextSolarSystem, "Suivant"))
			{
				numSS++;
				Destroy(GameObject.Find("SolarSystem").gameObject);
				GameObject solarSystem = Instantiate (Resources.Load ("SolarSystem/SolarSystem" + numSS.ToString()), new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
				solarSystem.name = "SolarSystem";
			}
		}
		if(numSS > 1)
		{
			if(GUI.Button (prevSolarSystem, "Precedent"))
			{
				numSS--;
				Destroy(GameObject.Find("SolarSystem").gameObject);
				GameObject solarSystem = Instantiate (Resources.Load ("SolarSystem/SolarSystem" + numSS.ToString()), new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
				solarSystem.name = "SolarSystem";
			}
		}

		//CALCUL DES POS DES BOUTONS
		rectButtonLevel.Clear();
		rectButtonLevel = new List<Rect> ();
		for(int i =0; i < GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().nbPlanet; i++)
		{
			rectButtonLevel.Add(new Rect(Camera.main.WorldToScreenPoint (GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().listPlanet[i].transform.position).x - 150, GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().HAUTEURpx - Camera.main.WorldToScreenPoint (GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().listPlanet[i].transform.position).y + GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().listDecalage[i], 300, 40));
		}

		//BOUTON DES NIVEAUX
		for(int i = 0; i < GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().nbPlanet; i++)
		{
			if(GUI.Button (rectButtonLevel[i], GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().nomAff[i]))
			{
				GameObject patternToLaunch = GameObject.Find("patternToLaunch");
				patternToLaunch.GetComponent<PatternToLaunch>().nomPattern = GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().nomReel[i];
				patternToLaunch.GetComponent<PatternToLaunch>().flotte = GameObject.Find("SolarSystem").GetComponent<CaracSolarSystem>().flotte[i];
				Application.LoadLevel("SHOOT");
			}
		}

		//RETOUR
		if(GUI.Button (rectButtonRetour, "Retour") || escapePush)
		{
			GetComponent<AudioSource>().Play();
			GameObject.Find("LightInter").GetComponent<Light>().enabled = true;
			GameObject.Find("LightG1").GetComponent<Light>().enabled = false;
			Destroy (GameObject.Find("SolarSystem").gameObject);
			GetComponent<MenuIntermediaire>().enabled = true;
			Destroy(GetComponent<MenuChoixLevel>());
		}

		//NOM DU SYSTEM SOLAIRE
		GUI.skin = skinlabelDescr;
		GUI.Label (rectLabelNomSS, "Système solaire : " + GameObject.Find ("SolarSystem").GetComponent<CaracSolarSystem> ().nom);
		GUI.skin = skin;
	}
}