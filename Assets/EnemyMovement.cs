using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	Transform suitcase;
	NavMeshAgent nav;

	Transform enemy;
	GameObject[] waypoints;
	int waypoint_index;

	bool suitcase_found = false;

	bool suitcaseSighted() {
		bool sighted = false;
		Vector3 targetDir = suitcase.position - enemy.transform.position;
        Vector3 forward = enemy.transform.forward;
        float angle = Vector3.Angle(targetDir, forward);
        if (angle < 35.0F) {
			sighted = true;
            print("sighted!!!!");
		}
		else {
			sighted = false;
            print("not in cone of sight");
		}

		return sighted;
	}

	void patrol() {
		GameObject wp = waypoints[waypoint_index];
		Vector3 cur_pos = wp.transform.position;

		if (Vector3.Distance(enemy.transform.position, cur_pos) < 5.0f ) {
			waypoint_index += 1;
			if (waypoint_index >= waypoints.Length) {
				waypoint_index = 0;
			}
		}
		else {
			nav.SetDestination(cur_pos);
		}
	}

	void Awake() {
		suitcase = GameObject.FindGameObjectWithTag("Suitcase").transform;
		nav = GetComponent<NavMeshAgent>();
		enemy = nav.gameObject.transform;
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
		waypoint_index = 0;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		bool sighted = suitcaseSighted();
		if (!sighted && !suitcase_found) {
			patrol();
		}
		else {
			suitcase_found = true;
			nav.SetDestination(suitcase.transform.position);
		}


	}
}
