using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour, IPointerDownHandler {
  public bool selected { get; set; }
  public Material selectedMaterial;
  public Material normalMaterial;

  public int number;
  public Study study;

  void Start() {
    selected = false;
  }

  void updateColor(){
    if (selected){
      foreach (MeshRenderer m in this.GetComponentsInChildren<MeshRenderer>()){
        m.material=selectedMaterial;
      }
    }
    else{
      foreach (MeshRenderer m in this.GetComponentsInChildren<MeshRenderer>()){
      m.material=normalMaterial;
      }
    }
  }

  public void OnPointerDown(PointerEventData eventData) {

    selected = !selected;
    study.Selection(number, selected);
    updateColor();
  }
}
