using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
Contient tous les pattern launchable
*/

public class ListGroupEnemy : MonoBehaviour 
{
	private bool createEnemy;
	private Vector3 posCreaAct;
	private Vector3 posCreaBase;
	private static int cptSousGroupe;
	private Transform enemyCreated;
	private static int nbActuelVague = 0;
	
	public GameObject LaunchVGroupEscapeUp(Transform enemy, Vector3 posCreaBase, float freqTir, float timeBfShot)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		groupeEnemy.transform.position = new Vector2(GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x, 0);
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveEscapeUp>();
		groupeEnemy.GetComponent<EnnemyMoveEscapeUp> ().listEnemy = new List<Transform> ();
		

		//_____________________________________________//
		
		//On vérifie que les enemy n'apparaitront pas hors écran
		if(posCreaBase.y + 3*0.4f + 0.7f >  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y - 3*0.4f - 0.7f;
		}
		if(posCreaBase.y - 3*0.4f - 0.7f <  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y + 3*0.4f + 0.7f;
		}
		
		
		//On crée celui devant
		posCreaAct = posCreaBase;
		posCreaAct.x = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 1;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeEnemy.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveEscapeUp";
		groupeEnemy.GetComponent<EnnemyMoveEscapeUp> ().listEnemy.Add (enemyCreated);
		
		
		//Puis deux par deux les autres
		while(cptSousGroupe < 3)
		{
			//On crée les enemy
			posCreaAct.x += 1;
			//haut
			posCreaAct.y = posCreaBase.y + (1+cptSousGroupe)* 0.4f;
			enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
			enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
			enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveEscapeUp";
			enemyCreated.parent = groupeEnemy.transform;
			groupeEnemy.GetComponent<EnnemyMoveEscapeUp> ().listEnemy.Add (enemyCreated);
			
			
			//bas
			posCreaAct.y = posCreaBase.y + (1+cptSousGroupe)*(-0.4f);
			enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform;
			enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
			enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveEscapeUp";
			enemyCreated.parent = groupeEnemy.transform;
			groupeEnemy.GetComponent<EnnemyMoveEscapeUp> ().listEnemy.Add (enemyCreated);
			
			
			cptSousGroupe++;
		}

		for(int i = 0; i < groupeEnemy.GetComponent<EnnemyMoveEscapeUp>().listEnemy.Count; i++)
		{
			
			groupeEnemy.GetComponent<EnnemyMoveEscapeUp>().listEnemy[i].gameObject.AddComponent<Fire>();
			groupeEnemy.GetComponent<EnnemyMoveEscapeUp>().listEnemy[i].gameObject.GetComponent<Fire>().tempsFireAct = freqTir - timeBfShot;
			groupeEnemy.GetComponent<EnnemyMoveEscapeUp>().listEnemy[i].gameObject.GetComponent<Fire>().tempsFireMax = freqTir;
		}

		cptSousGroupe = 0;
		nbActuelVague++;

