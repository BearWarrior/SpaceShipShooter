using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternS2_1 : MonoBehaviour 
{
	//Model3D des enemy utilisés dans ce script
	public Transform enemyStatic;	
	public Transform enemyAile;
	public Transform enemyBase;

	//Gestion des vagues
	private float tempsActuel;
	public float frequenceApparition;
	bool beginGroup;
	public float posMax; //Pos min et max en Y de l'apparition de la vague, vérifié par ListGroupEnemy a la création
	public float posMin;
	public int cptVague;

	//Change la fréquence d'apparition des vagues
	private float tempsChangeFreq;
	private float tempsChangeFreqMax;

	private bool endLevel;

	public bool lastWaveLaunched;

	public bool lastWaveFinished;

	private GameObject lastWave1;
	private GameObject lastWave2;
	private GameObject lastWave3;
	private GameObject lastWave4;
	private GameObject lastWave5;

	private float bordDroite;

	private bool gain;

	// Use this for initialization
	void Start () 
	{
		enemyStatic = Resources.Load<Transform> ("Vaisseau/Ennemis/EnnemiBase2/VaisseauBase3");
		enemyAile = Resources.Load<Transform> ("Vaisseau/Ennemis/EnnemiAile/VaisseauAile");
		enemyBase = Resources.Load<Transform> ("Vaisseau/Ennemis/EnnemiBase/EnnemiBase");
		
		posMax = 3;
		posMin = -3;
		frequenceApparition = 5;


		tempsChangeFreqMax = 20;
		beginGroup = true;

		cptVague = 0;
		endLevel = false;
		lastWaveLaunched = false;
		lastWaveFinished = false;
		endLevel = false;

		bordDroite = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x;

		gain = true;
	}

	GameObject a;

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
			if(cptVague < 10)
			{
				//On fait arriver les enemy groupe par groupe ...
				if(beginGroup)
				{
					//... en piochant parmi les patternes (présent dasn ListGroupEnemy)
					switch(cptVague) //
					{
					case 0 :
						ListGroupEnemy.LaunchUpDownComeStopFireGroup(enemyStatic, new Vector3(bordDroite + 1, 4f, -15));
						a = ListGroupEnemy.ArrowGroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;

						break;
					case 1 :
						a = ListGroupEnemy.ArrowGroup("SplineHorUp1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						a = ListGroupEnemy.ArrowGroup("SplineHorDown1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 2;
						break;
					case 2 :
						a = ListGroupEnemy.LaunchUpDownComeStopFireGroup(enemyStatic, new Vector3(bordDroite + 1, 4f, -15));
						a = ListGroupEnemy.CircleGroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						break;
					case 3 :
						a = ListGroupEnemy.Vgroup("SplineHorUp1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						a = ListGroupEnemy.Vgroup("SplineHorDown1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 2;
						frequenceApparition = 2;
						break;
					case 4 :
						a = ListGroupEnemy.ArrowGroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						frequenceApparition = 5;
						break;
					case 5 :
						a = ListGroupEnemy.Igroup("SplineHorUp1");
						//a.AddComponent<FireOneByOne>();
						//a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a = ListGroupEnemy.Igroup("SplineHorDown1");
						//a.AddComponent<FireOneByOne>();
						//a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						break;
					case 6 :
						a = ListGroupEnemy.LaunchUpDownComeStopFireGroup(enemyStatic, new Vector3(bordDroite + 1, 2f, -15));
						a = ListGroupEnemy.Vgroup("SplineHorDown2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a = ListGroupEnemy.Vgroup("SplineHorUp2");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						break;
					case 7 :
						a = ListGroupEnemy.ArrowGroup("SplineHorUp1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 0;
						a = ListGroupEnemy.ArrowGroup("SplineHorDown1");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						a.GetComponent<FireOneByOne>().tempsFireAct = 2;
						frequenceApparition = 3;
						break;
					case 8:
						a = ListGroupEnemy.ArrowGroup("SplineHorCenter");
						a.AddComponent<FireOneByOne>();
						a.GetComponent<FireOneByOne>().tempsFireMax = 4;
						frequenceApparition = 10;
						break;
					case 9 : 
						lastWave1 = ListGroupEnemy.LaunchUpDownComeStopFireGroup(enemyStatic, new Vector3(bordDroite + 1, 4f, -1.5f));
						lastWave1.AddComponent<FireOneByOne>();
						lastWave1.GetComponent<FireOneByOne>().tempsFireMax = 4;
						lastWave2 = ListGroupEnemy.LaunchUpDownComeStopFireGroup(enemyStatic, new Vector3(bordDroite + 1, 1.5f, -1.5f));
						lastWave2.AddComponent<FireOneByOne>();
						lastWave2.GetComponent<FireOneByOne>().tempsFireMax = 4;
						lastWave3 = ListGroupEnemy.Vgroup("SplineHorUp1");
						lastWave3.AddComponent<FireOneByOne>();
						lastWave3.GetComponent<FireOneByOne>().tempsFireMax = 4;
						lastWave4 =ListGroupEnemy.Vgroup("SplineHorDown1");
						lastWave4.AddComponent<FireOneByOne>();
						lastWave4.GetComponent<FireOneByOne>().tempsFireMax = 4;
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
					lastWave5 = ListGroupEnemy.Vgroup("SplineHorCenter");

					lastWaveLaunched = true;
				}
				else
				{
					tempsActuel += Time.deltaTime;
				}
				
				
				if(lastWaveLaunched && lastWave1 == null && lastWave2 == null && lastWave3 == null && lastWave4 == null && lastWave5 == null)
				{
					lastWaveFinished = true;
				}

			}
		
			//UTILISER SI ON VEUT QUE LES VAGUES ARRIVENT DE PLUS EN PLUS VITE
			if(tempsChangeFreq > tempsChangeFreqMax)
			{
				tempsChangeFreq = 0;
				frequenceApparition *= 0.9f;
			}
			else
			{
				tempsChangeFreq += Time.deltaTime;
			}
		}
		else //FIN DU NIVEAU
		{
			GameObject.FindWithTag("Pattern").GetComponent<EndLevel>().endLevel = true;
			if(gain)
			{
				gain = false;
			}
		}
	}
}