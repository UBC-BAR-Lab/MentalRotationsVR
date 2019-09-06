using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownTemplateModifier : MonoBehaviour {

  void Start() {
    GraphicRaycaster gr = GetComponent<GraphicRaycaster>();
    OVRRaycaster ovrrc = GetComponent<OVRRaycaster>();
    if (gr != null) {
      gr.enabled = false;
    }
    if (ovrrc != null) {
      ovrrc.enabled = true;
    }
  }
}
