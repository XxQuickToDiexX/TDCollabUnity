using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	GameObject pathing;

	Transform targetPathNode;
	int pathIndex = 0;

	float speed = 5f;

	public float health = 1f;

	void Start () 
	{
		pathing = GameObject.Find("Path");
	}

	void GetNextPathNode()
	{
		if(pathIndex < pathing.transform.childCount)
		{
			targetPathNode = pathing.transform.GetChild(pathIndex);
			pathIndex++;
		}
		else 
		{
			targetPathNode = null;
			ReachedLastNode();
		}
	}

	void Update () {
		if(targetPathNode == null)
		{
			GetNextPathNode();
			if(targetPathNode == null) 
			{
				ReachedLastNode();
				return;
			}
		}

		Vector3 direction = targetPathNode.position - this.transform.localPosition;

		float distance = speed * Time.deltaTime;

		if(direction.magnitude <= distance) {
			// We reached the node
			targetPathNode = null;
		}
		else {
			// TODO: Consider ways to smooth this motion.

			// Move towards node
			transform.Translate( direction.normalized * distance, Space.World );

			//Rotate to look at Node
			Quaternion targetRotation = Quaternion.LookRotation( direction );

			//Smooth the rotation
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime*5);
		}

	}

	void ReachedLastNode() {
		Destroy(gameObject);
	}
}
