using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternS1_1 : MonoBehaviour 
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

	private bool gain;

	private float bordDroite;

	private bool tutoFinished;


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

		tutoFinished = false;

		//On lance la phase de tutoriel
		GameObject.FindWithTag ("Pause").GetComponent<MenuPause> ().tuto = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(lastWaveFinished == false)
		{
			//Nombre de vague avant le bosss
			if(cptVague < 8)
			{
				//On fait arriver les enemy groupe par groupe ...
				if(beginGroup)
				{
					//... en piochant parmi les patternes (présent dasn ListGroupEnemy)
					switch(cptVague) //
					{
					case 0 :
						//GameObject.FindWithTag("Pattern").GetComponent<ListGroupEnemy>().Igroup(enemy, "SplineHorCenter");
						ListGroupEnemy.Igroup("SplineHorCenter");
						break;
					case 1 :
						ListGroupEnemy.Igroup("SplineHorUp2");
						break;
					case 2 :
						ListGroupEnemy.Igroup("SplineHorDown2");
						break;
					case 3 :
						ListGroupEnemy.Vgroup("SplineHorCenter");
						break;
					case 4 :
						ListGroupEnemy.Igroup("SplineCenterBackDown");
						ListGroupEnemy.Igroup("SplineCenterBackUp");
						break;
					case 5 :
						ListGroupEnemy.CircleGroup("SplineHorCenter");
						break;
					case 6 :
						ListGroupEnemy.CircleGroup("SplineHorUp1");
						break;
					case 7 :
						ListGroupEnemy.CircleGroup("SplineHorDown2");
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
					lastWave = ListGroupEnemy.Vgroup("SplineCenterUp");
					lastWave2 = ListGroupEnemy.Vgroup("SplineCenterDown");

					lastWaveLaunched = true;
				}
				else
				{
					tempsActuel += Time.deltaTime;
				}
					

				if(lastWaveLaunched && lastWave == null && lastWave2 == null)
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