
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    public float xLimit;
    public float maxTimeLife = 3f;
    private float spawnNext=0;
    // Update is called once per frame
    void Update()
    {
      if(Time.time > spawnNext) {
        spawnNext = Time.time + 60 / spawnRatePerMinute;
        spawnRatePerMinute += spawnRateIncrement;
        float rand = Random.Range(-xLimit, xLimit);
        Vector2 spawnPosition = new Vector2(rand,8f);
        //GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        GameObject meteor = MeteorPool.Instance.RequestMeteor();
        meteor.transform.position = spawnPosition;
      }
    }
}
