using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using System;

public struct ResponseData {
  public int trialNum;
  public float trialTime;
  public int score;
  public List<int> responses;

  public ResponseData(int trial, float time, int results, List<int> resp) {
    trialNum = trial;
    score = results;
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

  private List<int[]> answers;
  private int[] one = {1,3};
  private int[] two = {1,4};
  private int[] three = {2,4};
  private int[] four = {2,3};
  private int[] five = {1,3};
  private int[] six = {1,4};
  private int[] seven = {2,4};
  private int[] eight = {2,3};
  private int[] nine = {2,4};
  private int[] ten = {1,4};
  private int[] eleven = {2,4};
  private int[] twelve = {2,4};
  private int[] thirteen = {2,4};
  private int[] fourteen = {1,4};
  private int[] fifteen = {2,4};
  private int[] sixteen = {2,3};
  private int[] seventeen = {1,3};
  private int[] eighteen = {1,4};
  private int[] nineteen = {2,4};
  private int[] twenty = {2,3};

  // Start is called before the first frame update
  void Start() {
    QuestionNum = 0;
    oneSelected = false;
    SetMotionParallax(motionParallax.isOn);
    responses = new List<ResponseData>();
    shapes = new List<GameObject>();
    answers = new List<int[]>();
    answers.Add(one);
    answers.Add(two);
    answers.Add(three);
    answers.Add(four);
    answers.Add(five);
    answers.Add(six);
    answers.Add(seven);
    answers.Add(eight);
    answers.Add(nine);
    answers.Add(ten);
    answers.Add(eleven);
    answers.Add(twelve);
    answers.Add(thirteen);
    answers.Add(fourteen);
    answers.Add(fifteen);
    answers.Add(sixteen);
    answers.Add(seventeen);
    answers.Add(eighteen);
    answers.Add(nineteen);
    answers.Add(twenty);
    nextTrial();
  }

  public void ExperimenterStart() {
    if (string.IsNullOrEmpty(Participant)) {
      Participant = "Error: Unknown";
    }
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
      selectedShapes.Reverse();
      int score = 0;
      for (int i = 0; i < 2; i++) {
        if (Array.Exists(answers[QuestionNum-1], num => (num == selectedShapes[i]))) {
          score++;
        }
      }
      ResponseData test = new ResponseData(QuestionNum, Time.time - trialStartTime,
                                           score, selectedShapes);
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
