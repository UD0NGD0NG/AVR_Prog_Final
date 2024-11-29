using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCam : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject player;
    public GameObject GVR;

    private Vector3 distanceToPlayer;

    void Start()
    {
        distanceToPlayer = player.transform.position - transform.position;
        GVR.SetActive(true);
    }

    void Update()
    {
        player.transform.rotation = mainCam.transform.rotation;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position - mainCam.transform.TransformDirection(distanceToPlayer);
        GVR.transform.position = player.transform.position - mainCam.transform.TransformDirection(distanceToPlayer);
    }
}
