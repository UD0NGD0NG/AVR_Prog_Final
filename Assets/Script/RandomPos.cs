using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

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

    public Canvas result;
    public Text resultText;
    public GameObject cameraCanvas;

    private double timer = 0.0;
    private bool isPlaying = true;


    void Start()
    {
        idToTarget = TargetManager.instance.idToTarget;
        targetToId = TargetManager.instance.targetToId;
        targets = TargetManager.instance.targets;
        size = targets.Count;

        startNodeID = Random.Range(1, size + 1);
        TargetManager.instance.curNodeID = startNodeID;
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
                isPlaying = false;
                ShowResult();
            }
        }
    }

    async void ShowResult()
    {
        result.gameObject.SetActive(true);

        await WaitForStop();
        result.transform.position = cameraCanvas.transform.position + cameraCanvas.transform.forward * 50;
        result.transform.rotation = cameraCanvas.transform.rotation;

        resultText.text = "Escape in " + timer + " seconds!";
    }

    async Task WaitForStop()
    {
        await Task.Delay(700);

    }
}