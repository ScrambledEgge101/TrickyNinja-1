﻿//Monk script
//Last edited by Jason Ege on 02/20/2014 at 9:23am
//Handles the monk enemy. The guy that runs up to you and then jumps before landing on you.

using UnityEngine;
using System.Collections;

public class MonkScript : EnemyScript {

	public float fInitSpeed; //The set "normal" speed of the monk.
	public float fJumpHeight; //The maximum jump height that the monk will reach.
	public GameObject gPlayer; //The player game object.

	float fSpeed; //The current speed of the monk.
	float fVerticalSpeed; //The vertical speed of the monk when he begins his jump.
	bool bInAir; //Is the monk in the air?

	// Use this for initialization
	void Start () {
		gPlayer = GameObject.FindGameObjectWithTag ("Player"); //The definition of the player game object is any object tagged as a player.
		fSpeed = fInitSpeed; //Set the current speed of the monk to the "normal", initial speed.
		fVerticalSpeed = fJumpHeight; //Set the vertical speed of the monk to its jump height.
		bInAir = false; //The monk does not start in the air.
	}
	
	//Derived from the "Move" function of "EntityScript". It tells the enemies how to move.
	public override void Move()
	{
		MonkChasePlayer (); //Tell the monk to chase the player using the monk's own ChasePlayer method.
		MonkGravity(); //The monk has its own special gravity command.
	}
	
	//Derived from the "Die" function of "EntityScript".
	public override void Die()
	{
		Destroy (gameObject); //Destroy the current monk.
	}
	
	//Derived from the "Hurt" function of "EntityScript". It handles damage that an entity takes.
	//"Hurt" is indirectly called by a broadcasted SendMessage command.
	//It takes a specified amount of damage that the enemy should take (typically 1).
	public override void Hurt(int aiDamage)
	{
		fHealth -= aiDamage; //Damage the enemy by the amount specified.
		if (fHealth <= 0) //If health is equal to or less than 0...
		{
			Die (); //Tell the monk to go die in a hole.
		}
	}
	
	//This keeps the monk falling back down to the ground after he jumps to attack the player.
	void MonkGravity()
	{
		transform.Translate (0.0f,fVerticalSpeed*Time.deltaTime,0.0f); //Transalate the monk so that he actually moves.
		fVerticalSpeed -=  20.0f*Time.deltaTime; //Subtract from the monk's vertical speed so that he eventually comes back down after he goes up.
	}
	
	//This pre-defined method handles what happens when the monk stays collided with something.
	void OnCollisionStay(Collision c)
	{
		if (c.gameObject.tag == "Ground") //If it has collided with an object tagged as ground...
		{
			bInAir = false; //The monk is not longer set as in the air.
		}
	}
	
	//The special command that tells the monk how to chase the player.
	void MonkChasePlayer()
	{	
		if (CollidingWithPlayer (gPlayer) && bInAir == false) //If the monk is almost colliding with player (defined in "EnemyScript")...
		{
			fSpeed = 0.0f; //Set the horizontal speed of the monk to 0 so it no longer will chase the player back and forth while in the air.
			fVerticalSpeed = fJumpHeight; //Set its vertical speed to the specified jump amount so that it shoots up in the air.
			bInAir = true; //Tell the monk that he is in the air.
		}
		else if (!CollidingWithPlayer (gPlayer) && bInAir == false) //If the monk is not colliding with the player and he is in the air.
		{
			fSpeed = fInitSpeed; //The horizontal speed is the "normal" initial speed.
			ChasePlayer(gPlayer, fSpeed*Time.deltaTime); //Tell the monk to chase the player like a normal enemy would (defined in "EnemyScript").
		}
		if (bInAir == false) //If the monk is not in the air (and it doesn't matter whether he is colliding with the player or not).
		{
			transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z); //Tell the monk to stay at vertical position 1.5.
			fVerticalSpeed = 0; //Tell the monk to not jump.
		}
	}
	
	// Update is called once per frame
	void Update () {
		Move (); //The monk's move method.
	}
}