		return groupeEnemy;
	}
	
	//_______________________________________________________________________________________________________________________________________________________________//

	public GameObject LaunchVGroupEscapeDown(Transform enemy, Vector3 posCreaBase, float freqTir, float timeBfShot)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		groupeEnemy.transform.position = new Vector2(GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x, 0);
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveEscapeDown>();
		groupeEnemy.GetComponent<EnnemyMoveEscapeDown> ().listEnemy = new List<Transform> ();
		
		//_____________________________________________//
		
		//On vérifie que les enemy n'apparaitront pas hors écran
		if(posCreaBase.y + 3*0.4f + 0.7f >  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y - 3*0.4f - 0.7f;
		}
		if(posCreaBase.y - 3*0.4f - 0.7f <  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y + 3*0.4f + 0.7f;
		}
		
		
		//On crée celui devant
		posCreaAct = posCreaBase;
		posCreaAct.x = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 1;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeEnemy.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveEscapeDown";
		groupeEnemy.GetComponent<EnnemyMoveEscapeDown> ().listEnemy.Add (enemyCreated);
		
		
		//Puis deux par deux les autres
		while(cptSousGroupe < 3)
		{
			//On crée les enemy
			posCreaAct.x += 1;
			//haut
			posCreaAct.y = posCreaBase.y + (1+cptSousGroupe)* 0.4f;
			enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
			enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
			enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveEscapeDown";
			enemyCreated.parent = groupeEnemy.transform;
			groupeEnemy.GetComponent<EnnemyMoveEscapeDown> ().listEnemy.Add (enemyCreated);
			
			
			//bas
			posCreaAct.y = posCreaBase.y + (1+cptSousGroupe)*(-0.4f);
			enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform;
			enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
			enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveEscapeDown";
			enemyCreated.parent = groupeEnemy.transform;
			groupeEnemy.GetComponent<EnnemyMoveEscapeDown> ().listEnemy.Add (enemyCreated);
			
			
			cptSousGroupe++;
		}

		for(int i = 0; i < groupeEnemy.GetComponent<EnnemyMoveEscapeDown>().listEnemy.Count; i++)
		{
			
			groupeEnemy.GetComponent<EnnemyMoveEscapeDown>().listEnemy[i].gameObject.AddComponent<Fire>();
			groupeEnemy.GetComponent<EnnemyMoveEscapeDown>().listEnemy[i].gameObject.GetComponent<Fire>().tempsFireAct = freqTir - timeBfShot;
			groupeEnemy.GetComponent<EnnemyMoveEscapeDown>().listEnemy[i].gameObject.GetComponent<Fire>().tempsFireMax = freqTir;
		}

		cptSousGroupe = 0;
		nbActuelVague++;

		return groupeEnemy;
	}

	public GameObject LaunchLineEscUpGroup(Transform enemy, Vector3 posCreaBase)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveEscapeUp>();
		groupeEnemy.GetComponent<EnnemyMoveEscapeUp> ().listEnemy = new List<Transform> ();
		
		
		//On vérifie que les enemy n'apparaitront pas hors écran
		if(posCreaBase.y + 1 + 0.7f >  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y - 1 - 0.7f;
		}
		if(posCreaBase.y - 1 - 0.7f <  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y + 1 + 0.7f;
		}
		
		posCreaAct = posCreaBase;
		posCreaAct.x = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 1;

		groupeEnemy.transform.position = posCreaAct;

		while(cptSousGroupe < 6)
		{
			//On crée les enemy
			posCreaAct.x += 1;
			enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
			//enemyCreated.gameObject.AddComponent<EnnemyMoveType1>();
			enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
			enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveEscapeUp";
			enemyCreated.parent = groupeEnemy.transform;
			groupeEnemy.GetComponent<EnnemyMoveEscapeUp> ().listEnemy.Add (enemyCreated);
			
			
			cptSousGroupe++;
		}

		
		cptSousGroupe = 0;
		nbActuelVague++;

		return groupeEnemy;
	}

	public GameObject LaunchLineGroupNoFire(Transform enemy, Vector3 posCreaBase)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveRectNoFire>();
		groupeEnemy.GetComponent<EnnemyMoveRectNoFire> ().listEnemy = new List<Transform> ();
		
		
		//On vérifie que les enemy n'apparaitront pas hors écran
		if(posCreaBase.y + 1 + 0.7f >  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y - 1 - 0.7f;
		}
		if(posCreaBase.y - 1 - 0.7f <  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y + 1 + 0.7f;
		}
		
		posCreaAct = posCreaBase;
		posCreaAct.x = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 1;
		
		groupeEnemy.transform.position = posCreaAct;
		
		while(cptSousGroupe < 6)
		{
			//On crée les enemy
			posCreaAct.x += 1;
			enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
			enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
			enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveRectNoFire";
			enemyCreated.parent = groupeEnemy.transform;
			groupeEnemy.GetComponent<EnnemyMoveRectNoFire> ().listEnemy.Add (enemyCreated);
			
			
			cptSousGroupe++;
		}
		
		
		cptSousGroupe = 0;
		nbActuelVague++;
		
		return groupeEnemy;
	}
	
	//_______________________________________________________________________________________________________________________________________________________________//


	public GameObject LaunchLineEscBackUpGroup(Transform enemy, Vector3 posCreaBase)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveEscapeBackUp>();
		groupeEnemy.GetComponent<EnnemyMoveEscapeBackUp> ().listEnemy = new List<Transform> ();
		
		
		//On vérifie que les enemy n'apparaitront pas hors écran
		if(posCreaBase.y + 1 + 0.7f >  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y - 1 - 0.7f;
		}
		if(posCreaBase.y - 1 - 0.7f <  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y + 1 + 0.7f;
		}
		
		posCreaAct = posCreaBase;
		posCreaAct.x = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 1;
		
		groupeEnemy.transform.position = posCreaAct;
		
		while(cptSousGroupe < 6)
		{
			//On crée les enemy
			posCreaAct.x += 1;
			enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
			enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
			enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveEscapeBackUp";
			enemyCreated.parent = groupeEnemy.transform;
			groupeEnemy.GetComponent<EnnemyMoveEscapeBackUp> ().listEnemy.Add (enemyCreated);
			
			
			cptSousGroupe++;
		}
		
		
		cptSousGroupe = 0;
		nbActuelVague++;

		return groupeEnemy;
	}


	//_______________________________________________________________________________________________________________________________________________________________//
	
	
	public GameObject LaunchLineEscBackDownGroup(Transform enemy, Vector3 posCreaBase)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveEscapeBackDown>();
		groupeEnemy.GetComponent<EnnemyMoveEscapeBackDown> ().listEnemy = new List<Transform> ();
		
		
		//On vérifie que les enemy n'apparaitront pas hors écran
		if(posCreaBase.y + 1 + 0.7f >  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y - 1 - 0.7f;
		}
		if(posCreaBase.y - 1 - 0.7f <  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y + 1 + 0.7f;
		}
		
		posCreaAct = posCreaBase;
		posCreaAct.x = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 1;
		
		groupeEnemy.transform.position = posCreaAct;
		
		while(cptSousGroupe < 6)
		{
			//On crée les enemy
			posCreaAct.x += 1;
			enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
			enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
			enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveEscapeBackDown";
			enemyCreated.parent = groupeEnemy.transform;
			groupeEnemy.GetComponent<EnnemyMoveEscapeBackDown> ().listEnemy.Add (enemyCreated);
			
			
			cptSousGroupe++;
		}
		
		
		cptSousGroupe = 0;
		nbActuelVague++;

		return groupeEnemy;
	}


	//_______________________________________________________________________________________________________________________________________________________________//


	public GameObject LaunchLineEscDownGroup(Transform enemy, Vector3 posCreaBase)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveEscapeDown>();
		groupeEnemy.GetComponent<EnnemyMoveEscapeDown> ().listEnemy = new List<Transform> ();
		
		
		//On vérifie que les enemy n'apparaitront pas hors écran
		if(posCreaBase.y + 1 + 0.7f >  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y - 1 - 0.7f;
		}
		if(posCreaBase.y - 1 - 0.7f <  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y + 1 + 0.7f;
		}
		
		posCreaAct = posCreaBase;
		posCreaAct.x = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 1;
		
		groupeEnemy.transform.position = posCreaAct;
		
		while(cptSousGroupe < 6)
		{
			//On crée les enemy
			posCreaAct.x += 1;
			enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
			enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
			enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveEscapeDown";
			enemyCreated.parent = groupeEnemy.transform;
			groupeEnemy.GetComponent<EnnemyMoveEscapeDown> ().listEnemy.Add (enemyCreated);
			
			
			cptSousGroupe++;
		}
		
		
		cptSousGroupe = 0;
		nbActuelVague++;

		return groupeEnemy;
	}
	
	//_______________________________________________________________________________________________________________________________________________________________//


	public GameObject LaunchSnakeGroup(Transform enemy, Vector3 posCreaBase)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		groupeEnemy.transform.position = new Vector2(GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x, 0);
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveSin>();
		groupeEnemy.GetComponent<EnnemyMoveSin> ().listEnemy = new List<Transform> ();


		//On vérifie que les enemy n'apparaitront pas hors écran
		if(posCreaBase.y + 1f + 0.7f >  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y - 1f - 0.7f;
		}
		if(posCreaBase.y - 1f - 0.7f <  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y + 1f + 0.7f;
		}

		posCreaAct = posCreaBase;
		int signe = -1;
		if(Random.Range(-20f, 20f) > 0)
		{
			signe = 1;
		}
		else
		{
			signe = -1;
		}
		while(cptSousGroupe < 6)
		{
			//On crée les enemy
			posCreaAct.x +=1f;
			posCreaAct.y = posCreaBase.y + Mathf.Cos(cptSousGroupe*0.1F * 2 * Mathf.PI)*signe*1f;
			enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
			enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
			enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveSin";
			enemyCreated.parent = groupeEnemy.transform;
			groupeEnemy.GetComponent<EnnemyMoveSin> ().listEnemy.Add (enemyCreated);
			cptSousGroupe++;
		}

		
		cptSousGroupe = 0;
		nbActuelVague++;

		return groupeEnemy;
	}



	public GameObject LaunchCroiseurGroup(Transform enemy, Transform croiseur, Vector3 posCreaBase)
	{
		GameObject groupeBoss = new GameObject("GroupBoss");
		groupeBoss.tag = "GroupBoss";
		groupeBoss.transform.position = new Vector2(GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 3, 0);
		//On ajoute le script de déplacement
		groupeBoss.AddComponent<EnnemyMoveBossGroup>();
		GameObject.FindWithTag ("GroupBoss").GetComponent<EnnemyMoveBossGroup> ().listEnemy = new List<Transform> ();
		//On ajoute le script de repop et on lui donne quel enemy repop
		groupeBoss.AddComponent<groupBossRepop>();
		groupeBoss.GetComponent<groupBossRepop> ().enemy = enemy;


		//_________________________________//

		posCreaAct = posCreaBase;

		//tout seul devant
		posCreaAct.x = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 1;
		//On crée les enemy
		posCreaAct.y = 0f;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeBoss.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
	
		GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(enemyCreated);

		//_________________________________//
		posCreaAct.x += 1f;
		//On crée les enemy
		posCreaAct.y = 0.7f;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeBoss.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
	
		GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(enemyCreated);

		posCreaAct.y = -0.7f;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeBoss.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
	
		GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(enemyCreated);

		//_________________________________//
		posCreaAct.x += 1f;
		//On crée les enemy
		posCreaAct.y = 1.4f;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeBoss.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
	
		GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(enemyCreated);

		posCreaAct.y = -1.4f;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeBoss.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
	
		GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(enemyCreated);

		//_________________________________//
		posCreaAct.x += 1f;
		//On crée les enemy
		posCreaAct.y = 2.1f;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeBoss.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
	
		GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(enemyCreated);

		posCreaAct.y = -2.1f;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeBoss.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
	
		GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(enemyCreated);

		//_________________________________//
		posCreaAct.y = 0f;
		enemyCreated = Instantiate(croiseur, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeBoss.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
	
		GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(enemyCreated);

		//_________________________________//
		posCreaAct.x += 1f;
		//On crée les enemy
		posCreaAct.y = 1.4f;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeBoss.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
	
		GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(enemyCreated);
		
		posCreaAct.y = -1.4f;
		enemyCreated = Instantiate(enemy, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.parent = groupeBoss.transform;
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "GroupBoss";
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossGroup";
	
		GameObject.FindWithTag("GroupBoss").GetComponent<EnnemyMoveBossGroup>().listEnemy.Add(enemyCreated);


		cptSousGroupe = 0;
		nbActuelVague++;

		return groupeBoss;
	}

	
	public static GameObject LaunchUpDownComeStopFireGroup(Transform enemy, Vector3 posCreaBase)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		groupeEnemy.transform.position = new Vector2(GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x, 0);
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveComeFire>();
		groupeEnemy.GetComponent<EnnemyMoveComeFire> ().listEnemy = new List<Transform> ();
		groupeEnemy.GetComponent<EnnemyMoveComeFire> ().distance = 1.5f;
		

		//On vérifie que les enemy n'apparaitront pas hors écran
		if(posCreaBase.y + 0.7f >  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y - 0.7f;
		}
		if(posCreaBase.y - 0.7f <  GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y)
		{
			posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y + 0.7f;
		}

		Vector3 posCreaActuel = new Vector3 ();
		Transform enemyT;
		
		posCreaActuel = posCreaBase;
		posCreaActuel.x = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 1;
		posCreaActuel.x -= 1;
		
		//Deux fois deux enemys devant
		
		//On crée les enemy
		posCreaActuel.x += 1;
		posCreaActuel.y = posCreaBase.y;
		enemyT = Instantiate(enemy, posCreaActuel, new Quaternion(0,0,0,0)) as Transform; 
		enemyT.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
		enemyT.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveComeFire";
		enemyT.parent = groupeEnemy.transform;
		groupeEnemy.GetComponent<EnnemyMoveComeFire> ().listEnemy.Add (enemyT);
		
		posCreaActuel.y = -posCreaBase.y;
		enemyT = Instantiate(enemy, posCreaActuel, new Quaternion(0,0,0,0)) as Transform; 
		enemyT.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
		enemyT.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveComeFire";
		enemyT.parent = groupeEnemy.transform;
		groupeEnemy.GetComponent<EnnemyMoveComeFire> ().listEnemy.Add (enemyT);

		

		cptSousGroupe = 0;
		nbActuelVague++;
		
		return groupeEnemy;
	}

	//_______________________________________________________________________________________________________________________________________________________________//
	
	public GameObject LaunchBossP1(Transform boss, Vector3 posCreaBase)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		groupeEnemy.transform.position = new Vector2(GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 4, 0);
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveBossPhase1>();
		groupeEnemy.GetComponent<EnnemyMoveBossPhase1> ().listEnemy = new List<Transform> ();
		

		posCreaAct = posCreaBase;

		//On crée les enemy
		posCreaAct.x += 4;
		posCreaAct.y = posCreaBase.y;
		posCreaAct.z = -15;
		enemyCreated = Instantiate(boss, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossPhase1";
		enemyCreated.parent = groupeEnemy.transform;
		groupeEnemy.GetComponent<EnnemyMoveBossPhase1> ().listEnemy.Add (enemyCreated);
		

		cptSousGroupe = 0;
		nbActuelVague++;
		
		return groupeEnemy;
	}

	//_______________________________________________________________________________________________________________________________________________________________//
	
	public GameObject LaunchBossP2(Transform boss, Vector3 posCreaBase)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		groupeEnemy.transform.position = new Vector2(GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x  + 4, 0);
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveBossPhase2>();
		groupeEnemy.GetComponent<EnnemyMoveBossPhase2> ().listEnemy = new List<Transform> ();

		posCreaAct = posCreaBase;
		
		//On crée les enemy
		posCreaAct.x += 4;
		posCreaAct.y = posCreaBase.y;
		posCreaAct.z = -15;
		enemyCreated = Instantiate(boss, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
		enemyCreated.GetComponent<DestroyLocation>().gameObjectName = "groupeEnemy" + nbActuelVague;
		enemyCreated.GetComponent<DestroyLocation>().scriptName = "EnnemyMoveBossPhase2";
		enemyCreated.parent = groupeEnemy.transform;
		groupeEnemy.GetComponent<EnnemyMoveBossPhase2> ().listEnemy.Add (enemyCreated);

		
		cptSousGroupe = 0;
		nbActuelVague++;
		
		return groupeEnemy;
	}

	//_______________________________________________________________________________________________________________________________________________________________//
	
	public GameObject LaunchMineField(Transform mine, Vector3 posCreaBase, float freqTir, float timeBfShot)
	{
		GameObject groupeEnemy = new GameObject("groupeEnemy" + nbActuelVague);
		groupeEnemy.tag = "GroupEnemy";
		groupeEnemy.transform.position = new Vector2(GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x, 0);
		//On ajoute le script de déplacement
		groupeEnemy.AddComponent<EnnemyMoveRect>();
		groupeEnemy.AddComponent<Group>();
		groupeEnemy.GetComponent<Group> ().listEnemy = new List<Transform> ();

		posCreaAct = posCreaBase;

		for(int i =0; i < 5; i++)
		{
			//On crée les enemy
			posCreaAct.x += Random.Range(3f, 4.5f);
			posCreaAct.y = Random.Range(posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.y - 0.7f, posCreaBase.y = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerDownLeft.y + 0.7f);
			posCreaAct.z = -15;
			enemyCreated = Instantiate(mine, posCreaAct, new Quaternion(0,0,0,0)) as Transform; 
			enemyCreated.parent = groupeEnemy.transform;

			groupeEnemy.GetComponent<Group>().listEnemy.Add(enemyCreated);
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().gameObjectName = groupeEnemy.name;
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().scriptName = "Group";

		}

		for(int i = 0; i < groupeEnemy.GetComponent<Group>().listEnemy.Count; i++)
		{
			
			groupeEnemy.GetComponent<Group>().listEnemy[i].gameObject.AddComponent<Fire>();
			groupeEnemy.GetComponent<Group>().listEnemy[i].gameObject.GetComponent<Fire>().tempsFireAct = freqTir - timeBfShot;
			groupeEnemy.GetComponent<Group>().listEnemy[i].gameObject.GetComponent<Fire>().tempsFireMax = freqTir;
		}

		cptSousGroupe = 0;
		nbActuelVague++;
		
		return groupeEnemy;
	}

	
	public static GameObject Igroup(string spline)
	{
		GameObject groupeEnemy = Instantiate(Resources.Load ("Vaisseau/Groups/Igroup"), new Vector3(0, 30, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
		groupeEnemy.name = "groupeEnemy" + nbActuelVague;
		groupeEnemy.GetComponent<Group> ().pathName = spline;
		for(int i =0; i < groupeEnemy.GetComponent<Group>().listEnemy.Count; i++)
		{
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<SplineWalker>().spline = GameObject.Find( groupeEnemy.GetComponent<Group>().pathName).GetComponent<BezierSpline>();
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().gameObjectName = groupeEnemy.name;
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().scriptName = "Group";
		}
		nbActuelVague++;
		return groupeEnemy;
	}

	//_______________________________________________________________________________________________________________________________________________________________//
	
	public static GameObject Vgroup(string spline)
	{
		GameObject groupeEnemy = Instantiate(Resources.Load ("Vaisseau/Groups/Vgroup"), new Vector3(0, 30, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
		groupeEnemy.name = "groupeEnemy" + nbActuelVague;
		groupeEnemy.GetComponent<SplineWalker>().spline = GameObject.Find(spline).GetComponent<BezierSpline>();
		for(int i =0; i < groupeEnemy.GetComponent<Group>().listEnemy.Count; i++)
		{
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().gameObjectName = groupeEnemy.name;
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().scriptName = "Group";
		}
		nbActuelVague++;
		return groupeEnemy;
	}

	//_______________________________________________________________________________________________________________________________________________________________//
	
	public static GameObject CircleGroup(string spline)
	{
		GameObject groupeEnemy = Instantiate(Resources.Load ("Vaisseau/Groups/CircleGroup"), new Vector3(0, 30, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
		groupeEnemy.name = "groupeEnemy" + nbActuelVague;
		groupeEnemy.GetComponent<SplineWalker>().spline = GameObject.Find(spline).GetComponent<BezierSpline>();
		for(int i =0; i < groupeEnemy.GetComponent<Group>().listEnemy.Count; i++)
		{
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().gameObjectName = groupeEnemy.name;
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().scriptName = "Group";
		}
		nbActuelVague++;
		return groupeEnemy;
	}

	//_______________________________________________________________________________________________________________________________________________________________//

	public static GameObject ArrowGroup(string spline)
	{
		GameObject groupeEnemy = Instantiate(Resources.Load ("Vaisseau/Groups/ArrowGroup"), new Vector3(0, 30, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
		groupeEnemy.name = "groupeEnemy" + nbActuelVague;
		groupeEnemy.GetComponent<SplineWalker>().spline = GameObject.Find(spline).GetComponent<BezierSpline>();
		for(int i =0; i < groupeEnemy.GetComponent<Group>().listEnemy.Count; i++)
		{
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().gameObjectName = groupeEnemy.name;
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().scriptName = "Group";
		}
		nbActuelVague++;
		return groupeEnemy;
	}

	//_______________________________________________________________________________________________________________________________________________________________//
	
	public static GameObject MineField()
	{
		GameObject groupeEnemy = Instantiate(Resources.Load ("Vaisseau/Groups/MineField"), new Vector3(0, 30, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
		groupeEnemy.name = "groupeEnemy" + nbActuelVague;
		groupeEnemy.GetComponent<SplineWalker>().spline = GameObject.Find("SplineHorCenter").GetComponent<BezierSpline>();
		for(int i =0; i < groupeEnemy.GetComponent<Group>().listEnemy.Count; i++)
		{
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().gameObjectName = groupeEnemy.name;
			groupeEnemy.GetComponent<Group>().listEnemy[i].GetComponent<DestroyLocation>().scriptName = "Group";
		}
		nbActuelVague++;
		return groupeEnemy;
	}
}