using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternS2_4 : MonoBehaviour 
{
	//Model3D des enemy utilisés dans ce script
	public Transform enemy;	
	public Transform Croiseur;

	//Gestion des vagues
	public float tempsActuel;
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
	public bool lastWaveLaunched;

	public bool lastWaveFinished;
	
	private float tempsAvantFinLvlAct;
	private float tempsAvantFinLvlMax;

	private GameObject lastWave1;
	private GameObject lastWave2;
	private GameObject lastWave3;
	private GameObject lastWave4;

	private float bordDroite;

	private bool gain;

	private GameObject a;

	// Use this for initialization
	void Start () 
	{
		enemy = Resources.Load<Transform> ("Vaisseau/Ennemis/EnnemiBase/EnnemiBase");
		Croiseur = Resources.Load<Transform> ("Vaisseau/Ennemis/Croiseur/Croiseur");

		posMax = 3;
		posMin = -3;
		frequenceApparition = 6;

		beginGroup = true;

		cptVague = 0;
		endLevel = false;
		lastWaveLaunched = false;
		lastWaveFinished = false;
		endLevel = false;

		bordDroite = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x;

		gain = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(lastWaveFinished)
		{
			endLevel = true;
		}
		//Si le boss de fin n'est pas mort, le niveau n'est pas fini
		if(endLevel == false)
		{
			//Nombre de vague avant le boss
			if(cptVague < 9)
			{
				//On fait arriver les enemy groupe par groupe ...
				if(beginGroup)
				{
					//... en piochant parmi les patternes (présent dasn ListGroupEnemy)
					switch(cptVague) //
					{
					case 0 :
						a = ListGroupEnemy.Vgroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 2;
						frequenceApparition = 4;
						break;
					case 1 :
						a = ListGroupEnemy.Igroup("SplineHorUp1");
						a = ListGroupEnemy.ArrowGroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 2;
						a = ListGroupEnemy.Igroup("SplineHorDown2");
						frequenceApparition = 7;
						break;
					case 2 :
						a = ListGroupEnemy.Vgroup("SplineCenterBackUp");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a = ListGroupEnemy.Vgroup("SplineCenterBackDown");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a = ListGroupEnemy.Vgroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						break;
					case 3 :
						a = ListGroupEnemy.Igroup("SplineSnakeUp1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a = ListGroupEnemy.Igroup("SplineSnakeDown1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						frequenceApparition = 1.5f;
						break;
					case 4:
						ListGroupEnemy.ArrowGroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						frequenceApparition = 7;
						break;
					case 5 :
						a = ListGroupEnemy.Vgroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						frequenceApparition = 2;
						break;
					case 6 :
						a = ListGroupEnemy.Vgroup("SplineHorUp1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a = ListGroupEnemy.Vgroup("SplineHorDown1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						frequenceApparition = 4;
						break;
					case 7 :
						a = ListGroupEnemy.Vgroup("SplineHorUp2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a = ListGroupEnemy.Vgroup("SplineHorDown2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a = ListGroupEnemy.Vgroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						frequenceApparition = 7;
						break;
					case 8 :
						a =ListGroupEnemy.Vgroup("SplineSnakeUp1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a = ListGroupEnemy.Vgroup("SplineSnakeDown1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						break;
					case 9 :
						lastWave1 = ListGroupEnemy.ArrowGroup("SplineHorCenter");
						frequenceApparition = 1;
						break;
					case 10 : 
						lastWave1 = ListGroupEnemy.ArrowGroup("SplineCenterBackDown");
						lastWave1.AddComponent<FireOneByOne>();
						lastWave1.GetComponent<FireOneByOne>().tempsFireMax = 4;
						lastWave2 = ListGroupEnemy.ArrowGroup("SplineDownHor1");
						lastWave2.AddComponent<FireOneByOne>();
						lastWave2.GetComponent<FireOneByOne>().tempsFireMax = 4;
						lastWave3 = ListGroupEnemy.ArrowGroup("SplineUpHor1");
						lastWave3.AddComponent<FireOneByOne>();
						lastWave3.GetComponent<FireOneByOne>().tempsFireMax = 4;
						frequenceApparition = 3;
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
				frequenceApparition = 7;
				if(tempsActuel > frequenceApparition && !lastWaveLaunched)
				{
					tempsActuel = 0;
					lastWave4 = ListGroupEnemy.CircleGroup("SplineHorCenter");

					lastWaveLaunched = true;
				}
				else
				{
					tempsActuel += Time.deltaTime;
				}


				if(lastWaveLaunched && lastWave1 == null && lastWave2 == null && lastWave3 == null && lastWave4 == null)
				{
					lastWaveFinished = true;
				}

			}
		}
		else //FIN DU NIVEAU
		{
			GameObject.FindWithTag("Pattern").GetComponent<EndLevel>().endLevel = true;
			if(gain)
			{
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().GagnerArgent(3500);
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().Gagnerferraille(25);
				gain = false;
			}

		}
	}
}