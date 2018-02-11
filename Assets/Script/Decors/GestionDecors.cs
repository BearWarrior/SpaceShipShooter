using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestionDecors : MonoBehaviour {

	GameObject fondEtoile; 
	GameObject neb1; 
	GameObject neb2; 
	GameObject decorsCree;
	GameObject decorsCree2;


	List<GameObject> listNeb;

	public struct minMax
	{
		public float max;
		public float min;
	}


	// Use this for initialization
	void Start () 
	{
		//Chargement es différentes ressources (fond etoile, neb1, neb2)
		fondEtoile = Resources.Load<GameObject> ("BackGround/BackGStar");
		neb1 = Resources.Load<GameObject> ("BackGround/nebuleuse1");
		neb2 = Resources.Load<GameObject> ("BackGround/nebuleuse2");

		listNeb = new List<GameObject> ();
		listNeb.Add (Resources.Load<GameObject> ("BackGround/Nebula/N1"));
		listNeb.Add(Resources.Load<GameObject>("BackGround/Nebula/N2"));
		listNeb.Add(Resources.Load<GameObject>("BackGround/Nebula/N3"));
		listNeb.Add(Resources.Load<GameObject>("BackGround/Nebula/N4"));
		listNeb.Add(Resources.Load<GameObject>("BackGround/Nebula/N5"));
		listNeb.Add (Resources.Load<GameObject> ("BackGround/Nebula/N6"));
		listNeb.Add (Resources.Load<GameObject> ("BackGround/Nebula/N7"));

		//Création des 3 BackGround initiaux
		CreateBG (-30,0,1);
		CreateBG (10,0,1);
		CreateBG (50,0,1);
		CreateBG (90,0,1);
	}

	//appelé à chaque destruction de décors
	public void CreateDecors()
	{
		CreateBG (90,0,1);
	}

	void CreateBG(int x, int y, int z)
	{
		//Position de la nébuleuse sur le fond etoile (+ taille)
		Vector3 posNeb;
		float scaleNeb;

		//Definition des constantes min max (scale et pos)
		minMax posNebx;
		minMax posNeby;
		float scaleNebMin;
		float scaleNebMax;
		posNebx.min = -12;
		posNebx.max = 12;
		posNeby.min = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.x;
		posNeby.max = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x;
		scaleNebMin = 0.6f;
		scaleNebMax = 1f;

		//On crée le fond étoilé à la position envoyé en paramètre
		decorsCree = Instantiate (fondEtoile, new Vector3(x, y, z), new Quaternion(0,0,0,0)) as GameObject;
		decorsCree.transform.parent = GameObject.FindWithTag ("BackGround").transform;

		//On place une valeur aléatoire dans la dimension et la position (neb1)
		scaleNeb = Random.Range (scaleNebMin, scaleNebMax);
		posNeb = new Vector3 (Random.Range (posNebx.min, posNebx.max), Random.Range (posNeby.min, posNeby.max), -0.001f);

		//On instancie la nébuleuse 1, on la met en enfant du fond etoile, on la repositionne et redimensionne.
		decorsCree2 = Instantiate (listNeb[Random.Range(0, 6)], Vector3.zero, new Quaternion(0,0,0,0)) as GameObject;
		decorsCree2.transform.parent = decorsCree.transform;
		decorsCree2.transform.localScale = new Vector3 (scaleNeb, scaleNeb, 1);
		decorsCree2.transform.localPosition = posNeb;
	}
}



