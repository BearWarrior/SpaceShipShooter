using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChassisEnemy : Chassis 
{
	private float bordLeftX;
	private float bordRightX;

	public string nom;

	public List<string> listNomArme;

	public float nbPointDeVieMax;

	public int degatCollision;
	public bool destrOnCol;

	// Use this for initialization
	void Awake() 
	{
		bordLeftX = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.x;
		bordRightX = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x;
		nbPointDattache = ListTArme.Count;
			
		// Compteur utilisé pour la création des armes (boucles)
		int compteurCreation = 0;

		// Tableau fixe donnant la position sur le vaisseau des différentes armes
		posArme = new List<Vector3>();
		for( compteurCreation =0; compteurCreation<nbPointDattache; compteurCreation++)
		{
			posArme.Add (ListTArme[compteurCreation].transform.localPosition);
		}

		nbPointDeVieMax = pointDeVie;

		for(int i = 0; i < ListTArme.Count; i++)
		{
			ListTArme[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("soundEffects");
		}
	}


	void Update () 
	{
		//Si l'enemy sort de la carte, on le détruit
		if (transform.position.x < bordLeftX - 0.5 || transform.position.x > bordRightX + 30)
		{
			Touche(500000, false); //false -> pas de loot si l'enemy sort de la carte
		}

		//Reload
		for(int i = 0; i < nbPointDattache; i++)
		{
			ListTArme[i].GetComponent<Arme>().Reload();
		}
	}

	public void Fire()
	{
		for(int i = 0; i < nbPointDattache; i++)
		{
			Vector3 vec = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			posArmeGlobal = vec + posArme[i];
			ListTArme[i].GetComponent<Arme>().Fire(posArmeGlobal, -1, 1, -ListTArme[i].transform.eulerAngles.z);
		}
	}

	public void FireFromTo(int from, int to)
	{
		if(to >= nbPointDattache)
		{
			to = nbPointDattache;
		}
		for(int i = from; i < to; i++)
		{
			posArmeGlobal = posArme[i] + new Vector3(transform.position.x, transform.position.y, transform.position.z);
			ListTArme[i].GetComponent<Arme>().Fire(posArmeGlobal, -1, 1, -ListTArme[i].transform.eulerAngles.z);
		}
	}
}
