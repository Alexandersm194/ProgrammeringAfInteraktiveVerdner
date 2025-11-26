using System;
using System.Collections;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SpawnBikers : MonoBehaviour
{
    [SerializeField] private int nrOfBiker1st;
    [SerializeField] private int nrOfBiker1st2;
    [SerializeField] private Transform SecondSpawn1st;
    [SerializeField] private int nrOfBiker2nd;
    [SerializeField] private int nrOfBiker2nd2;
    [SerializeField] private Transform SecondSpawn2nd;
    [SerializeField] private Transform[] waypoints1st;
    [SerializeField] private Transform[] waypoints2nd;
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject bikerPrefab;
    [SerializeField] private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public void Spawn1stPhase()
    {
        StartCoroutine(SpawnBikes(nrOfBiker1st, waypoints1st, true, transform));
        StartCoroutine(SpawnBikes(nrOfBiker1st2, waypoints1st, true, SecondSpawn1st));
    }
    
    public void Spawn2ndPhase()
    {
        StartCoroutine(SpawnBikes(nrOfBiker2nd, waypoints2nd, true, transform));
    }

    public void Spawn3rdPhase()
    {
        StartCoroutine(SpawnBikes(nrOfBiker2nd2, waypoints2nd, true, SecondSpawn2nd));
    }
    
    

    IEnumerator SpawnBikes(int nrOfBikers, Transform[] waypoints, bool isChasing, Transform spawnPoint)
    {
        for (int i = 0; i < nrOfBikers; i++)
        {
            GameObject bike = Instantiate(bikerPrefab, spawnPoint.position, Quaternion.identity);
            EnemyScript bikeScript = bike.GetComponent<EnemyScript>();
            if(!isChasing) bikeScript.SetTarget(waypoints[i]);
            else bikeScript.SetTarget(player.transform);
            yield return new WaitForSeconds(spawnRate);
        }
        StopCoroutine(SpawnBikes(nrOfBikers, waypoints, isChasing, spawnPoint));
    }
}