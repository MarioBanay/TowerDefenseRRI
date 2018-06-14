using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private Raketa raketa;


    // Use this for initialization
    void Start()
    {

        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < GameManager.Instance.EnemyList.Count; i++)
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("Broj u enemylisti (for): " + GameManager.Instance.EnemyList.Count);
            Debug.Log("Distanca: " + Vector2.Distance(GameManager.Instance.EnemyList[i].transform.position, transform.position));
            if (Vector2.Distance(GameManager.Instance.EnemyList[i].transform.position, transform.position) < 5)
            {
                Raketa novaRaketa = Instantiate(raketa) as Raketa;
                novaRaketa.TargetEnemyIndex = i;
            }
        }


    }
}
