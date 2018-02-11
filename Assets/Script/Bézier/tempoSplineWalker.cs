using UnityEngine;
using System.Collections;

public class tempoSplineWalker : MonoBehaviour {

	public float tempsMax;
	private float tempsActuel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(tempsActuel > tempsMax){
			GetComponent<SplineWalker>().enabled = true;
			Destroy(GetComponent<tempoSplineWalker>());
		}
		else{
			tempsActuel+=Time.deltaTime;
		}
	}
}
