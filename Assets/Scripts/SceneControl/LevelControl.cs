using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance = null;

    public bool isGamePaused = false;

    public bool puzzleCleared = false;

    public int levelIndex;

    public Timer timer;

    public GameObject extraButtons;
    public GameObject showQuestionButton;

    public UILabel purpleLabel;
    public UILabel greenLabel;
    public UILabel orangeLabel;

    public GameObject beginObject;
    public NoteControl noteControl;
    public HelpNoteControl helpControl;
    public CompleteNoteControl completeControl;
    public QuestionControl questionControl;
    public GameObject failObject;
    public GameObject timeUpObject;

    public GameObject player;
    public GameObject[] ghosts;

    public bool allowAnswerQuestion = true;

    private PuzzleObject targetPuzzle;
    private bool secondTime;

    public ExitObject exitObject;

    private PuzzleObject[] totalPuzzle;

    private List<PuzzleObject> purplePuzzleList = new List<PuzzleObject>();
    private List<PuzzleObject> greenPuzzleList = new List<PuzzleObject>();
    private List<PuzzleObject> orangePuzzleList = new List<PuzzleObject>();

    public UIToggle musicToggle;
    public AudioSource musicSource;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isGamePaused = true;
        SetupTotalPuzzles();

        //if (beginObject == null)
        //{

        //}
        InitHelp();

        if (musicToggle != null)
        {
            musicToggle.value = !GameManager.instance.allowMusic;
            OnMute(!GameManager.instance.allowMusic);
        }
    }

    public void muteBackgroundMusic(bool mute)
    {
        if (mute)
        {
            musicSource.Stop();
        }
        else
        {
            musicSource.Play();
        }
    }

    public void OnMute(bool value)
    {
        GameManager.instance.allowMusic = !value;
        if (musicSource != null)
        {
            if (value)
            {
                musicSource.Stop();
            }
            else
            {
                musicSource.Play();
            }
        }
    }

    public void OnPauseButtonClick()
    {
        if (!puzzleCleared)
            isGamePaused = !isGamePaused;
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
        if (isGamePaused)
            return;

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

                // all colors go to green
                greenLabel.text = (greenPuzzleList.Count + purplePuzzleList.Count + orangePuzzleList.Count) + "/" + (greenPuzzleList.Capacity + purplePuzzleList.Capacity + orangePuzzleList.Capacity);

                if (ghosts != null && ghosts.Length > 0)
                {
                    ghosts[0].GetComponent<Enemy>().ScaleDown();
                }
            }
        }
        else if (obj.startType == PuzzleObject.StartType.Green)
        {
            if (!greenPuzzleList.Contains(obj))
            {
                Animation anime = greenLabel.GetComponent<Animation>();
                if (!anime.isPlaying) anime.Play();

                greenPuzzleList.Add(obj);
                //greenLabel.text = greenPuzzleList.Count + "/" + greenPuzzleList.Capacity;

                greenLabel.text = (greenPuzzleList.Count + purplePuzzleList.Count + orangePuzzleList.Count) + "/" + (greenPuzzleList.Capacity + purplePuzzleList.Capacity + orangePuzzleList.Capacity);

                if (ghosts != null && ghosts.Length > 0)
                {
                    ghosts[0].GetComponent<Enemy>().ScaleDown();
                }
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

                // all colors go to green
                greenLabel.text = (greenPuzzleList.Count + purplePuzzleList.Count + orangePuzzleList.Count) + "/" + (greenPuzzleList.Capacity + purplePuzzleList.Capacity + orangePuzzleList.Capacity);

                if (ghosts != null && ghosts.Length > 0)
                {
                    ghosts[0].GetComponent<Enemy>().ScaleDown();
                }
            }
        }

        if (!noteControl.gameObject.activeSelf)
        {
            noteControl.Puzzle = obj;
            noteControl.gameObject.SetActive(true);
        }

        GameManager.instance.SendEmail("type: puzzle, found puzzle, title: " + obj.noteTitle);
    }

    public void OnHelpFinish()
    {
        isGamePaused = false;

        if (helpControl.gameObject.activeSelf)
            helpControl.gameObject.SetActive(false);

        if (!player.activeSelf)
            player.SetActive(true);

        foreach (GameObject obj in ghosts)
        {
            if (!obj.activeSelf)
                obj.SetActive(true);
        }

        GameManager.instance.SendEmail("type: level, start lebel, index: " + levelIndex);
    }

    public void OnNoteCloseButtonClick(PuzzleObject obj)
    {
        isGamePaused = false;
        if (noteControl.gameObject.activeSelf)
        {
            noteControl.gameObject.SetActive(false);
        }
        StartQuestion(obj);
    }

    public void OnShowQuestionButtonClick()
    {
        secondTime = true;
        isGamePaused = true;
        //if (showQuestionButton.activeSelf)
        //    showQuestionButton.SetActive(false);

        if (exitObject.gameObject.activeSelf)
            exitObject.gameObject.SetActive(false);

        if (!questionControl.gameObject.activeSelf)
            questionControl.gameObject.SetActive(true);

        if (noteControl.gameObject.activeSelf)
            noteControl.gameObject.SetActive(false);
    }

    public void AllowAnswerQuestion()
    {
        allowAnswerQuestion = true;

        StartQuestion(null);
    }

    public void StartQuestion(PuzzleObject obj)
    {
        if (puzzleCleared) return;
        if (!allowAnswerQuestion) return;

        if ((!secondTime && totalPuzzle.Length == purplePuzzleList.Count + greenPuzzleList.Count + orangePuzzleList.Count) || (secondTime && targetPuzzle != null && targetPuzzle == obj))
        {
            //if (!showQuestionButton.activeSelf)
            //    showQuestionButton.SetActive(true);


            if (!exitObject.gameObject.activeSelf)
                exitObject.gameObject.SetActive(true);
        }
    }

    public void QuestionStoped(PuzzleObject obj)
    {
        isGamePaused = false; ;
        if (secondTime)
        {
            targetPuzzle = obj;
        }
    }

    public void QuestionFinished()
    {
        puzzleCleared = true;
        //exitObject.ActivateSelf();

        OnExitObjectClick();

        if (questionControl.gameObject.activeSelf)
            questionControl.gameObject.SetActive(false);
    }

    public void OnExitObjectClick()
    {
        if (player != null && player.activeSelf)
        {
            player.SetActive(false);
        }

        foreach (GameObject obj in ghosts)
        {
            if (obj.activeSelf)
                obj.SetActive(false);
        }

        if (noteControl.gameObject.activeSelf)
            noteControl.gameObject.SetActive(false);

        if (!completeControl.gameObject.activeSelf)
            completeControl.gameObject.SetActive(true);
    }

    public void GameFailed()
    {
        isGamePaused = true;

        if (noteControl.gameObject.activeSelf)
            noteControl.gameObject.SetActive(false);

        if (failObject != null && !failObject.activeSelf)
            failObject.SetActive(true);
    }

    public void LevelComplete()
    {
        GameManager.instance.SendEmail("type: level, complete level, index: " + levelIndex);
        GameManager.instance.GameClear();
        GameManager.instance.loadManager.LoadSelectScene();
    }

    protected void InitHelp()
    {
        if (player.activeSelf)
            player.SetActive(false);

        if (helpControl != null)
        {
            if (!helpControl.gameObject.activeSelf)
            {
                helpControl.gameObject.SetActive(true);
            }
        }

        if (noteControl != null)
        {
            if (noteControl)
            {
                if (noteControl.gameObject.activeSelf)
                    noteControl.gameObject.SetActive(false);
            }
        }

        if (completeControl != null)
        {
            if (completeControl.gameObject.activeSelf)
            {
                completeControl.gameObject.SetActive(false);
            }
        }

        if (questionControl != null)
        {
            if (questionControl.gameObject.activeSelf)
                questionControl.gameObject.SetActive(false);
        }

        if (failObject)
        {
            if (failObject.activeSelf)
                failObject.SetActive(false);
        }

        if (timeUpObject)
        {
            if (timeUpObject.activeSelf)
                timeUpObject.SetActive(false);
        }

        if (exitObject)
        {
            if (exitObject.gameObject.activeSelf)
                exitObject.gameObject.SetActive(false);
        }
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
        greenLabel.text = greenPuzzleList.Count + "/" + (greenPuzzleList.Capacity + purplePuzzleList.Capacity + orangePuzzleList.Capacity);
        orangeLabel.text = orangePuzzleList.Count + "/" + orangePuzzleList.Capacity;
    }

    public void CountDownFinished()
    {
        if (timeUpObject != null && !timeUpObject.activeSelf)
            timeUpObject.SetActive(true);
    }
}
