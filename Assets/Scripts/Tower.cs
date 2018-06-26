using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private int towerPrice;
    private Projectile newProjectile;
    [SerializeField]
    private Projectile projectile;
    private Enemy nearestEnemy;
    private Transform targetPosition;

    private bool enemyInRange;

    public int TowerPrice{
        get{
            return towerPrice;
        }
    }

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
            Debug.Log("Before");
            newProjectile = Instantiate(projectile) as Projectile;
            Debug.Log("After");
            newProjectile.transform.position = transform.position;
            targetPosition = nearestEnemy.transform;
            if (newProjectile.ProjectileType == ProType.arrow)
            {
                GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Arrow);
            } else if (newProjectile.ProjectileType == ProType.fireball)
            {
                GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Fireball);

            } else if (newProjectile.ProjectileType == ProType.rock)
            {
                GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Rock);              
            }
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
