using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour {

    [SerializeField]
    private GameObject tower;
	[SerializeField]
	private int towerPrice;

	public int TowerPrice {
		get {
			return towerPrice;
		}
	}
}
