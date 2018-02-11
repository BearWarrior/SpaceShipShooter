using UnityEngine;
using System.Collections;

public class DrawCircle : MonoBehaviour {   
	float theta_scale = 0.01f;        //Set lower to add more points
	int size; //Total number of points in circle
	//public float radius = 3f;
	LineRenderer lineRenderer;

	public GameObject planet;

	float radius;
	
	void Awake () 
	{
		Vector3 planetPos = new Vector3 (planet.transform.position.x, planet.transform.position.y*2, planet.transform.position.z);
		radius = Mathf.Abs (Vector2.Distance (planetPos, transform.position));

		float sizeValue = (2.0f * Mathf.PI) / theta_scale; 
		size = (int)sizeValue;
		size++;
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetWidth(0.01f, 0.01f); //thickness of line
		lineRenderer.SetVertexCount(size);      
	}
	
	void Update () 
	{   
		Vector3 pos;
		float theta = 0f;
		for(int i = 0; i < size; i++)
		{        
			theta += (2.0f * Mathf.PI * theta_scale);         
			float x = radius * Mathf.Cos(theta);
			float y = radius * Mathf.Sin(theta) * 0.5f;          
			x += gameObject.transform.position.x;
			y += gameObject.transform.position.y;
			pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);
		}
	}
}