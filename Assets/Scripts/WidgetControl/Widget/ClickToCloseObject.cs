using UnityEngine;
using System.Collections;

public class ClickToCloseObject : MonoBehaviour {
    public GameObject closeobject;

    public void CloseObject()
    {
        if (closeobject.activeSelf)
            closeobject.SetActive(false);
    }
}
