using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollChange : MonoBehaviour
{
     public Image image;
     public List<Sprite> diceChoices;

     private int counter;
     private int currentDiceRoll;
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
         currentDiceRoll = Random.Range(0,6);

          image.sprite = diceChoices[currentDiceRoll];
          
    }
}
