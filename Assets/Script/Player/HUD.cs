using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//____________//
/*
Script servant à afficher les informations relatives
au joueur (point de vie, bouclier, nomberes d'ennemis tués,
nombre de point etc.)
*/
//____________//

public class HUD : MonoBehaviour 
{
	//public Camera camera;

	private Rect rectBoxVie;
	private Rect rectBoxScore;
	private Rect rectBoxRes;

	private Rect rectBoxGainArgent;
	private List<int> listGainArgent;
	private float tempsAffGainActArgent;
	private float tempsAffGainMaxArgent;

	private Rect rectBoxGainferraille;
	private List<int> listGainferraille;
	private float tempsAffGainActferraille;
	private float tempsAffGainMaxferraille;

	private Rect rectBonusMuni;
	private float tempsAffBunusMuniAct;
	private float tempsAffBunusMuniMax;
	private int bonus;

	private GUISkin skin;

	// Use this for initialization
	void Start () 
	{
		//GAIN
		listGainArgent = new List<int> ();
		tempsAffGainMaxArgent = 1f;
		listGainferraille = new List<int> ();
		tempsAffGainMaxferraille = 2f;

		//PRINCIPAL
		rectBoxVie.width = 200;
		rectBoxVie.height = 30;
		rectBoxVie.x = 10; 
		rectBoxVie.y = Screen.height - 50;

		rectBoxScore.width = 300;
		rectBoxScore.height = 30;
		rectBoxScore.x = Screen.width - 800; 
		rectBoxScore.y = Screen.height - 50;

		rectBoxRes.width = 450;
		rectBoxRes.height = 30;
		rectBoxRes.x = Screen.width - 470; 
		rectBoxRes.y = Screen.height - 50;

		rectBoxGainArgent = new Rect (Screen.width - 300, Screen.height - 90, 50, 30);
		rectBoxGainferraille = new Rect (Screen.width - 190, Screen.height - 90, 50, 30);

		tempsAffBunusMuniMax = 3f;

		rectBonusMuni = new Rect(240, Screen.height - 50, 120, 30);

		skin = Resources.Load ("Menu/SkinHUD") as GUISkin;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI()
	{
		GUI.skin = skin;

		//Si le Player est encore en vie
		if(GameObject.FindWithTag("Player"))
		{
			//On affiche ses poits de vie
			GUI.Box (rectBoxVie, "Point de vie : " + GameObject.FindWithTag("Player").GetComponent<ChassisPlayer>().pointDeVie + " / " + GameObject.FindWithTag("Player").GetComponent<ChassisPlayer>().chassiActuel.pointDeVie);
			//GUI.Box (rectBoxScore, "Nombre d'ennemis tués : " + GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nbDePoints);
			GUI.Box (rectBoxRes, "Argent : " + GameObject.FindWithTag("PlayerScript").GetComponent<Player>().argent + "   ferraille : " + GameObject.FindWithTag("PlayerScript").GetComponent<Player>().ferraille);

			//Si on doit afficher un gain argent
			if(listGainArgent.Count > 0)
			{
				GUI.Box (rectBoxGainArgent, "+" + listGainArgent[0]);

				if(tempsAffGainActArgent > tempsAffGainMaxArgent)
				{
					tempsAffGainActArgent = 0;
					listGainArgent.RemoveAt(0);
				}
				else
				{
					tempsAffGainActArgent+= Time.deltaTime;
				}
			}

			//Si on doit afficher un gain ferraille
			if(listGainferraille.Count > 0)
			{
				GUI.Box (rectBoxGainferraille, "+" + listGainferraille[0]);
				
				if(tempsAffGainActferraille > tempsAffGainMaxferraille)
				{
					tempsAffGainActferraille = 0;
					listGainferraille.RemoveAt(0);
				}
				else
				{
					tempsAffGainActferraille += Time.deltaTime;
				}
			}


			if(GameObject.FindWithTag("Player"))
			{
				if(GameObject.FindWithTag("Player").GetComponent<ChassisPlayer>().changerMun)
				{
					GUI.Box (rectBonusMuni, "Degats x" + bonus);
				}
			}
		}
	}

	public void afficherGainArgent(int gain)
	{
		if(gain > 0)
		{
			listGainArgent.Add (gain);
		}
	}

	public void afficherGainferraille(int gain)
	{
		if(gain > 0)
		{
			listGainferraille.Add (gain);
		}
	}

	public void afficherBonusMuni(int p_bonus, float duree)
	{
		bonus = p_bonus;
		tempsAffBunusMuniMax = duree;
	}
}
