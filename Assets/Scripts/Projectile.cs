using UnityEngine;
using System.Collections;

public enum ProType
{
    arrow, rock, fireball
}

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private int strenght;
    [SerializeField]
    private ProType type;

    public int Strenght
    {
        get
        {
            return strenght;
        }
    }
    public ProType Type
    {
        get
        {
            return type;
        }
    }

}
