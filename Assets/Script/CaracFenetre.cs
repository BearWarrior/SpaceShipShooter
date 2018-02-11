using UnityEngine;
using System.Collections;

public class CaracFenetre : MonoBehaviour 
{	
	//Fenetre
	public int HAUTEURpx;
	public int LARGEURpx;
	//Extremums camera -> world
	public Vector2 CornerDownLeft;
	public Vector2 CornerUpRight;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (this.gameObject);

		//Fenetre
		HAUTEURpx = Screen.height;
		LARGEURpx = Screen.width;

		//Extremums camera -> world
		CornerDownLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
		CornerUpRight = Camera.main.ScreenToWorldPoint(new Vector2(LARGEURpx, HAUTEURpx));
	}

	public void checkFenetre()
	{
		//Fenetre
		HAUTEURpx = Screen.height;
		LARGEURpx = Screen.width;
		
		//Extremums camera -> world
		CornerDownLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
		CornerUpRight = Camera.main.ScreenToWorldPoint(new Vector2(LARGEURpx, HAUTEURpx));
	}
}
