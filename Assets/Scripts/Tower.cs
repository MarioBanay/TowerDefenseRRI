using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    private Enemy nearestEnemy;

    [SerializeField]
    private GameObject newProjectile;
    private Transform targetPosition;

    private bool enemyInRange;

    // Use this for initialization
    void Start()
    {
        enemyInRange = false;
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (newProjectile != null && enemyInRange && targetPosition != null ) 
        {
            var dir = targetPosition.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            newProjectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            newProjectile.transform.position = Vector2.MoveTowards(newProjectile.transform.position, targetPosition.position, Time.deltaTime * 15);
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1.5f);
        Enemy nearestEnemy = getNearestEnemy();
        if (nearestEnemy != null && enemyInRange)
        {
            newProjectile = Instantiate(projectile) as GameObject;
            newProjectile.transform.position = transform.position;
            targetPosition = nearestEnemy.transform;
        }
            StartCoroutine(Shoot());
    }

    public Enemy getNearestEnemy()
    {
        nearestEnemy = null;
        var smallestDistance = float.PositiveInfinity;
        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            if (!enemy.IsDead && (Vector2.Distance(enemy.transform.position, transform.position) <= smallestDistance))
            {
                smallestDistance = Vector2.Distance(enemy.transform.position, transform.position);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    private void OnTriggerEnter2D(Collider2D triggeredCollider)
    {
        if (triggeredCollider.tag == "Enemy")
        {
            enemyInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D triggeredCollider)
    {
        if (triggeredCollider.tag == "Enemy")
        {
            enemyInRange = false;
        }
    }
    

}
