using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private int totalEnemies = 6;
    private int enemiesPerSpawn = 3;

    private List<Enemy> enemyList = new List<Enemy>();

    public List<Enemy> EnemyList
    {
        get
        {
            return enemyList;
        }
    }

    // [SerializeField]
    //private GameObject spawnPoint;

    [SerializeField]
    private Enemy[] enemies;

    private Enemy enemy;

    private const float TIME_BETWEEN_SPAWN = 6f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Instanciranje enemy-a svakih TIME_BETWEEN_SPAWN sekundi
    IEnumerator Spawn()
    {
        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            Enemy newEnemy = Instantiate(enemies[0]) as Enemy;
            yield return new WaitForSeconds(TIME_BETWEEN_SPAWN);
        }
    }

    // dodaje enemy u listu
    public void RegisterEnemy (Enemy enemy)
    {
        enemyList.Add(enemy);
    }

    // briše enemy iz liste
    public void UnregisterEnemy (Enemy enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
