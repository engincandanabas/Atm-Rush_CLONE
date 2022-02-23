using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectableMoney"))
        {
            if (!ATMRush.instance.collected.Contains(other.gameObject))
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<BoxCollider>().size=new Vector3(0.003f,0.005f,0.001f);
                other.gameObject.tag = "Money";
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                ATMRush.instance.StackCube(other.gameObject, ATMRush.instance.collected.Count - 1);
            }
        }
        if (other.gameObject.CompareTag("CollectableGold"))
        {
            if (!ATMRush.instance.collected.Contains(other.gameObject))
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<BoxCollider>().size=new Vector3(0.007f,0.005f,0.002f);
                other.gameObject.tag = "Gold";
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                ATMRush.instance.StackCube(other.gameObject, ATMRush.instance.collected.Count - 1);
            }
        }
        if (other.gameObject.CompareTag("CollectableDiamond"))
        {
            if (!ATMRush.instance.collected.Contains(other.gameObject))
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<BoxCollider>().size=new Vector3(0.02f,0.02f,0.01f);
                other.gameObject.tag = "Diamond";
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                ATMRush.instance.StackCube(other.gameObject, ATMRush.instance.collected.Count - 1);
            }
        }
    }
}
