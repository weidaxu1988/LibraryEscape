using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragDropQuizMultiContainer : DragDropQuizContainer
{
    public DragDropQuizItem[] correctItems;

    private List<DragDropQuizItem> selectedItems;

    private UIGrid grid;

    protected override void Start()
    {
        base.Start();

        grid = GetComponent<UIGrid>();
    }

    public override void SetSelectItem(DragDropQuizItem item)
    {
        if (selectedItems == null) selectedItems = new List<DragDropQuizItem>();
        selectedItems.Add(item);
        //item.transform.parent = transform;
        //item.transform.localPosition = Vector3.zero;
        item.setDepth(0);

        grid.AddChild(item.transform);
    }

    public override void ReleaseItem()
    {
        if (selectedItems != null)
        {
            foreach (DragDropQuizItem item in selectedItems)
            {
                item.Reset();
                grid.RemoveChild(item.transform);
            }
            selectedItems.Clear();
        }
    }

    public override void ReleaseFalseItem()
    {
        List<DragDropQuizItem> correctList = new List<DragDropQuizItem>(correctItems);
        List<DragDropQuizItem> removeList = new List<DragDropQuizItem>();

        if (selectedItems == null)
        {
            Debug.Log("selected items is null");
            return;
        }
        else
            Debug.Log("selected items's size is: " + selectedItems.Count);


        foreach (DragDropQuizItem item in selectedItems)
        {
            if (!correctList.Contains(item))
            {
                removeList.Add(item);
                item.Reset();
                grid.RemoveChild(item.transform);
            }
        }

        foreach (DragDropQuizItem item in removeList)
        {
            selectedItems.Remove(item);
        }
    }

    public override bool getResult()
    {
        if (selectedItems != null)
        {
            if (correctItems.Length == selectedItems.Count)
            {
                foreach (DragDropQuizItem item in correctItems)
                {
                    if (!selectedItems.Contains(item))
                        return false;

                }
                return true;
            }
        }
        return false;
    }
}
