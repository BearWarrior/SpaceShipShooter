using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuHangar : MonoBehaviour {
	

	//CHASSIS
	private Rect rectBoxChassis;
	private Rect rectButtonChassis1;
	private Rect rectButtonChassis2;
	private Rect rectButtonChassis3;
	private Rect rectDescrChassi;

	//RETOUR
	private Rect rectButtonRetour;

	//Variable de comptage
	public int nbArmeInvPlayer;
	public int nbArmeChassi;

	//Armes
	private Rect rectBoxExplArmes;

	//Lists
	public List<bool> posClic;
	public List<Arme> list;
	
	//Constante pour hauteur - largeur des bouttons
	private const int H = 40;
	private const int L = 180;

	//RESSOURCES
	private Rect rectBoxRes;
	
	//FENETRE
	public int WinLarg;
	public int WinHaut;

	List<Vector2> posArmeScreen;


	private GUISkin skin;
	private GUISkin skinButtonPush;

	public int numeroChassi;


	private bool escapePush;
	// Use this for initialization
	void Start () 
	{
		escapePush = false;

		//FENETRE
		WinLarg = Screen.width;
		WinHaut = Screen.height;


		//QUITTER
		rectButtonRetour.width = 200;
		rectButtonRetour.height = 40;
		rectButtonRetour.x = Screen.width - 250;
		rectButtonRetour.y = Screen.height - 90;


		//RESSOURCES
		rectBoxRes.width = 275;
		rectBoxRes.height = 40;
		rectBoxRes.x = Screen.width - rectBoxRes.width - 25; 
		rectBoxRes.y = 25;


		
		//CHASSIS
		rectBoxChassis.width = 275;
		rectBoxChassis.height = 270;
		rectBoxChassis.x = WinLarg - 300; 
		rectBoxChassis.y = 135;
		
		rectButtonChassis1.width = 150;
		rectButtonChassis1.height = 40;
		rectButtonChassis1.x = WinLarg - 220;
		rectButtonChassis1.y = 240;
		
		rectButtonChassis2.width = 150;
		rectButtonChassis2.height = 40;
		rectButtonChassis2.x = WinLarg - 220;
		rectButtonChassis2.y = 300;
		
		rectButtonChassis3.width = 150;
		rectButtonChassis3.height = 40;
		rectButtonChassis3.x = WinLarg - 220; 
		rectButtonChassis3.y = 360;

		rectDescrChassi = new Rect (WinLarg - 210, 440, 200, 200);

		GameObject player = new GameObject ("VaisseauPlayer");
		player.tag = "Player";
		//Ajout du script AssemblageVaisseau		
		GameObject.FindWithTag("Player").AddComponent <AssemblageVaisseau>();	
		//Positionnement du joueur dans l'espace + rescale
		GameObject.FindWithTag ("Player").gameObject.transform.localScale = new Vector3 (3, 3, 3); 
		
		
		
		//recuperation du nombre d'arme dans son inventaire
		nbArmeInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire.Count;
		//recuperation du nombre du nombre de point d'attache sur le vaisseau choisi
		nbArmeChassi = GameObject.FindWithTag ("Player").GetComponent<AssemblageVaisseau> ().chassiActuel.nbPointDattache;
		
		posClic = new List<bool> ();
		//Creation des positions des boutons du choix des chassis
		for(int i = 0; i < nbArmeChassi; i++)
		{
			posClic.Add(false);
		}

		//On choisit par défaut la première position
		posClic[0] = true;

		skin = Resources.Load ("Menu/SkinMenuHangar") as GUISkin;
		skinButtonPush = Resources.Load ("Menu/SkinMenuHangarPush") as GUISkin;

		//rectBoxExplArmes = new Rect (75, 75, L + 150, 50);
		rectBoxExplArmes = new Rect (75, 135, L + 150, 50);


		//Affichage des bouton de choix de position pres des armes du vaisseau
		posArmeScreen = new List<Vector2> ();
		for(int i = 0; i < nbArmeChassi; i++)
		{
			posArmeScreen.Add(Camera.main.WorldToScreenPoint (GameObject.FindWithTag ("Player").GetComponent<AssemblageVaisseau> ().ListTArme [i].position));
			posArmeScreen[i] = new Vector3 (posArmeScreen[i].x, GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre> ().HAUTEURpx - posArmeScreen[i].y);
		}



		if(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nomChassis == "Chass")
		{
			numeroChassi = 0;
		}if(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nomChassis == "Chassi2")
		{
			numeroChassi = 1;
		}
		if(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nomChassis == "Chassi3")
		{
			numeroChassi = 2;
		}
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


	//Position choisie (utilisée pour les label rectlabelNumPosition et rectLabelNomArme)
	public int position = 0;
	private Vector2 vector2ScrollView = Vector2.zero;

	void OnGUI()
	{
		GUI.skin = skin;


		//Mise à jour du nombre du nb d'arme dans l'inventaire et des points d'attache
		nbArmeInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire.Count;
		nbArmeChassi = GameObject.FindWithTag ("Player").GetComponent<AssemblageVaisseau> ().chassiActuel.nbPointDattache;

		posArmeScreen.Clear ();
		//Mise a jour de la position des bouton des armes du vaisseau du joueur
		for(int i = 0; i < nbArmeChassi; i++)
		{
			posArmeScreen.Add(Camera.main.WorldToScreenPoint (GameObject.FindWithTag ("Player").GetComponent<AssemblageVaisseau> ().ListTArme [i].position));
			posArmeScreen[i] = new Vector3 (posArmeScreen[i].x, GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre> ().HAUTEURpx - posArmeScreen[i].y);
		}

	
		GUI.Label (rectBoxExplArmes, "Choix des armes : ");
		Rect rectBoxExplArmes2 = new Rect (rectBoxExplArmes.x, rectBoxExplArmes.y + 45, rectBoxExplArmes.width + 300, rectBoxExplArmes.height + 40);
		GUI.skin = skinButtonPush;
		GUI.Label (rectBoxExplArmes2, "Sélectionnez d'abord l'arme que vous voulez changer sur votre vaisseau puis cliquez sur l'arme que vous voulez placer parmi celles de la liste ci dessous :" +
			"\nLes armes affectées à un chassis n'apparaissent pas dans l'inventaire. Vous pouvez donc retirer les armes des chassis que vous n'utilisez pas (affecter \"LanceMissile\") afin de vous en reservir sur un autre chassis.");
		GUI.skin = skin;

		posClic.Clear ();
		for(int i = 0; i < nbArmeChassi; i++)
		{
			posClic.Add(false);
		}
		//On choisit par défaut la première position
		posClic[0] = true;

		//Pour chaque position sur le vaisseau
		int choixPos = 0;
		int choixArme = 0;
		for(choixPos = 0; choixPos < nbArmeChassi; choixPos++)
		{

			//Mise en surbrillance de la position arme selectionné en changeant le skin par un skin dont le bouton normal correspond au hover
			if(position == choixPos)
			{
				GUI.skin = skinButtonPush;
			}
			else
			{
				GUI.skin = skin;
			}
			if(GUI.Button(new Rect(posArmeScreen[choixPos].x + 150, posArmeScreen[choixPos].y - H/2, L, H), GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listArmeParVaisseau[numeroChassi][choixPos]))
			{
				//Choix de la position
				for(int j = 0; j < nbArmeChassi; j++)
				{
					posClic[j] = false;
				}
				posClic[choixPos] = true;
				position = choixPos;
			}

			//On revient en skin normal
			GUI.skin = skin;

			int nbDifferentesInventairePlayer = 0;;
			//Pout chaque arme dans l'inventaire du jouuer
			string nomArmePrec = "";
			for(int i = 0; i < nbArmeInvPlayer; i++)
			{
				if(GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire[i] == nomArmePrec)
				{

				}
				else
				{
					nbDifferentesInventairePlayer++;
					nomArmePrec = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire[i];
				}
			}


			//Pour la position choisie
			if(posClic[choixPos])
			{
				//SCROLL VIEW
				//vector2ScrollView = GUI.BeginScrollView(new Rect (75, 210, 275, 500), vector2ScrollView, new Rect (0, 0, 220, nbArmeInvPlayer*60));
				vector2ScrollView = GUI.BeginScrollView(new Rect (75, 270, 275, 400), vector2ScrollView, new Rect (0, 0, 220, /*nbArmeInvPlayer*/ nbDifferentesInventairePlayer*60));

				int cptArmeAffiche = 0;
				//Pour chaque arme dans l'inventaire du joueur, on affiche un boutton
				for(choixArme = 0; choixArme < nbArmeInvPlayer; choixArme++)
				{

					int choixArmeIntermediaire = choixArme;
					int cptArmeIdentique = 0;
					//tant que l'arme choisi est la meme que la suivante (suivante-suivante etc.))
					bool stay = true;
					while(choixArmeIntermediaire < nbArmeInvPlayer && stay)
					{
						if(GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire[choixArme] == GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire[choixArmeIntermediaire])
						{
							cptArmeIdentique++;
							choixArmeIntermediaire++;
						}
						else
						{
							stay = false;
						}
					}

					if(GUI.Button (new Rect(0, /*choixArme*/ cptArmeAffiche*60, 250, 40), cptArmeIdentique + "x  " + GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire[choixArme]))
					{

						//On ajoute à l'inventaire d'arme du joueur l'arme déjà présente dans l'empalcement (sauf si c'est un LanceMissile)
						if(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listArmeParVaisseau[numeroChassi][position] != "LanceMissile")
						{
							GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire.Add(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listArmeParVaisseau[numeroChassi][position]);
						}

						//On vient ici mettre à jour le tableau d'arme du joueur (avec le string de la nouvelle arme)
						GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listArmeParVaisseau[numeroChassi][position] = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire[choixArme];

						//On retire ce string du tableau d'arme du joueur
						if(GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire[choixArme] != "LanceMissile")
						{
							GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire.RemoveAt(choixArme);

							//Mise a jour du nombre d'arme pour finir la boucle
							nbArmeInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire.Count;
						
						}
						//Et procéder au changement sur le vaisseau (physique)
						GameObject.FindWithTag("Player").GetComponent<AssemblageVaisseau>().Changement();	
					}
					cptArmeAffiche++;

					if(choixArme != choixArmeIntermediaire)
					{
						choixArme = choixArmeIntermediaire -1;
					}
					else
					{
						choixArme = choixArmeIntermediaire;
					}
				}

				GUI.EndScrollView ();
			}
		}


		//CHASSIS		
		GUI.Label (rectBoxChassis, "Choix du chassis : ");
		Rect rectBoxChassis2 = new Rect (rectBoxChassis.x, rectBoxChassis.y + 45, rectBoxChassis.width + 30, rectBoxChassis.height + 40);
		GUI.skin = skinButtonPush;
		GUI.Label (rectBoxChassis2, "Liste des vaisseaux : \nLe dernier vaisseau séléctionné sera celui avec lequel vous jouerez.");
		GUI.skin = skin;


		if(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nomChassis == "Chassi1")
		{
			GUI.skin = skinButtonPush;
		}
		if(GUI.Button (rectButtonChassis1, "Chassis n°1"))
		{
			GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nomChassis = "Chassi1";
			GameObject.FindWithTag("Player").GetComponent<AssemblageVaisseau>().Changement();
			chassiChanged();
			numeroChassi = 0;
		}

		GUI.skin = skin;


		if(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nomChassis == "Chassi2")
		{
			GUI.skin = skinButtonPush;
		}
		if(GUI.Button (rectButtonChassis2, "Chassis n°2"))
		{
			GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nomChassis = "Chassi2";
			GameObject.FindWithTag("Player").GetComponent<AssemblageVaisseau>().Changement();	
			chassiChanged();
			numeroChassi = 1;
		}

		GUI.skin = skin;


		if(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nomChassis == "Chassi3")
		{
			GUI.skin = skinButtonPush;
		}
		if(GUI.Button (rectButtonChassis3, "Chassis n°3"))
		{
			GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nomChassis = "Chassi3";
			GameObject.FindWithTag("Player").GetComponent<AssemblageVaisseau>().Changement();	
			chassiChanged();
			numeroChassi = 2;
		}

		GUI.skin = skin;

		afficherCaracChassi (numeroChassi);


		//RESSOURCES
		GUI.Button (rectBoxRes, "Argent : " + GameObject.FindWithTag("PlayerScript").GetComponent<Player>().argent + "   ferraille : " + GameObject.FindWithTag("PlayerScript").GetComponent<Player>().ferraille);


		//QUITTER
		if(GUI.Button (rectButtonRetour, "Retour") || escapePush)
		{
			GetComponent<AudioSource>().Play();
			GameObject.FindWithTag("PlayerScript").GetComponent<Player>().Save();
			//Application.LoadLevel("MenuIntermediaire");

			Destroy (GameObject.FindWithTag("Player"));
			GetComponent<MenuIntermediaire>().enabled = true;
			Destroy(GetComponent<MenuHangar>());
		}


	}

	void chassiChanged()
	{
		position = 0;
		posClic[0] = true;
	}


	void afficherCaracChassi(int numeroChassi)
	{
		switch(numeroChassi)
		{
		case 0:
			GUI.skin = skinButtonPush;
			GUI.Label(rectDescrChassi, "Vitesse : " + GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.vitesse + "\n" +
			          "Point de vie : " + GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.pointDeVie + "\n" + 
			          "Vitesse : " + GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c1.vitesse);
			GUI.skin = skin;
			break;
		case 1:
			GUI.skin = skinButtonPush;
			GUI.Label(rectDescrChassi, "Vitesse : " + GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.vitesse + "\n" +
			          "Point de vie : " + GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.pointDeVie + "\n" + 
			          "Vitesse : " + GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c2.vitesse);
			GUI.skin = skin;
			break;
		case 2:
			GUI.skin = skinButtonPush;
			GUI.Label(rectDescrChassi, "Vitesse : " + GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.vitesse + "\n" +
			          "Point de vie : " + GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.pointDeVie + "\n" + 
			          "Vitesse : " + GameObject.FindWithTag("PlayerScript").GetComponent<ParametresChassis>().c3.vitesse);
			GUI.skin = skin;
			break;
		}
	}
}

