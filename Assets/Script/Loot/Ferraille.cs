using UnityEngine;
using System.Collections;

public class Ferraille : MonoBehaviour 
{
	Transform player;
	public int ferraille_;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player != null)
		{
			if(Vector2.Distance(player.transform.position, this.transform.position) < 1)
			{
				GameObject.FindWithTag("PlayerScript").GetComponent<Player>().Gagnerferraille(ferraille_);	
				Destroy(this.gameObject);
			}
		}
	}
}
