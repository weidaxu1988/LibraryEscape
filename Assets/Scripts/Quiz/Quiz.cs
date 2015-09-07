using UnityEngine;
using System.Collections;

public class Quiz : MonoBehaviour
{

    public GameObject questionContainer;
    public GameObject feedbackContainer;

    public UILabel feedbackLabel;

    public PuzzleObject puzzleObj;

    protected QuestionControl questionControl;

    protected int finalScore = -1;
    protected int totalScore = 0;
    protected int failCount = 0;

    public PuzzleObject PuzzleObject
    {
        get { return puzzleObj; }
    }
    public int FinalScore { get { return finalScore; } }

    void Start()
    {
        
    }

    void OnEnable()
    {
         Reset();
    }

    public void AddFailCount()
    {
        failCount++;
    }

    public virtual void Reset()
    {
        ShowFeedBack(false);
        ClearResult();
    }

    public virtual void SecondReset()
    {
        Reset();
    }

    public virtual int getScore() { return 0; }

    public virtual void ClearResult() { }

	public void HideQuestionContent() {
		if (questionContainer != null && questionContainer.activeSelf)
			questionContainer.SetActive(false);
	}

    public virtual void InitFeedback() {
        ShowFeedBack(true);

        finalScore = getScore();

        if (finalScore >= 1)
        {
            GameManager.instance.player.AddTotalScore(failCount);
            HandleCorrectFeedback();
            
            if (questionControl == null)
                FindQuestionControl();

            questionControl.QuestionCorrect();
        }
        else
        {
            failCount++;
            HandleIncorrectFeedback();

            if (questionControl == null)
                FindQuestionControl();

            questionControl.QuestionIncorrect();
        }
    }

    public void HideFeedBack()
    {
        if (feedbackContainer != null && feedbackContainer.activeSelf == true)
            feedbackContainer.SetActive(false);
    }

    protected void ShowFeedBack(bool show)
    {
        if (questionContainer != null && questionContainer.activeSelf == show)
            questionContainer.SetActive(!show);

        if (feedbackContainer != null && feedbackContainer.activeSelf != show)
            feedbackContainer.SetActive(show);
    }

    protected virtual void HandleIncorrectFeedback()
    {

    }

    protected virtual void HandleCorrectFeedback()
    {

    }

    public void FindQuestionControl()
    {
        questionControl = NGUITools.FindInParents<QuestionControl>(gameObject);
    }
}
