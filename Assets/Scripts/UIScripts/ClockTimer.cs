﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockTimer : MonoBehaviour
{
    private GameStatusManager gsm;

    public float startDay = 8;
    public float endDay = 20;
    public float daySpeed = 1;

    public float gameLengthInSeconds = 50;
    private float currentTime;
    private Image clockImage;
    public UnityEngine.Sprite[] clock;
    private bool dayEnded = false;

    private FadeToBlack ftb;
    // Start is called before the first frame update
    void Start()
    {
        gsm = FindObjectOfType<GameStatusManager>();
        ftb = FindObjectOfType<FadeToBlack>();
        clockImage = GameObject.Find("ClockImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += (Time.deltaTime / gameLengthInSeconds) * daySpeed;
        //print(currentTime);
      
        int idx = (int)Mathf.Floor(currentTime * clock.Length);
        if (idx < clock.Length)
        {
            clockImage.sprite = clock[idx];
        }
        if (currentTime >= 1 && !dayEnded)
        {
            dayEnded = true;
            print("Day Ends");
            ftb.FadeOut();
            StartCoroutine(NextDay());
        }
    }

    IEnumerator NextDay()
    {
        yield return new WaitForSeconds(3);
        // Wait 3 seconds before changing days
        gsm.DayStart();
        ftb.FadeIn();
        currentTime = 0;
        dayEnded = false;
        Debug.Log("Next Day Begins");
        
    }
}
