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

	public GameObject bigLibrary;

    private Animator libraianAnimator;

    private int lastQuizIndex = 0;
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

        quizIndex = lastQuizIndex;
        incorrectQuestionCount = 0;

        ShowQuiz(quizIndex);
        ShowNextButton(false);
    }

	public void OnQuestionSubmit() {
//		if (bigLibrary != null) {
//			bigLibrary.SetActive (true);
//		} else {
//			AQuestionSubmit();
//		}

		if (!librarian.activeSelf) {
			librarian.SetActive (true);
		}

		if (submitButton.activeSelf)
			submitButton.SetActive(false);

		Quiz quiz = totalQuiz[quizIndex];
		quiz.HideQuestionContent ();

		StartCoroutine (AQuestionSubmit());
	}

    public IEnumerator AQuestionSubmit()
    {
		yield return new WaitForSeconds(2f);

        Quiz quiz = totalQuiz[quizIndex];

//        if (!librarian.activeSelf)
//            librarian.SetActive(true);

        if (quiz.getScore() < 1)
        {
            incorrectQuestionCount++;

            if (incorrectQuestionCount >= incorrectCapacity)
            {
                ShowFailedContent();
                ShowNextButton(true);
                yield break;
            }
        }
        quiz.InitFeedback();
        ShowNextButton(true);
    }

    public void NextQuestion()
    {
        Quiz quiz = totalQuiz[quizIndex];
        if (incorrectQuestionCount >= incorrectCapacity)
        {
            quiz.AddFailCount();
            OnQuestionFinish();
            return;
        }

        //Quiz quiz = totalQuiz[quizIndex];

        ShowNextButton(false);

        if (incorrectQuestionCount < incorrectCapacity)
        {
            if (quiz.getScore() >= 1)
            {
                //save answer

                quizIndex++;
                if (quizIndex >= totalQuiz.Length)
                {
                    LevelControl.instance.QuestionFinished();
                    //OnQuestionFinish();
                }
                else
                {
                    incorrectQuestionCount = 0;
                    ShowQuiz(quizIndex);
                }
            }
            else
            {
                ResetQuiz(quizIndex);
            }
        }
    }

    public void OnQuestionFinish()
    {
        //Debug.Log("index " + quizIndex);
        //Debug.Log("length " + totalPuzzle.Length);

        if (quizIndex > 0)
        {
            lastQuizIndex = quizIndex;
        }

        if (gameObject.activeSelf)
        {
            foreach (Quiz q in totalQuiz)
            {
                q.ClearResult();
            }
            gameObject.SetActive(false);
        }

        LevelControl.instance.QuestionStoped(totalQuiz[quizIndex].PuzzleObject);
    }

    public void QuestionCorrect()
    {
        if (libraianAnimator == null)
        {
            Debug.Log("incorrect null");
            libraianAnimator = librarian.GetComponentInChildren<Animator>();
        }


        if(libraianAnimator != null)
        {
            

            float random = Random.value;

            Debug.Log("random value: " + random);
            if (random > 0.66f)
            {
                libraianAnimator.SetTrigger("yes");
            }
            else if (random > 0.33f)
            {
                libraianAnimator.SetTrigger("rite");
            }
            else { 
            
            libraianAnimator.SetTrigger("clapping");
            }
        }

        
    }

    public void QuestionIncorrect()
    {
        if (libraianAnimator == null)
        {
            Debug.Log("incorrect null");
            libraianAnimator = librarian.GetComponentInChildren<Animator>();
        }
        

        if (libraianAnimator != null)
        {
            Debug.Log("incorrect not null");
            libraianAnimator.SetTrigger("look");
        }

        
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

        libraianAnimator.SetTrigger("concerned");
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
        if (librarian.activeSelf)
            librarian.SetActive(false);

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

    protected void ResetQuiz(int index)
    {
		if (librarian.activeSelf)
			librarian.SetActive(false);

        Quiz quiz = totalQuiz[index];
        quiz.SecondReset();
    }
}
