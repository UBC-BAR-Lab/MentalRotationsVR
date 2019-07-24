using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour, IPointerDownHandler {
  public Selectable Block;

  public void OnPointerDown(PointerEventData eventData) {
    Debug.Log("Top right was clicked");
  }
}
