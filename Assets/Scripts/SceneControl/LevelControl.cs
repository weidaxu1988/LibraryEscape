using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance = null;

    public int incorrectCapacity = 1;

    public GameObject extraButtons;

    public UILabel purpleLabel;
    public UILabel greenLabel;
    public UILabel orangeLabel;

    public GameObject notePanel;
    public UILabel noteTtitle;
    public UILabel noteContent;

    public GameObject questionPanel;

    public Quiz[] totalQuiz;

    private PuzzleObject[] totalPuzzle;

    private List<PuzzleObject> purplePuzzleList = new List<PuzzleObject>();
    private List<PuzzleObject> greenPuzzleList = new List<PuzzleObject>();
    private List<PuzzleObject> orangePuzzleList = new List<PuzzleObject>();

    private int quizIndex = 0;
    private int incorrectQuestionCount = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        setupTotalPuzzles();
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
        if (obj.startType == PuzzleObject.StartType.Purple)
        {
            if (!purplePuzzleList.Contains(obj))
            {
                purplePuzzleList.Add(obj);
                purpleLabel.text = purplePuzzleList.Count + "/" + purplePuzzleList.Capacity;
            }
        }
        else if (obj.startType == PuzzleObject.StartType.Green)
        {
            if (!greenPuzzleList.Contains(obj))
            {
                greenPuzzleList.Add(obj);
                greenLabel.text = greenPuzzleList.Count + "/" + greenPuzzleList.Capacity;
            }
        }
        else if (obj.startType == PuzzleObject.StartType.Orange)
        {
            if (!orangePuzzleList.Contains(obj))
            {
                orangePuzzleList.Add(obj);
                orangeLabel.text = orangePuzzleList.Count + "/" + orangePuzzleList.Capacity;
            }
        }

        if (!notePanel.activeSelf)
        {
            noteTtitle.text = obj.noteTitle;
            noteContent.text = obj.noteContent;

            notePanel.SetActive(true);
        }
    }

    public void OnNoteCloseButtonClick()
    {
        if (notePanel.activeSelf)
        {
            notePanel.SetActive(false);
        }
    }

    public void OnUserNoteSubmit(UILabel label)
    {
        string note = label.text;
        if (!string.IsNullOrEmpty(note) && !note.Equals(GameConfig.TXT_INPUT_DEFAULT))
        {
            Debug.Log(note);
        }
    }

    public void StartQuestion()
    {
        quizIndex = 0;

        if (totalPuzzle.Length == purplePuzzleList.Count + greenPuzzleList.Count + orangePuzzleList.Count)
        {
            if (!questionPanel.activeSelf)
            {
                questionPanel.SetActive(true);
            }

            ShowQuiz(quizIndex);
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
                LevelComplete();
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
        if (questionPanel.activeSelf)
        {
            questionPanel.SetActive(false);
        }
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

    protected void LevelComplete()
    {
        OnQuestionFinish();
    }
}
