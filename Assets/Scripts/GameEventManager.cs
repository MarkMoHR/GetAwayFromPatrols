using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

public class GameEventManager : MonoBehaviour {
    public delegate void GameScoreAction();
    public static event GameScoreAction myGameScoreAction;

    public delegate void GameOverAction();
    public static event GameOverAction myGameOverAction;

    private SceneController scene;

    void Start () {
        scene = SceneController.getInstance();
        scene.setGameEventManager(this);
    }
	
	void Update () {
		
	}

    public void heroEscapeAndScore() {
        if (myGameScoreAction != null)
            myGameScoreAction();
    }

    public void patrolHitHeroAndGameover() {
        if (myGameOverAction != null)
            myGameOverAction();
    }
}
