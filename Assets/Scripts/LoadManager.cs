using UnityEngine;
using System.Collections;

public class LoadManager : MonoBehaviour {

    private AsyncOperation mAsync;

    public float Progress
    {
        get
        {
            if (mAsync != null)
                return mAsync.progress;
            else
                return 0;
        }
    }

    public void LoadMenuScene()
    {
        StartCoroutine(LoadScene(GameConfig.NAME_MENU_SCENE));
    }

    public void LoadStoryScene()
    {
        StartCoroutine(LoadScene(GameConfig.NAME_STORY_SCENE));
    }

    public void LoadLevelScene(int level)
    {
        Application.LoadLevel(GameConfig.NAME_LOAD_SCENE);
        StartCoroutine(LoadScene(GameConfig.NAME_LEVEL_SCENE + level));
    }

    public void LoadSelectScene()
    {
        StartCoroutine(LoadScene(GameConfig.NAME_SELECT_SCENE));
    }
         
    private IEnumerator LoadScene(string scene)
    {
        mAsync = Application.LoadLevelAsync(scene);
        DontDestroyOnLoad(gameObject);

        yield return mAsync;
        Debug.Log("Load Complete: " + scene);
    }
}
