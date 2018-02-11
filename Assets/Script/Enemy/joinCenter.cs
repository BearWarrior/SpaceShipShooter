using UnityEngine;
using System.Collections;

public class joinCenter : MonoBehaviour 
{
	public float vitesse;
	private Vector2 deplacement;
	public bool waitingForOrder;

	// Use this for initialization
	void Start () 
	{
		waitingForOrder = false;
		vitesse = 1.5f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.position.y >= 0.1 )
		{
			deplacement = new Vector2 (0, -vitesse);
			transform.Translate (deplacement * Time.deltaTime, Space.World);
		}
		else
		{
			if(transform.position.y <= -0.1)
			{
				deplacement = new Vector2 (0, vitesse);
				transform.Translate (deplacement * Time.deltaTime, Space.World);
			}
		}
		
		//Si ils ont atteint le centre, on repart comme avant
		if(transform.position.y <= 0.1 && transform.position.y >= -0.1)
		{
			waitingForOrder = true;
		}
	}
}
