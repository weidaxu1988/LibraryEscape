using UnityEngine;
using System.Collections;

public class HelpNoteControl : MonoBehaviour
{
    public int delayTime = 3;
    public bool useExtra;
    //public UILabel noteTtitle;
    public UILabel noteContent;

    public GameObject librarian;
    public GameObject nextButton;
    public GameObject previousButton;

    public string[] helpArray;
    public string[] extraArray;

    private TweenAlpha tAlpha;
    private int textIndex;

    void Start()
    {
        tAlpha = GetComponent<TweenAlpha>();

        InitPage();
    }

    public void nextPage()
    {
        int max = useExtra ? extraArray.Length : helpArray.Length;

        if (textIndex < max - 1)
        {
            textIndex++;
            setPage(textIndex);
            checkNavButtons();

            if (textIndex == max - 1)
            {
                Invoke("HideNote", delayTime);
            }
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

        if (useExtra && textIndex == extraArray.Length - 1)
        {
            Invoke("HideNote", delayTime);
        }
    }

    private void checkNavButtons()
    {
        if (textIndex <= 0)
        {
            if (previousButton.activeSelf)
                previousButton.SetActive(false);
        }
        else
        {
            if (!previousButton.activeSelf)
                previousButton.SetActive(true);
        }

        int max = useExtra ? extraArray.Length : helpArray.Length;

        if (textIndex >= max - 1)
        {
            if (nextButton.activeSelf)
                nextButton.SetActive(false);
        }
        else
        {
            if (!nextButton.activeSelf)
                nextButton.SetActive(true);
        }
    }

    private void setPage(int index)
    {
        //noteTtitle.text = puzzle.noteTitle;
        int max = useExtra ? extraArray.Length : helpArray.Length;

        if (index >= 0 && index < max)
        {
            noteContent.text = useExtra ? extraArray[index] : helpArray[index];
        }
    }

    private void HideNote()
    {
        if (!useExtra)
        {
            useExtra = true;
            
            if (librarian.activeSelf) librarian.SetActive(false);

            tAlpha.PlayForward();
            
            Invoke("ShowNote", delayTime);
        }
        else
        {
            if (LevelControl.instance != null)
            {
                LevelControl.instance.OnHelpFinish();
            }
            
            //if (gameObject.activeSelf)
            //    gameObject.SetActive(false);
        }

    }

    private void ShowNote()
    {
        tAlpha.PlayReverse();
        if (!librarian.activeSelf) librarian.SetActive(true);

        InitPage();
    }
}
