using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardController : MonoBehaviour
{
    private bool moveleft=true;
    public float duration,waitTime;
    private void Update() {
        StartCoroutine(MoveCard());
    }
    IEnumerator MoveCard()
    {
        Vector3 moveVectortoRight=new Vector3(0.00870000012f,-0.0200999994f,0.000729446416f);
        Vector3 moveVectortoLeft =new Vector3(0.00870000012f,0.0198999997f,0.000729446416f);
        if(moveleft)
        {
            transform.DOLocalMove(moveVectortoLeft,duration).OnComplete(()=>{
                moveleft=false;
            });
        }
        else
        {
            transform.DOLocalMove(moveVectortoRight,duration).OnComplete(()=>{
                moveleft=true;
            });
        }
        yield return new WaitForSeconds(waitTime);
    }
}
