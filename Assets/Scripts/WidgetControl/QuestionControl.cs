using UnityEngine;
using System.Collections;

public class QuestionControl : MonoBehaviour
{
    public int incorrectCapacity = 1;

    public Quiz[] totalQuiz;
    public GameObject librarian;

    public GameObject failedContent;

    public GameObject nextButton;
    public GameObject submitButton;

    private Animator libraianAnimator;

    private int quizIndex = 0;
    private int incorrectQuestionCount = 0;

    void Start()
    {
        libraianAnimator = librarian.GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        if (failedContent.activeSelf)
        {
            failedContent.SetActive(false);
        }

        quizIndex = 0;
        incorrectQuestionCount = 0;

        ShowQuiz(quizIndex);
        ShowNextButton(false);
    }

    public void OnQuestionSubmit()
    {
        Quiz quiz = totalQuiz[quizIndex];

        quiz.InitFeedback();
        ShowNextButton(true);
    }

    public void NextQuestion()
    {
        if (incorrectQuestionCount > incorrectCapacity)
        {
            OnQuestionFinish();
            return;
        }

        Quiz quiz = totalQuiz[quizIndex];

        ShowNextButton(false);

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
                LevelControl.instance.QuestionFinished();
                OnQuestionFinish();
            }
            else
            {
                ShowQuiz(quizIndex);
            }
        }
        else
        {
            ShowFailedContent();
        }
    }

    public void OnQuestionFinish()
    {
        //Debug.Log("index " + quizIndex);
        //Debug.Log("length " + totalPuzzle.Length);

        if (gameObject.activeSelf)
        {
            foreach (Quiz q in totalQuiz)
            {
                q.ClearResult();
            }
            gameObject.SetActive(false);
        }
    }

    public void QuestionCorrect()
    {
        libraianAnimator.SetTrigger("comfort");
    }

    public void QuestionIncorrect()
    {
        libraianAnimator.SetTrigger("cheer");
    }

    protected void ShowFailedContent()
    {
        ShowNextButton(true);

        if (!failedContent.activeSelf)
        {
            failedContent.SetActive(true);
        }

        if (quizIndex < totalQuiz.Length)
        {
            Quiz quiz = totalQuiz[quizIndex];
            if (quiz.gameObject.activeSelf)
                quiz.gameObject.SetActive(false);
        }

        libraianAnimator.SetTrigger("console");
    }

    protected void ShowNextButton(bool show)
    {
        if (nextButton.activeSelf != show)
            nextButton.SetActive(show);
        if (submitButton.activeSelf == show)
            submitButton.SetActive(!show);
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
