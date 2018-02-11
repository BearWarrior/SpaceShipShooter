using UnityEngine;
using System.Collections;

public class collisionChassi : MonoBehaviour 
{
	//Collision avec des enemy
	public void OnTriggerStay2D(Collider2D other)
	{
		if(this.tag == "ChassiPlayer")
		{
			if(other.tag == "Enemy")
			{
				GetComponentInParent<ChassisPlayer>().Touche(other.GetComponent<ChassisEnemy>().degatCollision, false); //false -> pas de loot lors de collision entre vaisseau
				Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().smallExplosion, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));
				if(other.GetComponent<ChassisEnemy>().destrOnCol)
				{
					other.gameObject.GetComponent<ChassisEnemy>().Touche(5000, false);//false -> pas de loot si collision chassi a chassi
				}
			}
		}

		if(this.tag == "ChassiAllie")
		{
			if(other.tag == "Enemy")
			{
				GetComponentInParent<ChassisAllie>().Touche(other.GetComponent<ChassisEnemy>().degatCollision, false); //false -> pas de loot lors de collision entre vaisseau
				Instantiate(GameObject.FindWithTag("PlayerScript").GetComponent<loadExpl>().smallExplosion, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), new Quaternion(0, 0, 0, 0));
				if(other.GetComponent<ChassisEnemy>().destrOnCol)
				{
					other.gameObject.GetComponent<ChassisEnemy>().Touche(5000, false);//false -> pas de loot si collision chassi a chassi
				}
			}
		}
	}
}
