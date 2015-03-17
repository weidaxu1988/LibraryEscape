using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public LoadManager loadManager;

    public Player player = new Player();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        loadManager = GetComponent<LoadManager>();
    }

    void OnLevelWasLoaded(int index)
    {
        
    }

}
