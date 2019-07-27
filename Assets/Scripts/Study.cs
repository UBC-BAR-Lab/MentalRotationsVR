using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Study : MonoBehaviour {
  public List<GameObject> shapes;

  private List<Tuple<int, int>> responses;
  private List<int> selected;
  public int maxselections;
  public int questionnum = 0;
  public Transform[] Pos;


  // Start is called before the first frame update
  void Start() {
    responses = new List<Tuple<int, int>>();
    shapes = new List<GameObject>();
    selected = new List<int>();
    updateTrial();
  }

  public void updateTrial() {
    questionnum++;
    if (shapes != null) {
      foreach (GameObject s in shapes) {
        shapes.Remove(s);
        Destroy(s);
      }
    }
    Debug.Log("adding " + $"prefabs/shapes/Q{questionnum}/{questionnum}.A");
    shapes.Add((GameObject)Resources.Load($"prefabs/shapes/Q{questionnum}/{questionnum}.A", typeof(GameObject)));
    shapes.Add((GameObject)Resources.Load($"prefabs/shapes/Q{questionnum}/{questionnum}.1", typeof(GameObject)));
    shapes.Add((GameObject)Resources.Load($"prefabs/shapes/Q{questionnum}/{questionnum}.2", typeof(GameObject)));
    shapes.Add((GameObject)Resources.Load($"prefabs/shapes/Q{questionnum}/{questionnum}.3", typeof(GameObject)));
    shapes.Add((GameObject)Resources.Load($"prefabs/shapes/Q{questionnum}/{questionnum}.4", typeof(GameObject)));
    for (int i = 0; i< shapes.Count; i++) {
         Instantiate(shapes[i], Pos[i]);
       }
    }

  public void Selection(int number, bool selectionState) {
    if (selectionState) {
      selected.Add(number);
    }
    else{
      selected.Remove(number);
    }
    if (selected.Count >= maxselections) {
      responses.Add(new Tuple<int,int>(selected[0], selected[1]));
      updateTrial();
    }
  }
}
