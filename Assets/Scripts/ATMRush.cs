using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ATMRush : MonoBehaviour
{
    public static ATMRush instance;
    public List<GameObject> collected = new List<GameObject>();

    [Header("Movement && Speeds")]
    [Space(10)]
    public float movementTailDelay = .25f;
    public float movementOriginDelay;
    public GameObject test;

    [Space(30)]
    [Header("Mesh && Materials")]
    [Space(10)]
    public Mesh goldMesh;
    public Mesh diamondMesh;
    public Material goldMaterial, diamondMaterial;
    [Space(30)]

    [Header("Particle Effects")]
    [Space(10)]
    public ParticleSystem cashDestroyedParticle;
    public ParticleSystem goldDestroyedParticle;
    public ParticleSystem diamondDestroyedParticle;

    [Header("Finish")]
    [Space(10)]
    public GameObject finishTarget;
    public GameObject cashPrefab, playerPrefab;
    private Animator anim;
    public float targetToMoveDelay,finishDelay,cameraLerpTime;

    public int moneyValue = 0;
    public TextMeshProUGUI moneyValueText;
    public enum GameState { playing, finished }
    public GameState gameState;
    private bool finish = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            moneyValue = 0;
            gameState = GameState.playing;
        }
    }
    void Update()
    {
        if (gameState == GameState.playing)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                MoveElements();
            }
            else
            {
                MoveOrigin();
            }
        }
        if (gameState == GameState.finished && finish)
        {
            finish = false;
            Camera.main.transform.parent=null;
            Camera.main.GetComponent<CameraFollow>().offset=new Vector3(0,1,-6);
            Camera.main.transform.rotation=Quaternion.Euler(new Vector3(0,0,0));
            anim = playerPrefab.GetComponent<Animator>();
            anim.SetTrigger("finish");
            playerPrefab.transform.rotation=Quaternion.Euler(new Vector3(0,180,0));
            StartCoroutine(GameFinished());
        }
        if(gameState==GameState.finished)
        {
            
            //Camera.main.transform.position=Vector3.Lerp(Camera.main.transform.position,playerPrefab.transform.position+new Vector3(0,0,6),cameraLerpTime);
        }
    }


    public void StackCube(GameObject other, int index)
    {
        //parent ayarladım.
        other.gameObject.name = "Money" + collected.Count;
        moneyValue++;
        moneyValueText.text = moneyValue.ToString();
        other.gameObject.transform.parent = transform;
        Vector3 newPos = collected[index].transform.localPosition; //son elemanın pozisyonu
        if (index == 0)
        {
            newPos.z += .5f;
        }
        newPos.z += .7f; //boşluk kalmadan sıralamak için 1 birim ileri aldık.
        newPos.y = -3.50f;
        other.transform.localPosition = newPos;
        collected.Add(other); //listeye ekle.
        StartCoroutine(MakeObjectsBigger());
    }
    public void DestroyMoney(GameObject other, int index, GameObject obstacle)
    {
        if (index == 0)
        {
            index = 1;
        }
        int countList = collected.Count;
        for (int i = index; i < countList; i++)
        {
            GameObject gameObject = collected[i];
            //VFX
            if (i == collected.Count - 1)
            {
                Instantiate(cashDestroyedParticle, obstacle.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
            if (gameObject.CompareTag("Money"))
            {
                moneyValue--;
                moneyValueText.text = moneyValue.ToString();
            }
            else if (gameObject.CompareTag("Gold"))
            {
                moneyValue -= 2;
                moneyValueText.text = moneyValue.ToString();
            }
            else if (gameObject.CompareTag("Diamond"))
            {
                moneyValue -= 3;
                moneyValueText.text = moneyValue.ToString();
            }
            Destroy(gameObject);
        }
        collected.RemoveRange(index, collected.Count - index);
    }

    public void DistributeCollectibles(GameObject other, int index, GameObject obstacle)
    {
        if (index == 0)
        {
            index = 1;
        }
        for (int i = collected.Count - 1; i > index; i--)
        {
            GameObject gameObject = collected[i];
            collected.Remove(gameObject);
            // BallLauncher ballLauncher=new BallLauncher(gameObject,new Vector3(Random.Range(-2f, 2f), -3.6f, obstacle.transform.position.z + Random.Range(2, 20)));
            // ballLauncher.Launch();
            Destroy(gameObject.GetComponent<Collision>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = null;
            Vector3 target = new Vector3(Random.Range(-2f, 2f), -3.6f, obstacle.transform.position.z + Random.Range(2, 20));
            Vector3 targetUpPos = target - new Vector3(0, -3, (target.z - gameObject.transform.position.z) / 2);
            gameObject.transform.DOMove(targetUpPos, 0.1f).OnComplete(() =>
            {
                gameObject.transform.DOMove(target, 0.1f);
            });
            if (gameObject.CompareTag("Money"))
            {
                moneyValue--;
                moneyValueText.text = moneyValue.ToString();
                gameObject.tag = "CollectableMoney";
            }
            else if (gameObject.CompareTag("Gold"))
            {
                moneyValue -= 2;
                moneyValueText.text = moneyValue.ToString();
                gameObject.tag = "CollectableGold";
            }
            else if (gameObject.CompareTag("Diamond"))
            {
                moneyValue -= 3;
                moneyValueText.text = moneyValue.ToString();
                gameObject.tag = "CollectableDiamond";
            }
        }
    }
    public IEnumerator ATMKeepMoney(GameObject other, int index)
    {
        if (index == 0)
            index = 1;
        if (index == collected.Count - 1)
        {
            GameObject gameObject = collected[collected.Count - 1];
            Vector3 scale = gameObject.transform.localScale;
            Vector3 doScale = scale * .4f;
            collected.Remove(gameObject);
            gameObject.transform.DOScale(doScale, 0.02f).OnComplete(() =>
                     gameObject.transform.DOScale(scale, 0.02f)).OnComplete(() => { Destroy(gameObject); });

        }
        else
        {
            for (int i = collected.Count - 1; i >= index; i--)
            {
                GameObject gameObject = collected[i];
                Vector3 scale = gameObject.transform.localScale;
                Vector3 doScale = scale * .4f;
                collected.Remove(gameObject);
                gameObject.transform.DOScale(doScale, 0.06f).OnComplete(() =>
                         gameObject.transform.DOScale(scale, 0.06f)).OnComplete(() => { Destroy(gameObject); });
                yield return new WaitForSeconds(0.02f);

            }
        }

    }

    public IEnumerator MakeObjectsBigger()
    {
        for (int i = collected.Count - 1; i >= 1; i--)
        {
            int index = i;

            Vector3 scale = Vector3.zero;
            if (collected[index].gameObject.tag == "Money")
            {
                scale = new Vector3(200, 200, 200);
                Vector3 doScale = scale * 1.8f;
                collected[index].transform.DOScale(doScale, 0.06f).OnComplete(() =>
                     collected[index].transform.DOScale(scale, 0.06f));
                yield return new WaitForSeconds(0.02f);
            }
            else if (collected[index].gameObject.tag == "Gold")
            {
                scale = new Vector3(150, 130, 180);
                Vector3 doScale = scale * 1.8f;
                collected[index].transform.DOScale(doScale, 0.06f).OnComplete(() =>
                     collected[index].transform.DOScale(scale, 0.06f));
                yield return new WaitForSeconds(0.02f);
            }
            else if (collected[index].gameObject.tag == "Diamond")
            {
                scale = new Vector3(50, 50, 50);
                Vector3 doScale = scale * 1.8f;
                collected[index].transform.DOScale(doScale, 0.06f).OnComplete(() =>
                     collected[index].transform.DOScale(scale, 0.06f));
                yield return new WaitForSeconds(0.02f);
            }

        }
    }
    public void MoveElements()
    {
        for (int i = 1; i < collected.Count; i++)
        {
            Vector3 pos = collected[i].transform.position;
            pos.x = collected[i - 1].transform.position.x;
            collected[i].transform.position = Vector3.Lerp(collected[i].transform.position, pos, movementTailDelay * Time.deltaTime);
            //collected[i].transform.DOLocalMove(pos, movementDelay);
        }
    }

    public void MoveOrigin()
    {
        for (int i = 1; i < collected.Count; i++)
        {
            Vector3 pos = collected[i].transform.position;
            pos.x = collected[0].transform.position.x;
            collected[i].transform.position = Vector3.Lerp(collected[i].transform.position, pos, movementOriginDelay * Time.deltaTime);


        }
    }
    IEnumerator UpgradeCollectableAll()
    {
        for (int i = collected.Count - 1; i >= 0; i--)
        {
            if (collected[i].gameObject.CompareTag("Money"))
            {
                collected[i].gameObject.tag = "Gold";
                collected[i].GetComponent<MeshFilter>().mesh = goldMesh;
                collected[i].GetComponent<MeshRenderer>().material = goldMaterial;
                collected[i].GetComponent<BoxCollider>().size = new Vector3(0.007f, 0.005f, 0.002f);
                collected[i].transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
                collected[i].transform.localScale = new Vector3(150, 130, 180);


                int index = i;

                Vector3 scale = collected[i].transform.localScale;
                Vector3 doScale = scale * 1.8f;

                collected[index].transform.DOScale(doScale, 0.06f).OnComplete(() =>
                     collected[index].transform.DOScale(scale, 0.06f));
                yield return new WaitForSeconds(0.02f);
            }
            else if (collected[i].gameObject.CompareTag("Gold") || collected[i].gameObject.CompareTag("Diamond"))
            {
                collected[i].gameObject.tag = "Diamond";
                collected[i].GetComponent<MeshFilter>().mesh = diamondMesh;
                collected[i].GetComponent<MeshRenderer>().material = diamondMaterial;
                collected[i].GetComponent<BoxCollider>().size = new Vector3(0.02f, 0.02f, 0.01f);
                collected[i].transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
                collected[i].transform.localScale = new Vector3(50, 50, 50);


                int index = i;

                Vector3 scale = collected[i].transform.localScale;
                Vector3 doScale = scale * 1.8f;

                collected[index].transform.DOScale(doScale, 0.06f).OnComplete(() =>
                     collected[index].transform.DOScale(scale, 0.06f));
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
    public IEnumerator UpgradeCollectable(GameObject gameObject)
    {

        if (gameObject.CompareTag("Money"))
        {
            moneyValue++;
            moneyValueText.text = moneyValue.ToString();
            gameObject.tag = "Gold";
            gameObject.GetComponent<MeshFilter>().mesh = goldMesh;
            gameObject.GetComponent<MeshRenderer>().material = goldMaterial;
            gameObject.GetComponent<BoxCollider>().size = new Vector3(0.007f, 0.005f, 0.002f);
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            gameObject.transform.localScale = new Vector3(150, 130, 180);


            Vector3 scale = gameObject.transform.localScale;
            Vector3 doScale = scale * 1.8f;

            gameObject.transform.DOScale(doScale, 0.06f).OnComplete(() =>
                 gameObject.transform.DOScale(scale, 0.06f));
            yield return new WaitForSeconds(0.02f);
        }
        else if (gameObject.CompareTag("Gold") || gameObject.CompareTag("Diamond"))
        {
            moneyValue++;
            moneyValueText.text = moneyValue.ToString();
            gameObject.tag = "Diamond";
            gameObject.GetComponent<MeshFilter>().mesh = diamondMesh;
            gameObject.GetComponent<MeshRenderer>().material = diamondMaterial;
            gameObject.GetComponent<BoxCollider>().size = new Vector3(0.02f, 0.02f, 0.01f);
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            gameObject.transform.localScale = new Vector3(50, 50, 50);



            Vector3 scale = gameObject.transform.localScale;
            Vector3 doScale = scale * 1.8f;

            gameObject.transform.DOScale(doScale, 0.06f).OnComplete(() =>
                 gameObject.transform.DOScale(scale, 0.06f));
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void FinishCollecterMoney(GameObject gameObject)
    {
        gameObject.tag = "Untagged";
        gameObject.transform.parent = null;
        collected.Remove(gameObject);
        gameObject.transform.DOMove(finishTarget.transform.position, targetToMoveDelay);
    }
    IEnumerator GameFinished()
    {
        playerPrefab.transform.parent=null;
        for (int i = 0; i < moneyValue; i++)
        {
            GameObject go = Instantiate(cashPrefab, new Vector3(0, -3.61f+(i * 0.4f), 205), Quaternion.Euler(new Vector3(-90, 90, 0)));
            go.tag = "Untagged";
            playerPrefab.transform.position = new Vector3(0, go.transform.position.y+.06f, 204.73f);
            yield return new WaitForSeconds(finishDelay);
        }
    }

}
