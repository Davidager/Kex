using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class ReadText {
    public TextAsset textAsset;
    private string textContents;
    private StringReader myStringReader;
    private int numberOfSplines;
    private Spline[] splineArray;
    public int frameCounter;
    public Dictionary<int, ArrayList> firstFrameTable;
    private Dictionary<int, ArrayList> lastFrameTable;
    private Dictionary<int, Spline> activeSplineTable;
    private Dictionary<int, ExampleInfluences> exampleInfluencesTable;
    private List<ExampleInfluences> exampleList;
    private ArrayList exampleInfluencesList;

    public StringBuilder sb;
    public Transform CylinderPre;
    public ReadText()
    {
        
        string textContents;
        try
        {
            using (StreamReader sr = new StreamReader("crowds_zara01_copy.txt"))
            {
                textContents = sr.ReadToEnd();
                               
            }
        } catch (Exception e)
        {
            Debug.Log("Reading file failed: " + e);
            return; 
            
            //return;
        }

        frameCounter = 0;
        //string textContents = sr.ReadToEnd();
      
        exampleInfluencesList = new ArrayList();
        exampleList = new List<ExampleInfluences>();
        sb = new StringBuilder();
        myStringReader = new StringReader(textContents);
        string firstLine = myStringReader.ReadLine();
        string[] firstLineArray = firstLine.Split(' ');

        //Ha med try?
        numberOfSplines = Int32.Parse(firstLineArray[0]);

        splineArray = new Spline[numberOfSplines];
        firstFrameTable = new Dictionary<int, ArrayList>(80);
        lastFrameTable = new Dictionary<int, ArrayList>(100);
        activeSplineTable = new Dictionary<int, Spline>(10);
        int firstFrame;
        int lastFrame;
        for (int j = 0; j < numberOfSplines; j++)
        {
            string controlPointLine = myStringReader.ReadLine();
            string[] controlPointArray = controlPointLine.Split(' ');
            int numOfControlPoints = Decimal.ToInt32(Decimal.Parse(controlPointArray[0]));
            float[] coordArray = new float[4 * numOfControlPoints];  //coordArray are to be sent to the Spline object (includes coords and frame number and looking direction)
            int cccounter = 0;
            for (int i = 0; i < 4 * numOfControlPoints; i = i + 4)
            {

                string infoLine = myStringReader.ReadLine();  //infoline is the line with coord-info and frame number
                string[] infoStringArray = infoLine.Split(' ');
                // divided by 100 to fit the ground plane
                coordArray[i] = (float)Decimal.Parse(infoStringArray[0]) / 100;  // x-coord
                coordArray[i + 1] = (float)Decimal.Parse(infoStringArray[1]) / 100;  // z-coord

                int frameNumber = Decimal.ToInt32(Decimal.Parse(infoStringArray[2]));
                coordArray[i + 2] = frameNumber;
                float lookingDirection = (float)Decimal.Parse(infoStringArray[3]);
                coordArray[i + 3] = lookingDirection;
                cccounter++;
            }
            firstFrame = (int)coordArray[2];
            if (!firstFrameTable.ContainsKey(firstFrame))
            {
                firstFrameTable.Add(firstFrame, new ArrayList());
            } 
            firstFrameTable[firstFrame].Add(j);

            lastFrame = (int)coordArray[coordArray.Length - 2];
            if (!lastFrameTable.ContainsKey(lastFrame))
            {
                lastFrameTable.Add(lastFrame, new ArrayList());
            }
            lastFrameTable[lastFrame].Add(j);

            splineArray[j] = (Spline)new Spline(this, j);
            splineArray[j].setCoordArray(coordArray);
        }
        for(int i = 0; i < 9015; i++)
        nextFrame();
        
        string path = @"C:\Users\David\Documents\GitHub\Kex\Database\MyTest.txt";
        File.WriteAllText(path, sb.ToString());
    }

    private void nextFrame() 
    {
        if (firstFrameTable.ContainsKey(frameCounter))
        {
            foreach (int j in firstFrameTable[frameCounter])
            {
                activeSplineTable.Add(j, splineArray[j]);
            }
        }
        if (lastFrameTable.ContainsKey(frameCounter - 1))
        {
            foreach (int j in lastFrameTable[frameCounter - 1])
            {
                activeSplineTable.Remove(j);
            }
        }
        foreach (KeyValuePair<int, Spline> s in activeSplineTable)
        {
            s.Value.updateSpline();
            sb.AppendLine(frameCounter.ToString() + s.Value.movingSplineTransform.position.ToString("F4"));
            if(frameCounter%40 == 0)
            {
                exampleInfluencesTable.Add(s.Key, new ExampleInfluences());           
            }
            if (exampleInfluencesTable.ContainsKey(s.Key))   // Saves the speed for this frame if there
            {                                               // exists an ExampleInfluences instance.
                //exampleInfluencesTable[s.Key].exampleSpline = s.Value;
                Transform splineTransform = s.Value.movingSplineTransform;
                exampleInfluencesTable[s.Key].speed = s.Value.speed;
                exampleInfluencesTable[s.Key].saveToPath(splineTransform.position.x,
                        splineTransform.position.z);
            }     
        }

        foreach (KeyValuePair<int, ExampleInfluences> e in exampleInfluencesTable)
        {
            foreach (KeyValuePair<int, Spline> s in activeSplineTable)
            {
                if (s.Key == e.Key)   // if s is i   (the subject of an example)
                {
                    
                    // saves the speed for influence calculations
                } else     // if s is j   (an influencing agent of an example)
                {
                    Transform splineTransform = s.Value.movingSplineTransform;
                    exampleInfluencesTable[e.Key].calculateInfluences(splineTransform.position.x,
                        splineTransform.position.z);
                }
                
            }
            
        }

        if (frameCounter % 40 == 39)
        {

        }
        frameCounter++;
    }
  

    // Use this for initialization
    
	
	// Update is called once per frame
	void Update ()
    {
    }
}
