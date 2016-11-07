using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour 
{
	Transform tower;

	void Start () 
	{
		tower = this.transform; // Get the turret
	}

	void Update () 
	{
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy> (); // Array of all enemies

		Enemy nearestEnemy = null;

		float distance = Mathf.Infinity;

		foreach (Enemy e in enemies)
		{
			float dist = Vector3.Distance (this.transform.position, e.transform.position);

			if (nearestEnemy == null || dist < distance) {
				nearestEnemy = e;
				distance = dist;
			} 
			else if (nearestEnemy == null) 
			{
				Debug.Log ("No Enemies");
				return;
			}
		}

		if (nearestEnemy != null)
		{
			LookToEnemy(nearestEnemy);
		}

	}

	void LookToEnemy(Enemy e)
	{
		//Make vector from enemy to tower
		Vector3 direction = e.transform.position - this.transform.position;

		//Rotate to look at the enemy
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		tower.rotation = Quaternion.Euler (0, lookRotation.eulerAngles.y, 0);
	}
}
