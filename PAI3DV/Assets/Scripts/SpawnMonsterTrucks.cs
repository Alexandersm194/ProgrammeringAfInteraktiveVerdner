using System;
using System.Collections;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SpawnMonsterTrucks : MonoBehaviour
{
   [SerializeField] private int nrOfTrucks;
   [SerializeField] private float spawnRate;
   [SerializeField] private GameObject[] truckPrefab;

   public void Spawn()
   {
      StartCoroutine(SpawnTruck());
   }

   IEnumerator SpawnTruck()
   {
      for (int i = 0; i < nrOfTrucks; i++)
      {
         int enemyType = UnityEngine.Random.Range(0, truckPrefab.Length);
         Instantiate(truckPrefab[enemyType], transform.position, Quaternion.identity);
         yield return new WaitForSeconds(spawnRate);
      }
      StopCoroutine(SpawnTruck());
   }
}
