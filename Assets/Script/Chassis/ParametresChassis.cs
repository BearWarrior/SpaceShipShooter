using UnityEngine;
using System.Collections;

using System.Collections.Generic;

//______//
/*
Ici on stockera toutes les informations au sujet
des chassis (nb de point de vie, nb de point d'attache,
pos des différents points d'attache).
Les script iront chercher les infos nécessaires ici.
Il est donc affilié (tout le temps) au PlayerScript
qui suit le joueur dans chaque Scene
*/
//_____//


public struct structChassis
{
	public int pointDeVie;
	public int nbPointDattache;
	public float vitesse;
	public List<Vector3> posArme;
	public int nbReacteurs;
	public List<Vector3> posReacteur;
	public int nbFumee;
	public List<Vector3> posFumées;
} 

public class ParametresChassis : MonoBehaviour 
{
	public structChassis c1;
	public structChassis c2;
	public structChassis c3;

	int cptCrea = 0;

	void Start () 
	{
		//__________________CHASSI 1
		c1.pointDeVie = 10;	
		c1.nbPointDattache = 4;
		c1.vitesse = 7;
		
		//Tableau fixe donnant la position sur le vaisseau des différentes armes
		c1.posArme = new List<Vector3>();
		// On crée autant de case qu'il le faut qu'on initialise à zéro (position)
		for( cptCrea =0; cptCrea<c1.nbPointDattache; cptCrea++)
		{
			c1.posArme.Add(Vector3.zero);
		}
		// Et on les remplit avec les positions que l'on veut
		c1.posArme[0] = new Vector2 (-0.13f, 0.55f);
		c1.posArme[1] = new Vector2 (-0.022f, 0.28f);
		c1.posArme[2] = new Vector2 (-0.022f, -0.28f);
		c1.posArme[3] = new Vector2 (-0.13f, -0.55f);
		//Gestion des réacteurs
		c1.nbReacteurs = 1;
		c1.posReacteur = new List<Vector3>();
		c1.posReacteur.Add (Vector2.zero);
		c1.posReacteur[0] = new Vector2 (-0.22f, 0);
		//Gestion de la fumée
		c1.nbFumee = 3;
		c1.posFumées = new List<Vector3> ();
		c1.posFumées.Add(new Vector3(0.07f, 0.6f, -0.13f));
		c1.posFumées.Add(new Vector3(-0.2f, -0.4f, -0.17f));
		c1.posFumées.Add(new Vector3(0f, 0.2f, -0.14f));


		//__________________CHASSI 2
		c2.pointDeVie = 10;	
		c2.nbPointDattache = 2;
		c2.vitesse = 10;
		
		//Tableau fixe donnant la position sur le vaisseau des différentes armes
		c2.posArme = new List<Vector3>();
		// On crée autant de case qu'il le faut qu'on initialise à zéro (position)
		for( cptCrea =0; cptCrea<c2.nbPointDattache; cptCrea++)
		{
			c2.posArme.Add(Vector2.zero);
		}
		// Et on les remplit avec les positions que l'on veut
		c2.posArme[0] = new Vector2 (-0.03f, 0.35f);
		c2.posArme[1] = new Vector2 (-0.03f, -0.35f);

		//Gestion des réacteurs
		c2.nbReacteurs = 1;
		c2.posReacteur = new List<Vector3>();
		c2.posReacteur.Add (Vector2.zero);
		c2.posReacteur[0] = new Vector2 (-0.4f, 0);
		//Gestion de la fumée
		c2.nbFumee = 3;
		c2.posFumées = new List<Vector3> ();
		c2.posFumées.Add(new Vector3(0f, 0.55f, -0.1f));
		c2.posFumées.Add(new Vector3(0.19f, 0.11f, -0.2f));
		c2.posFumées.Add(new Vector3(-0.06f, -0.26f, -0.1f));



		//__________________CHASSI 3
		c3.pointDeVie = 20;	
		c3.nbPointDattache = 2;
		c3.vitesse = 7;

		//Tableau fixe donnant la position sur le vaisseau des différentes armes
		c3.posArme = new List<Vector3>();
		// On crée autant de case qu'il le faut qu'on initialise à zéro (position)
		for( cptCrea =0; cptCrea<c3.nbPointDattache; cptCrea++)
		{
			c3.posArme.Add(Vector3.zero);
		}
		// Et on les remplit avec les positions que l'on veut
		c3.posArme[0] = new Vector2 (0.15f, 0.3f);
		c3.posArme[1] = new Vector2 (0.15f, -0.3f);

		//Gestion des réacteurs
		c3.nbReacteurs = 1;
		c3.posReacteur = new List<Vector3>();
		c3.posReacteur.Add (Vector2.zero);
		c3.posReacteur[0] = new Vector2 (-0.55f, 0);
		//Gestion de la fumée
		c3.nbFumee = 3;
		c3.posFumées = new List<Vector3> ();
		c3.posFumées.Add(new Vector3(0.16f, 0.47f, -0.2f));
		c3.posFumées.Add(new Vector3(0.33f, 0.17f, -0.2f));
		c3.posFumées.Add(new Vector3(-0.04f, -0.29f, -0.1f));
	}
}
