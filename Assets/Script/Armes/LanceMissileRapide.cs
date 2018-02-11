using UnityEngine;
using System.Collections;

public class LanceMissileRapide : Arme 
{
	void Awake()
	{
		nom = "LanceMissileRapide";
		
		locArme = "Arme/LanceMissileRapide";
		locProjectile = "Projectiles/Missile";
		
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
			//On instantie un projectile à la position de l'arme + un offset (dépendant de l'arme)
			Transform proj;
			projectileT = Resources.Load<Transform>(locProjectile);
			
			proj = Instantiate(projectileT, posArme + posDepart, new Quaternion(0, 0, 0, 0)) as Transform;
			proj.GetComponent<Missile>().direction = direction;
			proj.GetComponent<Missile>().degats *= bonusDegat;
			proj.GetComponent<Missile>().angle = Random.Range(-30, 30);
			if(direction == -1)
			{
				proj.tag = "ProjectilesEnemy";
				proj.GetComponent<Missile>().vitesse /= 3;
				proj.GetComponent<Missile>().degats /= 3;
				posArme = new Vector2(posArme.x - 0.8f, posArme.y);
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Red") as Texture;
			}
			else
			{
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Blue") as Texture;
			}
			proj.Rotate(new Vector3(0, 0, direction*angleZ));
			proj.GetComponent<Missile>().Launch();

			if(!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}

			canShoot = false;
		}
	}

	public override string getName()
	{
		return nom;
	}
}
