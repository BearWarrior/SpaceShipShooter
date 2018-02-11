using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class groupBossRepop : MonoBehaviour 
{
	public GameObject bas;
	private List<Transform> listEnemyBas;
	public GameObject haut;
	public List<Transform> listEnemyHaut;

	private Vector3 posCreaBase;
	private Vector3 posCreaAct;

	public Transform enemy;
	private Transform hautT;
	private Transform basT;

	//public bool bossDead;

	private bool sndWaveLaunched;

	// Use this for initialization
	void Start () 
	{
		listEnemyHaut = new List<Transform>();
		listEnemyBas = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//EN HAUT
		if(GetComponent<EnnemyMoveBossGroup>().bossDead == false && GetComponent<EnnemyMoveBossGroup>().listEnemy.Count == 1 && !sndWaveLaunched)
		{
			sndWaveLaunched = true;
			haut = new GameObject();
			haut.transform.position = new Vector2(0, 4.5f);
			haut.AddComponent<joinCenter>();

			for(int i = 0; i < 3; i++)
			{
				posCreaAct.x = GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x -4 + i;
				posCreaAct.y = 5f + 0.5f*i;
				hautT = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
				hautT.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
				hautT.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
				hautT.parent = haut.transform;
				GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(hautT);
				listEnemyHaut.Add (hautT);
			}
			
			//EN BAS
			bas = new GameObject();
			bas.transform.position = new Vector2(0, -4.5f);
			bas.AddComponent<joinCenter>();
			Transform basT;
			for(int i = 0; i < 3; i++)
			{
				posCreaAct.x = GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x -4 + i;
				posCreaAct.y = -5f + -0.5f*i;
				basT = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
				basT.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
				basT.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
				listEnemyBas.Add(basT);
				basT.parent = bas.transform;
				GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(basT);
			}
			
			//On ramene le boss
			GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().joinCenter = true;
		}

		if(haut != null && bas != null)
		{
			
			if(haut.GetComponent<joinCenter>().waitingForOrder && bas.GetComponent<joinCenter>().waitingForOrder && GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().waitingForOrder)
			{
				while(listEnemyBas.Count != 0)
				{
					if(listEnemyBas[0] != null)
					{
						listEnemyBas[0].parent = GameObject.FindWithTag("GroupBoss").transform;
					}
					
					listEnemyBas.RemoveAt(0);
				}
				Destroy(bas.gameObject);
				
				while(listEnemyHaut.Count != 0)
				{
					if(listEnemyHaut[0] != null)
					{
						listEnemyHaut[0].parent = GameObject.FindWithTag("GroupBoss").transform;
					}
					
					listEnemyHaut.RemoveAt(0);
				}
				Destroy(haut.gameObject);

				GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().waitingForOrder = false;
				GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().joinCenter = false;

				sndWaveLaunched = false;
			}
		}
	}
}
