using System;
using System.Collections;
using System.Collections.Generic;
using GameMain;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    [Serializable]
    public class EnemyData
    {
        public GameObject prefab;
        public float percent;
    }

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyData[] prefabs;
        [SerializeField] private float spawnInterval;
        [SerializeField] private int beatInterval;
        [SerializeField] private int lineCount;

        private List<Enemy> enemies;
        [SerializeField] private int[] coolCounter;

        private int currentBeat;

        private void Start()
        {
            enemies = new List<Enemy>(prefabs.Length);
            coolCounter = new int[lineCount];

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
            SubtractCoolCounter();
            currentBeat++;

            if (currentBeat < beatInterval)
                return;

            (GameObject prefab, int index) enemyData = GetEnemy();
            Vector3 spawnPoint = transform.position + new Vector3(0f, spawnInterval * GetSpawnIndex(), 0f);
            spawnPoint += new Vector3(0f, 0f, -5f);
            GameObject enemyObj = Instantiate(enemyData.prefab, spawnPoint, Quaternion.identity);
            Enemy enemy = enemyObj.GetComponent<Enemy>();

            enemies.Add(enemy);
            coolCounter[enemyData.index] = enemy.Length;

            enemy.OnDestroyed += () => enemies.Remove(enemy);
            currentBeat = 0;
        }

        private void SubtractCoolCounter()
        {
            for (int i = 0; i < coolCounter.Length; i++)
            {
                coolCounter[i] = coolCounter[i] > 0 ? coolCounter[i] - 1 : 0;
            }
        }

        private int GetSpawnIndex()
        {
            bool found;
            int resultIndex = 0;

            do
            {
                int indexData = Random.Range(0, lineCount);

                found = coolCounter[indexData] == 0;

                if (found)
                {
                    resultIndex = indexData;
                }
            }
            while (!found);

            return resultIndex;
        }

        private (GameObject prefab, int index) GetEnemy()
        {
            float percent = Random.Range(0f, 100f);
            float basePercent = 0f;

            for (int i = 0; i < prefabs.Length; i++)
            {
                EnemyData enemy = prefabs[i];
                if (percent <= basePercent + enemy.percent)
                {
                    return (enemy.prefab, i);
                }

                basePercent += enemy.percent;
            }

            return (null, -1);
        }
    }
}