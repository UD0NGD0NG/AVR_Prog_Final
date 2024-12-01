using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriorityQueue;
using System.Linq;
using System.Runtime.ExceptionServices;
using System;

public class CalculateDistance : MonoBehaviour
{
    private Dictionary<int, GameObject> idToTarget = new Dictionary<int, GameObject>();
    private Dictionary<GameObject, int> targetToId = new Dictionary<GameObject, int>();
    private HashSet<GameObject> targets;

    private PQ pq;
    private const double INF = double.MaxValue;
    private int size;
    private List<double> dist;
    private List<List<(double, int)>> edge;
    private List<int> prev;
    private int departureNodeId, arrivalNodeId;

    void Awake()
    {
        
    }

    void Start()
    {
        targets = TargetManager.instance.targets;
        size = targets.Count;

        int nodeID = 1;
        foreach (GameObject target in targets)
        {
            idToTarget.Add(nodeID, target);
            targetToId.Add(target, nodeID++);
        }

        pq = new PQ();

        MakeGraph();
        SetDepArr();
        Dijkstra();
        TargetManager.instance.getShortenPath(dist[arrivalNodeId]);
        PrintShortenPath();
    }

    private void MakeGraph()
    {
        dist = Enumerable.Repeat(INF, size + 1).ToList();
        edge = new List<List<(double, int)>>(size + 1);
        for (int i = 0; i <= size; i++)
        {
            edge.Add(new List<(double, int)>());
        }

        for (int i = 1; i <= size; i++)
        {
            for (int j = 1; j <= size; j++)
            {
                if (i == j) continue;

                double d = Vector3.Distance(idToTarget[i].transform.position, idToTarget[j].transform.position);
                edge[i].Add((d, j));
            }
        }
    }

    private void SetDepArr()
    {
        departureNodeId = UnityEngine.Random.Range(1, size + 1);
        
        do
        {
            arrivalNodeId = UnityEngine.Random.Range(1, size + 1);
        } while (departureNodeId == arrivalNodeId);
    }

    private void Dijkstra()
    {
        prev = Enumerable.Repeat(-1, size + 1).ToList();
        dist[departureNodeId] = 0;
        for (int i = 1; i <= size; i++)
        {
            pq.push(-dist[i], i);
        }

        while (!pq.empty)
        {
            int x = pq.top().Item2;
            pq.pop();
            foreach (var y in edge[x])
            {
                if (dist[y.Item2] != Math.Min(dist[y.Item2], dist[x] + y.Item1))
                {
                    dist[y.Item2] = Math.Min(dist[y.Item2], dist[x] + y.Item1);
                    prev[y.Item2] = x;
                    pq.push(-dist[y.Item2], y.Item2);
                }
            }
        }
    }

    private void PrintShortenPath()
    {
        Stack<int> path = new Stack<int>();

        for (int node = arrivalNodeId; node != -1; node = prev[node])
        {
            path.Push(node);
        }

        Debug.Log(idToTarget[departureNodeId].name + " -> " + idToTarget[arrivalNodeId].name);

        string res = "";
        while (path.Count > 1)
        {
            res += idToTarget[path.Pop()].name + " -> ";
        }
        res += idToTarget[path.Pop()].name;

        Debug.Log(res);
    }
}
