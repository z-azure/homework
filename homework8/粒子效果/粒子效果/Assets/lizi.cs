using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve
{
    private float x = 0.0f;
    private float y = 0.0f;

    private float z = 0.0f;

    public float alpha, belta;

    public float angle;

    public Curve(float angle)
    {
        this.angle = angle;
    }

    public float getX()
    {
        return x;
    }

    public float getY()
    {
        return y;
    }

    public void Draw()
    {
        float t = angle / 180.0f * Mathf.PI;
        float[] randomArr = { 0.4f, 0.5f, 0.6f, 0.7f };
        float a = randomArr[Random.Range(0, randomArr.Length)];
        x = a * (16 * Mathf.Pow(Mathf.Sin(t), 3));
        y = a * (13 * Mathf.Cos(t) - 5 * Mathf.Cos(2 * t) - 2 * Mathf.Cos(3 * t) - Mathf.Cos(4 * t));
    }


    public void draw_3d(float changeangle)
    {
        float tempangle = changeangle / 180f * Mathf.PI;
        float the = angle / 180f * Mathf.PI;
        float p = 5f * Mathf.Sin(2f * the);
        x = p * Mathf.Cos(the - tempangle);
        y = p * Mathf.Sin(the - tempangle);
    }
}
