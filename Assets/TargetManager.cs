using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;

    public readonly HashSet<GameObject> targets = new HashSet<GameObject>();

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
}
