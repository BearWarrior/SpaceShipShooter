using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuAtelier : MonoBehaviour 
{
	//public List<Arme> armeTir;

	//ACHATS
	public List<Rect> rectArmeToBuy;
	private Rect rectExplArmeToBuy;

	//ARMES DU JOUEURS
	public List<Rect> rectArmesPlayer;
	private Rect rectExplArmeJoueur;

	//PropriétéPlayer
	public int nbArmeInvPlayer;
	public int nbPlanInvPlayer;

	//DESCRIPTION
	public Rect rectDescr;

	//CONSTRUIRE 
	public Rect rectAcheter;
	public Rect rectNotEnResLabel;
	public bool notEnRes;
	private float tempsNERact;
	private float tempsNERmax;
	
	private Rect rectBoxDepArgent;
	private List<int> listDepArgent;
	private float tempsAffDepActArgent;
	private float tempsAffDepMaxArgent;

	private Rect rectBoxDepFerraille;
	private List<int> listDepFerraille;
	private float tempsAffDepActFerraille;
	private float tempsAffDepMaxFerraille;


	//RESSOURCES
	private Rect rectBoxRes;

	//ARMES INV PLAYER STRING
	public List<string> armePlayer;
	public List<string> planPlayer;

	//CONSTANTES
	public const int L = 150;
	public const int H = 40;
	
	//CHOIX
	public int choix;
	public int indice;
	public bool choixOK;

	//RETOUR
	public Rect rectButtonRetour;

	//FENETRE
	public int WinLarg;
	public int WinHaut;

	//ARME 3D
	[Header("Arme 3D rotate")]
	public GameObject arme3D;
	public GameObject arme3DTir;
	public GameObject lm;
	public GameObject lmr;
	public GameObject cp;
	public GameObject dl;
	public GameObject las;
	public GameObject trom;
	public GameObject al;
	private string armeAffich;

	private Vector3 posArme3D;
	private Vector3 posArme3DTir;

	private GUISkin skin;
	private GUISkin skinlabelDescrAndButtonPush;


	private bool escapePush;

	// Use this for initialization
	void Start () 
	{
		escapePush = false;
		lm = Resources.Load ("Arme/LanceMissile") as GameObject;
		lmr = Resources.Load ("Arme/LanceMissileRapide") as GameObject;
		cp = Resources.Load ("Arme/CanonPlasma") as GameObject;
		dl = Resources.Load ("Arme/DoubleLame") as GameObject;
		las = Resources.Load ("Arme/Laser") as GameObject;
		trom = Resources.Load ("Arme/Tromblon") as GameObject;
		al = Resources.Load ("Arme/Alien") as GameObject;

		GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().checkFenetre ();


		posArme3D = new Vector3(1f, 1f, -2f);
		posArme3DTir = new Vector3(-1f, -0.5f, -2f);

		Time.timeScale = 1f;


		//FENETRE
		WinLarg = Screen.width;
		WinHaut = Screen.height;

		
		nbPlanInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listPlanArme.Count;
		
		//ARMES INV PLAYER
		armePlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire;
		planPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listPlanArme;
		armePlayer.Sort ();
		

		rectArmeToBuy = new List<Rect> ();
		//ACHATS
		for(int i =0; i < nbPlanInvPlayer; i ++)
		{
			rectArmeToBuy.Add(new Rect(75, 210 + i*60, 250, 40));
		}	
		rectExplArmeToBuy = new Rect (75, 135, 250, 100);
		
		//recuperation du nombre d'arme dans son inventaire
		nbArmeInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire.Count;

		rectArmesPlayer = new List<Rect> ();
		//Création des positions des armes du joueur
		for(int i =0; i < nbArmeInvPlayer; i++)
		{
			rectArmesPlayer.Add(new Rect(WinLarg - 300, 210 + i*60, 250, H));
		}
		rectExplArmeJoueur = new Rect (WinLarg - 300, 135, 275, 270);
		
		

		//RETOUR 
		rectButtonRetour.width = 200;
		rectButtonRetour.height = 40;
		rectButtonRetour.x = Screen.width - 250;
		rectButtonRetour.y = Screen.height - 90;
		
		//DESCRIPTION
		rectDescr.width = 400;
		rectDescr.height = 400;
		rectDescr.x = 350; 
		rectDescr.y = 150;
		
		//RESSOURCES
		rectBoxRes.width = 275;
		rectBoxRes.height = 40;
		rectBoxRes.x = Screen.width - rectBoxRes.width - 25; 
		rectBoxRes.y = 25;
		
		//CONSTRUIRE
		rectAcheter.width = 150;
		rectAcheter.height = 40;
		rectAcheter.x = 500; 
		rectAcheter.y = 500;

		rectNotEnResLabel.width = 500;
		rectNotEnResLabel.height = 120;
		rectNotEnResLabel.x = 400; 
		rectNotEnResLabel.y = 550;
		
		notEnRes = false;
		tempsNERact = 0;
		tempsNERmax = 3;
		
		listDepArgent = new List<int> ();
		tempsAffDepMaxArgent = 2f;
		listDepFerraille = new List<int> ();
		tempsAffDepMaxFerraille = 2f;
		
		rectBoxDepArgent = new Rect (Screen.width - 230, 70, 65, 30);
		rectBoxDepFerraille = new Rect (Screen.width - 110, 70, 65, 30);




		skin = Resources.Load ("Menu/SkinMenuAchat") as GUISkin;
		skinlabelDescrAndButtonPush = Resources.Load ("Menu/SkinMenuAchatDescrAndPush") as GUISkin;

		if(nbPlanInvPlayer > 0)
		{
			choixOK = true;
			choix = 0;
			indice = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().getPosFromStr (GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listPlanArme[choix]);
		}
		else
		{
			choixOK = false;
			choix = 0;
			indice = 0;
		}
	}


	
	// Update is called once per frame
	void Update () 
	{
		if(arme3DTir != null)
		{
			arme3DTir.GetComponent<Arme>().Fire(posArme3DTir, 1, 1, arme3DTir.transform.eulerAngles.z);
			arme3DTir.GetComponent<Arme>().Reload();
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			escapePush = true;
		}
		else
		{
			escapePush = false;
		}
	}


	private Vector2 vector2ScrollView = Vector2.zero;
	void OnGUI()
	{
		GUI.skin = skin;

		//recuperation du nombre d'arme dans son inventaire
		nbArmeInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire.Count;
		
		//recuperation du nombre de plan dans l'inventaire du player
		nbPlanInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listPlanArme.Count;
		

		//Récupération du nom des armes déjà acquies par le joueur + trie par ordre alphabétique pour affichage
		armePlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire;
		armePlayer.Sort ();

		//On compte le nombre d'arme différentes qu'a le joueur pour ajuster la scrollView
		int nbDifferentesInventairePlayer = 0;;
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


		//ScrollView pour ne pas dépasser l'écran
		vector2ScrollView = GUI.BeginScrollView(new Rect (WinLarg - 300, 210, 275, 400), vector2ScrollView, new Rect (0, 0, 220, /*rectArmesPlayer.Count*/ nbDifferentesInventairePlayer*60));

		//compteur d'arme affiché
		int cptArmeAffiche = 0;
		//Affichage des armes du joueur 
		for(int i = 0; i < rectArmesPlayer.Count; i++)
		{
			int choixArmeIntermediaire = i;
			int cptArmeIdentique = 0;
			bool stay = true;
			//Tant que l'arme suivant est la meme que celle ci, on compte
			while(choixArmeIntermediaire < nbArmeInvPlayer && stay)
			{
				if(GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire[i] == GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire[choixArmeIntermediaire])
				{
					cptArmeIdentique++;
					choixArmeIntermediaire++;
				}
				else
				{
					stay = false;
				}
			}

			//On affiche le nombre de fois que le joueur possède l'arme
			if(GUI.Button(new Rect(0, /*i*/cptArmeAffiche*60, rectArmesPlayer[i].width, rectArmesPlayer[i].height), cptArmeIdentique + "x  " +  armePlayer[i]))
			{
				
			}

			//Mise a jour du compteur d'arme dans la boucle
			if(i != choixArmeIntermediaire)
			{
				i = choixArmeIntermediaire -1;
			}
			else
			{
				i = choixArmeIntermediaire;
			}

			cptArmeAffiche++;
		}

		GUI.EndScrollView ();
		
		//Affichage arme à acheter
		//Detecton clic sur l'arme choisie
		for(int i = 0; i < nbPlanInvPlayer; i++)
		{

			if(i == choix && choixOK)
			{
				GUI.skin = skinlabelDescrAndButtonPush;
			}
			else
			{
				GUI.skin = skin;
			}
			if(GUI.Button (rectArmeToBuy[i]	, GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listPlanArme[i]))	
			{
				choixOK = true;
				choix = i;
				indice = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().getPosFromStr (GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listPlanArme[i]);
			}
		}


		

		//Affichage description arme choisie seulement si une arme est choisi
		if(choixOK)
		{
			GUI.skin = skin;

			GUI.Label(rectDescr, GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].nomArme);

			GUI.skin = skinlabelDescrAndButtonPush;

			//rectDescr.y + 60;
			Rect rectDescr2 = rectDescr;
			rectDescr2.y += 60;
			GUI.Label (rectDescr2, "Cout : " + "\t" + GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixArmeArgent + " Argents \n" + 
			           "\t\t\t " + GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixArmeFerraille + " ferrailles \n\n" +
			           
			           "Caractéristiques de l'arme : \n" +
			           "\tFréquence :      " + (1.0/GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].frequence).ToString("F1") + " cps/sec\n" +
			           "\tDegats : " + GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].degats + "\n");
		}
		else
		{
			if(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listPlanArme.Count == 0)
			{
				GUI.skin = skin;
				Rect rectDescr2 = rectDescr;
				rectDescr2.x = 75;
				rectDescr2.y +=60;
				rectDescr2.width = 600;
				GUI.Label(rectDescr2, "Vous n'avez aucun plan");

				GUI.skin = skinlabelDescrAndButtonPush;
				rectDescr2.y += 60;
				GUI.Label (rectDescr2, "Vous pouvez acheter des plans dans le menu ACHAT. \n" +
					"Apres avoir acheté ces plans, vous pourrez construire les armes associées dans ce menu.");
				
			}
		}
		
		GUI.skin = skin;

		

		//AFFICHAGE ARME 3D
		//On va ici afficher l'arme en 3D et la faire tourner pour envoyer du steak
		if(choixOK)
		{

			if(armeAffich != GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listPlanArme[choix])
			{
				if(arme3D != null)
				{
					Destroy(arme3D.gameObject);
				}

				if(arme3DTir != null)
				{
					Destroy(arme3DTir.gameObject);
				}



				switch(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listPlanArme[choix])
				{
				case "LanceMissileRapide":
					armeAffich = "LanceMissileRapide";
					arme3D = Instantiate(lmr, posArme3D, new Quaternion(0, 0, 0, 0)) as GameObject;
					arme3DTir = Instantiate(lmr, posArme3DTir, new Quaternion(0, 0, 0, 0)) as GameObject;
					break;
				case "Laser":
					armeAffich = "Laser";
					arme3D = Instantiate(las, posArme3D, new Quaternion(0, 0, 0, 0)) as GameObject;
					arme3DTir = Instantiate(las, posArme3DTir, new Quaternion(0, 0, 0, 0)) as GameObject;
					break;
				case "CanonPlasma":
					armeAffich = "CanonPlasma";
					arme3D = Instantiate(cp, posArme3D, new Quaternion(0, 0, 0, 0)) as GameObject;
					arme3DTir = Instantiate(cp, posArme3DTir, new Quaternion(0, 0, 0, 0)) as GameObject;
					break;
				case "Tromblon" :
					armeAffich = "Tromblon";
					arme3D = Instantiate(trom, posArme3D, new Quaternion(0, 0, 0, 0)) as GameObject;
					arme3DTir = Instantiate(trom, posArme3DTir, new Quaternion(0, 0, 0, 0)) as GameObject;
					break;
				case "DoubleLame" :
					armeAffich = "DoubleLame";
					arme3D = Instantiate(dl, posArme3D, new Quaternion(0, 0, 0, 0)) as GameObject;
					arme3DTir = Instantiate(dl, posArme3DTir, new Quaternion(0, 0, 0, 0)) as GameObject;
					break;
				case "Alien" :
					armeAffich = "Alien";
					arme3D = Instantiate(al, posArme3D, new Quaternion(0, 0, 0, 0)) as GameObject;
					arme3DTir = Instantiate(al, posArme3DTir, new Quaternion(0, 0, 0, 0)) as GameObject;
					break;
				}
				arme3D.transform.localScale = new Vector3(6.5f, 6.5f, 6.5f);
				arme3D.transform.Rotate(new Vector3(0, 0, 75));
				arme3D.gameObject.AddComponent<ArmeRotate>();
			}

		}
		else
		{
			if(arme3D != null)
			{
				Destroy(arme3D.gameObject);
			}
			
			if(arme3DTir != null)
			{
				Destroy(arme3DTir.gameObject);
			}
		}


		//Gestion achat
		if(GUI.Button(rectAcheter, "Construire"))
		{
			if(choixOK)
			{
				if(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().AcheterArme(GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].nomArme, GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixArmeArgent, GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixArmeFerraille))
				{
					//choixOK = false;
					listDepArgent.Add (GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixArmeArgent);
					listDepFerraille.Add (GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixArmeFerraille);
				}
				else
				{
					notEnRes = true;
				}

				//Mise a jour du nombre et des positions des boutons du choix des armes
				rectArmesPlayer.Clear();
				nbArmeInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listArmeInventaire.Count;
				for(int i =0; i < nbArmeInvPlayer; i++)
				{
					rectArmesPlayer.Add(new Rect(WinLarg - 300, 210 + i*60, 250, H));
				}
			}
		}
		
		//Si le joueur n'a pas assez d'argent pour acheter l'arme
		if(notEnRes)
		{
			GUI.Label (rectNotEnResLabel, "Pas assez de ressources");
			
			if(tempsNERact > tempsNERmax)
			{
				tempsNERact = 0;
				notEnRes = false;
			}
			else
			{
				tempsNERact += Time.deltaTime;
			}
		}


		//Affichage des ressources du joueur (argent + ferraille)
		GUI.Button (rectBoxRes, "Argent : " + GameObject.FindWithTag("PlayerScript").GetComponent<Player>().argent + "   ferraille : " + GameObject.FindWithTag("PlayerScript").GetComponent<Player>().ferraille);
		//Si on doit afficher une perte en argent
		GUI.skin = skinlabelDescrAndButtonPush;
		if(listDepArgent.Count > 0)
		{
			GUI.Button (rectBoxDepArgent, "-" + listDepArgent[0]);
			
			if(tempsAffDepActArgent > tempsAffDepMaxArgent)
			{
				tempsAffDepActArgent = 0;
				listDepArgent.RemoveAt(0);
			}
			else
			{
				tempsAffDepActArgent+= Time.deltaTime;
			}
		}

		if(listDepFerraille.Count > 0)
		{
			GUI.Button (rectBoxDepFerraille, "-" + listDepFerraille[0]);
			
			if(tempsAffDepActFerraille > tempsAffDepMaxFerraille)
			{
				tempsAffDepActFerraille = 0;
				listDepFerraille.RemoveAt(0);
			}
			else
			{
				tempsAffDepActFerraille+= Time.deltaTime;
			}
		}
		GUI.skin = skin;


		GUI.Label (rectExplArmeToBuy, "Construction : ");
		if(nbPlanInvPlayer > 0	)
		{
			Rect rectExplArmeToBuy2 = new Rect (rectExplArmeToBuy.x, rectExplArmeToBuy.y + 45, rectExplArmeToBuy.width + 100, rectExplArmeToBuy.height);
			GUI.skin = skinlabelDescrAndButtonPush;
			GUI.Label (rectExplArmeToBuy2, "Liste des plans en votre possession");
		}
		GUI.skin = skin;
		GUI.Label (rectExplArmeJoueur, "Inventaire :");
		Rect rectExplArmeJoueur2 = new Rect (rectExplArmeJoueur.x, rectExplArmeJoueur.y + 45, rectExplArmeJoueur.width, rectExplArmeJoueur.height);
		GUI.skin = skinlabelDescrAndButtonPush;
		GUI.Label (rectExplArmeJoueur2, "Liste des armes que vous pourrez équiper");
		
		//RETOUR
		GUI.skin = skin;
		if(GUI.Button (rectButtonRetour, "Retour") || escapePush)
		{
			GetComponent<AudioSource>().Play();
			GameObject.FindWithTag("PlayerScript").GetComponent<Player>().Save();
			//Application.LoadLevel("MenuIntermediaire");

			if(arme3D != null)
			{
				Destroy(arme3D.gameObject);
			}
			if(arme3DTir != null)
			{
				Destroy(arme3DTir.gameObject);
			}
			GetComponent<MenuIntermediaire>().enabled = true;
			Destroy(GetComponent<MenuAtelier>());
		}
	}
}
