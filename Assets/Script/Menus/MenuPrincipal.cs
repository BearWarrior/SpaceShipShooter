using UnityEngine;
using System.Collections;

public class MenuPrincipal : MonoBehaviour 
{
	//PRINCIPAL
	private Rect rectBox;
	private Rect rectButtonPlay;
	private Rect rectButtonOption;
	private Rect rectButtonQuit;
	private Rect rectLabel;
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
	private Rect rectScrollSound;
	private float soundEffects;


	
	private int menuAff; //0 -> MenuPrincipal   1 -> Options

	private GUISkin skin;


	// Use this for initialization
	void Start ()
	{
		menuAff = 0;
		
		//PRINCIPAL
		rectBox.width = 400;
		rectBox.height = 300;
		rectBox.x = Screen.width / 2 - rectBox.width / 2; 
		rectBox.y = Screen.height / 2 - rectBox.height / 2;
		
		rectButtonPlay.width = 200;
		rectButtonPlay.height = 40;
		rectButtonPlay.x = Screen.width / 2 - rectButtonPlay.width / 2;
		rectButtonPlay.y = Screen.height / 2 - 60;
		
		rectButtonOption.width = 200;
		rectButtonOption.height = 40;
		rectButtonOption.x = Screen.width / 2 - rectButtonOption.width / 2;
		rectButtonOption.y = Screen.height / 2 ;
		
		rectButtonQuit.width = 200;
		rectButtonQuit.height = 40;
		rectButtonQuit.x = Screen.width / 2 - rectButtonQuit.width / 2;
		rectButtonQuit.y = Screen.height / 2 + 60;
		

		//OPTIONS
		rectButtonRetour.width = 150;
		rectButtonRetour.height = 30;
		rectButtonRetour.x = Screen.width / 2 - rectButtonRetour.width / 2 + 100;
		rectButtonRetour.y = Screen.height / 2 + 110;

		rectToggleDeplMouse.width = 100;
		rectToggleDeplMouse.height = 60;
		rectToggleDeplMouse.x = Screen.width / 2 - rectToggleDeplMouse.width / 2 - 75;
		rectToggleDeplMouse.y = Screen.height / 2 -60;

		rectToggleDeplKeyboard.width = 100;
		rectToggleDeplKeyboard.height = 60;
		rectToggleDeplKeyboard.x = Screen.width / 2 - rectToggleDeplKeyboard.width / 2 + 50;
		rectToggleDeplKeyboard.y = Screen.height / 2 -60;

		rectLabelDepl.width = 160;
		rectLabelDepl.height = 60;
		rectLabelDepl.x = Screen.width / 2 - rectLabelDepl.width / 2 - 45;
		rectLabelDepl.y = Screen.height / 2 - 85;

		rectToggleTirAuto.width = 250;
		rectToggleTirAuto.height = 20;
		rectToggleTirAuto.x = Screen.width / 2 - rectToggleTirAuto.width / 2 ;
		rectToggleTirAuto.y = Screen.height / 2;

		rectLabelTir.width = 160;
		rectLabelTir.height = 60;
		rectLabelTir.x = Screen.width / 2 - rectLabelTir.width / 2 - 45;
		rectLabelTir.y = Screen.height / 2 - 25;

		rectScrollSound.width = 160;
		rectScrollSound.height = 10;
		rectScrollSound.x = Screen.width / 2 - rectScrollSound.width / 2 - 45;
		rectScrollSound.y = Screen.height / 2 + 50;


		if(PlayerPrefs.GetInt("movePrefs") == 0)
		{
			//movePref = false;
			movePrefMouse = true;
			movePrefKeyboard = false;
		}
		if(PlayerPrefs.GetInt("movePrefs") == 1)
		{
			//movePref = true;
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

		//Gestion Son Armes
		if(!PlayerPrefs.HasKey("soundEffects"))
		{
			PlayerPrefs.SetFloat("soundEffects", 0.4f);
		}
		soundEffects = PlayerPrefs.GetFloat ("soundEffects");
		GameObject.Find ("Menuprincipal").GetComponent<AudioSource>().volume = soundEffects;

		skin = Resources.Load ("Menu/SkinMenu") as GUISkin;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
	void OnGUI()
	{

		GUI.skin = skin;


		if(menuAff == 0)
		{
			GUI.Box (rectBox, "M e n u  P r i n c i p a l");
			

			if(GUI.Button (rectButtonPlay, "JOUER"))
			{
				GetComponent<AudioSource>().Play();
				GetComponent<MenuIntermediaire>().enabled = true;
				GetComponent<MenuPrincipal>().enabled = false;
			}
			
			if(GUI.Button (rectButtonOption, "OPTIONS"))
			{
				GetComponent<AudioSource>().Play();
				menuAff = 1;
			}
			
			if(GUI.Button (rectButtonQuit, "QUITTER"))
			{
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().Save();
				Application.Quit();
			}
		}
		
		//Menu Option
		if(menuAff == 1)
		{
			GUI.Box (rectBox, "O p t i o n s");

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

			if(GUI.Button (rectButtonRetour, "R e t o u r") || Input.GetKey(KeyCode.Escape))
			{
				GetComponent<AudioSource>().Play();
				menuAff = 0;

				GameObject.Find ("Menuprincipal").GetComponent<AudioSource>().volume = soundEffects;
			}

			//Gestion Son Armes
			GUI.Label(new Rect(rectScrollSound.x, rectScrollSound.y, 250, 20), "Volume des effets sonores : ");
			soundEffects = GUI.HorizontalScrollbar(new Rect(rectScrollSound.x + 20, rectScrollSound.y + 25, rectScrollSound.width, rectScrollSound.height), soundEffects, 0.10F, 0.0F, 1.0F);
			GUI.Label(new Rect(rectScrollSound.x + rectScrollSound.width + 20, rectScrollSound.y + 20, 50, 20), soundEffects.ToString());
			PlayerPrefs.SetFloat("soundEffects", soundEffects);

		}
	}
}