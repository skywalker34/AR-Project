using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] routeSteps;
    public List<Transform> stepList = new List<Transform>();
    //public List<Transform> eels = new List<Transform>();
    //public List<Transform> eelsResult = new List<Transform>();
    //public List<Transform> escalators = new List<Transform>();
    //public List<Transform> escalatorsResult = new List<Transform>();


    public int[,] eels = new int[5, 2]
    { {14, 8 }, {28, 15}, {35, 25}, {38, 28}, {51, 34}};

    public int[,] escalators = new int[4, 2]
    { {3, 18}, {13, 30}, {23, 42}, {37, 48}};

    private void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        FillNodes();

        for(int i = 0; i < stepList.Count; i++)
        {
            Vector3 currentPos = stepList[i].position;
            if(i>0)
            {
                Vector3 prevPos = stepList[i-1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        stepList.Clear();

        routeSteps = GetComponentsInChildren<Transform>();

        foreach(Transform t in routeSteps)
        {
            if(t != this.transform)
            {
                stepList.Add(t);
            }
        }
    }
}
