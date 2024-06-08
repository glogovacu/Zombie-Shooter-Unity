using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private AnimationCurve _spawnRateCurve;
    [SerializeField] private float _spawnRadius = 1.0f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private float _totalTime = 10f; // Total time after which the spawn rate reaches the minimum interval
    [SerializeField] private int _initialPoolSize = 20; // Initial size of the object pool
    [SerializeField] private int _maxSpawnCount = 100; // Maximum number of spawns

    private int _currentSpawnCount = 0;
    private float _elapsedTime = 0f;
    private List<GameObject> _spawnPoints = new List<GameObject>();
    private Queue<GameObject> _objectPool = new Queue<GameObject>();

    void Start() {
        InitializeSpawnPoints();
        InitializeObjectPool();
        StartCoroutine(SpawnRoutine());
    }

    void InitializeSpawnPoints() {
        GameObject[] points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        Debug.Log("Points" + points.Length);
        foreach (GameObject point in points) {
            _spawnPoints.Add(point);
        }
    }

    void InitializeObjectPool() {
        for (int i = 0; i < _initialPoolSize; i++) {
            GameObject obj = Instantiate(_zombiePrefab);
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
    }

    IEnumerator SpawnRoutine() {
        while (true) {
            float interval = _spawnRateCurve.Evaluate(_elapsedTime / _totalTime);
            yield return new WaitForSeconds(interval);

            Spawn();
            _elapsedTime += interval;
            
        }
    }

    void Spawn() {
        if (_spawnPoints.Count == 0 || _currentSpawnCount >= _maxSpawnCount) return;

        GameObject spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        GameObject obj = GetPooledObject();
        if (obj != null) {
            obj.transform.position = spawnPoint.transform.position;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(true);
        }
    }

    GameObject GetPooledObject() {
        if (_objectPool.Count > 0) {
            return _objectPool.Dequeue();
        } else {
            GameObject obj = Instantiate(_zombiePrefab);
            obj.SetActive(false);
            return obj;
        }
    }
}
