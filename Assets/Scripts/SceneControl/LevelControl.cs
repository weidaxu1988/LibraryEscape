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
    public UILabel questionContent;

    private PuzzleObject[] totalPuzzle;

    private List<PuzzleObject> purplePuzzleList = new List<PuzzleObject>();
    private List<PuzzleObject> greenPuzzleList = new List<PuzzleObject>();
    private List<PuzzleObject> orangePuzzleList = new List<PuzzleObject>();

    private int questionIndex = 0;
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
        questionIndex = 0;
    }

    public void OnQuestionSubmit(UILabel label)
    {
        string answer = label.text;
        if (!string.IsNullOrEmpty(answer) && !answer.Equals(GameConfig.TXT_INPUT_DEFAULT))
        {
            Debug.Log(answer);

            PuzzleObject puzzle = totalPuzzle[questionIndex];
            if (puzzle.checkAnswer(answer))
            {
                //save answer
            }
            else
            {
                incorrectQuestionCount++;
            }

            if (incorrectQuestionCount <= incorrectCapacity)
            {
                questionIndex++;
                if (questionIndex >= totalPuzzle.Length)
                {
                    LevelComplete();
                }
                else
                {
                    ShowPuzzleQuestion(questionIndex);
                }
            }
            else
            {
                OnQuestionCloseButtonClick();
            }
        }
    }

    public void OnQuestionCloseButtonClick()
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

    protected void ShowPuzzleQuestion(int index)
    {
        if (index < totalPuzzle.Length)
        {
            questionContent.text = totalPuzzle[index].question;
        }
    }

    protected void LevelComplete()
    {

    }
}
