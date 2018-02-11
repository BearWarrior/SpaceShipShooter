using UnityEngine;
using System.Collections;


public class MenuPause : MonoBehaviour 
{
	public bool pause;
	public bool endLvl;
	public bool playerMort;


	private Rect rectQuitter;
	private Rect rectButtonOption;
	private Rect rectRevenir;

	private Rect rectBoxPause;
	private Rect rectBoxOption;

	private GUISkin skin;

	public int menuAff;

	//OPTIONS
	private Rect rectButtonRetour;
	private Rect rectLabelDepl;
	private Rect rectToggleDeplMouse;
	private Rect rectToggleDeplKeyboard;
	private Rect rectToggleTirAuto;
	private bool movePref;
	private bool movePrefMouse; //move pref = 0
	private bool movePrefKeyboard;  //move pref = 1
	private Rect rectLabelTir;
	private bool tirAuto;
		//Son
	private Rect rectScrollSound;
	private float soundEffects;

	private bool escapePush;

	//TUTORIEL
	public bool tuto;
	private int step;
	private GUISkin skinTuto;
	private Vector2 caracFenetre;
	private Rect rectCadrePrincipal;
	private Rect rectButtonPasser;
	private Rect rectButtonPrecedent;
	private Rect rectButtonSuivant;
	private Rect rectLabelTuto;	
	private int stepMax;
	

