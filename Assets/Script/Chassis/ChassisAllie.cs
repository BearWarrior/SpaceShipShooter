using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChassisAllie : Chassis 
{

	public structChassis chassiActuel;

	public Transform ReacteurGreen;
	
	public Transform React;
	

	private List<bool> fumeeAff;
	private List<Transform> fumeeT;
	public Transform fumeeObj;

	public float vitesse;

	public int numeroChassi;

	public string nom;

	public Transform chassiCree;

	// Use this for initialization
	void Awake() 
	{
		//___CREATION DU CHASSIS___//
		//On crée un transform pour stocker le model3D du chassis dont on récupère le nom dans le script global Player
		Transform chassis = Resources.Load<Transform> ("Vaisseau/" + GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().nomChassis  + '/' + GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().nomChassis);
		//On instantie le chassis souhaité à une position de départ (que l'on pourra modifier par la suite)  -> 1/4 de l'écran a gauche et centré en hauteur
		float posXplayer = (GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x - GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.x) / 4 - GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x;
		chassisCree = Instantiate (chassis, new Vector3(posXplayer, 0, -15), new Quaternion (0, 0, 0, 0)) as Transform;




		chassisCree.tag = "ChassiAllie";

		//On stock ici la position des armes 
		posArme = new List<Vector3>();
		
		//Liste des armes
		ListTArme = new List<Transform>();
		
		//On initialise la struc
		chassiActuel.nbPointDattache = 0;
		chassiActuel.pointDeVie = 0;
		chassiActuel.posArme = new List<Vector3>();
		chassiActuel.nbReacteurs = 0;
		chassiActuel.posReacteur = new List<Vector3>();
		chassiActuel.nbFumee = 0;
		chassiActuel.posFumées = new List<Vector3>();

		//On récupère le nom du chassi pour l'analyser 
		string nom = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().nomChassis;

		//On recupere les infos du chassi (à partir du nom) dans le script parametreChassis
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
			chassiActuel.nbFumee = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.nbFumee;
			for(int i = 0; i < chassiActuel.nbFumee; i++)
			{
				chassiActuel.posFumées.Add(Vector3.zero);
				chassiActuel.posFumées[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.posFumées[i];
			}
			chassiActuel.vitesse = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.vitesse;
			numeroChassi= 0;

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
			chassiActuel.nbFumee = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.nbFumee;
			for(int i = 0; i < chassiActuel.nbFumee; i++)
			{
				chassiActuel.posFumées.Add(Vector3.zero);
				chassiActuel.posFumées[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.posFumées[i];
			}
			chassiActuel.vitesse = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.vitesse;
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
			chassiActuel.nbFumee = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.nbFumee;
			for(int i = 0; i < chassiActuel.nbFumee; i++)
			{
				chassiActuel.posFumées.Add(Vector3.zero);
				chassiActuel.posFumées[i] = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.posFumées[i];
			}
			chassiActuel.vitesse = GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.vitesse;
			numeroChassi = 2;
		}
	
		//CREATION DES ARMES
		for(int i = 0; i < chassiActuel.nbPointDattache; i++)
		{
			//On récupère les postions
			posArme.Add(Vector2.zero);
			posArme [i] = chassiActuel.posArme [i];
			
			//CREATION DE L'ARME
			//On va chercher le nom de l'arme à placer à chaque position
			string arme = GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listArmeParVaisseau[numeroChassi][i].ToString();

			//On va créer physiquement l'arme dans le jeu qu'on va stocker dans un tableau de Transform pour pouvoir agir dessus (deplacement - suppression)
			Transform armeT = Resources.Load<Transform> ("Arme/" + arme);
			ListTArme.Add(Instantiate(armeT, Vector3.zero, new Quaternion(0, 0, 0, 0)) as Transform);
			//on le place comme parent du chassis pour modifier sa position en local directement et le déplacer lorsque le vaisseau se déplace
			ListTArme[i].transform.parent = gameObject.transform;
			ListTArme[i].transform.localPosition = posArme [i];
		}

		//CREATION DES REACTEURS
		ReacteurGreen = Resources.Load<Transform> ("Particle/Réacteurs/RéacteurBlue");
		React = Instantiate(ReacteurGreen, new Vector3(-0.5f, 0.05f, 0.5f), new Quaternion(0, 0, 0, 0)) as Transform;
		React.transform.parent = gameObject.transform;
		React.transform.localPosition = chassiActuel.posReacteur [0];

		pointDeVie = chassiActuel.pointDeVie;

		//Gestion fumée degats		
		fumeeAff = new List<bool>();
		fumeeT = new List<Transform> ();
		for(int i = 0; i < chassiActuel.nbFumee; i++)
		{
			fumeeAff.Add(false);
			fumeeT.Add(null);
		}

		fumeeObj = Resources.Load<Transform>("Particle/Smoke");
	}

	
	void Update () 
	{
		if(changerMun)
		{
			if(tempsBonusActuel > tempsBonusMax)
			{
				changerMun = false;
				bonusMult = 1;
				tempsBonusActuel = 0;
			}
			else
			{
				tempsBonusActuel += Time.deltaTime;
			}
		}

		if(PlayerPrefs.GetInt ("tirAuto") == 1) //Tir Automatique activé
		{
			if(GameObject.Find("Flotte").GetComponent<FlotteController>().shootOK)
			{
				//On tir seulement si le menu pause n'est pas affiché (et existe)
				if(GameObject.FindWithTag("Pause") != null)
				{
					if(GameObject.FindWithTag("Pause").GetComponent<MenuPause>().pause == false)
					{
						//A la rigueur, vérifier si l'emplacement est plein ou vide
						for(int i = 0; i < chassiActuel.nbPointDattache; i++)
						{
							posArmeGlobal = posArme[i] + new Vector3(GameObject.Find(nom).transform.position.x, GameObject.Find(nom).transform.position.y, GameObject.Find(nom).transform.position.z);
							ListTArme [i].gameObject.GetComponent<Arme>().Fire(posArmeGlobal, 1, bonusMult, ListTArme[i].transform.eulerAngles.z);	
						}
					}
				}
			}
		}
		else
		{
			//On tir si il appuie sur clic gauche, entrée, entrée pav num ou espace 
			if(Input.GetMouseButton(0) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter))
			{
				if(GameObject.Find("Flotte").GetComponent<FlotteController>().shootOK)
				{
					//On tir seulement si le menu pause n'est pas affiché (et existe)
					if(GameObject.FindWithTag("Pause") != null)
					{
						if(GameObject.FindWithTag("Pause").GetComponent<MenuPause>().pause == false)
						{
							//A la rigueur, vérifier si l'emplacement est plein ou vide
							for(int i = 0; i < chassiActuel.nbPointDattache; i++)
							{
								posArmeGlobal = posArme[i] + new Vector3(GameObject.Find(nom).transform.position.x, GameObject.Find(nom).transform.position.y, GameObject.Find(nom).transform.position.z);
								ListTArme [i].gameObject.GetComponent<Arme>().Fire(posArmeGlobal, 1, bonusMult, ListTArme[i].transform.eulerAngles.z);	
							}
						}
					}
				}
			}
		}

		//On recharge toutes les armes
		for(int i = 0; i < chassiActuel.nbPointDattache; i++)
		{
			ListTArme [i].gameObject.GetComponent<Arme>().Reload();
		}

		gestionFumeeDegats ();
	}




	//gestion de l'apparition de fumée si les degats depassent certauins seuils
	private void gestionFumeeDegats()
	{
		//On vérifie quelle fumée afficher
		if(pointDeVie < chassiActuel.pointDeVie / 4 )
		{
			fumeeAff[0] = true;
			fumeeAff[1] = true;
			fumeeAff[2] = true;
		}
		else
		{
			if(pointDeVie < 2*chassiActuel.pointDeVie / 4 )
			{
				fumeeAff[0] = true;
				fumeeAff[1] = true;
				fumeeAff[2] = false;
			}
			else
			{
				if(pointDeVie < 3*chassiActuel.pointDeVie /4 )
				{
					fumeeAff[0] = true;
					fumeeAff[1] = false;
					fumeeAff[2] = false;
				}
				else
				{
					fumeeAff[0] = false;
					fumeeAff[1] = false;
					fumeeAff[2] = false;
				}
			}
		}

		//On affiche les fumées
		if(fumeeAff[0] && fumeeT[0] == null) //On affiche la fumee 0 ssi fummeAff[0] = true et fumeeT n'existe pas
		{
			fumeeT[0] = Instantiate(fumeeObj,chassiActuel.posFumées[0], new Quaternion(0, 0, 0, 0)) as Transform;
			fumeeT[0].parent = GameObject.Find(nom).transform;
			fumeeT[0].localPosition = chassiActuel.posFumées[0];
		}

		if(fumeeAff[0] == false && fumeeT[0] != null)
		{
			Destroy(fumeeT[0].gameObject);
		}

		if(fumeeAff[1] && fumeeT[1] == null) //On affiche la fumee 0 ssi fummeAff[0] = true et fumeeT n'existe pas
		{
			fumeeT[1] = Instantiate(fumeeObj,chassiActuel.posFumées[1], new Quaternion(0, 0, 0, 0)) as Transform;
			fumeeT[1].parent = GameObject.Find(nom).transform;
			fumeeT[1].localPosition = chassiActuel.posFumées[1];
		}

		if(fumeeAff[1] == false && fumeeT[1] != null)
		{
			Destroy(fumeeT[1].gameObject);
		}

		if(fumeeAff[2] && fumeeT[2] == null) //On affiche la fumee 0 ssi fummeAff[0] = true et fumeeT n'existe pas
		{
			fumeeT[2] = Instantiate(fumeeObj,chassiActuel.posFumées[2], new Quaternion(0, 0, 0, 0)) as Transform;
			fumeeT[2].parent = GameObject.FindWithTag("Player").transform;
			fumeeT[2].localPosition = chassiActuel.posFumées[2];
		}

		if(fumeeAff[2] == false && fumeeT[2] != null)
		{
			Destroy(fumeeT[2].gameObject);
		}
	}


	//Fonction appelée lors du ramassage d'un pack de réparation (loot)
	public bool Repair(int rp)
	{
		//On utilise le pack seulement si on a perdu de la vie
		if(pointDeVie == chassiActuel.pointDeVie)
		{
			return false;
		}
		else
		{
			pointDeVie += rp;
			if(pointDeVie > chassiActuel.pointDeVie)
			{
				pointDeVie = chassiActuel.pointDeVie;
			}
			return true;
		}
	}


	public int bonusMult = 1;
	public float tempsBonusActuel;
	public float tempsBonusMax;
	public bool changerMun = false;

	public void ChangerMun(int p_bonusMult, float duree)
	{
		bonusMult = p_bonusMult;
		changerMun = true;
		tempsBonusMax = duree;
		GameObject.Find ("HUD").GetComponent<HUD> ().afficherBonusMuni (p_bonusMult, duree);
	}
}
