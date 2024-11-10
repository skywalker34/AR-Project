using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPieceMover : MonoBehaviour
{
    public Route currentRoutePos;

    int routePos;

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
            Vector3 nextPos = currentRoutePos.stepList[routePos + 1].position;
            while (MoveToNextSpace(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            steps--;
            routePos++;
        }
        isMoving=false;

        if(steps == 0)
        {
            enemyTurn = false;
            player.playerTurn = true;
        }
    }

    bool MoveToNextSpace(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 10.0f*Time.deltaTime));
    }
}
