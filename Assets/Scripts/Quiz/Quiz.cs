﻿using UnityEngine;
using System.Collections;

public class Quiz : MonoBehaviour
{

    public GameObject questionContainer;
    public GameObject feedbackContainer;

    protected QuestionControl questionControl;

    protected int finalScore = -1;

    public PuzzleObject puzzleObj;

    protected int totalScore = 0;
    protected int failCount = 0;

    public void AddFailCount()
    {
        failCount++;
    }

    public PuzzleObject PuzzleObject
    {
        get { return puzzleObj; }
    }
    public int FinalScore { get { return finalScore; } }

    void Awake()
    {
        questionControl = NGUITools.FindInParents<QuestionControl>(gameObject);
    }

    void OnEnable()
    {
         Reset();
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

    public virtual void InitFeedback() { }

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
}
