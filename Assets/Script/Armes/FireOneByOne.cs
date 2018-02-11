using UnityEngine;
using System.Collections;

public class FireOneByOne : MonoBehaviour 
{
	public float tempsFireAct;
	public float tempsFireMax;
	private bool fire;
	private float tempsEntreDeuxTirs;
	private float tempsEntreDeuxTirsMax;
	private int cptTir;
	// Use this for initialization
	void Start () 
	{
		fire = true;
		//tempsFireAct = 2;
		tempsEntreDeuxTirs = 0;
		tempsEntreDeuxTirsMax = 0.2f;
		cptTir = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Si le decompte est bon, la vague tir
		if(tempsFireAct > tempsFireMax)
		{
			tempsFireAct = 0;
			fire = true;
		}
		else
		{
			tempsFireAct += Time.deltaTime;
		}

		//Si le vague peut tirer
		if(fire)
		{
			//Si le temps entre deux tir est bon , on fait tirer l'enemi et on change d'enemy
			if(tempsEntreDeuxTirs > tempsEntreDeuxTirsMax)
			{
				tempsEntreDeuxTirs = 0;
				if(GetComponent<Group>().listEnemy.Count > cptTir)
				{
					if(GetComponent<Group>().listEnemy[cptTir].position.x < GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x)
					{
						if(GetComponent<Group>().listEnemy[cptTir].position.y < GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.y  &&  GetComponent<Group>().listEnemy[cptTir].position.y > GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerDownLeft.y)
						{
							GetComponent<Group>().listEnemy[cptTir].GetComponent<ChassisEnemy>().Fire ();
						}
					}
				}
				cptTir++;
			}
			else
			{
				tempsEntreDeuxTirs += Time.deltaTime;
			}

			//Si on depasse le nombre d'enemy, on revient a 0
			if(cptTir > GetComponent<Group>().listEnemy.Count)
			{
				cptTir = 0;
				fire = false;
			}
		}
	}
}
