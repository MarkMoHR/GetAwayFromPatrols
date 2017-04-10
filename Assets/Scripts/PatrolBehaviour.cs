using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

//----------------------------------
// 此脚本加在巡逻兵上
//----------------------------------

public class PatrolBehaviour : MonoBehaviour {
    private IAddAction addAction;

    void Start () {
        addAction = SceneController.getInstance() as IAddAction;

    }
	
	void Update () {
		
	}

    void OnCollisionStay(Collision e) {
        if (e.gameObject.name.Contains("Patrol") || e.gameObject.name.Contains("fence")
            || e.gameObject.tag.Contains("FenceAround")) {
            //Debug.Log(this.gameObject.name + " : " + e.gameObject.name);
            addAction.addRandomMovement(this.gameObject, false);
        }
    }
}
