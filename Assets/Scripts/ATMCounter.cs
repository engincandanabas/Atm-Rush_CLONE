using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ATMCounter : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    private int count=0;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Money" )
        {
            count++;
        }
        else if(other.gameObject.tag == "Gold" )
        {
            count+=2;
        }
        else if(other.gameObject.tag=="Diamond")
        {
            count+=3;
        }
        textMeshProUGUI.text=count.ToString();
    }
}
