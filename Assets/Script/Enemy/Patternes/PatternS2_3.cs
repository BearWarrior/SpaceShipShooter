using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternS2_3 : MonoBehaviour 
{
	//Model3D des enemy utilisés dans ce script
	public Transform enemy;	
	public Transform enemyStatic;
	public Transform Croiseur;

	//Gestion des vagues
	private float tempsActuel;
	public float frequenceApparition;
	bool beginGroup;
	public float posMax; //Pos min et max en Y de l'apparition de la vague, vérifié par ListGroupEnemy a la création
	public float posMin;
	public int cptVague;

	//Change la fréquence d'apparition des vagues
	private float tempsChangeFreq;

	private bool endLevel;
	
	public bool lastWaveLaunched;

	public bool lastWaveFinished;

	private GameObject lastWave1;
	private GameObject lastWave2;
	private GameObject lastWave3;
	private GameObject lastWave4;
	private GameObject lastWave5;
	private GameObject lastWave6;


	private float bordDroite;

	private bool gain;

	private GameObject a;

	// Use this for initialization
	void Start () 
	{
		enemyStatic = Resources.Load<Transform> ("Vaisseau/Ennemis/EnnemiBase2/VaisseauBase3");
		Croiseur = Resources.Load<Transform> ("Vaisseau/Croiseur 1");
		
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
			if(cptVague < 13)
			{
				//On fait arriver les enemy groupe par groupe ...
				if(beginGroup)
				{
					//... en piochant parmi les patternes (présent dasn ListGroupEnemy)
					switch(cptVague) //
					{
					case 1 :
						a = ListGroupEnemy.Igroup("SplineUp1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						a = ListGroupEnemy.Igroup("SplineDown1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						a = ListGroupEnemy.Vgroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 2;
						break;
					case 2 :
						a = ListGroupEnemy.Igroup("SplineCenterUp");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						a = ListGroupEnemy.Igroup("SplineCenterDown");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						a = ListGroupEnemy.ArrowGroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 2;
						break;
					case 3 :
						a = ListGroupEnemy.ArrowGroup("SplineHorUp2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						a = ListGroupEnemy.ArrowGroup("SplineHorDown2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 2;
						a = ListGroupEnemy.Igroup("SplineSnakeCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						break;
					case 4 :
						a = ListGroupEnemy.LaunchUpDownComeStopFireGroup(enemyStatic, new Vector3(bordDroite, 1.5f, -15));
						a = ListGroupEnemy.Igroup("SplineUpHor1");
						a = ListGroupEnemy.Igroup("SplineDownHor1");
						break;
					case 5 :
						a = ListGroupEnemy.ArrowGroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						frequenceApparition = 1f;
						break;
					case 6 :
						a = ListGroupEnemy.CircleGroup("SplineHorUp2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 3;
						a = ListGroupEnemy.CircleGroup("SplineHorDown2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 1;
						frequenceApparition = 6;
						break;
					case 7 :
						a = ListGroupEnemy.ArrowGroup("SplineHorUp2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						a = ListGroupEnemy.ArrowGroup("SplineHorDown2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 2;
						frequenceApparition = 2;
						break;
					case 8 :
						a = ListGroupEnemy.ArrowGroup("SplineHorUp1");
						a = ListGroupEnemy.ArrowGroup("SplineHorDown1");
						frequenceApparition = 2;
						break;
					case 9 :
						a = ListGroupEnemy.ArrowGroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 3;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						frequenceApparition = 6;
						break;
					case 10 :
						a = ListGroupEnemy.Igroup("SplineSnakeUp2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						a = ListGroupEnemy.Igroup("SplineSnakeDown2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 2;
						frequenceApparition = 2;
						break;
					case 11 :
						a = ListGroupEnemy.CircleGroup("SplineHorCenter");
						frequenceApparition = 6;
						break;
					case 12 : 
						lastWave1 = ListGroupEnemy.Vgroup("SplineHorUp1");
						lastWave2 = ListGroupEnemy.Vgroup("SplineHorDown1");
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
				frequenceApparition = 2;
				if(tempsActuel > frequenceApparition && !lastWaveLaunched)
				{
					tempsActuel = 0;
					lastWave3 = ListGroupEnemy.Vgroup("SplineHorUp2");
					lastWave3.AddComponent<FireOneByOne>();
					lastWave3.GetComponent<FireOneByOne>().tempsFireMax = 4;
					lastWave3.GetComponent<FireOneByOne>().tempsFireAct = 0;
					lastWave4 = ListGroupEnemy.Vgroup("SplineHorDown2");
					lastWave4.AddComponent<FireOneByOne>();
					lastWave4.GetComponent<FireOneByOne>().tempsFireMax = 4;
					lastWave4.GetComponent<FireOneByOne>().tempsFireAct = 2;
					lastWave5 = ListGroupEnemy.LaunchUpDownComeStopFireGroup(enemyStatic, new Vector3(bordDroite, 1.5f, -15));
					lastWave6 = ListGroupEnemy.LaunchUpDownComeStopFireGroup(enemyStatic, new Vector3(bordDroite, 4f, -15));
					lastWaveLaunched = true;
				}
				else
				{
					tempsActuel += Time.deltaTime;
				}
				
				
				if(lastWaveLaunched && lastWave1 == null && lastWave2 == null && lastWave3 == null && lastWave4 == null && lastWave5 == null && lastWave6 == null)
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
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().GagnerArgent(20000);
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().Gagnerferraille(100);
				gain = false;
			}

		}
	}
}