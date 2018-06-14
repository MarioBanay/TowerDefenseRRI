using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    private static T instance;

    public static T Instance
    {
        get
        {
            // Provjerava postoji li već instanca tipa T
            if (instance == null)
            {
                // Ako ne postoji kreiraj je
                instance = FindObjectOfType<T>();
            }
            // ako postoji instanca ali nije posljednje kreirani objekt tipa T
            else if (instance != FindObjectOfType<T>())
            {
                // Uništi posljednje kreirani objekt tipa T
                Destroy(FindObjectOfType<T>());
            }

            return instance;
        }
    }
}
