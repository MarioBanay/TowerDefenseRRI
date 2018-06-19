using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private List<Projectile> projectileList;
    [SerializeField]
    private Enemy nearestEnemy;
    Projectile newProjectile;
    Transform targetPosition;



    // Use this for initialization
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (newProjectile != null)
        {
            var dir = targetPosition.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            newProjectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            newProjectile.transform.position = Vector2.MoveTowards(newProjectile.transform.position, targetPosition.position, Time.deltaTime * 20);
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2f);
        Enemy nearestEnemy = getNearestEnemy();
        if (nearestEnemy != null)
        {
            newProjectile = Instantiate(projectileList[0]) as Projectile;
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


}
