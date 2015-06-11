using UnityEngine;
using System.Collections;

public class DragDropQuizContainer : UIDragDropContainer
{
    public DragDropQuizItem correctItem;

    private DragDropQuizItem selectedItem;

    public virtual void SetSelectItem(DragDropQuizItem item)
    {
        selectedItem = item;
        selectedItem.transform.parent = transform;
        selectedItem.transform.localPosition = Vector3.zero;
        selectedItem.setDepth(0);
    }

    public bool HasItem
    {
        get
        {
            return selectedItem != null;
        }
    }

    public virtual void ReleaseItem()
    {
        if (selectedItem != null)
        {
            selectedItem.Reset();
            selectedItem = null;
        }
    }

    public virtual void ReleaseFalseItem()
    {
        if (!getResult())
            ReleaseItem();
    }

    public virtual bool getResult()
    {
        bool result = false;
        if (selectedItem != null && selectedItem == correctItem)
            result = true;
        return result;
    }
}
