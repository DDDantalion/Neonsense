﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatrolFollow : MonoBehaviour
{

    GameObject[] waypoint;
    int rand;
    GameObject waypointSelected;
    UnityEngine.AI.NavMeshAgent agent;
    public Transform target;
    [SerializeField] private RangedWeapon weapon;

    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Seed();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Seed()
    {
        waypoint = GameObject.FindGameObjectsWithTag("waypoint");
        rand = Random.Range(0, waypoint.Length);
        waypointSelected = waypoint[rand];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.transform.position, target.transform.position) < 5)
        {
            agent.SetDestination(target.transform.position);
            weapon.StartTryingToFire();
            Seed();
        }

        else
        {
            if (Vector3.Distance(agent.transform.position, waypointSelected.transform.position) >= 2)
            {
                // pursue 'waypointSelected'
                agent.SetDestination(waypointSelected.transform.position);
            }

            else
            {
                // if the distance is too small, find a new 'waypointSelected'
                Seed();
            }
        }
    }
}
