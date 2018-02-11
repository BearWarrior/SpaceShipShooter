using UnityEngine;
using System.Collections;


/*
Ce menu se trouve juste après le menu principal (Jouer option quitter)
Il présente les 4 possibilités qu'a le joueur :
	-Marché : achat de plans d'armes et de chassis
	-Atelier : Construction des armes relatifs aux plans
	-Hangar : Assemblage de ses vaisseaux (choix chassis, armes etc.)
	-Jouer : Il va choisir parmis ces différents vaisseau et lancer la partie
*/


public class MenuIntermediaire : MonoBehaviour 
{
	private int maxX;
	private int maxY;

	private Rect rectBox1;
	private Rect rectBox2;
	private Rect rectButtonJouer;
	private Rect rectButtonMarche;
	private Rect rectButtonAtelier;
	private Rect rectButtonHangar;
	private Rect rectButtonQuitter;

	private Rect rectBoxDescrMarche;
	private Rect rectBoxDescrAtelier;
	private Rect rectBoxDescrHangar;

	private GUISkin skin;

	private bool escapePush;

	// Use this for initialization
	void Start () 
	{
		maxX = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().LARGEURpx;
		maxY = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().HAUTEURpx;

		rectBox1 = new Rect (175, 75, 250, 400);
		rectBox2 = new Rect (175, maxY - 50 - 100 -25, 250, 100);
		rectButtonJouer = new Rect(200, 100, 200, 50);
		rectButtonMarche = new Rect(200, 200, 200, 50);
		rectButtonAtelier = new Rect(200, 300, 200, 50);
		rectButtonHangar = new Rect(200, 400, 200, 50);

		rectBoxDescrMarche = new Rect (500, 200, 400, 40);
		rectBoxDescrAtelier = new Rect (500, 300, 400, 80);
		rectBoxDescrHangar = new Rect (500, 400, 400, 80);

		rectButtonQuitter = new Rect(200, maxY - 50 - 100, 200, 50);

		skin = Resources.Load ("Menu/SkinMenu") as GUISkin;

		escapePush = false;
	}


	void OnGUI()
	{
		Vector2 mousePos = new Vector2(Input.mousePosition.x, GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().HAUTEURpx - Input.mousePosition.y);

		GUI.skin = skin;

		GUI.Box (rectBox1, "");
		GUI.Box (rectBox2, "");

		if(GUI.Button(rectButtonJouer, "Jouer "))
		{
			GetComponent<AudioSource>().Play();
			GameObject.Find("LightInter").GetComponent<Light>().enabled = false;
			GameObject.Find("LightG1").GetComponent<Light>().enabled = true;
			gameObject.AddComponent<MenuChoixLevel>();
			GetComponent<MenuIntermediaire>().enabled = false;
		}

		//MARCHE DE PLAN
		if(GUI.Button(rectButtonMarche, "Marché de plans"))
		{
			GetComponent<AudioSource>().Play();
			gameObject.AddComponent<MenuAchat>();
			GetComponent<MenuIntermediaire>().enabled = false;
		}
		if(mousePos.x > rectButtonMarche.x && mousePos.x < (rectButtonMarche.x+rectButtonMarche.width) && mousePos.y > rectButtonMarche.y && mousePos.y < (rectButtonMarche.y+rectButtonMarche.height))
		{
			GUI.Box(new Rect(rectBoxDescrMarche.x - 20, rectBoxDescrMarche.y - 20, rectBoxDescrMarche.width + 40, rectBoxDescrMarche.height+40), "");
			GUI.Label(rectBoxDescrMarche, "Ici, vous pouvez acheter des plans d'arme qui vous serviront à construire des armes " +
				"dans le menu Atelier.");
		}

		//ATELIER
		if(GUI.Button(rectButtonAtelier, "Atelier"))
		{
			GetComponent<AudioSource>().Play();
			gameObject.AddComponent<MenuAtelier>();
			GetComponent<MenuIntermediaire>().enabled = false;
		}
		if(mousePos.x > rectButtonAtelier.x && mousePos.x < (rectButtonAtelier.x+rectButtonAtelier.width) && mousePos.y > rectButtonAtelier.y && mousePos.y < (rectButtonAtelier.y+rectButtonAtelier.height))
		{
			GUI.Box(new Rect(rectBoxDescrAtelier.x - 20, rectBoxDescrAtelier.y - 20, rectBoxDescrAtelier.width + 40, rectBoxDescrAtelier.height+40), "");
			GUI.Label(rectBoxDescrAtelier, "Ici, vous pouvez construire les armes dont vous avez les plans avec de l'argent et de la ferraille.\n" +
				"Pensez à construire autant d'armes que vous voulez en placer sur vos vaisseaux.");
		}

		//HANGAR
		if(GUI.Button(rectButtonHangar, "Hangar"))
		{
			GetComponent<AudioSource>().Play();
			gameObject.AddComponent<MenuHangar>();
			GetComponent<MenuIntermediaire>().enabled = false;
		}
		if(mousePos.x > rectButtonHangar.x && mousePos.x < (rectButtonHangar.x+rectButtonHangar.width) && mousePos.y > rectButtonHangar.y && mousePos.y < (rectButtonHangar.y+rectButtonHangar.height))
		{
			GUI.Box(new Rect(rectBoxDescrHangar.x - 20, rectBoxDescrHangar.y - 20, rectBoxDescrHangar.width + 40, rectBoxDescrHangar.height+40), "");
			GUI.Label(rectBoxDescrHangar, "Ici, vous pouvez placer les différentes armes que vous avez construites.\n" +
				"Le dernier vaisseau que vous choisissez sera celui avec lequel vous jouerez.");
		}

		if(GUI.Button(rectButtonQuitter, "Quitter") || escapePush)
		{
			GetComponent<AudioSource>().Play();
			GameObject.FindWithTag("PlayerScript").GetComponent<Player>().Save();
			GetComponent<MenuIntermediaire>().enabled = false;
			GetComponent<MenuPrincipal>().enabled = true;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			escapePush = true;
		}
		else
		{
			escapePush = false;
		}
	}
}
