﻿using UnityEngine;
using System.Collections;

public class HealthBarControllerScript : MonoBehaviour 
{
	public Vector3 vPlayerPositionOffset = new Vector3();
	public GameObject goMyPlayer;
	public GameObject goHealthBar;
	public Vector4 healthColor;


	Vector3 vOriginalScale;

	PlayerScriptDeven scriptPSD;

	// Use this for initialization
	void Start () 
	{
		vOriginalScale = transform.localScale;
		scriptPSD = goMyPlayer.GetComponent<PlayerScriptDeven>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = goMyPlayer.transform.position + vPlayerPositionOffset;
		transform.localScale =	new Vector3(vOriginalScale.x * scriptPSD.fHealth/100.0f, vOriginalScale.y, vOriginalScale.z);


		if(scriptPSD.fHealth >= 50){
			Color  c = new Color((1f/50f)*(100f-scriptPSD.fHealth), 1,0);
			goHealthBar.renderer.materials[0].color = c;
			healthColor = new Vector4(c.r,c.g,c.b,c.a);
		}
		else
		{
			Color  c = new Color(1, (1f/50f)*(scriptPSD.fHealth),0);
			goHealthBar.renderer.materials[0].color = c;
			healthColor = new Vector4(c.r,c.g,c.b,c.a);
		}
			

		//print(goHealthBar.renderer.material.color);
	}
}
