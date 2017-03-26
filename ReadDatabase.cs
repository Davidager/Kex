using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;



public class ReadDatabase{


    public ExampleContainer exampleContainer;
    public ReadDatabase()
    {
       
        public XmlSerializer serializer = new XmlSerializer(typeof(ExampleContainer));
        public Stream filereader = new FileStream(@"D:\KEX\Kex - master\Database", FileMode.Open);
        exampleContainer = serializer.Deserialize(filereader) as ExampleContainer;
        filereader.Close();
    }

	// Update is called once per frame
}

    //https://msdn.microsoft.com/en-us/library/dsh84875(v=vs.110).aspx

