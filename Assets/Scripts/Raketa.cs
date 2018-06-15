using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raketa : MonoBehaviour
{

    private const float SPEED_MULTYPLY = 3f;

    // Use this for initialization
    void Start()
    {
        transform.position = TowerManager.Instance.TowerList[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Raketa update");
        var direction = closestEnemy().transform.position - transform.position;
        var projectileAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!closestEnemy().IsDead)
        {
            transform.rotation = Quaternion.AngleAxis(projectileAngle, Vector3.forward);
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy().transform.position, Time.deltaTime * SPEED_MULTYPLY);
        } 

    }

    private Enemy closestEnemy()
    {
        Enemy nearestEnemy = null;
        var minDistance = float.PositiveInfinity;
        
        foreach (var enemy in GameManager.Instance.EnemyList)
        {
            if (Mathf.Abs(Vector2.Distance(transform.localPosition, enemy.transform.localPosition)) < minDistance)
            {
                minDistance = Mathf.Abs(Vector2.Distance(transform.localPosition, enemy.transform.localPosition));
                nearestEnemy = enemy;
            }            
        }
        
        return nearestEnemy;
    }

    public void destroyRaketuGameObject(){
        Destroy(this.gameObject);
    }

        private void OnTriggerEnter2D(Collider2D triggeredCollider)
    {
        if (triggeredCollider.tag == "Enemy")
        {
            destroyRaketuGameObject();
        }   
    }
}
