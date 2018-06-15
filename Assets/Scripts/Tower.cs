using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private Raketa raketa;

    public Raketa Raket
    {
        get
        {
            return raketa;
        }
    }

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
       for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(2f);
            Raketa novaRaketa = Instantiate(raketa) as Raketa;
        }

    }

}