using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float swipeSpeed;
    public float moveSpeed;
    public float swipeModifier;
    private Camera cam;
    private GameObject firstCube;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ATMRush.instance.gameState == ATMRush.GameState.playing)
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            // if(Input.GetButton("Fire1"))
            // {
            //     Move();
            // }
            GameObject firstCube = ATMRush.instance.collected[0];
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                firstCube.transform.localPosition = Vector3.MoveTowards(firstCube.transform.localPosition, new Vector3(Mathf.Clamp(Input.GetAxisRaw("Horizontal") * swipeModifier,-4.2f,4.2f), firstCube.transform.localPosition.y, firstCube.transform.localPosition.z), Time.fixedDeltaTime * moveSpeed);
            }
            //MoveWithMouse();
        }

    }
    private void Move()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.transform.localPosition.z;

        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            Vector3 hitVec = hit.point;
            hitVec.x = Mathf.Clamp(hitVec.x, -4.2f, 4.2f);
            hitVec.y = firstCube.transform.localPosition.y;
            hitVec.z = firstCube.transform.localPosition.z;

            firstCube.transform.localPosition = Vector3.MoveTowards(firstCube.transform.localPosition, hitVec, Time.fixedDeltaTime * moveSpeed);
        }

    }
    private void MoveWithMouse()
    {
        Vector3 startPos=Input.mousePosition;
        startPos=Camera.main.ScreenToWorldPoint(startPos);
        firstCube.transform.localPosition = Vector3.MoveTowards(firstCube.transform.localPosition, startPos, Time.fixedDeltaTime * moveSpeed);

    }
}
