using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameIF : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score= 0;
    
    void Start()
    {
        
        scoreText.text = score.ToString();
        
    }

    
   public void IncreaseScore()
    {
        score += 10;
        scoreText.text = score.ToString();        
    }

   
}
