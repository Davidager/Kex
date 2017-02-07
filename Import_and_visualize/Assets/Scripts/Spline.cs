using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for containing the positions of the cylinders
public class Spline : MonoBehaviour
{
    private float[] coordArray; //coordArray includes the framenumber for the pair of coords in the order x z framenumber
    private int frameCounter;
    private GameObject movingSpline;
    private Transform movingSplineTransform;
    public GameObject CylinderPre;
    public GameObject Nose;
    private GameObject nose;
    private Transform noseTransform;

    private Vector3 targetPos;
    private float speed;
    private int lastFrame;
    private float yCoord;
    private int firstFrame;
    private int currentTargetFrame;
    private int controlPointNumber; // the control point which the spline is moving towards 
                                    // (starts with 1 (0 is the starting point))

   
    // Start is used in unity in place of the usual constructor
    void Start ()
    {
        frameCounter = 0;
        controlPointNumber = 1;
        CylinderPre = Resources.Load("CylinderPre") as GameObject;
        Nose = Resources.Load("Nose") as GameObject;
        yCoord = 0.2f;
        
    }

    void Update ()
    {
        if (frameCounter >= firstFrame)
        {
            if (frameCounter == firstFrame)
            {
                startSpline();
            }
            if (frameCounter == currentTargetFrame)
            {
                //ändrar targetframe 
                
                controlPointNumber++;
                currentTargetFrame = (int)coordArray[(controlPointNumber * 3) + 2];
                targetPos = new Vector3(coordArray[controlPointNumber * 3], yCoord, coordArray[controlPointNumber * 3 + 1]);

                float dist = Vector3.Distance(targetPos, movingSplineTransform.position);
                speed = dist / ((currentTargetFrame - frameCounter));
            }
            moveSpline();
        }

        frameCounter++;
        if (frameCounter >= lastFrame)
        {
            Destroy(movingSpline);
            Destroy(this);
        }
    }

    public void setCoordArray(float[] coordArray)
    {
        this.coordArray = coordArray;
        firstFrame = (int)coordArray[2];
        currentTargetFrame = (int)coordArray[(controlPointNumber * 3) + 2];
        lastFrame = (int)coordArray[coordArray.Length-1];
    }

    private void startSpline ()
    {
        float startx = coordArray[0];
        float startz = coordArray[1];
        Vector3 startPosition = new Vector3(startx, yCoord, startz);
        movingSpline = Instantiate(CylinderPre, startPosition, Quaternion.identity);
        movingSplineTransform = movingSpline.transform;

        nose = Instantiate(Nose, Vector3.zero, Quaternion.identity);
        noseTransform = nose.transform;
        noseTransform.parent = movingSplineTransform;
        noseTransform.localPosition = new Vector3(0, 0.784f, 0.69f);

        
    }

    private void moveSpline ()
    {   
        movingSplineTransform.position = Vector3.MoveTowards(movingSplineTransform.position, targetPos, speed);

    }

}

