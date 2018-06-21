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
    [SerializeField]
    private Animator anim;
    private int targetPosition;
    private Transform enemyPosition;
    private Collider2D enemyCollider;
    [SerializeField]
    private int rewardAmount;

    [SerializeField]
    private int health;
    private bool isDead;

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
        isDead = false;
        enemyCollider = GetComponent<Collider2D>();
        enemyCollider.enabled = true;
        anim = GetComponent<Animator>();
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
            GameManager.Instance.RoundEscaped += 1;
            //GameManager.Instance.TotalEscaped += 1;
            GameManager.Instance.UnregisterEnemy(this);
            GameManager.Instance.isWaveOver();
        }
        else if (triggeredCollider.tag == "Projectile")
        {
            health -= triggeredCollider.gameObject.GetComponent<Projectile>().Strenght;  
            triggeredCollider.tag = "Untagged";
            anim.Play("Hurt");
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Hit);
        }
        if (health <= 0)
        {
            anim.SetTrigger("didDie");
            isDead = true;
            GameManager.Instance.TotalKilled += 1;
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Death);
            enemyCollider.enabled = false;    
            GameManager.Instance.addMoney(rewardAmount); 
            GameManager.Instance.isWaveOver();    
        }      
    }
}
