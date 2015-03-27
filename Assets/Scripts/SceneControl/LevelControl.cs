using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance = null;
    
    public int incorrectCapacity = 1;

    public bool isGamePaused = false;

    public GameObject extraButtons;

    public UILabel purpleLabel;
    public UILabel greenLabel;
    public UILabel orangeLabel;

    public NoteControl noteControl;
    public HelpNoteControl helpControl;
    
    public GameObject questionPanel;
    public GameObject exitPanel;
    public GameObject player;

    public Quiz[] totalQuiz;

    public ExitObject exitObject;

    private PuzzleObject[] totalPuzzle;

    private List<PuzzleObject> purplePuzzleList = new List<PuzzleObject>();
    private List<PuzzleObject> greenPuzzleList = new List<PuzzleObject>();
    private List<PuzzleObject> orangePuzzleList = new List<PuzzleObject>();

    private int quizIndex = 0;
    private int incorrectQuestionCount = 0;

    private bool puzzleCleared = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        setupTotalPuzzles();
        InitHelp();
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

    public void OnPuzzleObjectClick(PuzzleObject obj)
    {
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
        if (helpControl.gameObject.activeSelf)
            helpControl.gameObject.SetActive(false);
        
        if (!player.activeSelf)
            player.SetActive(true);
    }

    public void OnNoteCloseButtonClick()
    {
        if (noteControl.gameObject.activeSelf)
        {
            noteControl.gameObject.SetActive(false);
        }
    }

    public void StartQuestion()
    {
        if (!puzzleCleared)
        {
            quizIndex = 0;
            incorrectQuestionCount = 0;

            if (totalPuzzle.Length == purplePuzzleList.Count + greenPuzzleList.Count + orangePuzzleList.Count)
            {
                if (!questionPanel.activeSelf)
                {
                    questionPanel.SetActive(true);
                }

                ShowQuiz(quizIndex);
            }
        }
    }

    public void OnQuestionSubmit()
    {
        Quiz quiz = totalQuiz[quizIndex];
        if (quiz.getScore() >= 1)
        {
            //save answer
        }
        else
        {
            incorrectQuestionCount++;
        }

        if (incorrectQuestionCount <= incorrectCapacity)
        {
            quizIndex++;
            if (quizIndex >= totalQuiz.Length)
            {
                OnQuestionFinish();
                exitObject.ActivateSelf();
                puzzleCleared = true;
            }
            else
            {
                ShowQuiz(quizIndex);
            }
        }
        else
        {
            OnQuestionFinish();
        }
    }

    public void OnQuestionFinish()
    {
        //Debug.Log("index " + quizIndex);
        //Debug.Log("length " + totalPuzzle.Length);

        if (quizIndex < totalQuiz.Length)
        {
            Quiz quiz = totalQuiz[quizIndex];
            if (quiz.gameObject.activeSelf)
                quiz.gameObject.SetActive(false);
        }

        if (questionPanel.activeSelf)
        {
            foreach (Quiz q in totalQuiz)
            {
                q.ClearResult();
            }
            questionPanel.SetActive(false);
        }
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

    protected void setupTotalPuzzles()
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

    protected void ShowQuiz(int index)
    {
        if (index < totalQuiz.Length)
        {
            Quiz quiz = null;
            if (index > 0)
            {
                quiz = totalQuiz[index - 1];
                if (quiz.gameObject.activeSelf)
                    quiz.gameObject.SetActive(false);
            }
            quiz = totalQuiz[index];
            if (!quiz.gameObject.activeSelf)
                quiz.gameObject.SetActive(true);
        }
    }
}
