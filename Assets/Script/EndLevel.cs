using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour 
{
	public bool endLevel;
	private GameObject player;
	private float tempsMax;
	private float tempsAct;
	// Use this for initialization
	void Start () 
	{
		endLevel = false;
		tempsMax = 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(endLevel)
		{
			if(tempsAct > tempsMax)
			{
				if(GameObject.Find("patternToLaunch").GetComponent<PatternToLaunch>().flotte)
				{
					//On  désactive le script de mouvement
					GameObject.FindWithTag("flotte").GetComponent<FlotteController>().enabled = false;
					GameObject.FindWithTag("Player").GetComponent<ChassisPlayer>().invulnerable = true;
					
					player = GameObject.FindWithTag("flotte").gameObject;
					player.transform.Translate(new Vector2(1, 0) * Time.deltaTime * 5, Space.World);

					GameObject.FindWithTag("Player").gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
					for(int i = 0; i < player.GetComponent<FlotteController>().listAllie.Count; i++)
					{
						player.GetComponent<FlotteController>().listAllie[i].transform.eulerAngles = new Vector3(0, 0, 0);
					}


					if(player.transform.position.x > GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x + 3)
					{
						GameObject.FindWithTag("Pause").GetComponent<MenuPause>().pause = true;
					}
				}
				else
				{
					//On  désactive le script de mouvement
					GameObject.FindWithTag("Player").GetComponent<characterController>().enabled = false;
					GameObject.FindWithTag("Player").GetComponent<ChassisPlayer>().invulnerable = true;
					
					player = GameObject.FindWithTag("Player").gameObject;
					player.transform.Translate(new Vector2(1, 0) * Time.deltaTime * 5, Space.World);
					player.transform.eulerAngles = new Vector3(0, 0, 0);	
					if(player.transform.position.x > GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x + 3)
					{
						GameObject.FindWithTag("Pause").GetComponent<MenuPause>().pause = true;
					}
				}
			}
			else
			{
				tempsAct += Time.deltaTime;
			}
		}
	}
}
