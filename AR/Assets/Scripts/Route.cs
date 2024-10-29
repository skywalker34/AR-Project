using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] routeSteps;
    public List<Transform> stepList = new List<Transform>();

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
