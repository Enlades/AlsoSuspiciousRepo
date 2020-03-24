using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartLine : MonoBehaviour
{
    public TextMeshPro PartLineText;

    public void Init(int p_percentage){
        PartLineText.text = p_percentage + " %";
    }
}
