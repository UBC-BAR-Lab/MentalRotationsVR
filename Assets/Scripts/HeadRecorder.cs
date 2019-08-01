using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HeadData {
  public float time;
  public Vector3 position;
  public Quaternion rotation;
  public int QuestionNum;

  public HeadData(int quesNum, float t, Vector3 pos, Quaternion rot) {
    QuestionNum = quesNum;
    time = t;
    position = pos;
    rotation = rot;
  }
}


public class HeadRecorder : MonoBehaviour {
  public List<HeadData> headData = new List<HeadData>();
  public Camera headCamera;
  public Study experiment;

	void Start() {
		headCamera = GetComponent<Camera>();
	}

    void Update() {
    	headData.Add(new HeadData(experiment.QuestionNum, Time.time,
    		                         headCamera.transform.position,
                                 headCamera.transform.rotation));
    }

    public List<HeadData> getData(){
    	return headData;
    }
}
