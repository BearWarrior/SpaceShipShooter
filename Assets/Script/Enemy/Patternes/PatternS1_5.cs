using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternS1_5 : MonoBehaviour 
{
	//Model3D des enemy utilisés dans ce script
	public Transform enemy;	
	public Transform Croiseur;
	public Transform Mine;

	//Gestion des vagues
	private float tempsActuel;
	public float frequenceApparition;
	bool beginGroup;
	public float posMax; //Pos min et max en Y de l'apparition de la vague, vérifié par ListGroupEnemy a la création
	public float posMin;
	public int cptVague;

	//Change la fréquence d'apparition des vagues
	//private float tempsChangeFreq;
	//private float tempsChangeFreqMax;

	private bool endLevel;

	public float tempsAvantBoss;
	public float tempsAvantBossMax;
	public bool bossLaunched;

	public bool bossWaveFinished;
	
	private float tempsAvantFinLvlAct;
	private float tempsAvantFinLvlMax;

	private float bordDroite;

	private bool gain;

	// Use this for initialization
	void Start () 
	{
		enemy = Resources.Load<Transform> ("Vaisseau/Ennemis/EnnemiBase/EnnemiBase");
		Croiseur = Resources.Load<Transform> ("Vaisseau/Ennemis/Croiseur/Croiseur");
		Mine = Resources.Load<Transform> ("Vaisseau/Ennemis/Mine/Mine");

		posMax = 3;
		posMin = -3;
		frequenceApparition = 10;

		//tempsChangeFreqMax = 20;
		beginGroup = true;

		cptVague = 0;
		endLevel = false;
		tempsAvantBossMax = 15;
		bossLaunched = false;
		bossWaveFinished = false;
		endLevel = false;

		bordDroite = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x;

		gain = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(bossWaveFinished)
		{
			endLevel = true;
		}
		//Si le boss de fin n'est pas mort, le niveau n'est pas fini
		if(endLevel == false)
		{
			//Nombre de vague avant le boss
			if(cptVague < 6)
			{
				//On fait arriver les enemy groupe par groupe ...
				if(beginGroup)
				{
					//... en piochant parmi les patternes (présent dasn ListGroupEnemy)
					switch(cptVague) //
					{
					case 0 :
						ListGroupEnemy.Vgroup("SplineHorCenter");
						break;
					case 1 :
						ListGroupEnemy.Igroup("SplineCenterUp");
						break;
					case 2 :
						ListGroupEnemy.Igroup("SplineCenterDown");
						break;
					case 3 :
						ListGroupEnemy.Vgroup("SplineHorUp1");
						break;
					case 4 :
						ListGroupEnemy.Vgroup("SplineHorDown1");
						break;
					case 5 :
						ListGroupEnemy.Igroup("SplineCenterBackUp");
						ListGroupEnemy.Igroup("SplineCenterBackDown");
						break;
					}

					cptVague++;

					beginGroup = false;
				}
				else //Une fois un groupe lancé, on lance la tempo pour le suivant
				{
					tempsActuel += Time.deltaTime;
					if(tempsActuel > frequenceApparition)
					{
						tempsActuel = 0;
						beginGroup = true;
					}
				}
			}
			//Le nombre de vague avant le boss est atteint, on lance la phase du boss
			else
			{
				//Si le boss n'est pas lancé (pour le faire une seule fois)
				if(!bossLaunched)
				{
					//Si la durée entre la derniere vague et mtn est assez grande
					if(tempsAvantBoss > tempsAvantBossMax)
					{
						//On lance le boss
						GameObject.FindWithTag("Pattern").GetComponent<ListGroupEnemy>().LaunchCroiseurGroup(enemy, Croiseur, new Vector3(bordDroite + 1, Random.Range(posMin, posMax), -15f));
						bossLaunched = true;
					}
					else//Sinon, on attend
					{
						tempsAvantBoss+= Time.deltaTime;
					}
				}
				else
				{
					if(GameObject.FindWithTag ("GroupBoss").GetComponent<EnnemyMoveBossGroup> ().bossDead && GameObject.FindWithTag ("GroupBoss").GetComponent<EnnemyMoveBossGroup> ().listEnemy.Count == 0)
					{
						bossWaveFinished = true;
					}
				}


			}
		}
		else //FIN DU NIVEAU
		{
			GameObject.FindWithTag("Pattern").GetComponent<EndLevel>().endLevel = true;
			if(gain)
			{
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().GagnerArgent(1000);
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().Gagnerferraille(15);
				gain = false;
			}

		}
	}
}