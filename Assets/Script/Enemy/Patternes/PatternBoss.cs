using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternBoss : MonoBehaviour
{
	//Model3D des enemy utilisés dans ce script
	public Transform BossP1;
	public Transform BossP2;

	public Transform mine;

	//Gestion des vagues
	public float tempsActuel;
	public float frequenceApparition;
	bool beginGroup;
	public float posMax; //Pos min et max en Y de l'apparition de la vague, vérifié par ListGroupEnemy a la création
	public float posMin;
	public int phase;
	
	private bool endLevel;
	
	public bool lastWaveLaunched;

	public bool lastWaveFinished;

	private GameObject bossPhase1;
	private GameObject lastWave;

	private float bordDroite;

	private bool gain;

	// Use this for initialization
	void Start () 
	{
		BossP1 = Resources.Load<Transform> ("Vaisseau/Ennemis/Carrier/CarrierP1");
		BossP2 = Resources.Load<Transform> ("Vaisseau/Ennemis/Carrier/CarrierP2");
		mine = Resources.Load<Transform> ("Vaisseau/Ennemis/Mine/Mine");
		
		posMax = 3;
		posMin = -3;
		frequenceApparition = 1;

		beginGroup = true;

		phase = 1;
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
			if(phase < 5)
			{
				///On fait arriver les enemy groupe par groupe ...
				if(beginGroup)
				{
					//... en piochant parmi les patternes (présent dasn ListGroupEnemy)
					switch(phase) //
					{
					case 1:
						bossPhase1 = GameObject.FindWithTag("Pattern").GetComponent<ListGroupEnemy>().LaunchBossP1(BossP1, new Vector3(bordDroite + 4, 0, 0));
						break;
					case 2: 
						GameObject.FindWithTag("Pattern").GetComponent<ListGroupEnemy>().LaunchMineField(mine, new Vector3(bordDroite, 0, 0), 8, 0);
						frequenceApparition = 10;
						break;
					case 3: 
						GameObject.FindWithTag("Pattern").GetComponent<ListGroupEnemy>().LaunchMineField(mine, new Vector3(bordDroite, 0, 0), 8, 0);
						frequenceApparition = 15;
						break;
					case 4:
						GameObject.FindWithTag("LootScript").GetComponent<LootScript>().LootHealth(new Vector3(bordDroite,3f, 0));
						GameObject.FindWithTag("LootScript").GetComponent<LootScript>().LootHealth(new Vector3(bordDroite+1, 1.5f, 0));
						GameObject.FindWithTag("LootScript").GetComponent<LootScript>().LootHealth(new Vector3(bordDroite+1,-1.5f, 0));
						GameObject.FindWithTag("LootScript").GetComponent<LootScript>().LootHealth(new Vector3(bordDroite,-3f, 0));

						break;
					}

					beginGroup = false;
				}
				else //Une fois la phase 1 finie, on passe a la deux
				{
					if(bossPhase1 == null)
					{
						if(tempsActuel > frequenceApparition)
						{
							tempsActuel = 0;
							beginGroup = true;
							phase++;
						}
						else
						{
							tempsActuel += Time.deltaTime;
						}
					}
				}
			}
			//Le nombre de vague avant le boss est atteint, on lance la phase du boss
			else
			{
				frequenceApparition = 3;
				if(tempsActuel > frequenceApparition && !lastWaveLaunched)
				{
					tempsActuel = 0;
	
					lastWave = GameObject.FindWithTag("Pattern").GetComponent<ListGroupEnemy>().LaunchBossP2(BossP2, new Vector3(bordDroite + 4, 0, 0));

					lastWaveLaunched = true;
				}
				else
				{
					tempsActuel += Time.deltaTime;
				}
				
				if(lastWaveLaunched && lastWave == null)
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