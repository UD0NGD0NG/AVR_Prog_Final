using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;

    public readonly Dictionary<int, GameObject> idToTarget = new Dictionary<int, GameObject>();
    public readonly Dictionary<GameObject, int> targetToId = new Dictionary<GameObject, int>();
    public readonly HashSet<GameObject> targets = new HashSet<GameObject>();

    public int curNodeID;

    public Canvas escape;
    public Canvas cameraCanvas;
    private bool ESC = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GameObject[] t = GameObject.FindGameObjectsWithTag("Target");
        foreach (var x in t)
        {
            targets.Add(x);
        }

        int nodeID = 1;
        foreach (GameObject target in targets)
        {
            idToTarget.Add(nodeID, target);
            targetToId.Add(target, nodeID);
            nodeID++;
        }
    }

    void Start()
    {
        
    }

    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESC = !ESC;
            await Wait(1);
            escape.transform.position = cameraCanvas.transform.position + cameraCanvas.transform.forward * 70;
            escape.transform.rotation = cameraCanvas.transform.rotation;
            escape.gameObject.SetActive(ESC);
        }
    }

    public void setCurNodeID(int id)
    {
        curNodeID = id;
    }

    public void ChangeScene(string name)
    {
        if (name == "StartScene")
        {
            ESC = false;
        }
        SceneManager.LoadScene(name);
    }

    public async Task Wait(int sec)
    {
        await Task.Delay(sec * 1000);

    }
}
