using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _scoreCount;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            _scoreCount += 1;
            scoreText.text = scoreText.gameObject.name+" score: "+_scoreCount.ToString();
        }
    }
}
