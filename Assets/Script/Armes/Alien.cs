using UnityEngine;
using System.Collections;

public class Alien : Arme 
{
	public string locProjectileSin;

	void Awake()
	{
		nom = "Alien";
		locArme = "Arme/Alien";
		
		locProjectile = "Projectiles/projTromblon";
		locProjectileSin = "Projectiles/BouleSin";
		
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
			{
				proj.tag = "ProjectilesEnemy";
				proj.GetComponent<projTromblon>().vitesse /= 3;
				proj.GetComponent<projTromblon>().degats /= 3;
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Red") as Texture;
			}
			else
			{
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Blue") as Texture;
			}
			proj.Rotate(new Vector3(0, 0, direction*angleZ));
			proj.GetComponent<projTromblon>().Launch();

			projectileT = Resources.Load<Transform>(locProjectileSin);

			proj = Instantiate(projectileT, posArme + posDepart/* + new Vector2(0, 0.1f)*/, new Quaternion(0, 0, 0, 0)) as Transform;
			proj.GetComponent<BouleSin>().direction = direction;
			proj.GetComponent<BouleSin>().degats *= bonusDegat;
			proj.GetComponent<BouleSin>().amplMax = proj.GetComponent<BouleSin>().amplMax/2;
			if(direction == -1)
			{
				proj.tag = "ProjectilesEnemy";
				proj.GetComponent<BouleSin>().vitesse /= 3;
				proj.GetComponent<BouleSin>().degats /= 3;
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Red") as Texture;
			}
			else
			{
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Blue") as Texture;
			}
			proj.Rotate(new Vector3(0, 0, direction*angleZ));
			proj.GetComponent<BouleSin>().Launch();
			
			proj = Instantiate(projectileT, posArme + posDepart/* + new Vector2(0, -0.1f)*/, new Quaternion(0, 0, 0, 0)) as Transform;
			proj.GetComponent<BouleSin>().direction = direction;
			proj.GetComponent<BouleSin>().degats *= bonusDegat;
			proj.GetComponent<BouleSin>().amplMax = -proj.GetComponent<BouleSin>().amplMax/2;
			if(direction == -1)
			{
				proj.tag = "ProjectilesEnemy";
				proj.GetComponent<BouleSin>().vitesse /= 3;
				proj.GetComponent<BouleSin>().degats /= 3;
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Red") as Texture;
			}
			else
			{
				proj.GetComponent<Renderer>().material.mainTexture = Resources.Load("Projectiles/Blue") as Texture;
			}
			proj.Rotate(new Vector3(0, 0, direction*angleZ));

			GetComponent<AudioSource>().Play();

			proj.GetComponent<BouleSin>().Launch();
			
			canShoot = false;
		}
	}

	public override string getName()
	{
		return nom;
	}
}
