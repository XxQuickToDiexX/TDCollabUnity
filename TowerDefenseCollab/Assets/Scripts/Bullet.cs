using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{

	public float speed = 10f;
	public float damage = 0.5f;
	public Transform target;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 direction = target.position - this.transform.localPosition;

		float distance = speed * Time.deltaTime;

		if (direction.magnitude <= distance) {
			target = null;
			BulletHit ();

		} 
		else 
		{
			transform.Translate (direction.normalized * distance, Space.World);
			Quaternion targetRotation = Quaternion.LookRotation (direction);
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime * 5f);
		}
	}

	void BulletHit()
	{
		gameObject.GetComponent<Enemy>().TakeDamage(damage);
		Destroy (gameObject);
	}
}