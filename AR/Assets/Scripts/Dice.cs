using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    int value = 1;

    public void RollDice()
    {
        value = Random.Range(1, value + 1);
    }
}

