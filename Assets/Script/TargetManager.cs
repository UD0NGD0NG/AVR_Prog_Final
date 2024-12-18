using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;

    public readonly Dictionary<int, GameObject> idToTarget = new Dictionary<int, GameObject>();
    public readonly Dictionary<GameObject, int> targetToId = new Dictionary<GameObject, int>();
    public readonly HashSet<GameObject> targets = new HashSet<GameObject>();

    public int curNodeID;


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

    void Update()
    {
        
    }

    public void getShortenPath(double dist)
    {
        Debug.Log(dist);
    }

    public void setCurNodeID(int id)
    {
        curNodeID = id;
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
