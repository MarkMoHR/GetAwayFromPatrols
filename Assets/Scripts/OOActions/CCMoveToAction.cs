using System;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction: SSAction {
    public Vector3 target;
    public float speed;

    public static CCMoveToAction CreateSSAction(Vector3 _target, float _speed) {
        CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();
        action.target = _target;
        action.speed = _speed;
        return action;
    }

    public override void Start() {
        
    }

    public override void Update() {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed);
        if (this.transform.position == target) {
            this.destroy = true;
            this.callBack.SSActionEvent(this);
        }
    }
}
