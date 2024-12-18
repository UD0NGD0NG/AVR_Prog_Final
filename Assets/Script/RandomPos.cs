using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomPos : MonoBehaviour
{
    private Dictionary<int, GameObject> idToTarget;
    private Dictionary<GameObject, int> targetToId;
    private HashSet<GameObject> targets;
    private int size;

    private int startNodeID;
    private int endNodeID;

    public GameObject player;
    public Text info;
    public Text goal;
    public Text resultText;
    public Button restartButton;
    public Button exitButton;

    private double timer = 0.0;
    private bool isPlaying = true;


    void Start()
    {
        idToTarget = TargetManager.instance.idToTarget;
        targetToId = TargetManager.instance.targetToId;
        targets = TargetManager.instance.targets;
        size = targets.Count;

        startNodeID = Random.Range(1, size + 1);
        endNodeID = startNodeID;
        while (endNodeID == startNodeID)
        {
            endNodeID = Random.Range(1, size + 1);
        }

        player.transform.position = idToTarget[startNodeID].transform.position;

        goal.text = "Go to\n" + idToTarget[endNodeID].name;
    }

    void Update()
    {
        info.text = "Your current location is\n" + idToTarget[TargetManager.instance.curNodeID].name;

        if (isPlaying)
        {
            timer += Time.deltaTime;
            if (TargetManager.instance.curNodeID == endNodeID)
            {
                Debug.Log("Clear");
                isPlaying = false;
                showResult();
            }
        }
    }

    void showResult()
    {
        resultText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

        resultText.text = "Escape in " + timer + " seconds!";
    }
}