using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;

public class Example {
    private float[] subjPosArray;
    private int influences;
    private float iSpeed;
    private float iDirection;

    private float influenceSum;
    private float maxInfluence;
    public Spline exampleSpline;
    private List<float> jPosList;
    private List<float> jSpeed;
    private List<float> jDirection;
    private Vector2 newOrigin;
    
    private float currentixCoord;
    private float currentizCoord;

    public ExampleData data = new ExampleData();
    private Transform originTransform;
    private float originDirection;

    public Example(int exampleNumber)
    {
        data.exampleNumber = exampleNumber;
        subjPosArray = new float[40];
        influences = new int();
        jPosList = new List<float>(20);   // i snitt 10 jAgents
        jSpeed = new List<float>();
        jDirection = new List<float>();
        

    }

    // inte klart ännu men!
    public void calculateInfluences(float jxCoord, float jzCoord)
    {
        float distance = (float)Math.Sqrt((jxCoord - currentixCoord) * (jxCoord - currentixCoord) +
            (jzCoord - currentizCoord) * (jzCoord - currentizCoord));
        float influenceFj;
        if (jInFrontofi(jxCoord, jzCoord))
        {
            influenceFj = (float)Math.Exp(-iSpeed * Math.Pow(distance, 2) / (2));
        }
        
    }

    private Boolean jInFrontofi(float jxCoord, float jzCoord)
    {
        return true;
    }

    public void savejInformation(Spline jSpline)
    {
        Transform splineTransformj = jSpline.movingSplineTransform;
        jPosList.Add(splineTransformj.position.x);
        jPosList.Add(splineTransformj.position.z);

        jSpeed.Add(jSpline.speed);
        jDirection.Add(jSpline.movingDirection);

        calculateInfluences(splineTransformj.position.x, splineTransformj.position.z);
    }
    
    public void saveiInformation(Spline iSpline)
    {
        
        Transform splineTransformi = iSpline.movingSplineTransform;
        this.currentixCoord = splineTransformi.position.x;
        this.currentizCoord = splineTransformi.position.z;

        iSpeed = iSpline.speed;
        iDirection = iSpline.movingDirection;
        
        if (newOrigin == default(Vector2))
        {
            newOrigin = new Vector2(currentixCoord, currentizCoord);
            originDirection = iDirection;
        }
    }

    private Vector2 globalToLocalVector2 (Vector2 position)
    {
        Vector2 retvec = new Vector2(position.x - newOrigin.x, position.y - newOrigin.y);
        retvec = Quaternion.Euler(new Vector3(0, 0, (iDirection - (Mathf.PI / 2)) * 180 / Mathf.PI))*retvec;
        return retvec;
    }

    private float globalToLocalDirection (float direction)
    {
        direction = direction - iDirection + Mathf.PI / 2;
        if (direction < 0) direction = direction + 2 * Mathf.PI;
        return direction;
    }

    public void storeData(int frameNumber)
    {
        FrameData frameData = new FrameData();
        frameData.frameNumber = frameNumber;
        frameData.subject = new Agent();
        frameData.subject.splineIndex = 1;
        frameData.subject.localPosition = globalToLocalVector2
            (new Vector2(currentixCoord, currentizCoord));
        frameData.subject.direction = globalToLocalDirection(iDirection);
        frameData.subject.speed = iSpeed;





        data.examples.Add(frameData);
    }


}

public class ExampleData
{
    [XmlAttribute("exampleNumber")]
    public int exampleNumber;

    [XmlArray("Frames")]
    [XmlArrayItem("Frame")]
    public List<FrameData> examples = new List<FrameData>();

    [XmlElement("InfluenceFunction")]
    public float influenceFunction;
}

public class FrameData
{
    [XmlAttribute("frameNumber")]
    public int frameNumber;

    [XmlElement("Subject")]
    public Agent subject;

    [XmlArray("jAgents")]
    [XmlArrayItem("jAgent")]
    public List<Agent> jAgents = new List<Agent>();
}

public class Agent
{
    [XmlAttribute("splineIndex")]
    public int splineIndex;

    [XmlElement("LocalPosition")]
    public Vector2 localPosition;

    [XmlElement("Direction")]
    public float direction;

    [XmlElement("Speed")]
    public float speed;
}