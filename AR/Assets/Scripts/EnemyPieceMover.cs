using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPieceMover : MonoBehaviour
{
    public Route currentRoutePos;

    public int routePos;

    public int steps;

    bool isMoving;

    //CIRCULAR DEPENDENCY
    public BasicPieceMover player;

    public bool enemyTurn;

    // Update is called once per frame
    void Update()
    {
        if(!player.playerTurn && steps == 0 && !isMoving)
        {
            steps = Random.Range(1, 7);

            if (routePos + steps < currentRoutePos.stepList.Count)
            {
                StartCoroutine(Move());
            }
            else
            {
                
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
            if(routePos == currentRoutePos.stepList.Count - 1)
            {
                steps = 0;
                Debug.LogWarning("AI Wins");
                break;
            }
            Vector3 nextPos = currentRoutePos.stepList[routePos + 1].position;
            while (MoveToNextSpace(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            steps--;
            routePos++;
        }
        isMoving=false;

        if(steps == 0 && enemyTurn && routePos != currentRoutePos.stepList.Count - 1)
        {
            if(!IsInvoking("SpaceJudge"))
            {
                Debug.LogWarning("I SHOULD BE INVOKING");
                Invoke("SpaceJudge", 0.1f);
            }

            enemyTurn = false;
            player.playerTurn = true;
        }
    }

    bool MoveToNextSpace(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 10.0f*Time.deltaTime));
    }

    void SpaceJudge()
    {
        for (int i = 0; i <= currentRoutePos.eels.GetLength(0) - 1; i++)
        {
            //Debug.LogWarning(i + " " + currentRoutePos.eels2[i, 0] + " " + currentRoutePos.eels2[i, 1]);
            //Debug.LogWarning(routePos + " " + currentRoutePos.eels2[i, 0]);
            //Debug.LogWarning(currentRoutePos.stepList.Count);
            if (routePos == currentRoutePos.eels[i, 0])
            {
                //Debug.LogWarning(currentRoutePos.stepList[currentRoutePos.eels2[i, 1]]);
                Vector3 nextPos = currentRoutePos.stepList[currentRoutePos.eels[i, 1]].position;
                transform.position = nextPos;
                routePos = currentRoutePos.eels[i, 1];
            }
        }

        for (int i = 0; i <= currentRoutePos.escalators.GetLength(0) - 1; i++)
        {
            //Debug.LogWarning(i + " " + currentRoutePos.escalators[i, 0] + " " + currentRoutePos.escalators[i, 1]);
            if (routePos == currentRoutePos.escalators[i, 0])
            {
                Vector3 nextPos = currentRoutePos.stepList[currentRoutePos.escalators[i, 1]].position;
                transform.position = nextPos;
                routePos = currentRoutePos.escalators[i, 1];
            }
        }
    }
}
