using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public string[] armeVaisseau;
	public Chassis m_chassis;
	public int nbPointDeVie;
	public string nom;
	
	// Use this for initialization
	void Start ()
	{
		this.gameObject.AddComponent <ChassisEnemy>();
	}
}
