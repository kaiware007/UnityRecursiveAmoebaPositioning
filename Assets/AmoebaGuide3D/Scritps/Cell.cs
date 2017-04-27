using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Cell {

    const float PI_HALF = Mathf.PI / 2f;

    /// <summary>
    /// 細胞分裂開始
    /// </summary>
    /// <param name="levelMax"></param>
    /// <param name="numX"></param>
    /// <param name="numY"></param>
    /// <param name="startRadius"></param>
    /// <param name="angleRandomRange"></param>
    /// <param name="dividePer"></param>
    /// <param name="radiusDecayRate"></param>
    /// <param name="act"></param>
    public static void CellDivision(int levelMax, int numX, int numY, float startRadius, float angleRandomRange, float dividePer, float radiusDecayRate, Action<int, int, Vector3, float> act)
    {
        Vector3 p;

        act(0, levelMax, Vector3.zero, startRadius);

        float randAngle = angleRandomRange * 0.25f * Mathf.Deg2Rad;

        for (int x = 0; x < numX; x++)
        {
            for (int y = 1; y < numY; y++)
            {
                float radX = (float)x / numX * 2f * Mathf.PI + (UnityEngine.Random.value * 2f - 1.0f) * randAngle;
                float radY = (float)y / numY * Mathf.PI - PI_HALF + (UnityEngine.Random.value * 2f - 1.0f) * randAngle;
                //float radX = (float)x / numX * 2f * Mathf.PI;
                //float radY = (float)y / numY * Mathf.PI - PI_HALF;
                p = KUtil.Utility.GetPositionOnSphere(radY, radX, startRadius);

                CellIteration(1, levelMax, numX, numY, startRadius, angleRandomRange, dividePer, radiusDecayRate, p, radX, radY, startRadius * radiusDecayRate, act);
            }
        }
    }

    /// <summary>
    /// 細胞分裂再帰処理
    /// </summary>
    /// <param name="lv"></param>
    /// <param name="lvMax"></param>
    /// <param name="numX"></param>
    /// <param name="numY"></param>
    /// <param name="startRadius"></param>
    /// <param name="angleRandomRange"></param>
    /// <param name="dividePer"></param>
    /// <param name="radiusDecayRate"></param>
    /// <param name="pos"></param>
    /// <param name="radX"></param>
    /// <param name="radY"></param>
    /// <param name="r"></param>
    /// <param name="act"></param>
    static void CellIteration(int lv, int lvMax, int numX, int numY, float startRadius, float angleRandomRange, float dividePer, float radiusDecayRate, Vector3 pos, float radX, float radY, float r, Action<int, int, Vector3, float> act)
    {
        if (lv >= lvMax - UnityEngine.Random.Range(0, lvMax / 4 + 1)) return;

        act(lv, lvMax, pos, r);

        int count = 1;

        // 分裂判定
        if (UnityEngine.Random.value < dividePer)
        {
            count++;
        }
        Vector3 p;
        float rr = r * radiusDecayRate;
        float randAngle = angleRandomRange * Mathf.Deg2Rad;

        for (int i = 0; i < count; i++)
        {
            float rx = radX + (UnityEngine.Random.value * 2f - 1.0f) * randAngle;
            float ry = radY + (UnityEngine.Random.value * 2f - 1.0f) * randAngle;

            p = KUtil.Utility.GetPositionOnSphere(ry, rx, r) + pos;

            CellIteration(lv+1, lvMax, numX, numY, startRadius, angleRandomRange, dividePer, radiusDecayRate, p, rx, ry, rr, act);
        }
    }

}
