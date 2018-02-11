using UnityEngine;
using System.Collections;

public class MissileEnemy : Projectile 
{
	public int vitesseH;

	public MissileEnemy()
	{
		vitesse = new Vector2(vitesseH, 0);
		degats = 1;
		//Sert à savoir si le projectile est entré en contact avec un 'enemy'
		touche = false;
	}

	// Use this for initialization
	public void Launch () 
	{
		vitesse = new Vector2(vitesseH, 0);
		//On le retourne (oui il est pas droit ...)
		transform.eulerAngles = new Vector3 (0, 0, 90);
		//On lui ajoute une vitesse
		GetComponent<Rigidbody2D>().AddForce(vitesse * direction, ForceMode2D.Force);
	}
}
