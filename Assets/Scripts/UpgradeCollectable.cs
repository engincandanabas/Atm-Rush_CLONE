using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCollectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Money" || other.gameObject.tag == "Gold" || other.gameObject.tag=="Diamond")
        {
            StartCoroutine(ATMRush.instance.UpgradeCollectable(other.gameObject));
        }
    }
}
