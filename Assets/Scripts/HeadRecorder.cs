using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HeadData {
  public float time;
  public Vector3 position;
  public Quaternion rotation;
  public int questionnum;

  public HeadData(int q, float t, Vector3 pos, Quaternion rot) {
    questionnum = q;
    time = t;
    position = pos;
    rotation = rot;
  }
}


public class HeadRecorder : MonoBehaviour
{

  public List<HeadData> headDatas = new List<HeadData>();
  public Camera headCamera;
  public Questionner questionner;
    // Start is called before the first frame update
    // Update is called once per frame

	void Start(){
		headCamera = GetComponent<Camera>();
	}

    void Update()
    {
    	headDatas.Add(new HeadData(questionner.currentQuestionNum, Time.time,
    		headCamera.transform.position,
            headCamera.transform.rotation));
    }

    public List<HeadData> getData(){
    	return headDatas;
    }
}
