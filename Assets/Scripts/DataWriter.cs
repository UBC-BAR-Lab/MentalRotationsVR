using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataWriter : MonoBehaviour
{

	public HeadRecorder headRec;
  public Experiment experiment;
  private static StreamWriter dataStream;
	private const string directory = "MENTROT Data\\";

	void Start(){
		Directory.CreateDirectory(directory);
		string headDataFileName = directory + $"HeadData-{experiment.participant}.csv";
		dataStream = new StreamWriter(headDataFileName);
	}

  void OnApplicationQuit(){
    writeHeadData(headRec.getData());
	}

	void writeHeadData(List<HeadData> hdata){
    	foreach (HeadData h in hdata){
    		string dataline = $"{experiment.participant},{h.questionnum},{h.time},";
    		dataline += $"{h.position.x},{h.position.y},{h.position.z},";
    		dataline += $"{h.rotation.eulerAngles.x},{h.rotation.eulerAngles.y},{h.rotation.eulerAngles.z},";
    		dataStream.WriteLine(dataline);
    	}
    	dataStream.Close();
	}

}
