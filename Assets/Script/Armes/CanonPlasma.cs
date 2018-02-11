using UnityEngine;
using System.Collections;

public class CanonPlasma : Arme 
{
	void Awake()
	{
		nom = "CanonPlasma";
		
		locArme = "Arme/CanonPlasma";
		locProjectile = "Projectiles/projCanonPlasma";
		
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
			proj.GetComponent<projCanonPlasma>().direction = direction;
			
			proj.GetComponent<projCanonPlasma>().degats *= bonusDegat;
			if(direction == -1)
			{
				proj.tag = "ProjectilesEnemy";
				proj.GetComponent<Missile>().vitesse /= 3;
				proj.GetComponent<Missile>().degats /= 3;
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Red") as Texture;
			}
			else
			{
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Blue") as Texture;
			}
			proj.Rotate(new Vector3(0, 0, direction*angleZ));

			GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.05f);
			GetComponent<AudioSource>().Play();

			proj.GetComponent<projCanonPlasma>().Launch();
			
			canShoot = false;
		}
	}

	public override string getName()
	{
		return nom;
	}
}
