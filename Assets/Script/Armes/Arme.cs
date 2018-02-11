using UnityEngine;
using System.Collections;

public class Arme : MonoBehaviour 
{
	public string locArme;
	public Transform armeT;
	public float frequenceTir;
	public string locProjectile;
	public Transform projectileT;
	public Vector3 posDepart;
	public bool verouillage;
	public bool reVerouillage;

	public bool canShoot;
	public float tempsShootActuel;

	protected string nom;


	public int degats;
	
	public int prixArmeArgent;
	public int prixArmeFerraille;
	public int prixPlanArgent;

	protected AudioSource audioS;

	// Use this for initialization
	void Start () 
	{
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("soundEffects");
	}

	//Méthode appelé lorsque l'on veut tirer avec cette arme
	//On prend en entrée la position GLOBAL de l'arme (correspondant à la position du vaisseau + position de l'arme sur le vaisseau
	public virtual void Fire(Vector3 posArme, int direction, int bonusDegat, float angleZ)
	{
		if(canShoot)
		{
			if(direction == -1)
			{
				posArme = new Vector3(posArme.x - 0.8f, posArme.y, posArme.z);
			}
			//On instantie un projectile à la position de l'arme + un offset (dépendant de l'arme)
			Transform proj;
			projectileT = Resources.Load<Transform>(locProjectile);
			proj = Instantiate(projectileT, posArme + posDepart, new Quaternion(0, 0, 0, 0)) as Transform;
			proj.GetComponent<Missile>().direction = direction;

			proj.GetComponent<Missile>().degats *= bonusDegat;
			if(direction == -1)
			{
				proj.tag = "ProjectilesEnemy";
				proj.GetComponent<Missile>().vitesse /= 2;
				proj.GetComponent<Missile>().degats /= 3;


				if(proj.GetComponent<Missile>().degats <= 0)
				{
					proj.GetComponent<Missile>().degats = 1;
				}

				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Red") as Texture;
			}
			else
			{
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Blue") as Texture;
			}

			proj.Rotate(new Vector3(0, 0, direction*angleZ));
			proj.GetComponent<Missile>().Launch();

			canShoot = false;
		}
	}

	public void Reload ()
	{
		if(!canShoot)
		{
			tempsShootActuel += Time.deltaTime;
			
			if(tempsShootActuel > frequenceTir)
			{
				canShoot = true;
				tempsShootActuel = 0;
			}
		}
	}

	//Ca envoie du steak severe mais ça sert a rien (pour le moment tadada...)
	public void Supprimer()
	{
		Destroy (this.gameObject);
	}

	public virtual string getName()
	{
		return nom;
	}
}
