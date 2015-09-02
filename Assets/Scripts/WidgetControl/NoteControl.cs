﻿using UnityEngine;
using System.Collections;

public class NoteControl : MonoBehaviour
{
    public UILabel noteTtitle;
    public UILabel noteContent;
    public UISprite noteContentSprite;
    public UILabel notePager;

    public GameObject nextButton;
    public GameObject previousButton;

    public GameObject libraian;
    public GameObject contentContainer;

    private UIInput input;

    private int textIndex;
    private PuzzleObject puzzle;

    private Animator libraianAnimator;

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
        libraianAnimator = libraian.GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        Debug.Log("pos check");

        if (libraianAnimator == null)
        {
            libraianAnimator = libraian.GetComponentInChildren<Animator>();
        }
        libraianAnimator.SetTrigger("clapping");

        contentContainer.SetActive(false);

        // delay show content
        StartCoroutine("ShowContent");
    }

    public IEnumerator ShowContent()
    {
        yield return new WaitForSeconds(1f);

        contentContainer.SetActive(true);
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
        GameManager.instance.SendEmail("type: note, " + puzzle.noteTitle + ", close");
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
            string content = puzzle.noteContentArray[index];

            if (content.Contains("image_sprite"))
            {
                if (!noteContentSprite.gameObject.activeSelf)
                    noteContentSprite.gameObject.SetActive(true);

                if (noteContent.gameObject.activeSelf)
                    noteContent.gameObject.SetActive(false);

                if (noteTtitle.gameObject.activeSelf)
                    noteTtitle.gameObject.SetActive(false);
                
                noteContentSprite.spriteName = content;
                noteContentSprite.MakePixelPerfect();
                noteContentSprite.ResizeCollider();
            }
            else
            {
                if (!noteTtitle.gameObject.activeSelf)
                    noteTtitle.gameObject.SetActive(true);

                if (!noteContent.gameObject.activeSelf)
                    noteContent.gameObject.SetActive(true);

                if (noteContentSprite.gameObject.activeSelf)
                    noteContentSprite.gameObject.SetActive(false);

                noteContent.text = content.Replace("\\n", "\n"); ;
                noteContent.ResizeCollider();
            }
            notePager.text = (index + 1) + "/" + puzzle.noteContentArray.Length;

            GameManager.instance.SendEmail("type: note, " + puzzle.noteTitle + ", page: " + index);
        }

        
    }
}
