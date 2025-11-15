using System;
using System.Collections;
using UnityEngine;

public class SpawnMonsterTrucks : MonoBehaviour
{
   [SerializeField] private int nrOfTrucks;
   [SerializeField] private float spawnRate;
   [SerializeField] private GameObject truckPrefab;

   public void Spawn()
   {
      StartCoroutine(SpawnTruck());
   }

   IEnumerator SpawnTruck()
   {
      for (int i = 0; i < nrOfTrucks; i++)
      {
         Instantiate(truckPrefab, transform.position, Quaternion.identity);
         yield return new WaitForSeconds(spawnRate);
      }
      StopCoroutine(SpawnTruck());
   }
}
