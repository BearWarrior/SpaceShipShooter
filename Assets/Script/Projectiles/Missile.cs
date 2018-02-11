using UnityEngine;
using System.Collections;

public class Missile : Projectile 
{
	public Missile()
	{
		angle = 0;
		vitesse = new Vector2(1000, 0);
		degats = 1;

		//Sert à savoir si le projectile est entré en contact avec un 'enemy'
		touche = false;
	}

	public void Launch()
	{
		//Si l'angle a changé, on recalcule la vitesse : 
		vitesse = new Vector2(vitesse.x, angle);
		//On lui ajoute une vitesse
		GetComponent<Rigidbody2D>().AddRelativeForce(vitesse/* * direction*/, ForceMode2D.Force);
	}
}
