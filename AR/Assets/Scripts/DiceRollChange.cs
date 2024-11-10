using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollChange : MonoBehaviour
{
    public Image image;
    public List<Sprite> diceChoices;

    public int currentDiceRoll;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        image.sprite = diceChoices[0];

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DiceRoll()
    {
        currentDiceRoll = Random.Range(1, 7);
        image.sprite = diceChoices[currentDiceRoll - 1];
        isMoving = true;
    }
}
