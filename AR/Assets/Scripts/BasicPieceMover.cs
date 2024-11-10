using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPieceMover : MonoBehaviour
{
    public Route currentRoutePos;

    private Coroutine myCoroutine;

    int routePos;

    public int steps;

    bool hasSetNewStep = false;

    public DiceRollChange dice;

    private void Update()
    {

        if (dice.isMoving)
        {
            if (myCoroutine == null)
            {
                hasSetNewStep = false;
            }
            if (!hasSetNewStep)
            {
                steps = dice.currentDiceRoll;
                hasSetNewStep = true;
                Debug.Log("steps");
                Debug.Log(steps);
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
    }

    bool MoveToNextSpace(Vector3 goal)
    {
        //hasSetNewStep = true;
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 10.0f * Time.deltaTime));
    }
}
