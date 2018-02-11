using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct structArme
{
	//Nom
	public string nomArme;
	public string nomPlan;
	//Arme
	public int degats;
	public float frequence;
	public bool verouillage;
	public bool reverouillage;
	//Prix Arme
	public int prixArmeArgent;
	public int prixArmeFerraille;
	//Prix Plan
	public int prixPlanArgent;
} 

public class CaracArmes : MonoBehaviour 
{
	public List<structArme> tabArme;

	public structArme LM;
	public structArme LMRapide;
	public structArme CP;
	public structArme Laser;
	public structArme Tromblon;
	public structArme DoubleLame;
	public structArme Alien;
	public structArme Turret;

	// Use this for initialization
	void Start () 
	{
		tabArme = new List<structArme> ();

		//LM
		LM.nomArme = "LanceMissile";
		LM.nomPlan = "PlanLanceMissile";
		LM.degats = 1;
		LM.frequence = 0.3f;
		LM.verouillage = false;
		LM.reverouillage = false;
		LM.prixArmeArgent = 0;
		LM.prixArmeFerraille = 0;
		LM.prixPlanArgent = 0;
		
		tabArme.Add (LM);

		//LMRapide
		LMRapide.nomArme = "LanceMissileRapide";
		LMRapide.nomPlan = "PlanLanceMissileRapide";
		LMRapide.degats = 1;
		LMRapide.frequence = 0.15f;
		LMRapide.verouillage = false;
		LMRapide.reverouillage = false;
		LMRapide.prixArmeArgent = 200;
		LMRapide.prixArmeFerraille = 2;
		LMRapide.prixPlanArgent = 6000;

		tabArme.Add (LMRapide);

		//CP
		CP.nomArme = "CanonPlasma";
		CP.nomPlan = "PlanCanonPlasma";
		CP.degats = 5;
		CP.frequence = 0.6f;
		CP.verouillage = false;
		CP.reverouillage = false;
		CP.prixArmeArgent = 200;
		CP.prixArmeFerraille = 2;
		CP.prixPlanArgent = 8500;

		tabArme.Add (CP);

		//Laser
		Laser.nomArme = "Laser";
		Laser.nomPlan = "PlanLaser";
		Laser.degats = 10;
		Laser.frequence = 0.7f;
		Laser.verouillage = false;
		Laser.reverouillage = false;
		Laser.prixArmeArgent = 1000;
		Laser.prixArmeFerraille = 10;
		Laser.prixPlanArgent = 12500;

		tabArme.Add (Laser);

		//Tromblon
		Tromblon.nomArme = "Tromblon";
		Tromblon.nomPlan = "PlanTromblon";
		Tromblon.degats = 1;
		Tromblon.frequence = 0.7f;
		Tromblon.verouillage = false;
		Tromblon.reverouillage = false;
		Tromblon.prixArmeArgent = 750;
		Tromblon.prixArmeFerraille = 13;
		Tromblon.prixPlanArgent = 11250;
		
		tabArme.Add (Tromblon);

		//DoubleLame
		DoubleLame.nomArme = "DoubleLame";
		DoubleLame.nomPlan = "PlanDoubleLame";
		DoubleLame.degats = 5;
		DoubleLame.frequence = 0.7f;
		DoubleLame.verouillage = false;
		DoubleLame.reverouillage = false;
		DoubleLame.prixArmeArgent = 1000;
		DoubleLame.prixArmeFerraille = 10;
		DoubleLame.prixPlanArgent = 12500;
		
		tabArme.Add (DoubleLame);

		//Alien
		Alien.nomArme = "Alien";
		Alien.nomPlan = "PlanAlien";
		Alien.degats = 10;
		Alien.frequence = 0.7f;
		Alien.verouillage = false;
		Alien.reverouillage = false;
		Alien.prixArmeArgent = 750;
		Alien.prixArmeFerraille = 13;
		Alien.prixPlanArgent = 15000;
		
		tabArme.Add (Alien);

		//Turret
		Turret.nomArme = "Turret";
		Turret.nomPlan = "PlanTurret";
		Turret.degats = 1;
		Turret.frequence = 0.7f;
		Turret.verouillage = false;
		Turret.reverouillage = false;
		Turret.prixArmeArgent = 750;
		Turret.prixArmeFerraille = 13;
		Turret.prixPlanArgent = 30000;
		
		tabArme.Add (Turret);
	}

	public int getPosFromStr(string Pnom)
	{
		for(int i =0; i < tabArme.Count; i++)
		{
			if(tabArme[i].nomArme == Pnom)
			{
				return i;
			}
		}
		return -1;
	}
}
