using UnityEngine;
using System.Collections;

public class NoteControl : MonoBehaviour
{
    public UILabel noteTtitle;
    public UILabel noteContent;
    public UILabel notePager;

    public GameObject nextButton;
    public GameObject previousButton;

    private UIInput input;

    private int textIndex;
    private PuzzleObject puzzle;
    public PuzzleObject Puzzle
    {
        set
        {
            puzzle = value;
            textIndex = 0;
            setPage(textIndex);
            checkNavButtons();
        }
    }

    void Start()
    {
        input = GetComponentInChildren<UIInput>();
    }

    void OnDisable()
    {
        string note = "";
        if (input)
            note = input.value;
        if (!string.IsNullOrEmpty(note) && !note.Equals(GameConfig.TXT_INPUT_NOTE_DEFAULT))
        {
            Debug.Log(note);
        }
    }

    public void nextPage()
    {
        if (textIndex < puzzle.noteContentArray.Length - 1)
        {
            textIndex++;
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
    
    public void CloseNote()
    {
        LevelControl.instance.OnNoteCloseButtonClick(puzzle);
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

        if (textIndex >= puzzle.noteContentArray.Length - 1)
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
        noteTtitle.text = puzzle.noteTitle;
        if (index >= 0 && index < puzzle.noteContentArray.Length)
        {
            noteContent.text = puzzle.noteContentArray[index];
            noteContent.ResizeCollider();
            notePager.text = (index + 1) + "/" + puzzle.noteContentArray.Length;
        }
    }
}
