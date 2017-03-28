using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingFunctions{

    Agent infFactork;
    Agent infFactorj;
    float AffinityValue;
    //alla infAgents i configuration måste ha lokala parametrar
    MatchingFunctions(Configuration query, Configuration comparisment)
    {
        foreach(Agent infAgentk in query.infAgentList)
        {
            foreach(Agent infAgentj in comparisment.infAgentList)
            {
                AffinityFunction(infAgentk, infAgentj);
            }
        }
    }
}
    
public float AffinityFunction(Agent querySub, Agent k, Agent j) {
    private float newXcoord;
    private float newZcoord;
    private float newSpeed;
    private float newDirection;
    private float simVal;
    private float gauss1;
    private float gauss2;
    private float gauss3;
    private float[] xCoordListCopyk;
    private float[] zCoordListCopyk;
    private float[] speedListCopyk;
    private float[] directionListCopyk;
    private float[] xCoordListCopyj;
    private float[] zCoordListCopyj;
    private float[] speedListCopyj;
    private float[] directionListCopyj;
    private float[] speedListCopySubject;
    private float[] simVals = new float[40];
    private float Aff;
    
    FillConfig(k);
    FillConfig(j);
    k.xCoordList.CopyTo(xCoordListCopyk,0)
    k.yCoordList.CopyTo(yCoordListCopyk,0)
    k.speedList.CopyTo(speedListCopyk,0)
    k.directionList.CopyTo(directionListCopyk,0)
    j.xCoordList.CopyTo(xCoordListCopyj,0)
    j.yCoordList.CopyTo(yCoordListCopyj,0)
    j.speedList.CopyTo(speedListCopyj,0)
    j.directionList.CopyTo(directionListCopyj,0)
    querySub.speedList.CopyTo(speedListCopySubject,0)
    
    for (int i = 0; i<40; i++)
    {
        gauss1 = Math.Exp((((CoordListCopyk(i)-xCoordListCopyj(i))^2)+((yCoordListCopyk(i)-yCoordListCopyj(i))^2))/(1-speedListCopySubject(i)));
        gauss2 = Math.Exp(((speedListCopyk(i)-speedListCopyj(i))^2)/(1-speedListCopySubject(i)));
        gauss3 = Math.Exp(((directionListCopyk(i)-directionListCopyj(i))^2)/(1-speedListCopySubject(i)))
        simVal = gauss1 * gauss2 * gauss3;
        simVals(i)=simVal
    }
    Aff = Average(simVals);
    return Aff
    
}

 public void FillConfig(Agent agent)
{
    while (agent.xCoordList.Count() < 40)
    {
        newXcoord = agent.xCoordList.Last() + agent.speedList.Last() * Math.Cos(agent.directionList.Last());
        newYcoord = agent.yCoordList.Last() + agent.speedList.Last() * Math.Sin(agent.directionList.Last());
        newSpeed = agent.speedList.Last();
        newDirection = agent.directionlist.Last();
        agent.xCoordList.AddLast(newXcoord);
        agent.yCoordList.AddLast(newYcoord);
        agent.speedList.AddLast(newSpeed);
        agent.directionList.AddLast(newDirection);
        
    }
}

public float Average(float[] floatArray)
{
    int result = 0;
    for (int i = 0; i < floatArray.Length; i++)
    {
        result += Floatarray;
    }
    result = result / floatArray.Length;
    return result;
}
