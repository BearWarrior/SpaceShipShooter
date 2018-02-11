using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
	//CHASSI
	public ChassisPlayer m_chassisPlayer;
	public Transform chassis;
	public Chassis scriptChassi;
	public string nomChassis;

	public List<List<string>> listArmeParVaisseau; //listArmeParVaisseau.Count -> nbDeChassi __ listArmeParVaisseau[i].Count -> nb d'arme sur le chassi i

	public Transform player;


	//ARGENT, RESSOURCES, MATERIAUX DE CONSTRUCTION
	public int argent;
	public int ferraille;

	//POINTS
	public int nbDePoints;
	
	//tempo save
	float tempsSaveActuel = 0;
	float tempsSaveMax = 1;


	//Liste de plan d'armes (Les plans ne disparaissent pas une fois acquis)
	public List<string> listPlanArme;

	//Liste d'arme utilisable par le joueur (une fois l'arme utilisé, elle disparait de la liste / elle réapparait lorsque le joueur retire l'arme d'un chassi)
	public List<string> listArmeInventaire;

	//Liste de chassi utilisable par le joueur 
	public List<string> listChassiInventaire;
	
	// Use this for initialization
	void Start ()
	{

		//Vide la sauvegarde de l'inventaire
		//PlayerPrefs.DeleteKey ("listArmeInventaire");

		DontDestroyOnLoad (this);

		nbDePoints = 0;

		//Sera modifié par Load
		listArmeParVaisseau = new List<List<string>> ();
		listArmeParVaisseau.Add (new List<string> ());
		listArmeParVaisseau [0].Add ("LanceMissile");
		listArmeParVaisseau [0].Add ("LanceMissile");
		listArmeParVaisseau [0].Add ("LanceMissile");
		listArmeParVaisseau [0].Add ("LanceMissile");
		listArmeParVaisseau.Add (new List<string> ());
		listArmeParVaisseau [1].Add ("LanceMissile");
		listArmeParVaisseau [1].Add ("LanceMissile");
		listArmeParVaisseau.Add (new List<string> ());
		listArmeParVaisseau [2].Add ("LanceMissile");
		listArmeParVaisseau [2].Add ("LanceMissile");


		//Charger la partie
		Load ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		listArmeInventaire.Sort ();

		//On sauve toutes les 15 secondes
		if(tempsSaveActuel > tempsSaveMax)
		{
			Save();
			tempsSaveActuel = 0;
		}
		else
		{
			tempsSaveActuel += Time.deltaTime;
		}
	}
	

	public void CreateVaisseauPlayer()
	{
		float posXplayer = (GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x - GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.x) / 4 - GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x;
		Transform crea = Instantiate(player, new Vector3(posXplayer, 0, -15), new Quaternion(0, 0, 0, 0)) as Transform;
		crea.tag = "Player";

		GameObject.FindWithTag("Player").AddComponent <ChassisPlayer>();
	}




	//LOOT
	public void GagnerArgent(int somme)
	{
		argent += somme;
	}
	public void Gagnerferraille(int nb)
	{
		ferraille += nb;
		GameObject.Find("HUD").GetComponent<HUD>().afficherGainferraille(nb);
	}




	//ACHAT
	public bool AcheterArme(string nom, int prixArmeArgent, int prixArmeFerraille)
	{
		if(prixArmeArgent <= argent && prixArmeFerraille <= ferraille)
		{
			argent -= prixArmeArgent;
			ferraille -= prixArmeFerraille;
			listArmeInventaire.Add(nom);
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool AcheterPlan(string nom, int prixPlanArgent)
	{
		if(prixPlanArgent <= argent)
		{
			argent -= prixPlanArgent;
			listPlanArme.Add(nom);
			return true;
		}
		else
		{
			return false;
		}
	}


	//SAUVEGARDE / CHARGEMENT	
	public string temp;
	public string[] tempArray;
	public string[] tempArray2;
	public string[] tempArray3;

	public string tempPlan;
	public string[] tempPlanArray;

	public void Save()
	{
		PlayerPrefs.SetInt("argent", argent);
		PlayerPrefs.SetInt("ferraille", ferraille);


		//Sauvegarde de l'inventaire d'arme du joueur
		string temp = "";
		for(int i=0;i< listArmeInventaire.Count;i++)
		{ 
			if(i!=listArmeInventaire.Count-1)
				temp+=listArmeInventaire[i]+"*";//note that the last character you add //is important
			else temp+=listArmeInventaire[i]; 
		}

		temp += "_"; // inventaire _ armeParVaisseau

		//On rajoute les armes qui sont sur les vaisseau, si elles sont différentes de LanceMissile
		//Pour chaque vaisseau
		for(int vaisseau =0; vaisseau < listArmeParVaisseau.Count; vaisseau++)
		{
			//Pour chaque arme
			for(int arme =0; arme < listArmeParVaisseau[vaisseau].Count; arme++)
			{

				if(arme == listArmeParVaisseau[vaisseau].Count-1 && vaisseau == listArmeParVaisseau.Count -1)
				{
					temp+=listArmeParVaisseau[vaisseau][arme];
				}
				else
				{
					temp+=listArmeParVaisseau[vaisseau][arme]+"*";
				}
			}
		}
		//Inverser les deux lignes suivantes pour reset l'inventaire et les saves
		PlayerPrefs.SetString("listArmeInventaire",temp);
		//PlayerPrefs.SetString("listArmeInventaire","");

		//Sauvegarde des plans du joueur
		string tempPlan = "";
		for(int i=0;i< listPlanArme.Count;i++)
		{ 
			if(i!=listPlanArme.Count-1)
				tempPlan+=listPlanArme[i]+"*";//note that the last character you add //is important
			else tempPlan+=listPlanArme[i]; 
		}

		//Inverser les deux lignes suivantes pour reset l'inventaire et les saves
		PlayerPrefs.SetString("listPlanInventaire",tempPlan);
		//PlayerPrefs.SetString("listPlanInventaire","");
	}




	public void Load()
	{
		argent = PlayerPrefs.GetInt ("argent");
		ferraille = PlayerPrefs.GetInt ("ferraille");

		//Charger l'inventaire d'arme du joueur
		temp=PlayerPrefs.GetString("listArmeInventaire");
		if(temp != "")
		{
			tempArray=temp.Split("_".ToCharArray()); // inventaire _ armeParVaisseau
			if(tempArray.Length > 1)
			{
				if(tempArray[0] != "")
				{
					tempArray2=tempArray[0].Split("*".ToCharArray()); //inventaire
				}
				else
				{
					tempArray2=tempArray[0].Split("*".ToCharArray()); //inventaire
					tempArray2[0] = "LanceMissile";
				}
				if(tempArray.Length > 1)
				{
					tempArray3=tempArray[1].Split("*".ToCharArray()); //armeParVaisseau
				}

			}


			listArmeInventaire.Clear();

			for(int i=0;i<tempArray2.Length;i++)
			{ 
				listArmeInventaire.Add(tempArray2[i]);
			}


			if(tempArray3.Length != 0)
			{
				int tmp3Count = 0;
				for(int vaisseau =0; vaisseau < listArmeParVaisseau.Count; vaisseau++)
				{
					for(int arme =0; arme < listArmeParVaisseau[vaisseau].Count; arme++)
					{
						listArmeParVaisseau[vaisseau][arme] = tempArray3[tmp3Count];
						tmp3Count++;
					}
				}
			}

		}


		tempPlan = PlayerPrefs.GetString("listPlanInventaire");
		if(tempPlan != "")
		{
			tempPlanArray = tempPlan.Split("*".ToCharArray()); //inventaire

			for(int i=0;i<tempPlanArray.Length;i++)
			{ 
				listPlanArme.Add(tempPlanArray[i]);
			}
		}


		bool ok = false;
		for(int i = 0; i < listArmeInventaire.Count; i++)
		{
			if(listArmeInventaire[i] == "LanceMissile")
			{
				ok = true;
			}
		}
		if(ok == false)
		{
			listArmeInventaire.Add("LanceMissile");
		}
	}
}
