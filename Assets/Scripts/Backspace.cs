using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Backspace : MonoBehaviour {
  public TMP_InputField participant;

  public void Pressed() {
    participant.text = participant.text.Substring(0, participant.text.Length - 1);
  }
}
