using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public LoadManager loadManager;

    public Player player = new Player();

    private int currentLevel = 1;

    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
    }

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

    public bool allowLoad(int level)
    {
        if (level == currentLevel)
            return true;
        else
            return false;
    }

    public void GameClear()
    {
        currentLevel++;
    }

}
