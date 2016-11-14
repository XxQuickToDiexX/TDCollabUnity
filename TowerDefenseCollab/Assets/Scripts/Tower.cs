using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour 
{
	Transform tower;

	public GameObject bulletPrefab;

	public float fireCooldown = 1.0f;
	public float cooldownRemaining = 0;
	float range = 10f;

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
		}

		if (nearestEnemy == null) 
		{
			Debug.Log ("No Enemies");
			return;
		}
			
		Vector3 direction = nearestEnemy.transform.position - this.transform.position;

		//Rotate to look at the enemy
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		tower.rotation = Quaternion.Euler (0, lookRotation.eulerAngles.y, 0);

		cooldownRemaining -= Time.deltaTime;

		if (cooldownRemaining <= 0 && direction.magnitude <= range) 
		{
			cooldownRemaining = fireCooldown;
			Shoot(nearestEnemy);
		}

	}

	void Shoot(Enemy e)
	{
		GameObject bullet = (GameObject)Instantiate (bulletPrefab, this.transform.position, this.transform.rotation);

		Bullet b = bullet.GetComponent <Bullet> ();
		b.target = e.transform;
	}
}
