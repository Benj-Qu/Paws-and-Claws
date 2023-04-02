using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackIndicator : MonoBehaviour
{
    public Image circ;
    public PlayerAttack pa;

    void Update()
    {
        circ.fillAmount = pa.getTimeRatio();
    }
}
