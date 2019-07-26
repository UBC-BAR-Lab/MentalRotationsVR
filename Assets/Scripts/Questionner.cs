using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questionner : MonoBehaviour
{
	public int currentQuestionNum;
	void Start(){currentQuestionNum=0;}

	public int nextQuestion(){
		currentQuestionNum++;
		return currentQuestionNum;
	}
}
