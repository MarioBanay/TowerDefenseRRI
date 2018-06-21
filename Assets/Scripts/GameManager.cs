using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private int level;

    [SerializeField]
    private int escapedEnemies;
    [SerializeField]
    private int killedEnemies;

    public int KilledEnemies{
        get {
            return killedEnemies;
        }
        set {
            killedEnemies = value;
        }
    }
    private int enemiesPerWave = 3;

    private List<Enemy> enemyList = new List<Enemy>();

    public List<Enemy> EnemyList
    {
        get
        {
            return enemyList;
        }
    }

    [SerializeField]
    private Enemy[] enemyType;

    private const float TIME_BETWEEN_SPAWN = 4f;

    // Use this for initialization
    void Start()
    {
        killedEnemies = 0;
        escapedEnemies = 0;
        level = 1;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Instanciranje enemy-a svakih TIME_BETWEEN_SPAWN sekundi
    IEnumerator Spawn()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Enemy newEnemy = Instantiate(enemyType[0]) as Enemy;
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
        escapedEnemies += 1;
    }
    private void DestroyAllEnemies()
    {
        foreach (var enemy in enemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }
}
