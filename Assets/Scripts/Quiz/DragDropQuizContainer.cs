using UnityEngine;
using System.Collections;

public class DragDropQuizContainer : UIDragDropContainer
{
    public DragDropQuizItem correctItem;

    private DragDropQuizItem selectedItem;
    public DragDropQuizItem SelectedItem
    {
        set
        {
            selectedItem = value;
            selectedItem.transform.parent = transform;
            selectedItem.transform.localPosition = Vector3.zero;
            selectedItem.setDepth(0);
        }
    }

    public bool HasItem
    {
        get
        {
            return selectedItem != null;
        }
    }

    public void ReleaseItem()
    {
        selectedItem.Reset();
        selectedItem = null;
    }

    public bool getResult()
    {
        bool result = false;
        if (selectedItem != null && selectedItem == correctItem)
            result = true;
        return result;
    }
}
