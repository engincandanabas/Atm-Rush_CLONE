using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Guillotine : MonoBehaviour
{
    public float angle = 50f;
    private bool moveleft = true;
    public float duration, waitTime;
    private void Update()
    {
        StartCoroutine(RotateGuillotine());
    }
    IEnumerator RotateGuillotine()
    {
        Vector3 rotateVector=new Vector3(0,0,angle);
        if (moveleft)
        {
            
            transform.DORotate(-rotateVector, duration).OnComplete(() =>
            {
                moveleft = false;
            });
        }
        else
        {
            transform.DORotate(rotateVector, duration).OnComplete(() =>
            {
                moveleft = true;
            });
        }
        yield return new WaitForSeconds(waitTime);
    }
}
