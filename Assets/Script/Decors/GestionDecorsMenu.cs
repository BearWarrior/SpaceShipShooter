using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestionDecorsMenu : MonoBehaviour {

	GameObject fondEtoile; 
	GameObject decorsCree;
	
	// Use this for initialization
	void Start () 
	{
		fondEtoile = Resources.Load<GameObject> ("BackGround/BackGStar");

		//Création des 3 BackGround initiaux
		CreateBG (-30,0,1);
		CreateBG (10,0,1);
		CreateBG (50,0,1);
	}

	//appelé à chaque destruction de décors
	public void CreateDecors()
	{
		CreateBG (90,0,1);
	}

	void CreateBG(int x, int y, int z)
	{
		//On crée le fond étoilé à la position envoyé en paramètre
		decorsCree = Instantiate (fondEtoile, new Vector3(x, y, z), new Quaternion(0,0,0,0)) as GameObject;
		decorsCree.transform.parent = GameObject.FindWithTag ("BackGround").transform;
	}
}