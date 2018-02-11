using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chassis : MonoBehaviour 
{
	public float pointDeVie;	
	public int nbPointDattache;

	//Tableau fixe donnant la position sur le vaisseau des différentes armes
	public List<Vector3> posArme/* = new List<Vector2>()*/;

	//Utilisé pour l'instanciation d'une arme 
	public Transform armeCree;

	//Utilisé pour l'instanciation d'un chassi
	public Transform chassis;
	public Transform chassisCree;

	//Position global de l'arme (mis a jour avant chaque appel de la fonction Fire)
	public Vector3 posArmeGlobal;

	//Tableau contenant les GameObject arme
	public List<Transform> ListTArme;

	public bool invulnerable;

	// Use this for initialization
	void Start () 
	{
		invulnerable = false;
	}

	public void Touche (int degats, bool loot)
	{
		if(invulnerable == false)
		{
			if(pointDeVie > 0)
			{
				pointDeVie -= degats;
			}
		}


		if(pointDeVie <= 0)
		{
			//Si ce n'est pas le joueur qui est mort
			if(this.gameObject.tag != "Player")
			{
				if(GetComponent<DestroyLocation>() != null)
				{
					if(GetComponent<DestroyLocation>().gameObjectName != "" && GetComponent<DestroyLocation>().scriptName != "")
					{
						string gameObjectName = GetComponent<DestroyLocation>().gameObjectName;
						string scriptStr = GetComponent<DestroyLocation>().scriptName;

						Group script = GameObject.Find(gameObjectName).GetComponent(scriptStr) as Group;
						script.listEnemy.Remove(this.transform);

						//Si l'enemy a été tué par le player avec ses armes (pas de loot lorsque collision vaisseau a vaisseau ou sorti de carte)
						if(loot)
						{
							GameObject.FindWithTag("LootScript").GetComponent<LootScript>().Loot("Enemy1", this.transform.position, GetComponent<DestroyLocation>().ferrailleLoot, GetComponent<DestroyLocation>().probaLootFerraille);
							int argent = GetComponent<DestroyLocation>().argentLoot;
							GameObject.Find("HUD").GetComponent<HUD>().afficherGainArgent(argent);
							GameObject.FindWithTag("PlayerScript").GetComponent<Player>().GagnerArgent(argent);
						}
					}
				}
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().nbDePoints++;
			}
			else 
			{
				//Si le joueur est mort
				if( GameObject.Find ("patternToLaunch").GetComponent<PatternToLaunch> ().flotte )
				{
					GameObject.Find ("Flotte").GetComponent<FlotteController>().deplacementOK = false;
					GameObject.Find ("Flotte").GetComponent<FlotteController>().shootOK = false;
				}
				else
				{
					GameObject.FindWithTag ("Player").GetComponent<characterController>().deplacementOK = false;
					GameObject.FindWithTag ("Player").GetComponent<characterController>().shootOK = false;
				}
				GameObject.FindWithTag("Pause").GetComponent<MenuPause>().pause = true;
			}
			Destroy(this.gameObject);

		}
	}

	public bool switchArme(List<Arme> listArme, string nomArme, int cptCrea)
	{
		//RETOURNE FALSE SI LETABLEAU N'EST PAS ASSEZ GRAND
		if (cptCrea >= listArme.Count) 
		{
			listArme.Add(new LanceMissile());
			return false;
		}
		else
		{
			switch(nomArme)
			{
			case "LanceMissile" : 
				listArme[cptCrea] = new LanceMissile();
				break;
			case "LanceMissileRapide" : 
				listArme[cptCrea] = new LanceMissileRapide();
				break;
			case "Laser" : 
				listArme[cptCrea] = new Laser();
				break;
			case "CanonPlasma" : 
				listArme[cptCrea] = new CanonPlasma();
				break;
			case "Tromblon" :
				listArme[cptCrea] = new Tromblon();
				break;
			case "DoubleLame" :
				listArme[cptCrea] = new DoubleLame();
				break;
			case "Alien" :
				listArme[cptCrea] = new Alien();
				break;
			/*case "LanceMissileEnemy" :
				listArme[cptCrea] = new LanceMissileEnemy();*/
				break;
			case "Turret" :
				listArme[cptCrea] = new Turret();
				break;
			default :
				Debug.LogError ("Chassi : Nom d'arme inconnu" + nomArme);
				break;
			}

			return true;
		}
	}

	public void addArme(List<Arme> listArme, string nomArme)
	{
		switch(nomArme)
		{
		case "LanceMissile" : 
			listArme.Add(new LanceMissile());
			break;
		case "LanceMissileRapide" : 
			listArme.Add(new LanceMissileRapide());
			break;
		case "Laser" : 
			listArme.Add(new Laser());
			break;
		case "CanonPlasma" : 
			listArme.Add(new CanonPlasma());
			break;
		case "Tromblon" :
			listArme.Add(new Tromblon());
			break;
		case "DoubleLame" :
			listArme.Add(new DoubleLame());
			break;
		case "Alien" :
			listArme.Add(new Alien());
			break;
		case "Turret" :
			listArme.Add(new Turret());
			break;
		default :
			Debug.LogError ("Chassi : Nom d'arme inconnu" + nomArme);
			break;
		}
	}
}
