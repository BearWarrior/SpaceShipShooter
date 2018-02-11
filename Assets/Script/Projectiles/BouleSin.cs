using UnityEngine;
using System.Collections;

public class BouleSin : Projectile 
{


	private float temps;
	private float deplacementSin;
	private bool lauched;
	private float posDepart;

	[Header("Caractérsiques : ")]
	public float amplMax;
	public float vitesseVert;
	public float vitesseHoriz;


	public BouleSin()
	{
		degats = 1;
		//Sert à savoir si le projectile est entré en contact avec un 'enemy'
		touche = false;
		temps = 0;

	}

	public void Launch()
	{
		lauched = true;
	}

	// Use this for initialization
	void Start () 
	{
		posDepart = transform.position.y;
	}

	// Update is called once per frame
	void Update () 
	{
		if(lauched)
		{
			temps += Time.deltaTime;
			
			
			deplacementSin = amplMax * Mathf.Sin(2 * Mathf.PI * temps * vitesseVert) + posDepart;
			transform.position = new Vector3 (transform.position.x, deplacementSin, transform.position.z);
			transform.Translate (vitesseHoriz * Time.deltaTime, 0, 0);
		}
		chkDestroy ();
	}
}
