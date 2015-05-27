using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance = null;

    public bool isGamePaused = false;

    public bool puzzleCleared = false;

    public Timer timer;

    public GameObject extraButtons;

    public UILabel purpleLabel;
    public UILabel greenLabel;
    public UILabel orangeLabel;

    public GameObject beginObject;
    public NoteControl noteControl;
    public HelpNoteControl helpControl;
    public QuestionControl questionControl;

    public GameObject exitPanel;
    public GameObject player;

    public ExitObject exitObject;

    private PuzzleObject[] totalPuzzle;

    private List<PuzzleObject> purplePuzzleList = new List<PuzzleObject>();
    private List<PuzzleObject> greenPuzzleList = new List<PuzzleObject>();
    private List<PuzzleObject> orangePuzzleList = new List<PuzzleObject>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isGamePaused = true;
        SetupTotalPuzzles();
        
        if (beginObject == null)
        {
            InitHelp();
        }
    }

    public void OnPauseButtonClick()
    {
        Debug.Log("pause game");
    }

    public void OnFunctionButtonClick()
    {
        if (extraButtons.activeSelf)
            extraButtons.SetActive(false);
        else
            extraButtons.SetActive(true);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnHintButtonClick()
    {
        Debug.Log("hint show");
    }

    public void OnReplayButtonClick()
    {
        Debug.Log("reload");
    }

    public void OnSoundButtonClick()
    {
        Debug.Log("sound");
    }

    public void OnMenuButtonClick()
    {
        Debug.Log("menu");
    }

    public void StartGame()
    {
        if (beginObject != null)
        {
            beginObject.SetActive(false);
            InitHelp();
        }
    }

    public void OnPuzzleObjectClick(PuzzleObject obj)
    {
        isGamePaused = true;

        if (player != null)
        {
            player.GetComponent<PlayerControl>().ResetTimeCount();
        }

        if (obj.startType == PuzzleObject.StartType.Purple)
        {
            if (!purplePuzzleList.Contains(obj))
            {
                //TweenScale scale = purpleLabel.GetComponent<TweenScale>();
                //scale.PlayForward();

                Animation anime = purpleLabel.GetComponent<Animation>();
                if (!anime.isPlaying) anime.Play();

                purplePuzzleList.Add(obj);
                purpleLabel.text = purplePuzzleList.Count + "/" + purplePuzzleList.Capacity;
            }
        }
        else if (obj.startType == PuzzleObject.StartType.Green)
        {
            if (!greenPuzzleList.Contains(obj))
            {
                Animation anime = greenLabel.GetComponent<Animation>();
                if (!anime.isPlaying) anime.Play();

                greenPuzzleList.Add(obj);
                greenLabel.text = greenPuzzleList.Count + "/" + greenPuzzleList.Capacity;
            }
        }
        else if (obj.startType == PuzzleObject.StartType.Orange)
        {
            if (!orangePuzzleList.Contains(obj))
            {
                Animation anime = orangeLabel.GetComponent<Animation>();
                if (!anime.isPlaying) anime.Play();

                orangePuzzleList.Add(obj);
                orangeLabel.text = orangePuzzleList.Count + "/" + orangePuzzleList.Capacity;
            }
        }

        if (!noteControl.gameObject.activeSelf)
        {
            noteControl.Puzzle = obj;
            noteControl.gameObject.SetActive(true);
        }
    }

    public void OnHelpFinish()
    {
        isGamePaused = false;

        if (helpControl.gameObject.activeSelf)
            helpControl.gameObject.SetActive(false);
        
        if (!player.activeSelf)
            player.SetActive(true);
    }

    public void OnNoteCloseButtonClick()
    {
        isGamePaused = false;
        if (noteControl.gameObject.activeSelf)
        {
            noteControl.gameObject.SetActive(false);
        }

        StartQuestion();
    }

    public void StartQuestion()
    {
        if (puzzleCleared) return;

        if (totalPuzzle.Length == purplePuzzleList.Count + greenPuzzleList.Count + orangePuzzleList.Count)
        {
            isGamePaused = true;
            if (!questionControl.gameObject.activeSelf)
                questionControl.gameObject.SetActive(true);
        }
    }

    public void QuestionStoped()
    {
        isGamePaused = false; ;
    }

    public void QuestionFinished()
    {
        puzzleCleared = true;
        exitObject.ActivateSelf();

        if (questionControl.gameObject.activeSelf)
            questionControl.gameObject.SetActive(false);
    }

    public void OnExitObjectClick()
    {
        if (!exitPanel.activeSelf)
            exitPanel.SetActive(true);
    }

    public void LevelComplete()
    {
        GameManager.instance.loadManager.LoadSelectScene();
    }

    protected void InitHelp()
    {
        if (player.activeSelf)
            player.SetActive(false);

        if (!helpControl.gameObject.activeSelf)
            helpControl.gameObject.SetActive(true);
    }

    protected void SetupTotalPuzzles()
    {
        totalPuzzle = FindObjectsOfType<PuzzleObject>();
        int purple = 0, green = 0, orange = 0;
        foreach (PuzzleObject obj in totalPuzzle)
        {
            if (obj.startType == PuzzleObject.StartType.Purple)
                purple++;
            else if (obj.startType == PuzzleObject.StartType.Green)
                green++;
            else if (obj.startType == PuzzleObject.StartType.Orange)
                orange++;
        }
        purplePuzzleList.Capacity = purple;
        greenPuzzleList.Capacity = green;
        orangePuzzleList.Capacity = orange;

        purpleLabel.text = purplePuzzleList.Count + "/" + purplePuzzleList.Capacity;
        greenLabel.text = greenPuzzleList.Count + "/" + greenPuzzleList.Capacity;
        orangeLabel.text = orangePuzzleList.Count + "/" + orangePuzzleList.Capacity;
    }

    public void CountDownFinished()
    {
        Debug.Log("time's up");
    }

}
