using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuAchat : MonoBehaviour 
{
	//RETOUR
	public Rect rectButtonRetour;
	
	//ACHATS
	private Rect rectExplPlanToBuy;
	public List<Rect> rectPlanToBuy;
	public List<string> listPlanToBuy;

	
	//ARMES DU JOUEURS
	public List<Rect> rectPlanPlayer;
	private Rect rectExplPlanJoueur;
	
	//PropriétéPlayer
	public int nbPlanInvPlayer;
	
	//DESCRIPTION
	public Rect rectDescr;
	
	//ACHETER 
	public Rect rectAcheter;
	public Rect rectNotEnResLabel;
	public bool notEnRes;
	private float tempsNERact;
	private float tempsNERmax;

	private Rect rectBoxDepArgent;
	private List<int> listDepArgent;
	private float tempsAffDepActArgent;
	private float tempsAffDepMaxArgent;

	
	//RESSOURCES
	private Rect rectBoxRes;
	
	//PLANS INV PLAYER STRING
	public List<string> planPlayer;
	
	//CONSTANTES
	public const int L = 150;
	public const int H = 40;
	
	//CHOIX
	public int choix;
	public int indice;
	public bool choixOK;

	//FENETRE
	public int WinLarg;
	public int WinHaut;


	//Arme3D + Tir
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

	//public List<Arme> armeTir;


	private bool escapePush;

	private GUISkin skin;
	private GUISkin skinlabelDescrAndButtonPush;

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


		Time.timeScale = 1.0f;

		GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().checkFenetre ();


		posArme3D = new Vector3(2.5f, 1f, -2f);
		posArme3DTir = new Vector3(-1f, -0.5f, -2f);

		//armeTir = new List<Arme> ();

		//FENETRE
		WinLarg = GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().LARGEURpx;
		WinHaut = GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().HAUTEURpx;






		nbPlanInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listPlanArme.Count;

		//ARMES INV PLAYER
		planPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listPlanArme;
		planPlayer.Sort ();


		//Liste des plans achetable par le joueur
		listPlanToBuy = new List<string> ();
		listPlanToBuy.Add ("LanceMissileRapide");
		listPlanToBuy.Add ("Laser");
		listPlanToBuy.Add ("CanonPlasma");
		listPlanToBuy.Add ("Tromblon");
		listPlanToBuy.Add ("DoubleLame");
		listPlanToBuy.Add ("Alien");


		//On supprime les plans qu'il a déjà
		listPlanToBuy = checkPlanMagasin (listPlanToBuy);
	





		rectPlanPlayer = new List<Rect> ();
		//Création des positions des boutons des plans deja acquis par le joueur
		for(int i =0; i < nbPlanInvPlayer; i++)
		{
			rectPlanPlayer.Add(new Rect(WinLarg - 300, 210 + i*60, 250, H));
		}
		rectExplPlanJoueur = new Rect (WinLarg - 300, 135, 275, 270);


		rectPlanToBuy = new List<Rect> ();
		//Plan d'arme a acheter
		for(int i =0; i < listPlanToBuy.Count; i++)
		{
			//rectPlanToBuy.Add(new Rect (75, 150 + i*60, 250, H));
			rectPlanToBuy.Add(new Rect (75, 210 + i*60, 250, H));
		}
		//rectExplPlanToBuy = new Rect (75, 75, 180, 100);
		rectExplPlanToBuy = new Rect (75, 135, 180, 100);

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
		
		//ACHETER
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

		rectBoxDepArgent = new Rect (Screen.width - 230, 70, 65, 30);




		skin = Resources.Load ("Menu/SkinMenuAchat") as GUISkin;
		skinlabelDescrAndButtonPush = Resources.Load ("Menu/SkinMenuAchatDescrAndPush") as GUISkin;


		
		choixOK = true;
		choix = 0;
		if(listPlanToBuy.Count != 0)
		{
			indice = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().getPosFromStr (listPlanToBuy[choix]);
		}
		else
		{
			indice = -1;
		}

		GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().checkFenetre ();
	}

	void Awake()
	{

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


		if(arme3DTir != null)
		{
			arme3DTir.GetComponent<Arme>().Fire(posArme3DTir, 1, 1, arme3DTir.transform.eulerAngles.z);
			arme3DTir.GetComponent<Arme>().Reload();
		}
	}
	
	
	void OnGUI()
	{
		GUI.skin = skin;

		//recuperation du nombre de plan dans l'inventaire du player
		nbPlanInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listPlanArme.Count;

		//Mise a jour des plans achetable par le joueur
		listPlanToBuy = checkPlanMagasin (listPlanToBuy);

		if(listPlanToBuy.Count == 0)
		{
			indice = -1;
			choixOK  = false;
		}

		/*if(indice == -1)
		{
			choixOK = false;
		}*/

		//Affichage des plan du joueur 
		for(int i = 0; i < nbPlanInvPlayer; i++)
		{
			if(GUI.Button(rectPlanPlayer[i], "Plan : " + planPlayer[i]))
			{
				
			}
		}



		//Affichage Plan à acheter
		for(int i = 0; i < listPlanToBuy.Count; i++)
		{
			if(i == choix && choixOK)
			{
				GUI.skin = skinlabelDescrAndButtonPush;
			}
			else
			{
				GUI.skin = skin;
			}

			if(GUI.Button (rectPlanToBuy[i]	, "Plan : " + listPlanToBuy[i]))
			{
				choixOK = true;
				choix = i;
				indice = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().getPosFromStr (listPlanToBuy[i]);
			}
		}



		GUI.skin = skinlabelDescrAndButtonPush;

		//Affichage description arme choisie seulement si une arme est choisi
		if(choixOK)
		{
			GUI.skin = skin;


			GUI.Label(rectDescr, "Plan " + GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].nomArme);
			
			GUI.skin = skinlabelDescrAndButtonPush;
			Rect rectDescr2 = rectDescr;
			rectDescr2.y += 60;
			GUI.Label (rectDescr2, 
			           "Cout du plan (argent) :" + GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixPlanArgent + "\n\n" );

			GUI.skin = skin;

			rectDescr2.y += 40;
			GUI.Label(rectDescr2, "Caractéristiques de l'arme : \n" );
			
			GUI.skin = skinlabelDescrAndButtonPush;

			rectDescr2.y += 60;

			GUI.Label (rectDescr2, 
			          "Construction : " + GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixArmeArgent + " Argent \n" + 
			           "Construction : " + GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixArmeFerraille + " Ferraille\n\n" +
			           "Fréquence : " +  (1.0/GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].frequence).ToString("F1") + " cps/sec\n" + 
			           "Degats : " + GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].degats + "\n"
			           );
		}
		else
		{
			if(listPlanToBuy.Count == 0)
			{
				GUI.skin = skin;
				Rect rectDescr2 = rectDescr;
				rectDescr2.x = 75;
				rectDescr2.y += 60;
				rectDescr2.width = 600;
				GUI.Label (rectDescr2,  "Le magasin n'a plus de plan a vous proposer");

			}
		}

		GUI.skin = skin;


		if(choixOK)
		{
			
			if(armeAffich != GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].nomArme)
			{
				if(arme3D != null)
				{
					Destroy(arme3D.gameObject);
				}
				
				if(arme3DTir != null)
				{
					Destroy(arme3DTir.gameObject);
				}

				switch(GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].nomArme)
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
				
				//armeTir.Clear();
				//addArmeTir(armeTir, armeAffich);

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
		if(GUI.Button(rectAcheter, "Acheter"))
		{
			if(choixOK)
			{
				if(GameObject.FindWithTag("PlayerScript").GetComponent<Player>().AcheterPlan(GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].nomArme, GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixPlanArgent))
				{
					choix = 0;
					listDepArgent.Add (GameObject.FindWithTag("PlayerScript").GetComponent<CaracArmes>().tabArme[indice].prixPlanArgent);
				}
				else
				{
					notEnRes = true;
				}



				//On remet le choix au premier de la liste et on affiche les bonnes caractéristiques
				//choix = 0;
				listPlanToBuy = checkPlanMagasin (listPlanToBuy);
				//SI il y a encore des plans, on fait come d'hab, sinon, on met indice a -1.
				if(listPlanToBuy.Count != 0)
				{
					indice = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().getPosFromStr (listPlanToBuy[choix]);
				}
				else
				{
					indice = -1;
				}


				//Mise a jour du nombre et des positions des boutons du choix des armes
				rectPlanPlayer.Clear();
				nbPlanInvPlayer = GameObject.FindWithTag ("PlayerScript").GetComponent<Player> ().listPlanArme.Count;
				for(int i =0; i < nbPlanInvPlayer; i++)
				{
					rectPlanPlayer.Add(new Rect(WinLarg - 300, 210 + i*60, 250, H));
				}

			}
		}


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
		GUI.skin = skin;


		GUI.Label (rectExplPlanToBuy, "Magasin : ");
		if(listPlanToBuy.Count > 0)
		{
			Rect rectExplPlanToBuy2 = new Rect (rectExplPlanToBuy.x, rectExplPlanToBuy.y + 45, rectExplPlanToBuy.width + 100, rectExplPlanToBuy.height);
			GUI.skin = skinlabelDescrAndButtonPush;
			GUI.Label (rectExplPlanToBuy2, "liste des plans que vous pouvez acheter");
		}
		GUI.skin = skin;
		GUI.Label (rectExplPlanJoueur, "Inventaire :");
		Rect rectExplPlanJoueur2 = new Rect (rectExplPlanJoueur.x, rectExplPlanJoueur.y + 45, rectExplPlanJoueur.width, rectExplPlanJoueur.height);
		GUI.skin = skinlabelDescrAndButtonPush;
		GUI.Label (rectExplPlanJoueur2, "liste des plans que vous possédez déjà");
		GUI.skin = skinlabelDescrAndButtonPush;


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
			Destroy(GetComponent<MenuAchat>());
		}
	}


	public List<string> checkPlanMagasin(List<string> list)
	{
		List<string> listPlayer = GameObject.FindWithTag("PlayerScript").GetComponent<Player>().listPlanArme;

		bool sortie = true;
		bool continu = true;
		while(sortie)
		{
			int i = 0;
			continu = true;
			while(i<list.Count && continu)
			{
				int p = 0;
				while(p < listPlayer.Count && continu)
				{
					if(list[i] == listPlayer[p])
					{
						list.RemoveAt(i);
						//Si on en trouve 2 pareils, on sort des deux boucles et on recommence à chercher
						continu = false;
					}
					p++;
				}
				i++;

			}
			//Si on a pas trouvé deux noms pareils, on sort
			if(continu == true)
			{
				sortie = false;
			}
		}

		return list;
	}


	public void addArmeTir(List<Arme> armeTir, string nomArme)
	{
		switch(nomArme)
		{
		case "LanceMissile" : 
			armeTir.Add(new LanceMissile());
			break;
		case "LanceMissileRapide" : 
			armeTir.Add(new LanceMissileRapide());
			break;
		case "Laser" : 
			armeTir.Add(new Laser());
			break;
		case "CanonPlasma" : 
			armeTir.Add(new CanonPlasma());
			break;
		case "Tromblon" :
			armeTir.Add(new Tromblon());
			break;
		case "DoubleLame" :
			armeTir.Add(new DoubleLame());
			break;
		case "Alien" :
			armeTir.Add(new Alien());
			break;
		case "Turret" :
			armeTir.Add(new Turret());
			break;
		default :
			Debug.LogError ("Menu Acaht : Nom d'arme inconnu" + nomArme);
			break;
		}
	}
}


