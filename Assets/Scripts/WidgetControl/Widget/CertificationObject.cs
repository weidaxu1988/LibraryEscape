using UnityEngine;
using System;
using System.Collections;

public class CertificationObject : MonoBehaviour {

    public UIInput nameInput;

    public UILabel dayLabel;
    public UILabel monthLabel;
    public UILabel yearLabel;

    void OnEnable()
    {
        DateTime now = DateTime.Now;

        dayLabel.text = now.Day+"";
        monthLabel.text = now.Month + "";
        yearLabel.text = (now.Year + "").Substring(2);
    }
}
