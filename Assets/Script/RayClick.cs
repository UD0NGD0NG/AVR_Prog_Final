using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RayClick : MonoBehaviour
{
    public Image cursorGaugeImage;
    public GameObject mainCam;

    private float gaugeTimer = 0.0f;
    private float gazeTime = 1.5f;

    private float moveSpeed = 100.0f;
    private bool isMoving = false;
    private Vector3 goalPos;

    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;
        Vector3 forward = mainCam.transform.TransformDirection(Vector3.forward) * 1000;
        Debug.DrawRay(mainCam.transform.position, forward, Color.yellow);
        cursorGaugeImage.fillAmount = gaugeTimer;

        if (Physics.Raycast(mainCam.transform.position, forward, out hit))
        {
            if (hit.transform.tag == "Button")
            {
                gaugeTimer += 1.0f / gazeTime * Time.deltaTime;
                if (gaugeTimer >= 1.0f)
                {
                    hit.transform.GetComponent<Button>().onClick.Invoke();
                }
            }
            else if (TargetManager.instance.targets.Contains(hit.transform.gameObject) && !isMoving)
            {
                gaugeTimer += 1.0f / gazeTime * Time.deltaTime;
                if (gaugeTimer >= 1.0f)
                {
                    gaugeTimer = 0.0f;
                    goalPos = hit.transform.position;
                    isMoving = true;
                    StartCoroutine(MoveToTarget());
                }
            }
            else
            {
                gaugeTimer = 0.0f;
            }
        }
    }

    IEnumerator MoveToTarget()
    {
        while (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (TargetManager.instance.targets.Contains(other.gameObject))
        {
            TargetManager.instance.setCurNodeID(TargetManager.instance.targetToId[other.gameObject]);
            isMoving = false;
            other.gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(setCollider(other.gameObject));
        }
    }

    IEnumerator setCollider(GameObject gameObject)
    {
        while (!isMoving)
        {
            yield return null;
        }
        while (isMoving)
        {
            yield return null;
        }
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
