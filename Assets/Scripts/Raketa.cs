using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raketa : MonoBehaviour
{

    // Dodano u git
    
    private int targetEnemyIndex;

    private const float SPEED_MULTYPLY = 3f;

    public int TargetEnemyIndex
    {
        set
        {
            targetEnemyIndex = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        targetEnemyIndex = 0;
        transform.position = TowerManager.Instance.TowerList[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = GameManager.Instance.EnemyList[0].transform.position - transform.position;
        var projectileAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!GameManager.Instance.EnemyList[0].IsDead)
        {
            transform.rotation = Quaternion.AngleAxis(projectileAngle, Vector3.forward);
            transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.EnemyList[0].transform.position, Time.deltaTime * SPEED_MULTYPLY);
        }

    }

    private void OnTriggerEnter2D(Collider2D triggeredCollider)
    {
        if (triggeredCollider.tag == "Enemy")
        {
            GameManager.Instance.EnemyList[0].Health -= 5;
            if (GameManager.Instance.EnemyList[0].Health <= 0)
            {
                GameManager.Instance.EnemyList[0].IsDead = true;
            }

        }
    }


}
