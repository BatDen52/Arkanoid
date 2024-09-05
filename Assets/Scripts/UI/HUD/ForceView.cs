using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceView : CountView
{
    private Ball _ball;

    public void SetBall(Ball ball)
    {
        _ball = ball;

        _ball.Died += OnDied;
        _ball.ForceChenged += OnCountChanged;
    }

    private void OnDied(Ball obj)
    {
        OnCountChanged(0);

        _ball.Died -= OnDied;
        _ball.ForceChenged -= OnCountChanged;
    }
}
