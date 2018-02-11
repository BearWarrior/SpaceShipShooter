using UnityEngine;
using System.Collections;

/*
Ce script rassemblera les probabilité de loot des différents bonus.
On viendra appeler la fonction Loot à chaque destruction d'ennemi (avant de préférence)
et la fonction fera apparataitre (ou non) le loot sur la carte à la position de l'ennemi.
Il prendra en entrée le nom de l'ennemi pour les différents calculs de probabilités.

Loot :
Réparation du vaisseau
Bouclier (temporaire, ou non)
Munitions perforantes
etc.
*/

public struct probaLootStruct //en pourcentage (ex : 10)
{
	public int repVaiseeau;
	//public int bouclier;
	public int munPerf;
	public float ferraille;
}


public class LootScript: MonoBehaviour 
{
	public probaLootStruct probaActuel;

	private Transform repVaisseau;
	private Transform munPerf;
	private Transform ferraille;

	// Use this for initialization
	void Start () 
	{
		//ENEMY 1
		probaActuel.repVaiseeau = 5;
		//probaActuel.bouclier = 10;
		probaActuel.munPerf = 5;
		probaActuel.ferraille = 5;
	}
	
	// Update is called once per frame
	void Update () 
	{
		repVaisseau = Resources.Load<Transform> ("Loot/RepVaisseau");
		munPerf = Resources.Load<Transform> ("Loot/MunPerforantes");
		ferraille = Resources.Load<Transform> ("Loot/ferraille");
	}

	public void Loot(string nom, Vector3 pos, int nbFerraille, float probaLootFer)
	{
		probaActuel.ferraille = probaLootFer;


		if(Random.Range(0, 100) <= probaActuel.repVaiseeau)
		{
			Transform loot = Instantiate(repVaisseau, pos, new Quaternion(0,0,0,0)) as Transform;
		}
		if(Random.Range(0, 100) <= probaActuel.munPerf)
		{
			Transform loot = Instantiate(munPerf, pos, new Quaternion(0,0,0,0)) as Transform;

		}
		if(Random.Range(0, 100) <= probaActuel.ferraille)
		{	
			Transform loot = Instantiate(ferraille, pos, new Quaternion(0,0,0,0)) as Transform;
			loot.GetComponent<Ferraille>().ferraille_ = nbFerraille;
		}
	}

	public void LootHealth(Vector3 pos)
	{
		Transform loot = Instantiate(repVaisseau, pos, new Quaternion(0,0,0,0)) as Transform;
	}
}



