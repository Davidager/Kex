using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExampleInfluences {
    private float[] subjPosArray;
    private int influences;
    private ArrayList path;
    public float speed;
    private float influenceSum;
    private float maxInfluence;
    public Spline exampleSpline;
    
    private float currentixCoord;
    private float currentizCoord;

    public ExampleInfluences()
    {
        subjPosArray = new float[40];
        path = new ArrayList(82);
        influences = new int();

    }

    // inte klart ännu men!
    public void calculateInfluences(float jxCoord, float jzCoord)
    {
        float distance = (float)Math.Sqrt((jxCoord - currentixCoord) * (jxCoord - currentixCoord) +
            (jzCoord - currentizCoord) * (jzCoord - currentizCoord));
        float influenceFj;
        if (jInFrontofi)
        {
            influenceFj = (float)Math.Exp(-speed * Math.Pow(distance, 2) / (2));
        }
        
    }

    public void savejPositions(float jxCoord, float jzCoord)
    {
         
    }
    public void saveToPath(float ixCoord, float izCoord)
    {
        path.Add(ixCoord);
        path.Add(izCoord);
        this.currentixCoord = ixCoord;
        this.currentizCoord = izCoord;
    }


}
