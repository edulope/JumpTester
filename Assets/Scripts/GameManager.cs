using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int currentGold;
    public bool [] redCoins;
    public int numberRedCoins;
    public TMP_Text goldText;
    public TMP_Text redCoinsText;


    void Start()
    {
        currentGold = 0;
        List<bool> redCoinsList = new List<bool>();
        redCoinsText.text = "";
        for(int i = 0; i < numberRedCoins; i++)
        {
            redCoinsList.Add(false);
            redCoinsText.text = redCoinsText.text + "X";
        }
        redCoins = redCoinsList.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int value){
        currentGold = currentGold + value;
        goldText.text = "Coins: " + currentGold + "";
    }

    public void gotRedCoin(int index){
        redCoins[index] = true;
        redCoinsText.text = redCoinsText.text.Remove(index, 1).Insert(index, "O");
    }
}
