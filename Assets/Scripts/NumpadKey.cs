using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumpadKey : MonoBehaviour {
  public int value;
  public TMP_InputField participant;

  public void Pressed() {
    participant.text = participant.text + value;
  }
}
