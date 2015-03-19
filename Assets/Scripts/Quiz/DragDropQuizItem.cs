using UnityEngine;
using System.Collections;

public class DragDropQuizItem : UIDragDropItem
{
    private UISprite sprite;

    private Transform oriParent;
    private Vector3 oriPosition;
    private int oriDepth;

    protected override void Start()
    {
        base.Start();

        sprite = GetComponent<UISprite>();
        oriParent = transform.parent;
        oriPosition = transform.localPosition;
        oriDepth = sprite.depth;
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        if (!cloneOnDrag)
        {
            // Re-enable the collider
            if (mButton != null) mButton.isEnabled = true;
            else if (mCollider != null) mCollider.enabled = true;
            else if (mCollider2D != null) mCollider2D.enabled = true;

            DragDropQuizContainer container = surface.GetComponent<DragDropQuizContainer>();
            DragDropQuizItem item = surface.GetComponent<DragDropQuizItem>();
            if (item != null)
            {
                container = item.transform.parent.GetComponent<DragDropQuizContainer>();
            }

            if (container != null)
            {
                if (container.HasItem)
                {
                    container.ReleaseItem();
                }
                container.SelectedItem = this;
            }
            else
            {
                Reset();
            }

            NGUITools.MarkParentAsChanged(gameObject);
        }
        else NGUITools.Destroy(gameObject);
    }

    public void setDepth(int d)
    {
        sprite.depth = d;
    }

    public void Reset()
    {
        mTrans.parent = oriParent;
        mTrans.localPosition = oriPosition;
        sprite.depth = oriDepth;
    }

}
