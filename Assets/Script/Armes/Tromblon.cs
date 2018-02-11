using UnityEngine;
using System.Collections;

public class Tromblon : Arme 
{


	void Awake()
	{
		nom = "Tromblon";
		
		locArme = "Arme/Tromblon";
		locProjectile = "Projectiles/projTromblon";
		
		//Position relative par rapport à l'arme
		posDepart = new Vector2(0.7f, 0);
		
		verouillage = false;
		reVerouillage = false;
		
		canShoot = true;
		
		//Caractéristique de l'arme
		int pos = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().getPosFromStr (nom);
		frequenceTir = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().tabArme [pos].frequence;
		degats = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().tabArme [pos].degats;
		prixArmeArgent = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().tabArme [pos].prixArmeArgent;
		prixArmeFerraille = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().tabArme [pos].prixArmeFerraille;
		prixPlanArgent = GameObject.FindWithTag ("PlayerScript").GetComponent<CaracArmes> ().tabArme [pos].prixPlanArgent;

		
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("soundEffects");
	}


	public override void Fire(Vector3 posArme, int direction, int bonusDegat, float angleZ)
	{
		if(canShoot)
		{
			if(direction == -1)
			{
				posArme = new Vector2(posArme.x - 0.8f, posArme.y);
			}
			//On instantie un projectile à la position de l'arme + un offset (dépendant de l'arme)
			Transform proj;
			projectileT = Resources.Load<Transform>(locProjectile);

			proj = Instantiate(projectileT, posArme + posDepart, new Quaternion(0, 0, 0, 0)) as Transform;
			proj.GetComponent<projTromblon>().direction = direction;
			proj.GetComponent<projTromblon>().degats *= bonusDegat;
			proj.GetComponent<projTromblon>().angle = 0;
			if(direction == -1)
			//if(transform.parent.tag == "Enemy")
			{
				proj.tag = "ProjectilesEnemy";
				proj.GetComponent<projTromblon>().vitesseH /= 2;
				proj.GetComponent<projTromblon>().degats /= 3;
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Red") as Texture;
			}
			else
			{
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Blue") as Texture;
			}

			proj.GetComponent<projTromblon>().Launch();

			proj = Instantiate(projectileT, posArme + posDepart, new Quaternion(0, 0, 0, 0)) as Transform;
			proj.GetComponent<projTromblon>().direction = direction;
			proj.GetComponent<projTromblon>().degats *= bonusDegat;
			proj.GetComponent<projTromblon>().angle = 50;
			if(direction == -1)
			//if(transform.parent.tag == "Enemy")
			{
				proj.tag = "ProjectilesEnemy";
				proj.GetComponent<projTromblon>().vitesseH /= 2;
				proj.GetComponent<projTromblon>().degats /= 3;
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Red") as Texture;
			}
			else
			{
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Blue") as Texture;
			}

			proj.GetComponent<projTromblon>().Launch();

			proj = Instantiate(projectileT, posArme + posDepart, new Quaternion(0, 0, 0, 0)) as Transform;
			proj.GetComponent<projTromblon>().direction = direction;
			proj.GetComponent<projTromblon>().degats *= bonusDegat;
			proj.GetComponent<projTromblon>().angle = -50;
			if(direction == -1)
			//if(transform.parent.tag == "Enemy")
			{
				proj.tag = "ProjectilesEnemy";
				proj.GetComponent<projTromblon>().vitesseH /= 2;
				proj.GetComponent<projTromblon>().degats /= 3;
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Red") as Texture;
			}
			else
			{
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Blue") as Texture;
			}

			GetComponent<AudioSource>().Play();

			proj.GetComponent<projTromblon>().Launch();
			
			canShoot = false;
		}
	}

	public override string getName()
	{
		return nom;
	}
}
