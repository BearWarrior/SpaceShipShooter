using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour 
{
	public float vitesse;
	public float vitesseRotate;
	public float vitesseRotationSouris;
	
	public bool mvtRight;
	public bool mvtLeft;

	public bool shootOK;
	public bool deplacementOK;


	//Permet de calculer la valeur de la rotation à donner au vaisseau
	//Dans le cadre du déplacement à la souris afin d'avoir une rotation fluide
	float CalculRotation (float rotationFinale)
	{
		//Récupération de la valeur actuelle de la rotation
		float rotationActuelle = transform.rotation.eulerAngles.x;
		
		//Vérification de l'angle obtenu (on cherche à obtenir un angle entre -45° et 45°)
		if (rotationActuelle > 180)
		{
			rotationActuelle -= 360;
		}
		
		//On compare la valeur de la rotation à la valeur à atteindre
		if (rotationFinale > rotationActuelle)
		{
			//Il va falloir incrémenter la rotation de façon à se rapprocher de la rotation à atteindre sans la dépasser
			rotationActuelle += vitesseRotationSouris*Time.deltaTime;
			if (rotationActuelle > rotationFinale) {rotationActuelle = rotationFinale;}
		} 
		else if (rotationFinale == rotationActuelle)
		{
			//On ne fait rien : la rotation est déjà à la valeur souhaitée.
		} 
		else
		{
			//Il va falloir décrémenter la rotation de façon à se rapprocher de la rotation à atteindre sans passer en-dessous
			rotationActuelle -= vitesseRotationSouris*Time.deltaTime;
			if (rotationActuelle < rotationFinale) {rotationActuelle = rotationFinale;}
		}
		return rotationActuelle;
	}


	// Use this for initialization
	void Start () 
	{
		mvtRight = true;
		mvtLeft = true;
		vitesse = GameObject.FindWithTag("Player").GetComponent<ChassisPlayer>().chassiActuel.vitesse;
		vitesseRotate = 2000;
		vitesseRotationSouris = 150;
		shootOK = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Récuperation du point inferieur gauche de la camera en position dans le monde 
		Vector3 posCamBG = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)); 
		//Récuperation du point inferieur gauche de la camera en position dans le monde 
		Vector3 posCamHD = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));


		if(deplacementOK)
		{
			//Déplacement au clavier
			if (PlayerPrefs.GetInt ("movePrefs") == 1)
			{
				//Gestion haut bas
				float vecVert = Input.GetAxis("Vertical");	
				if(transform.position.y  > posCamHD.y && vecVert > 0 ) //On a atteint le bord de la camera HAUT et on veut encore monter -> On bloque
				{
					vecVert = 0;
				}
				if(transform.position.y  < posCamBG.y && vecVert < 0 ) //On a atteint le bord de la camera HAUT et on veut encore monter -> On bloque
				{
					vecVert = 0;
				}
				
				//Geston droite gauche
				float vecHor = Input.GetAxis ("Horizontal");
				if(transform.position.x  > GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x /2 /*0*/ /*(-posCamHD.x/3)*/ && vecHor > 0 ) //On a atteint le bord de la camera HAUT et on veut encore monter -> On bloque
				{
					vecHor = 0;
				}
				if(transform.position.x  < posCamBG.x && vecHor < 0 ) //On a atteint le bord de la camera HAUT et on veut encore monter -> On bloque
				{
					vecHor = 0;
				}
				
				Vector2 deplacement = new Vector2 (vecHor, vecVert);
				transform.Translate (deplacement * Time.deltaTime * vitesse, Space.World);
				
				//On garde des rotations fixées (évite que le vaisseau parte en cacahuète quand il heurte un mur)
				transform.eulerAngles = new Vector3(Input.GetAxis("Vertical") * Time.deltaTime * vitesseRotate, 0, 0);
			}
			
			
			//Déplacement à la souris
			if (PlayerPrefs.GetInt("movePrefs") == 0)
			{
				//Définition du pas maximal de déplacement
				float vitesseSouris = vitesse * Time.deltaTime;
				//Récupération de la position du curseur dans la scène
				Vector2 cibleSouris = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				
				
				
				//Gestion deplacement droite gauche
				if(transform.position.x  > GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x /2) //On ne peut pas se déplacer à droite
				{
					//Si un déplacement à droite était prévu
					if(cibleSouris.x > transform.position.x)
					{
						//On plafonne la position droite à la position actuelle du vaisseau.
						cibleSouris.x = transform.position.x;
					}
				}
				
				if(transform.position.x  < posCamBG.x) //On ne peut pas se déplacer à droite
				{
					//Si un déplacement à droite était prévu
					if(cibleSouris.x < transform.position.x)
					{
						//On plafonne la position droite à la position actuelle du vaisseau.
						cibleSouris.x = transform.position.x;
					}
				}
				
				//Gestion deplacement haut bas
				if(transform.position.y  > posCamHD.y) //On ne peut pas se déplacer à droite
				{
					//Si un déplacement à droite était prévu
					if(cibleSouris.y > transform.position.y)
					{
						//On plafonne la position droite à la position actuelle du vaisseau.
						cibleSouris.y = transform.position.y;
					}
				}
				if(transform.position.y  < posCamBG.y) //On ne peut pas se déplacer à droite
				{
					//Si un déplacement à droite était prévu
					if(cibleSouris.y < transform.position.y)
					{
						//On plafonne la position droite à la position actuelle du vaisseau.
						cibleSouris.y = transform.position.y;
					}
				}
				
				
				//Rotation à la souris
				float rotationSouris = 0;
				
				Vector2 deplacement = cibleSouris - new Vector2(transform.position.x, transform.position.y);
				
				//Vérification de la division par zéro :
				if(deplacement.x == 0)
				{
					//On fixe la rotation à une valeur arbitraire
					if (Mathf.Abs(deplacement.y)*20 > 45)
					{
						rotationSouris = 45;
					}
					else
					{
						rotationSouris = Mathf.Abs(deplacement.y)*20;
					}
				}
				else
				{
					//Rapport de la distance à parcourir suivant x et suivant y
					float rapportDeplacement = Mathf.Abs(deplacement.y)/Mathf.Abs(deplacement.x);
					
					//Si le rapport est supérieur à 1 (y > x) on fixe la rotation à 45°
					if(rapportDeplacement > 1)
					{
						rotationSouris = 45;
					}
					else
					{
						rotationSouris = rapportDeplacement*45;
					}
					
					//Adaptation de la valeur de rotation au signe du déplacement
					if(deplacement.y < 0)
					{
						rotationSouris = -rotationSouris;
					}
					
				}
				
				//Déplacement de l'objet vers le curseur
				transform.position = Vector3.MoveTowards(new Vector3(transform.position.x,transform.position.y,-15),
				                                         new Vector3(cibleSouris.x,cibleSouris.y,-15), vitesseSouris);
				
				//On garde des rotations fixées (évite que le vaisseau parte en cacahuète quand il heurte un mur)
				transform.eulerAngles = new Vector3(CalculRotation(rotationSouris), 0, 0);
			}
		}


		//RESET L'ARGENT LA ferraille etc.
		if (Input.GetKey (KeyCode.I))
		{
			GameObject.FindWithTag("PlayerScript").GetComponent<Player>().argent = 0;
			PlayerPrefs.SetInt ("argent", 0);
			GameObject.FindWithTag("PlayerScript").GetComponent<Player>().ferraille =0;
			PlayerPrefs.SetInt ("ferraille", 0);
		}
		
		
		if (Input.GetKey (KeyCode.L))
		{
			GameObject.FindWithTag("PlayerScript").GetComponent<Player>().GagnerArgent(250000);
			//PlayerPrefs.SetInt ("argent", 0);
			GameObject.FindWithTag("PlayerScript").GetComponent<Player>().Gagnerferraille(1000);
			//PlayerPrefs.SetInt ("ferraille", 0);
		}
	}	
}