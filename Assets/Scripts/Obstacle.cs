using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.CompareTag("Spine"))
        {
            if (other.gameObject.tag == "Money" || other.gameObject.tag == "Gold" || other.gameObject.tag == "Diamond")
            {
                for (int i = 0; i < ATMRush.instance.collected.Count - 1; i++)
                {
                    if (ATMRush.instance.collected[i] == other.gameObject)
                    {
                        ATMRush.instance.DestroyMoney(other.gameObject,i,this.gameObject);
                        break;
                    }
                }
            }
        }
        else if (this.gameObject.CompareTag("ATM"))
        {
            for (int i = 0; i < ATMRush.instance.collected.Count - 1; i++)
            {
                if (ATMRush.instance.collected[i] == other.gameObject)
                {
                    StartCoroutine(ATMRush.instance.ATMKeepMoney(other.gameObject,i));
                    break;
                }
            }
        }
        else if(this.gameObject.CompareTag("Card") || this.gameObject.CompareTag("Guillotine"))
        {
            if (other.gameObject.tag == "Money" || other.gameObject.tag == "Gold" || other.gameObject.tag == "Diamond")
            {
                for (int i = 0; i < ATMRush.instance.collected.Count - 1; i++)
                {
                    if (ATMRush.instance.collected[i] == other.gameObject)
                    {
                        ATMRush.instance.DistributeCollectibles(other.gameObject, i, this.gameObject);
                        break;
                    }
                }
            }
        }
        else if(this.gameObject.CompareTag("Finish"))
        {
            if (other.gameObject.tag == "Money" || other.gameObject.tag == "Gold" || other.gameObject.tag == "Diamond")
            {
                ATMRush.instance.FinishCollecterMoney(other.gameObject);
            }
            if(other.gameObject.tag=="Player")
            {
                ATMRush.instance.gameState=ATMRush.GameState.finished;
            }
        }
    }
}
