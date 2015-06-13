using UnityEngine;
using System.Collections;

public class TrigerObject : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.loadManager.LoadPrevioudLevelScene(4);
        }
    }

}
