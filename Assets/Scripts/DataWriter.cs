using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataWriter : MonoBehaviour {
  public Study experiment;

  private string dataline;
	private const string directory = "Mental Rotations Data\\";
  private const string responseHeader = "ParticipantID,MotionParallax," +
                                        "ArtsSciOrientation,GenderEffect," +
                                        "TrialNumber,Trial" +
                                        "Time,Score," +
                                        "Secondblock,FirstBlock," +
                                        "nthDeselected,n-1thDeselected,etc.";
  private const string headDataHeader = "ParticipantID,MotionParallax," +
                                        "ArtsSciOrientation,GenderEffect," +
                                        "TrialNumber,Time," +
                                        "headPositionX,headPositionY," +
                                        "headPositionZ,headRotationX," +
                                        "headRotationY,headRotationZ";

	void Start() {
		Directory.CreateDirectory(directory);
	}

	public void writeHeadData(List<HeadData> hdata) {
    string headDataFileName = directory + "HeadData-" + experiment.Participant + ".csv";
    StreamWriter headDataStream = new StreamWriter(headDataFileName);
    headDataStream.WriteLine(headDataHeader);
  	foreach (HeadData h in hdata){
  		dataline = $"{experiment.Participant},";
      dataline += experiment.motionParallax.isOn + ",";
      dataline += experiment.artsSciOrientation.value + ",";
      dataline += experiment.genderEffect.isOn + ",";
      dataline += $"{h.QuestionNum},{h.time},";
  		dataline += $"{h.position.x},{h.position.y},{h.position.z},";
  		dataline += $"{h.rotation.eulerAngles.x},{h.rotation.eulerAngles.y},{h.rotation.eulerAngles.z},";
      dataline += $"{h.hitPosition.x},{h.hitPosition.y},{h.hitPosition.z},";
      dataline += $"{h.hitObject},";
  		headDataStream.WriteLine(dataline);
  	}
  	headDataStream.Close();
	}

	public void writeResponses(List<ResponseData> responses) {
    string responseDataFileName = directory + "ResponseData-" + experiment.Participant + ".csv";
		StreamWriter responseDataStream = new StreamWriter(responseDataFileName);
    responseDataStream.WriteLine(responseHeader);
		for (int i = 0; i<responses.Count; i++){
      dataline = experiment.Participant + ",";
      dataline += experiment.motionParallax.isOn + ",";
      dataline += experiment.artsSciOrientation.value + ",";
      dataline += experiment.genderEffect.isOn + ",";
      dataline += responses[i].trialNum + ",";
      dataline += responses[i].trialTime + ",";
      dataline += responses[i].score + ",";
      List<int> answerObjects = responses[i].responses;
      foreach (int j in answerObjects) {
        dataline += j + ",";
      }
      responseDataStream.WriteLine(dataline);
		}
    responseDataStream.Close();
	}

}
