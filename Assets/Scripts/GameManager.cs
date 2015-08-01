using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public LoadManager loadManager;

    public Player player = new Player();

    public float toSecond = 60 * 60;

    public bool allowMusic = true;

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

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            GameManager.instance.loadManager.LoadSelectScene();
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
