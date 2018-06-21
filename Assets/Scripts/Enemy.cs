using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // property vezani za path
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform[] checkpoints;
    [SerializeField]
    private Transform finish;
    private int targetPosition;
    private Transform enemyPosition;
    private Collider2D enemyCollider;

    private bool isDead;

    [SerializeField]
    private int health;

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = true;
        }
    }
    
    // inicijalizacija
    void Start()
    {
        health = 15;
        isDead = false;
        enemyCollider = GetComponent<Collider2D>();
        enemyCollider.enabled = true;
        enemyPosition = GetComponent<Transform>();
        enemyPosition.position = spawnPoint.transform.position;
        targetPosition = 0;
        GameManager.Instance.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Mrtav? " + this.isDead + " Health: " + this.health);
        if (!isDead)
        {
            if (targetPosition < checkpoints.Length)
                {
        // kretanje enemy-a prema finish-u
                enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, checkpoints[targetPosition].position, Time.deltaTime);
                }
            else 
            {
                enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, finish.position, Time.deltaTime);
            }
        }

    }

    // funkcija se zove kad se aktivira collider
    private void OnTriggerEnter2D(Collider2D triggeredCollider)
    {
        if (triggeredCollider.tag == "Checkpoint")
        {
            targetPosition += 1;
        }
        else if (triggeredCollider.tag == "Finish")
        {
            GameManager.Instance.UnregisterEnemy(this);
        }
        else if (triggeredCollider.tag == "Projectile")
        {
            health -= 5;    
            triggeredCollider.tag = "Untagged";  
        }
        if (health <= 0)
        {
            GameManager.Instance.KilledEnemies += 1;
            isDead = true;
            enemyCollider.enabled = false;         
        }      
    }
}
