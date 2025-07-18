using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class ShootableBox : MonoBehaviour 
{

	//The box's current health point total
	public int currentHealth = 3;

	public virtual void Damage(int damageAmount)  // make public virtual void
	{
		//subtract damage amount when Damage function is called
		currentHealth -= damageAmount;

		//Check if health has fallen below zero
		if (currentHealth <= 0) 
		{
			//if health has fallen below zero, deactivate it 
			Destroy(gameObject);
		}
	}
}
