using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public struct ResponseData {
  public int trialNum;
  public float trialTime;
  // public int score;
  public List<int> responses;

  public ResponseData(int trial, float time, List<int> resp) {
    trialNum = trial;
    trialTime = time;
    responses = resp;
  }
}



public class Study : MonoBehaviour {
  public HeadRecorder headRec;
  public DataWriter dataWriter;
  public int lastQuestion;
  public GameObject[] positions;
  public GameObject trialObjects;
  public GameObject experimenterCanvas;
  public GameObject startPanel;
  public GameObject questionPanel;
  public GameObject endPanel;
  public TextMesh trialNumberText;
  public Toggle motionParallax;
  public Slider artsSciOrientation;
  public Toggle genderEffect;

  public string Participant { get; set; }
  public int QuestionNum { get; set; }

  private List<int> selectedShapes;
  private List<GameObject> shapes;
  private List<ResponseData> responses;
  private bool oneSelected;
  private float trialStartTime;

  // Start is called before the first frame update
  void Start() {
    QuestionNum = 0;
    oneSelected = false;
    SetMotionParallax(motionParallax.isOn);
    responses = new List<ResponseData>();
    shapes = new List<GameObject>();
    nextTrial();
  }

  public void ExperimenterStart() {
    if (!string.IsNullOrEmpty(Participant)) {
      experimenterCanvas.SetActive(false);
      startPanel.SetActive(true);
    }
  }

  public void SetMotionParallax(bool set) {
    foreach (GameObject g in positions) {
      g.GetComponent<AimConstraint>().constraintActive = !set;
    }
  }

  public void ParticipantStart() {
    startPanel.SetActive(false);
    trialObjects.SetActive(true);
  }

  public void nextTrial() {
    if (QuestionNum >= lastQuestion){
      FinalPanel();
      return;
    }
    QuestionNum++;
    trialNumberText.text = "Trial: " + QuestionNum;
    selectedShapes = new List<int>();
    while (shapes.Count > 0) {
      Destroy(shapes[0]);
      shapes.RemoveAt(0);
    }

    oneSelected = false;
    shapes.Add(Instantiate((GameObject)Resources.Load($"prefabs/shapes/Q{QuestionNum}/{QuestionNum}.{0}", typeof(GameObject)), positions[0].transform));
    for (int i = 1; i<5; i++) {
         positions[i].GetComponent<Shape>().Reset();
         shapes.Add(Instantiate((GameObject)Resources.Load($"prefabs/shapes/Q{QuestionNum}/{QuestionNum}.{i}", typeof(GameObject)), positions[i].transform));
    }
    trialStartTime = Time.time;
  }

  public void Selection(int number, bool selectionState) {
    if (selectionState) {
      selectedShapes.Add(number);
    }
    if (selectionState && oneSelected) {
      ResponseData test = new ResponseData(QuestionNum, Time.time - trialStartTime,
                                     selectedShapes);
      responses.Add(test);
      Invoke("nextTrial", 0.3f);
    } else {
      oneSelected = selectionState;
    }
  }

  void FinalPanel() {
    trialObjects.SetActive(false);
    questionPanel.SetActive(true);
  }

  public void End(){
    Toggle[] toggles = FindObjectsOfType<Toggle>();
    foreach (Toggle t in toggles) {
      if (t.isOn) {
        questionPanel.SetActive(false);
        endPanel.SetActive(true);
        writeData();
      }
    }
  }

  void writeData(){
      dataWriter.writeHeadData(headRec.getData());
      dataWriter.writeResponses(responses);
  }

  public void Quit() {
    Application.Quit();
  }
}
