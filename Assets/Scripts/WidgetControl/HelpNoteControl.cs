using UnityEngine;
using System.Collections;

public class HelpNoteControl : MonoBehaviour
{
    public int delayTime = 3;
    public int breakIndex = 2;

    public GameObject librarian;

    public GameObject nextButton;

    public GameObject[] stages;

    private TweenAlpha tAlpha;
    private int textIndex;

    void Start()
    {
        tAlpha = GetComponent<TweenAlpha>();

        InitPage();
    }

    public void nextPage()
    {
        textIndex++;

        if (textIndex == breakIndex)
        {
            HideNote();
        }
        else if (textIndex >= stages.Length)
        {
            if (LevelControl.instance != null)
            {
                LevelControl.instance.OnHelpFinish();
            }
        }
        else
        {
            setPage(textIndex);
            checkNavButtons();
        }
    }

    public void previousPage()
    {
        if (textIndex > 0)
        {
            textIndex--;
            setPage(textIndex);
            checkNavButtons();
        }
    }

    private void InitPage()
    {
        textIndex = 0;
        setPage(textIndex);
        checkNavButtons();
    }

    private void setPage(int index)
    {

        if (index >= 0 && index < stages.Length)
        {
            GameObject stage = null;
            if (index > 0)
            {
                stage = stages[index - 1];
                if (stage.activeSelf)
                    stage.SetActive(false);
            }
            if (index < stages.Length - 1)
            {
                stage = stages[index + 1];
                if (stage.activeSelf)
                    stage.SetActive(false);

            }
            stage = stages[index];
            if (!stage.activeSelf)
                stage.SetActive(true);
        }
    }

    private void HideNote()
    {
        if (librarian.activeSelf) librarian.SetActive(false);
        nextButton.GetComponent<UIButton>().enabled = false;
        tAlpha.PlayForward();

        Invoke("ShowNote", delayTime);
    }

    private void ShowNote()
    {
        tAlpha.PlayReverse();
        if (!librarian.activeSelf) librarian.SetActive(true);
        nextButton.GetComponent<UIButton>().enabled = true;
        setPage(textIndex);
    }

    private void checkNavButtons()
    {
        //if (textIndex <= 0)
        //{
        //    if (previousButton.activeSelf)
        //        previousButton.SetActive(false);
        //}
        //else
        //{
        //    if (!previousButton.activeSelf)
        //        previousButton.SetActive(true);
        //}

        //int max = useExtra ? extraArray.Length : helpArray.Length;

        //if (textIndex >= max - 1)
        //{
        //    if (nextButton.activeSelf)
        //        nextButton.SetActive(false);
        //}
        //else
        //{
        //    if (!nextButton.activeSelf)
        //        nextButton.SetActive(true);
        //}
    }
}
