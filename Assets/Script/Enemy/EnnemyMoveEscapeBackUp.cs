using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnnemyMoveEscapeBackUp : EnemyMove 
{
	//public float vitesse;
	public List<float> listVitesse;
	public float vitesseMax;
	private Vector2 deplacement;
	private Vector2 positionDepart;
	private float temps;
	private float deplacementSin;

	public float tempsTirGroupeMax;
	public float tempsTirGroupeAct;
	public bool shootGr;
	public bool shootInd;
	
	public float tempsTirIndMax;
	public float tempsTirIndAct;
	public int cptTir;

	private List<Transform> listEnemyIntern;
	
	private float angleMax;
	private List<float> angleAct;
	public float vitesseAng;

	public List<bool> back;
	public List<bool> up;

	private List<bool> shoot;
	// Use this for initialization
	void Start () 
	{
		vitesse = 2.5f;

		shootGr = false;
		shootInd = false;
		tempsTirGroupeMax = 1;
		tempsTirIndMax = 0.3f;
		cptTir = 0;

		listEnemyIntern = new List<Transform> ();
		for(int i = 0; i < listEnemy.Count; i++)
		{
			listEnemyIntern.Add(listEnemy[i]);
		}

		angleMax = 2;
		angleAct = new List<float>();
		up = new List<bool> ();
		back = new List<bool>();
		listVitesse = new List<float> ();
		shoot = new List<bool> ();
		for(int i = 0; i < listEnemyIntern.Count; i++)
		{
			angleAct.Add(0);
			up.Add(false);
			back.Add(false);
			listVitesse.Add(vitesse);
			shoot.Add(true);
		}
		vitesseAng = vitesse;
		vitesseMax = vitesse;
	}
	
	// Update is called once per frame
	void Update () 
	{
	 	//Pour chaque enemy du groupe interne
		for(int i = 0; i < listEnemyIntern.Count; i++)
		{
			//Si il n'a pas été détruit
			if(listEnemyIntern[i] != null)
			{
				//Si il arrive trop a gauche, on lace la phase deretour en arriere
				if(listEnemyIntern[i].transform.position.x < GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerDownLeft.x * 1/4/* + (GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.y - transform.position.y)*/)
				{
					back[i] = true;
					if(shoot[i])
					{
						listEnemyIntern[i].GetComponent<ChassisEnemy>().Fire();
						shoot[i] = false;
					}
				}

				//Si l'enemy doit revenir en arriere
				if(back[i]) //l'enemy doit faire un demi-tour (vitesse : max -> -max  et vitesseAng  0 -> max -> 0)
				{
					//Si il n'est pas encore en haut
					if(!up[i])
					{
						//On augment progressivement sa vitesse en y (jusqu'à angelMax) et on diminue sa vitesse en x (jusqu'à 0)
						if(angleAct[i] < angleMax)
						{
							angleAct[i] += vitesseAng*Time.deltaTime;
							listVitesse [i] -= vitesseAng*Time.deltaTime;
						}
						else
						{
							angleAct[i] = angleMax;
							up[i] = true;
							listVitesse[i] = 0;
						}
					}
					else //Si on est en haut
					{
						//On diminue progressivement sa vitesse en y (jusqu'à 0) et sa vitesse en x (jusqu'à -vitesseMax pour le faire reculer)
						if(angleAct[i] > 0)
						{
							angleAct[i] -= vitesseAng*Time.deltaTime;
							listVitesse [i] -= vitesseAng*Time.deltaTime;;
						}
						else
						{
							angleAct[i] = 0;
							listVitesse [i] = -vitesseMax;
						}
					}

					//On définit le nouveau vecteur de déplacement
					deplacement = new Vector2 (-listVitesse[i], angleAct[i]);

					Quaternion quat = Quaternion.Euler (0, 0, 270 + 90*(listVitesse[i]/vitesseMax));//  /*45 * angleAct[i]/angleMax)*/;
					listEnemyIntern[i].transform.rotation = quat;
					
				}
				else // il avance juste tout droit
				{
					deplacement = new Vector2 (-listVitesse[i], 0);
				}

				//On applique le vecteur de deplacement
				listEnemyIntern[i].transform.Translate (deplacement * Time.deltaTime, Space.World);
			}
		}
		
		chkIfEmpty ();
	}
}
