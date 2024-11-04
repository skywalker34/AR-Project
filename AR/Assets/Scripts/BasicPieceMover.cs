using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPieceMover : MonoBehaviour
{
    public Route currentRoutePos;

    int routePos;

    public int steps;

    bool isMoving;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            steps = Random.Range(1, 7);
            Debug.Log("Dice Shouldn't be here but i'll let vincent figure that out");

            if(routePos + steps < currentRoutePos.stepList.Count)
            {
                StartCoroutine(Move());
            }
            else
            {
                Debug.Log("Oh shit");
            }
        }
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {
            Vector3 nextPos = currentRoutePos.stepList[routePos + 1].position;
            while (MoveToNextSpace(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            steps--;
            routePos++;
        }

        isMoving = false;
    }

    bool MoveToNextSpace(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 10.0f*Time.deltaTime));
    }
}
