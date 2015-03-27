using UnityEngine;
using System.Collections;

public class Quiz : MonoBehaviour {

    public virtual int getScore() { return 0; }

    public virtual void ClearResult() { }
}
