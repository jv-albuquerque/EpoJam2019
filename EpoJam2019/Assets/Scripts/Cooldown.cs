﻿using UnityEngine;

public class Cooldown
{

    private float time; // time length of the cooldown
    private float timer; // the variable used to count

    // PAUSE VARIABLES
    private bool paused = false; // the cooldown is paused?
    private float pauseGap = 0; // store the amount remainder to finish
    private float pausePercent; // store the percent of the cooldown when was paused
    private float pauseTimeLeft; // store the time left of the cooldown when was paused


    /// <summary>
    /// Constructor of the class with param
    /// _time: is the cooldown time
    /// </summary>
    public Cooldown(float _time)
    {
        time = _time;
        timer = 0;
    }

    /// <summary>
    /// Set the cooldown to a new time
    /// _time: is the new time of the cooldown
    /// </summary>
    public void SetTime(float _time)
    {
        time = _time;
    }

    /// <summary>
    /// Return if the cooldown is finished
    /// </summary>
    public bool IsFinished
    {
        get {
            if (paused)
                return false;

            if (Time.time - timer >= 0)
                return true;
            return false;
        }
        
    }

    /// <summary>
    /// Reset the cooldown
    /// </summary>
    public void Start()
    {
        if (paused)
            paused = false;

        timer = time + Time.time;
    }

    /// <summary>
    /// add time to the cooldown
    /// exemple: if something make the cooldown bigger
    /// </summary>
    public void AddTime(float _time)
    {
        timer += _time;
    }

    /// <summary>
    /// Return the percent of the cooldown
    /// </summary>
    public float Percent
    {
        get
        {
            if (paused)
                return pausePercent;

            if (this.IsFinished)
                return 100;

            return 100 - (((timer - Time.time) / time) * 100);
        }
    }

    /// <summary>
    /// Return how many time is left to finish the cooldown
    /// </summary>
    //public float TimeLeft()
    //{
    //    if (paused)
    //        return pauseTimeLeft;

    //    if (this.IsFinished())
    //        return 0;

    //    return (timer - Time.time);
    //}

    public float TimeLeft {
        get {
            if (paused)
                return pauseTimeLeft;

            if (this.IsFinished)
                return 0;

            return (timer - Time.time);
        }
    }

    /// <summary>
    /// Pause the cooldown
    /// </summary>
    public void Pause()
    {
        if (this.IsFinished)
            return;

        pauseGap = timer - Time.time;
        pausePercent = this.Percent;
        pauseTimeLeft = this.TimeLeft;
        paused = true;
    }

    /// <summary>
    /// Reset from pause
    /// </summary>
    public void Restart()
    {
        if (paused)
            paused = false;

        timer = time + Time.time;
    }

    /// <summary>
    /// Force the cooldown to finish
    /// </summary>
    public void ForceFinish()
    {
        timer = 0;
        paused = false;
    }

    /// <summary>
    /// Reset the cooldown to the zero percent
    /// </summary>
    public void Reset()
    {
        this.Start();
    }

    /// <summary>
    /// Cooldown stop to work
    /// </summary>
    public void Stop()
    {
        paused = true;
    }

}