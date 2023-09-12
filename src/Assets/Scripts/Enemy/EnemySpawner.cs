using System;
using System.Collections;
using System.Collections.Generic;
using GameMain;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private float spawnInterval;
        [SerializeField] [Range(0f, 100f)] private float spawnPercent;
        private List<Enemy> enemies;

        private void Start()
        {
            enemies = new List<Enemy>();

            BeatObserver.Instance.OnBeat += () =>
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.Move();
                }

                SpawnEnemy();
            };
        }

        private void SpawnEnemy()
        {
            if (Random.Range(0, 100f) > spawnPercent)
                return;

            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            Vector3 spawnPoint = transform.position + new Vector3(0f, spawnInterval * Random.Range(0, prefabs.Length + 1), 0f);
            GameObject enemyObj = Instantiate(prefab, spawnPoint, Quaternion.identity);

            enemies.Add(enemyObj.GetComponent<Enemy>());
        }
    }
}