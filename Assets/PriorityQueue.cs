using System.Collections.Generic;
using UnityEngine;

namespace PriorityQueue
{
    public class PQ
    {
        private List<(double, int)> arr;

        public PQ()
        {
            arr = new List<(double, int)>() { (0, 0) };
        }

        public void push(double weight, int arrivalNode)
        {
            arr.Add((weight, arrivalNode));
            upHeap(size);
        }

        public (double, int) top()
        {
            if (empty) return (0, 0);

            return arr[1];
        }

        public void pop()
        {
            if (empty) return;

            swap(1, size);
            arr.RemoveAt(arr.Count - 1);
            downHeap(1);
        }

        public int size => arr.Count - 1; // Lamda

        public bool empty => size == 0; // Lamda

        private void upHeap(int idx)
        {
            if (idx == 1) return;

            int parent = idx / 2;
            if (!Compare(arr[parent], arr[1]))
            {
                swap(parent, idx);
                upHeap(parent);
            }
        }

        private void downHeap(int idx)
        {
            int left = 2 * idx;
            int right = 2 * idx + 1;
            int child;

            if (left > size) return;
            else if (left == size)
            {
                child = left;
            }
            else
            {
                if (Compare(arr[left], arr[right])) child = left;
                else child = right;
            }

            if (!Compare(arr[idx], arr[child]))
            {
                swap(child, idx);
                downHeap(child);
            }
        }

        private void swap(int idx1, int idx2)
        {
            arr[0] = arr[idx1];
            arr[idx1] = arr[idx2];
            arr[idx2] = arr[0];
        }

        private bool Compare((double, int) node1, (double, int) node2)
        {
            return node1.Item1 < node2.Item1;
        }
    }
}