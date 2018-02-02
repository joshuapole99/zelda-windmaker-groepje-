using UnityEngine;
using System.Collections;

//*********************************************************************************//
// Class: TimerVar
// Created By: Chuck (McMayhem) McMackin
//*********************************************************************************//
// The Timer Var class is used as a simple timer for anyting that requires timed 
// activation or intervals. This is used to replace the general means of timers which
// can cause convoluted code and poor management. The timer var must be updated in the 
// update function of any script it is attached to. For this reason, it can only be
// placed in MonoBehavior-based scripts.
//*********************************************************************************//

[System.Serializable]
public class TimerVar
{

    public float TimerSet; //The initially set time. This will be used to reset CurTime if no paramater is passed to Reset()
    public bool TimerDone; //Is the timer done?
    public bool Looping; //Should this timer automatically reset when finished?
    public float TimerSpeed; //The speed at which the timer counts down. (Multiplicative: .5f = 50% normal speed | 1.5f = 150% normal speed)
    public float CurTime; //Simple reference to the time remaining [_timer]
    public float Percent; //Percent (in whole numbers (0.0 - 100.0)

    private float _timer; //Current time of the timer. Not to be confused with the TimerSet variable

    public TimerVar(float tSet, bool loop)
    {
        TimerSet = tSet;
        Looping = loop;
        TimerDone = false;
        _timer = TimerSet;
    }

    public TimerVar(float tSet, float tSpeed, bool loop)
    {
        TimerSet = tSet;
        TimerSpeed = tSpeed;
        Looping = loop;
        TimerDone = false;
        _timer = TimerSet;
    }


    public void TimerUpdate()
    {
        if (_timer > 0)
        {
            if (TimerSpeed > 0)
            {
                _timer -= Time.deltaTime * TimerSpeed;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }

        if (_timer < 0)
        {
            _timer = 0;
        }

        CurTime = _timer;
        Percent = (CurTime / TimerSet) * 100;

        if (_timer == 0)
        {
            TimerDone = true;
            if (Looping)
            {
                Reset();
            }
        }

    }

    public void Reset()
    {
        Percent = 0;
        TimerDone = false;
        _timer = TimerSet;
    }

    public void Reset(float num)
    {
        TimerSet = num;
        TimerDone = false;
        _timer = TimerSet;
    }

    public void Reset(float num, float speed)
    {
        TimerDone = false;
        _timer = num;
        TimerSpeed = speed;

    }
}
