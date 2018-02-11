using UnityEngine;
using System.Collections;

public class DecorsMove : MonoBehaviour 
{
	public float vitesseTransl;
	public Transform BGstarIni;
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(new Vector3(-vitesseTransl * Time.deltaTime,0,0));

		if(transform.position.x <= -60)
		{
			GameObject.FindWithTag ("BackGround").GetComponent<GestionDecors>().CreateDecors();
			Destroy(this.gameObject);
		}
	}
}
