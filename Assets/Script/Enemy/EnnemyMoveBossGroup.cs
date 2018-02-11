using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnnemyMoveBossGroup : Group 
{
	//new public float vitesse;
	private Vector2 deplacement;
	private Vector2 positionDepart;

	private float positionXmax;

	private float tempsTirMax;
	private float tempsTirAct;

	private bool haut;
	private bool bas;
	private bool canMove;

	private float tempsPause;
	private float tempsPauseMax;

	public bool joinCenter;
	public bool waitingForOrder;

	public bool bossDead;

	private float vitesse;
	
	// Use this for initialization
	void Start () 
	{
		vitesse = 1.5f;
		tempsTirMax = 8;
		haut = false;
		bas = true;
		tempsPauseMax = 2;
		canMove = true;
		waitingForOrder = false;
		bossDead = false;
		positionXmax = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x -3;

		positionDepart = listEnemy[0].transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		bossDead = true;
		for(int i = 0; i < listEnemy.Count; i++)
		{
			if(listEnemy[i].GetComponent<ChassisEnemy>().nom == "Croiseur")
			{
				bossDead = false;
			}
		}


		if(!joinCenter)
		{
			//Il s'approche jusqu'au centre
			if(transform.position.x >= positionXmax)
			{
				deplacement = new Vector2 (-vitesse, 0);
				transform.Translate (deplacement * Time.deltaTime, Space.World);
			}
			else //Il sont au centre, mouvement verticaux avec pause haut et bas
				//On tire en arrivant en haut et en repartant (idem bas)	
			{
				if(haut) //Descente
				{
					if(canMove)
					{
						if(transform.position.y < positionDepart.y - 2.2) //Si il arrive en bas
						{
							//TIR
							for(int i = 0; i < listEnemy.Count; i++)
							{
								listEnemy[i].GetComponent<ChassisEnemy>().Fire();
							}
							bas = true;
							haut = false;
							canMove = false;
						}
						else
						{
							deplacement = new Vector2 (0, -vitesse);
							transform.Translate (deplacement * Time.deltaTime, Space.World);
						}
					}
					else
					{
						tempsPause += Time.deltaTime;
						if(tempsPause > tempsPauseMax)
						{
							//TIR
							for(int i = 0; i < listEnemy.Count; i++)
							{
								listEnemy[i].GetComponent<ChassisEnemy>().Fire();
							}
							canMove = true;
							tempsPause = 0;
						}
					}
				}
				if(bas)
				{
					if(canMove)
					{
						if(transform.position.y > positionDepart.y + 2.2) //Si il arrive en bas
						{
							//TIR
							for(int i = 0; i < listEnemy.Count; i++)
							{
								listEnemy[i].GetComponent<ChassisEnemy>().Fire();
							}
							bas = false;
							haut = true;
							canMove = false;
						}
						else
						{
							deplacement = new Vector2 (0, vitesse);
							transform.Translate (deplacement * Time.deltaTime, Space.World);
						}
					}
					else
					{
						tempsPause += Time.deltaTime;
						if(tempsPause > tempsPauseMax)
						{
							//TIR
							for(int i = 0; i < listEnemy.Count; i++)
							{
								listEnemy[i].GetComponent<ChassisEnemy>().Fire();
							}
							canMove = true;
							tempsPause = 0;
						}
					}
				}
			}
		}
		else//joincenter
		{
			if(transform.position.y >= 0.1 )
			{
				deplacement = new Vector2 (0, -vitesse);
				transform.Translate (deplacement * Time.deltaTime, Space.World);
			}
			else
			{
				if(transform.position.y <= -0.1)
				{
					deplacement = new Vector2 (0, vitesse);
					transform.Translate (deplacement * Time.deltaTime, Space.World);
				}
			}

			//Si ils ont atteint le centre, on repart comme avant
			if(transform.position.y <= 0.1 && transform.position.y >= -0.1)
			{
				waitingForOrder = true;
			}
		}


		chkIfEmpty ();
	}
}
