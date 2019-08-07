using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HeadData {
  public int QuestionNum;
  public float time;
  public Vector3 position;
  public Quaternion rotation;
  public Vector3 hitPosition;
  public string hitObject;


  public HeadData(int quesNum, float t, Vector3 pos, Quaternion rot, Vector3 hitPos, string hitObj) {
    QuestionNum = quesNum;
    time = t;
    position = pos;
    rotation = rot;
    hitPosition = hitPos;
    hitObject = hitObj;
  }

  public HeadData(int quesNum, float t, Vector3 pos, Quaternion rot) {
    QuestionNum = quesNum;
    time = t;
    position = pos;
    rotation = rot;
    hitPosition = new Vector3(-99,-99,-99);
    hitObject = "";
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
      Ray ray = new Ray(headCamera.transform.position, headCamera.transform.rotation * Vector3.forward);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
        Target target = hit.transform.gameObject.GetComponentInParent<Target>();
        string name;
        if (target != null){
          name=target.name;
        }
        else{
          name=hit.transform.gameObject.name;
        }
        Debug.Log("Raycast hit");
        headData.Add(new HeadData(experiment.QuestionNum, Time.time,
      		                         headCamera.transform.position,
                                   headCamera.transform.rotation, hit.point, name));
      }
      else{
        headData.Add(new HeadData(experiment.QuestionNum, Time.time,
      		                         headCamera.transform.position,
                                   headCamera.transform.rotation));
      }

    }

    public List<HeadData> getData(){
    	return headData;
    }
}
