using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssemblageVaisseau : Chassis {


	public structChassis chassiActuel;

	public Transform ReacteurGreen;

	public Transform React;

	public int numeroChassi;

	// Use this for initialization
	void Awake() 
	{
		pointDeVie = 10;	
		nbPointDattache = 2;

		//___CREATION DU CHASSIS___//
		//On crée un transform pour stocker le model3D du chassis dont on récupère le nom dans le script global Player
		chassis = Resources.Load<Transform> ("Vaisseau/" + GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().nomChassis + '/' + GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().nomChassis);
		//On instantie le chassis souhaité à une position de départ (que l'on pourra modifier par la suite)
		chassisCree = Instantiate (chassis, new Vector3(0, 0, /*-15*/ 0), new Quaternion (0, 0, 0, 0)) as Transform;
		//On le met maintenant comme enfant du Player
		chassisCree.parent = GameObject.FindWithTag ("Player").transform;

		// Tableau fixe donnant la position sur le vaisseau des différentes armes
		posArme = new List<Vector3>();
	
		ListTArme = new List<Transform>();

		//Initialisation avant récupération
		chassiActuel.nbPointDattache = 0;
		chassiActuel.pointDeVie = 0;
		chassiActuel.posArme = new List<Vector3>();
		chassiActuel.nbReacteurs = 0;
		chassiActuel.posReacteur = new List<Vector3>();

		string nom = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().nomChassis;
		int nbPointDattacheOLD = nbPointDattache;
		//On recupere les infos du chassi
		if (nom == "Chassi1") 
		{
			chassiActuel.nbPointDattache = GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c1.nbPointDattache;
			chassiActuel.pointDeVie =  GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c1.pointDeVie;
			for(int i = 0; i < chassiActuel.nbPointDattache; i++)
			{
				chassiActuel.posArme.Add(Vector2.zero);
				chassiActuel.posArme[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.posArme[i];
			}
			chassiActuel.nbReacteurs = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.nbReacteurs;
			for(int i = 0; i < chassiActuel.nbReacteurs; i++)
			{
				chassiActuel.posReacteur.Add(Vector2.zero);
				chassiActuel.posReacteur[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.posReacteur[i];
			}
			numeroChassi = 0;
		}
		if (nom == "Chassi2") 
		{
			chassiActuel.nbPointDattache = GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c2.nbPointDattache;
			chassiActuel.pointDeVie =  GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c2.pointDeVie;
			for(int i = 0; i < chassiActuel.nbPointDattache; i++)
			{
				chassiActuel.posArme.Add(Vector2.zero);
				chassiActuel.posArme[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.posArme [i];
			}
			chassiActuel.nbReacteurs = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.nbReacteurs;
			for(int i = 0; i < chassiActuel.nbReacteurs; i++)
			{
				chassiActuel.posReacteur.Add(Vector2.zero);
				chassiActuel.posReacteur[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.posReacteur[i];
			}
			numeroChassi = 1;
		}
		if (nom == "Chassi3") 
		{
			chassiActuel.nbPointDattache = GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c3.nbPointDattache;
			chassiActuel.pointDeVie =  GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c3.pointDeVie;
			for(int i = 0; i < chassiActuel.nbPointDattache; i++)
			{
				chassiActuel.posArme.Add(Vector2.zero);
				chassiActuel.posArme[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.posArme [i];
			}
			chassiActuel.nbReacteurs = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.nbReacteurs;
			for(int i = 0; i < chassiActuel.nbReacteurs; i++)
			{
				chassiActuel.posReacteur.Add(Vector2.zero);
				chassiActuel.posReacteur[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.posReacteur[i];
			}
			numeroChassi = 2;
		}

		//CREATION DES ARMES
		for(int i = 0; i < chassiActuel.nbPointDattache; i++)
		{
			posArme.Add(Vector3.zero);
			posArme [i] = chassiActuel.posArme [i];

			//CREATION DE L'ARME
			//On va chercher le nom de l'arme à placer à chaque position
			string arme = GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listArmeParVaisseau[numeroChassi][i].ToString();

			//On va créer physiquement l'arme dans le jeu qu'on va stocker dans un tableau de Transform pour pouvoir agir dessus (deplacement - suppression)
			Transform armeT = Resources.Load<Transform> ("Arme/" + arme);
			ListTArme.Add(Instantiate(armeT,  new Vector3(-0.5f, 0.05f, 0.5f), new Quaternion(0, 0, 0, 0)) as Transform);
			//on le place comme parent du chassis pour modifier sa position en local directement et le déplacer lorsque le vaisseau se déplace
			ListTArme[i].transform.parent = gameObject.transform;
			ListTArme[i].transform.localPosition = posArme [i];
			ListTArme[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("soundEffects");
		}

		//CREATION DES REACTEURS
		ReacteurGreen = Resources.Load<Transform> ("Particle/Réacteurs/RéacteurBlue");
		React = Instantiate(ReacteurGreen, Vector2.zero, new Quaternion(0, 0, 0, 0)) as Transform;
		React.transform.parent = gameObject.transform;
		React.transform.localPosition = chassiActuel.posReacteur [0];
	}
	
	public void Changement () 
	{
		for(int cptDes = 0; cptDes<chassiActuel.nbPointDattache; cptDes++)
		{
			//On va chercher le nom de l'arme à placer à chaque position
			Destroy(ListTArme[cptDes].gameObject);
		}

		//On change le chassi
		string nom = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().nomChassis;
		//On recupere les infos du chassi
		if (nom == "Chassi1") 
		{
			chassiActuel.nbPointDattache = GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c1.nbPointDattache;
			chassiActuel.pointDeVie =  GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c1.pointDeVie;
			for(int i = 0; i < chassiActuel.nbPointDattache; i++)
			{
				chassiActuel.posArme.Add(Vector2.zero);
				chassiActuel.posArme[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.posArme[i];
			}
			chassiActuel.nbReacteurs = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.nbReacteurs;
			for(int i = 0; i < chassiActuel.nbReacteurs; i++)
			{
				chassiActuel.posReacteur.Add(Vector2.zero);
				chassiActuel.posReacteur[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.posReacteur[i];
			}
			numeroChassi = 0;
		}
		if (nom == "Chassi2") 
		{
			chassiActuel.nbPointDattache = GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c2.nbPointDattache;
			chassiActuel.pointDeVie =  GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c2.pointDeVie;
			for(int i = 0; i < chassiActuel.nbPointDattache; i++)
			{
				chassiActuel.posArme.Add(Vector2.zero);
				chassiActuel.posArme[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.posArme [i];
			}
			chassiActuel.nbReacteurs = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.nbReacteurs;
			for(int i = 0; i < chassiActuel.nbReacteurs; i++)
			{
				chassiActuel.posReacteur.Add(Vector2.zero);
				chassiActuel.posReacteur[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.posReacteur[i];
			}
			numeroChassi = 1;
		}
		if (nom == "Chassi3") 
		{
			chassiActuel.nbPointDattache = GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c3.nbPointDattache;
			chassiActuel.pointDeVie =  GameObject.FindWithTag ("PlayerScript").GetComponent<ParametresChassis> ().c3.pointDeVie;
			for(int i = 0; i < chassiActuel.nbPointDattache; i++)
			{
				chassiActuel.posArme.Add(Vector2.zero);
				chassiActuel.posArme[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.posArme [i];
			}
			chassiActuel.nbReacteurs = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.nbReacteurs;
			for(int i = 0; i < chassiActuel.nbReacteurs; i++)
			{
				chassiActuel.posReacteur.Add(Vector2.zero);
				chassiActuel.posReacteur[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.posReacteur[i];
			}
			numeroChassi = 2;
		}

		gameObject.transform.localScale = new Vector3 (1, 1, 1);



		//On change le chassi (Destruction + création)
		//___DESTRUCTION DU CHASSI___//
		Destroy (chassisCree.gameObject	);
		//___CREATION DU CHASSI___//
		//On crée un transform pour stocker le model3D du chassis dont on récupère le nom dans le script global Player
		chassis = Resources.Load<Transform> ("Vaisseau/" + GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().nomChassis + '/' + GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().nomChassis);
		//On instantie le chassis souhaité à une position de départ (que l'on pourra modifier par la suite)
		chassisCree = Instantiate (chassis, new Vector3(0, 0, /*-15*/ 0), new Quaternion (0, 0, 0, 0)) as Transform;
		//On le met maintenant comme enfant du Player
		chassisCree.parent = GameObject.FindWithTag ("Player").transform;

		
		// Tableau fixe donnant la position sur le vaisseau des différentes armes
		posArme = new List<Vector3>();

		ListTArme.Clear ();
		for(int i = 0; i < chassiActuel.nbPointDattache; i++)
		{
			posArme.Add(Vector2.zero);

			posArme [i] = chassiActuel.posArme [i];

			//DESTRUCTION / CREATION DE L'ARME

			//On va chercher le nom de l'arme à placer à chaque position
			string arme = GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listArmeParVaisseau[numeroChassi][i].ToString();

		
			
			//On va créer physiquement l'arme dans le jeu
			//On utise un transform pour stocker le gameobject 
			//Pour pouvoir modifier sa position *
			ListTArme.Add (null);
			Transform armeT = Resources.Load<Transform> ("Arme/" + arme);
			ListTArme[i] = Instantiate(armeT, Vector3.zero, new Quaternion(0, 0, 0, 0)) as Transform;
			//on le place comme parent du chassis pour modifier sa position en local directement et le déplacer lorsque le vaisseau se déplace
			ListTArme[i].transform.parent = gameObject.transform;
			ListTArme[i].transform.localPosition = posArme [i];
		}

		//On aggrandit l'objet pour une meilleur visiblité des model3D :)
		gameObject.transform.localScale = new Vector3 (3, 3, 3);

		//DESTRUCTION / CREATION DES REACTEURS
		Destroy (React.gameObject);
		React = Instantiate(ReacteurGreen, Vector2.zero, new Quaternion(0, 0, 0, 0)) as Transform;
		React.transform.parent = gameObject.transform;
		React.transform.localPosition = chassiActuel.posReacteur [0];
	}
}
