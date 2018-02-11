using UnityEngine;
using System.Collections;

public class ParticleManagment : MonoBehaviour 
{
	public float vitesse;
	// Use this for initialization
	void Start () 
	{
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("soundEffects");
	}
}
