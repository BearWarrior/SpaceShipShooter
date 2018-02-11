using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	//On initialise la vitesse a (1000, 0)
	public Vector2 vitesse;
	public int degats;

	public int direction;
	public int angle;
	public bool start;
	
	//Sert à savoir si le projectile est entré en contact avec un 'enemy'
	public bool touche;

	
	// Update is called once per frame
	void Update () 
	{
		chkDestroy ();
	}

	protected void chkDestroy ()
	{
		//Si le projectile est trop a droite (hors champ camera)
		if(transform.position.x > GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x)
		{
			Destroy(this.gameObject);
		}
		//Si le projectile est trop a gauche (hors champ camera)
		if(transform.position.x < GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerDownLeft.x)
		{
			Destroy(this.gameObject);
		}
		//Si le projectile est trop en haut (hors champ camera)
		if(transform.position.y > GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.y)
		{
			Destroy(this.gameObject);
		}
		//Si le projectile est trop en bas (hors champ camera)
		if(transform.position.y < GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerDownLeft.y)
		{
			Destroy(this.gameObject);
		}
	}

	public void collision()
	{
		//Ici on pourra incrémenter un compteur de dégats du missile et entrer dans le SI seulement si ce compteru dépasse une valeur
		touche = true;
		//le projectile est entré en contact avec un 'enemy'
		if(touche)
		{
			//Pour l'instant on détruit le projectile, on pourra lui mettre des points de vie pour qu'il puisse traverser les 'enemy'
			Destroy(this.gameObject);
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		//Si c'est un projectile Enemy
		if(tag == "ProjectilesEnemy")
		{
			if(other.tag == "ChassiPlayer")
			{
				other.GetComponentInParent<ChassisPlayer>().Touche(degats, false); //false -> pas de loot du player
				if(other.GetComponentInParent<ChassisPlayer>().pointDeVie > 0)
				{
					Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().smallExplosion, new Vector3(transform.position.x, transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));
					Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().etincelle, new Vector3(transform.position.x, transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));

				}
				else
				{
					Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().mediumExplosion, new Vector3(transform.position.x, transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));
				}

				Destroy(this.gameObject);
			}

			if(other.tag == "ChassiAllie")
			{
				other.GetComponentInParent<ChassisAllie>().Touche(degats, false); //false -> pas de loot du player
				if(other.GetComponentInParent<ChassisAllie>().pointDeVie > 0)
				{
					Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().smallExplosion, new Vector3(transform.position.x, transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));
					Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().etincelle, new Vector3(transform.position.x, transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));
				}
				else
				{
					Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().mediumExplosion, new Vector3(transform.position.x, transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));
				}
				
				Destroy(this.gameObject);
			}

		}
		else
		{
			//Si c'est un projectile du joueur et que ça touche un Enemy : 
			if(tag == "Projectiles")
			{
				if(other.tag == "Enemy")
				{
				
					other.transform.GetComponent<ChassisEnemy>().Touche(degats, true);//true -> loot oK
					if(other.GetComponent<ChassisEnemy>().pointDeVie > 0)
					{
						Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().smallExplosion, new Vector3(transform.position.x, transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));

						Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().etincelle, new Vector3(transform.position.x, transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));
					}
					else
					{
						Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().mediumExplosion, new Vector3(transform.position.x, transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));
					}

					Destroy(this.gameObject);
				}
			}
		}
	}
}
