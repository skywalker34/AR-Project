using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPieceMover : MonoBehaviour
{
    public Route currentRoutePos;

    private Coroutine myCoroutine;

    public int routePos;

    public int steps;

    bool hasSetNewStep = false;

    public DiceRollChange dice;

    //CIRCULAR DEPENDENCY
    public EnemyPieceMover AI;

    public bool playerTurn = true;
   
    private void Update()
    {

        if (dice.isMoving && !AI.enemyTurn)
        {
            if (myCoroutine == null)
            {
                hasSetNewStep = false;
            }
            if (!hasSetNewStep)
            {
                steps = dice.currentDiceRoll;
                hasSetNewStep = true;
            }

            if (routePos + steps < currentRoutePos.stepList.Count)
            {
                myCoroutine = StartCoroutine(Move());
            }
        }
    }

    IEnumerator Move()
    {
        if (!dice.isMoving)
        {
            yield break;
        }
        dice.isMoving = true;

        while (steps > 0)
        {
            if (routePos == currentRoutePos.stepList.Count - 1)
            {
                steps = 0;
                Debug.LogWarning("Player Wins");
                break;
            }
            Vector3 nextPos = currentRoutePos.stepList[routePos + 1].position;
            while (MoveToNextSpace(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            if (steps > 0)
            {
                steps--;
                routePos++;
            }
        }

        dice.isMoving = false;
        myCoroutine = null;

        if(steps == 0 && playerTurn && routePos != currentRoutePos.stepList.Count - 1)
        {
            if (!IsInvoking("SpaceJudge"))
            {
                Invoke("SpaceJudge", 0.1f);
            }
            playerTurn = false;
            AI.enemyTurn = true;
        }
    }

    bool MoveToNextSpace(Vector3 goal)
    {
        //hasSetNewStep = true;
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 10.0f * Time.deltaTime));
    }

    void SpaceJudge()
    {

        for (int i = 0; i <= currentRoutePos.eels.GetLength(0) - 1; i++)
        {
            //Debug.LogWarning(i + " " + currentRoutePos.eels2[i, 0] + " " + currentRoutePos.eels2[i, 1]);
            //Debug.LogWarning(routePos + " " + currentRoutePos.eels2[i, 0]);
            Debug.LogWarning(currentRoutePos.stepList.Count);
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
