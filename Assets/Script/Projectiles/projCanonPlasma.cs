using UnityEngine;
using System.Collections;

public class projCanonPlasma : Projectile 
{
	public int vitesseH;

	public projCanonPlasma()
	{
		angle = 0;
		degats = 5;
		//Sert à savoir si le projectile est entré en contact avec un 'enemy'
		touche = false;
	}
	
	public void Launch()
	{
		//On ajoute l'angle à la vitesse
		vitesse = new Vector2(vitesseH, angle);
		//On le retourne (oui il est pas droit ...)
		transform.eulerAngles = new Vector3 (0, 0, 90);
		//On lui ajoute une vitesse
		GetComponent<Rigidbody2D>().AddForce(vitesse * direction, ForceMode2D.Force);
	}
}
