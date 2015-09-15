using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Seasons : MonoBehaviour
{

    public Sprite[] handles;
    public Slider slider;
    public Image handleImage;
    float timer;
    public static int currentSeason;

    public const int SUMMER = 0;
    public const int FALL = 1;
    public const int WINTER = 2;
    public const int SPRING = 3;

    public const int NUMBER_SEASONS = 4;
    public const int DURATION = 60;


    void Start()
    {
        currentSeason = SUMMER;
        handleImage.sprite = handles[currentSeason];
        timer = 0;
    }

    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= DURATION)
        {
            ChangeHandle();
        }
        slider.value = (float)timer / DURATION * 100;
    }

    void ChangeHandle()
    {
        currentSeason = (currentSeason+1)%4;
        handleImage.sprite = handles[currentSeason];
        timer = 0;
    }

    public static int GetCurrentSeason()
    {
        return currentSeason;
    }
}