	// Use this for initialization
	void Start () 
	{
		menuAff = 0;
		pause = false;
		endLvl = false;
		playerMort = false;
		tuto = false;

		rectRevenir.width = 300;
		rectRevenir.height = 50;
		rectRevenir.y = Screen.height / 2 - rectRevenir.height / 2;
		rectRevenir.x = Screen.width / 2 - rectRevenir.width / 2;


		rectQuitter = new Rect (Screen.width / 2 - rectRevenir.width / 2, Screen.height / 2 - rectRevenir.height / 2 + 120, 300, 50);
		rectButtonOption = new Rect (Screen.width / 2 - rectQuitter.width / 2, Screen.height / 2 - rectQuitter.height / 2 + 60, 300, 50);

		rectBoxPause = new Rect (Screen.width / 2 - rectQuitter.width / 2 - 25, Screen.height / 2 - rectQuitter.height / 2 - 75, 350, 270);

		skin = Resources.Load ("Menu/SkinMenu") as GUISkin;

		//OPTIONS
		rectButtonRetour.width = 150;
		rectButtonRetour.height = 30;
		rectButtonRetour.x = Screen.width / 2 - rectButtonRetour.width / 2;
		rectButtonRetour.y = Screen.height / 2 + 110;
		
		rectToggleDeplMouse.width = 100;
		rectToggleDeplMouse.height = 60;
		rectToggleDeplMouse.x = Screen.width / 2 - rectToggleDeplMouse.width / 2 - 75;
		rectToggleDeplMouse.y = Screen.height / 2 -60  +50;
		
		rectToggleDeplKeyboard.width = 100;
		rectToggleDeplKeyboard.height = 60;
		rectToggleDeplKeyboard.x = Screen.width / 2 - rectToggleDeplKeyboard.width / 2 + 50;
		rectToggleDeplKeyboard.y = Screen.height / 2 -60  +50;
		
		rectLabelDepl.width = 160;
		rectLabelDepl.height = 60;
		rectLabelDepl.x = Screen.width / 2 - rectLabelDepl.width / 2 - 45;
		rectLabelDepl.y = Screen.height / 2 - 85  +50;
		
		rectToggleTirAuto.width = 250;
		rectToggleTirAuto.height = 20;
		rectToggleTirAuto.x = Screen.width / 2 - rectToggleTirAuto.width / 2 ;
		rectToggleTirAuto.y = Screen.height / 2  +50;
		
		rectLabelTir.width = 160;
		rectLabelTir.height = 60;
		rectLabelTir.x = Screen.width / 2 - rectLabelTir.width / 2 - 45;
		rectLabelTir.y = Screen.height / 2 - 25  +50;
		
		rectScrollSound.width = 160;
		rectScrollSound.height = 10;
		rectScrollSound.x = Screen.width / 2 - rectScrollSound.width / 2 - 45;
		rectScrollSound.y = Screen.height / 2 + 75;


		if(PlayerPrefs.GetInt("movePrefs") == 0)
		{
			movePrefMouse = true;
			movePrefKeyboard = false;
		}
		if(PlayerPrefs.GetInt("movePrefs") == 1)
		{
			movePrefMouse = false;
			movePrefKeyboard = true;
		}
		
		if(PlayerPrefs.GetInt("tirAuto") == 0)
		{
			tirAuto = false;
		}
		if(PlayerPrefs.GetInt("tirAuto") == 1)
		{
			tirAuto = true;
		}

		soundEffects = PlayerPrefs.GetFloat ("soundEffects");



		//TUTORIEL 
		skinTuto = Resources.Load ("Menu/SkinTutorial") as GUISkin;
		
		step = 1;
		stepMax = 2;
		
		caracFenetre = new Vector2 (GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().LARGEURpx, GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().HAUTEURpx);
		
		rectCadrePrincipal = new Rect (caracFenetre.x/2 - 250, caracFenetre.y/2 - 300, 600, 600);
		rectLabelTuto = new Rect (caracFenetre.x / 2 + -225, caracFenetre.y / 2 - 225, 420, 500);
		rectButtonPasser = new Rect (caracFenetre.x / 2 + 70, caracFenetre.y / 2 + 310, 150, 40);
		rectButtonSuivant = new Rect (caracFenetre.x / 2 + -90, caracFenetre.y / 2 + 310, 150, 40);
		rectButtonPrecedent = new Rect (caracFenetre.x / 2 + -250, caracFenetre.y / 2 + 310, 150, 40);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Si on est pas dans le tutoriel
		if(!tuto)
		{
			//Et qu'on appuie sur echap
			if(Input.GetKeyDown (KeyCode.Escape))
			{
				pause = !pause;

				if(pause)
				{
					Time.timeScale = 0;
				}
				else
				{
					Time.timeScale = 1;
					menuAff = 0;
				}
			}
		}
		else
		{
			pause = true;
			Time.timeScale = 0;
		}

		if(GameObject.FindWithTag("Player"))
		{
			//le joueur existe encore
			playerMort = false;
		}
		else
		{
			playerMort = true;
		}


		if(GameObject.FindWithTag("Pattern"))
		{
			if(GameObject.FindWithTag("Pattern").GetComponent<EndLevel>().endLevel == true)
			{
				endLvl = true;	
			}
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


	void OnGUI()
	{
		//Si on a pas lancé le tutoriel
		if(!tuto)
		{
			if(pause)
			{
				if(endLvl) //Fin du niveau
				{
					GUI.skin = skin;
					rectBoxPause = new Rect (Screen.width / 2 - rectQuitter.width / 2 - 45, Screen.height / 2 - rectQuitter.height / 2 - 75, 390, 300);
					GUI.Box (rectBoxPause, "N I V E A U  T E R M I N E");
					//On pourra afficher ici les gain du niveau en ferraille et en argent
					if(GUI.Button(rectQuitter, "Quitter le niveau"))
					{
						Time.timeScale = 1.0f;
						Destroy(GameObject.Find("patternToLaunch").gameObject);
						Application.LoadLevel("MenuPrincipal");
					}
				}
				else
				{
					if(playerMort) // Si le joueur a perdu
					{
						GUI.skin = skin;
						rectBoxPause = new Rect (Screen.width / 2 - rectQuitter.width / 2 - 25, Screen.height / 2 - rectQuitter.height / 2 - 75, 350, 300);
						GUI.Box (rectBoxPause, "P E R D U");
						
						if(GUI.Button(rectQuitter, "Quitter le niveau"))
						{
							Time.timeScale = 1;
							Destroy(GameObject.Find("patternToLaunch").gameObject);
							Application.LoadLevel("MenuPrincipal");
						}
					}
					else //Menu pause classique affiché avec Echap
					{
						if(menuAff == 0)
						{
							GUI.skin = skin;
							rectBoxPause = new Rect (Screen.width / 2 - rectQuitter.width / 2 - 50, Screen.height / 2 - rectQuitter.height / 2 - 75, 400, 400);
							GUI.Box (rectBoxPause, "P A U S E");
							
							if(GUI.Button(rectQuitter, "Quitter le niveau"))
							{
								Time.timeScale = 1;
								Destroy(GameObject.Find("patternToLaunch").gameObject);
								Application.LoadLevel("MenuPrincipal");
							}
							
							if(GUI.Button (rectButtonOption, "Options"))
							{
								menuAff = 1;
							}
							
							if(GameObject.FindWithTag("Player"))
							{
								if(GUI.Button(rectRevenir, "Revenir au jeu"))
								{
									pause = false;
									Time.timeScale = 1;
									menuAff = 0;
								}
							}
						}
						if(menuAff == 1)
						{
							GUI.skin = skin;
							
							GUI.Box (rectBoxPause, "O P T I O N S");
							
							GUI.Label(rectLabelDepl, "Mode de déplacement :");
							
							//TOGGLE SOURIS
							//utiliser pour si clique alorsq ue déjà coché, pas de changement
							if(!movePrefKeyboard)
							{
								movePrefKeyboard = GUI.Toggle(rectToggleDeplKeyboard, movePrefKeyboard, " Clavier");
							}
							else
							{
								GUI.Toggle(rectToggleDeplKeyboard, movePrefKeyboard, " Clavier");
							}
							//mise a jour de l'autre toggle
							if(movePrefKeyboard)
							{
								movePrefMouse = false;
							}
							else
							{
								movePrefMouse = true;
							}
							
							//TOGGLE SOURIS
							//utiliser pour si clique alorsq ue déjà coché, pas de changement
							if(!movePrefMouse)
							{
								movePrefMouse = GUI.Toggle(rectToggleDeplMouse, movePrefMouse, " Souris");
							}
							else
							{
								GUI.Toggle(rectToggleDeplMouse, movePrefMouse, " Souris");
							}
							//mise a jour de l'autre toggle
							if(movePrefMouse)
							{
								movePrefKeyboard = false;
							}
							else
							{
								movePrefKeyboard = true;
							}
							
							//Mise a jour du playerPref
							if(movePrefKeyboard)
							{
								movePref = true;
							}
							else
							{
								movePref = false;
							}
							
							//0 -> souris    1 -> clavier
							if(movePref)
							{
								PlayerPrefs.SetInt("movePrefs", 1);
							}
							else
							{
								PlayerPrefs.SetInt("movePrefs", 0);
							}
							
							
							//TirAuto
							GUI.Label(rectLabelTir, "Mode de tir :");
							tirAuto =  GUI.Toggle(rectToggleTirAuto, tirAuto, "Tir automatique");
							if(tirAuto)
							{
								PlayerPrefs.SetInt("tirAuto", 1);
							}
							else
							{
								PlayerPrefs.SetInt("tirAuto", 0);
							}
							
							if(GUI.Button (rectButtonRetour, "R e t o u r"))
							{
								menuAff = 0;
								if(GameObject.FindWithTag("Player") != null)
								{
									for(int i =0; i < GameObject.FindWithTag("Player").GetComponent<ChassisPlayer>().ListTArme.Count; i++)
									{
										GameObject.FindWithTag("Player").GetComponent<ChassisPlayer>().ListTArme[i].GetComponent<AudioSource>().volume = soundEffects;
									}
								}
								
								//Si il on joue une flotte, on baisse le son de tous les alliés aussi
								if( GameObject.Find ("patternToLaunch").GetComponent<PatternToLaunch> ().flotte )
								{
									for (int i =0; i < GameObject.FindWithTag("flotte").GetComponent<FlotteController>().listAllie.Count; i++)
									{
										for(int j =0; j < GameObject.FindWithTag("flotte").GetComponent<FlotteController>().listAllie[i].GetComponent<ChassisAllie>().ListTArme.Count; j++)
										{
											GameObject.FindWithTag("flotte").GetComponent<FlotteController>().listAllie[i].GetComponent<ChassisAllie>().ListTArme[j].GetComponent<AudioSource>().volume = soundEffects;
										}
									}
								}
							}
							
							//Gestion Son Armes
							GUI.Label(new Rect(rectScrollSound.x, rectScrollSound.y, 150, 20), "Son des armes : ");
							soundEffects = GUI.HorizontalScrollbar(new Rect(rectScrollSound.x + 120, rectScrollSound.y + 5, rectScrollSound.width, rectScrollSound.height), soundEffects, 0.10F, 0.0F, 1.0F);
							GUI.Label(new Rect(rectScrollSound.x + rectScrollSound.width + 120, rectScrollSound.y, 50, 20), soundEffects.ToString());
							PlayerPrefs.SetFloat("soundEffects", soundEffects);
							
							
						}
					}
				}
			}
		}
		else //Si on a lancé le tutoriel (tuto = true)
		{
			GUI.skin = skinTuto;
			
			if(GUI.Button(rectButtonPasser, "Passer") || escapePush)
			{
				step = 3;
			}
			
			if(step != stepMax)
			{
				if(GUI.Button(rectButtonSuivant, "Suivant"))
				{
					step++;
				}
			}
			
			if(step > 1)
			{
				if(GUI.Button(rectButtonPrecedent, "Précédent"))
				{
					step--;
					
				}
			}
			
			
			GUI.Box(rectCadrePrincipal, "T u t o r i e l");
			
			switch(step)
			{
			case 1 :
				GUI.Label(rectLabelTuto, "Bienvenue dans Space Ship Shooter !!" + "\n\n" + 
				          "Utilisez les fleches directionnels ou\"z q s d\" pour vous déplacer sur l'écran." + "\n\n" +
				          "Utilisez \"Espace\", \"Entrée\" ou \"Clic gauche souris\" pour tirer." + "\n\n");
				break;
			case 2 :
				GUI.Label(rectLabelTuto, "Lorsque vous détruisez un adversaire, ce dernier peut laisser tomber des objets ramassables." +
				          "Ramassez les en les survolant avec votre vaisseau. La destruction d'un vaisseau vous rapporte également une quantité d'argent dépendant de son type." +
				          "\n\n" + "\n\n" + "\n\n" + 
				          "Réparations : Cet objet vous redonne (à vous ou vos alliés) 5 points de vie. Vous ne pouvez pas le ramasser si vous avez déjà toute votre vie." + 
				          "\n\n" + "\n\n" + "\n\n" + 
				          "Ferraille : Cet objet est une ressource utlisée lors de la construction d'arme dans le menu Atelier. la quantité ramassée dépend du type d'ennemi tué. " +
				          "\n\n" + "\n\n" + "\n\n" + 
				          "Munition : Cet objet multiplie les dégats de chacunes de vos armes par 2. Le bonus reste actif tant qu'il reste affiché en bas à gauche de l'écran. Cet effet ne peut pas etre cumulé.");
				GUI.Box(new Rect(rectCadrePrincipal.x + rectCadrePrincipal.width + 10, rectCadrePrincipal.y + 150, 128, 128), Resources.Load("Loot/Rep") as Texture);
				GUI.Box(new Rect(rectCadrePrincipal.x + rectCadrePrincipal.width + 10, rectCadrePrincipal.y + 300, 128, 128), Resources.Load("Loot/Fer") as Texture);
				GUI.Box(new Rect(rectCadrePrincipal.x + rectCadrePrincipal.width + 10, rectCadrePrincipal.y + 450, 128, 128), Resources.Load("Loot/Muni") as Texture);
				break;
			case 3 :
				tuto = false;
				break;
			}
		}
	}
}
