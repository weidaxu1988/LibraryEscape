using UnityEngine;
using System.Collections;

public class BlockObject : MonoBehaviour {

    void OnColliderEnter(Collider other)
    {
        Debug.Log("here");
    }
}
