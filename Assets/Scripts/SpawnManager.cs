using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _Enemy;
    [SerializeField] GameObject _AstroidSmall;
    [SerializeField] GameObject _EnemyContainer;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] float _spawnTime = 2;

    int _ScoreToCount;


    private bool _stopSpawning = false;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("SpawnManager player script is null");
        }
    }

    public void SpawnRoutine()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpwanPowerUpRoutine());
        StartCoroutine(SpawnSmallAstroid());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            float RandomX = Random.Range(-8f, 8f);
            Vector3 spawnPoint = new Vector3(RandomX, 11.5f, 0);
            GameObject newEnemy = Instantiate(_Enemy, spawnPoint, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;

            yield return new WaitForSeconds(_spawnTime);
        }
    }

    IEnumerator SpwanPowerUpRoutine()
    {
        yield return new WaitForSeconds(10f);

        while (_stopSpawning == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Vector3 vector3 = new Vector3(randomPowerup, 11.5f, 0);
            Instantiate(powerUps[randomPowerup],vector3, Quaternion.identity);

            float RandomSeconds = Random.Range(5, 15);
            yield return new WaitForSeconds(RandomSeconds);
        }
    }

    IEnumerator SpawnSmallAstroid()
    {
        yield return new WaitForSeconds(20f);
        
        {
           while (_stopSpawning == false)
            {
                float randomx = Random.Range(-8f, 8f);
                Vector3 SpawnPoint = new Vector3(randomx, 11.5f, 0);
                Instantiate(_AstroidSmall, SpawnPoint, Quaternion.identity);

                float RandomSeconds = Random.Range(5f, 20f);
                yield return new WaitForSeconds(RandomSeconds);
            }
           
                Debug.Log("astroid to go");
           
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

   
}
