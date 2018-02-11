using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternS1_3 : MonoBehaviour 
{
	//Model3D des enemy utilisés dans ce script
	public Transform enemy;	
	public Transform Croiseur;

	//Gestion des vagues
	private float tempsActuel;
	public float frequenceApparition;
	private bool beginGroup;

	public int cptVague;

	public bool lastWaveLaunched;

	public bool lastWaveFinished;

	private GameObject lastWave;
	private GameObject lastWave2;
	private GameObject lastWave3;

	private bool gain;

	private float bordDroite;


	// Use this for initialization
	void Start () 
	{
		enemy = Resources.Load<Transform> ("Vaisseau/Ennemis/EnnemiBase/EnnemiBase");

		frequenceApparition = 8;

		beginGroup = true;

		cptVague = 0;
		lastWaveLaunched = false;
		lastWaveFinished = false;

		bordDroite = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x;

		gain = true;

	}
	
	// Update is called once per frame
	void Update () 
	{

		if(lastWaveFinished == false)
		{
			//Nombre de vague avant le boss
			if(cptVague < 8)
			{
				//On fait arriver les enemy groupe par groupe ...
				if(beginGroup)
				{
					//... en piochant parmi les patternes (présent dasn ListGroupEnemy)
					switch(cptVague) //
					{
					case 0 :
						ListGroupEnemy.Vgroup("SplineHorUp1");
						break;
					case 1 :
						ListGroupEnemy.Vgroup("SplineHorDown1");
						break;
					case 2 :
						ListGroupEnemy.Igroup("SplineCenterUp");
						break;
					case 3 :
						ListGroupEnemy.Igroup("SplineCenterDown");
						break;
					case 4 :
						ListGroupEnemy.Igroup("SplineSnakeUp1");
						frequenceApparition = 4;
						break;
					case 5:
						ListGroupEnemy.Igroup("SplineSnakeDown1");
						frequenceApparition = 8;
						break;
					case 6 :
						ListGroupEnemy.Igroup("SplineHorCenter");
						break;
					case 7 :
						ListGroupEnemy.Vgroup("SplineHorUp1");
						ListGroupEnemy.Vgroup("SplineHorDown1");
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
				frequenceApparition = 10;
				if(tempsActuel > frequenceApparition && !lastWaveLaunched)
				{
					tempsActuel = 0;
					lastWave = ListGroupEnemy.CircleGroup("SplineHorCenter");
					lastWave2 = ListGroupEnemy.Igroup("SplineSnakeUp2");
					lastWave3 = ListGroupEnemy.Igroup("SplineSnakeDown2");

					
					lastWaveLaunched = true;
				}
				else
				{
					tempsActuel += Time.deltaTime;
				}
				
				
				if(lastWaveLaunched && lastWave == null && lastWave2 == null && lastWave3 == null)
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
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().GagnerArgent(2000);
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().Gagnerferraille(20);
				gain = false;
			}
		}
	}
}