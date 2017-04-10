using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

public class GameModel : SSActionManager, ISSActionCallback {
    public GameObject PatrolItem, HeroItem;

    private SceneController scene;
    private GameObject myHero;
    private List<GameObject> PatrolSet;
    private List<int> PatrolLastDir;

    private const float PERSON_SPEED = 0.05f;  //推荐0.05f / 0.1f

    void Awake() {
        PatrolFactory.getInstance().initItem(PatrolItem);
    }

    protected new void Start () {
        scene = SceneController.getInstance();
        scene.setGameModel(this);

        genHero();
        genPatrols();
    }

    protected new void Update() {
        base.Update();
    }

    void genHero() {
        myHero = Instantiate(HeroItem);
    }

    void genPatrols() {
        PatrolSet = new List<GameObject>(6);
        PatrolLastDir = new List<int>(6);
        Vector3[] posSet = PatrolFactory.getInstance().getPosSet();
        for (int i = 0; i < 6; i++) {
            GameObject newPatrol = PatrolFactory.getInstance().getPatrol();
            newPatrol.transform.position = posSet[i];
            newPatrol.name = "Patrol" + i;
            PatrolLastDir.Add(-2);
            addRandomMovement(newPatrol, true);
            PatrolSet.Add(newPatrol);
        }
    }

    public void heroMove(int dir) {
        myHero.transform.rotation = Quaternion.Euler(new Vector3(0, dir * 90, 0));
        switch (dir) {
            case Diretion.UP:
                myHero.transform.position += new Vector3(0, 0, 0.1f);
                break;
            case Diretion.DOWN:
                myHero.transform.position += new Vector3(0, 0, -0.1f);
                break;
            case Diretion.LEFT:
                myHero.transform.position += new Vector3(-0.1f, 0, 0);
                break;
            case Diretion.RIGHT:
                myHero.transform.position += new Vector3(0.1f, 0, 0);
                break;
        }
    }

    public void SSActionEvent(SSAction source, SSActionEventType eventType = SSActionEventType.Completed, int intParam = 0, string strParam = null, object objParam = null) {
        //Debug.Log("SSActionEvent");
        addRandomMovement(source.gameObject, true);
    }

    //isActive说明是否主动变向（动作结束）
    public void addRandomMovement(GameObject sourceObj, bool isActive) {
        int index = getIndexOfObj(sourceObj);
        int randomDir = getRandomDirection(index, isActive);
        PatrolLastDir[index] = randomDir;

        sourceObj.transform.rotation = Quaternion.Euler(new Vector3(0, randomDir * 90, 0));
        Vector3 target = sourceObj.transform.position;
        switch (randomDir) {
            case Diretion.UP:
                target += new Vector3(0, 0, 1);
                break;
            case Diretion.DOWN:
                target += new Vector3(0, 0, -1);
                break;
            case Diretion.LEFT:
                target += new Vector3(-1, 0, 0);
                break;
            case Diretion.RIGHT:
                target += new Vector3(1, 0, 0);
                break;
        }
        addSingleMoving(sourceObj, target, PERSON_SPEED);
    }
    int getIndexOfObj(GameObject sourceObj) {
        string name = sourceObj.name;
        char cindex = name[name.Length - 1];
        int result = cindex - '0';
        return result;
    }
    int getRandomDirection(int index, bool isActive) {
        int randomDir = Random.Range(-1, 3);
        if (!isActive) {    //当碰撞时，不走同方向
            //if (PatrolLastDir[index] == 0)
            //    randomDir = 2;
            //else if (PatrolLastDir[index] == 2)
            //    randomDir = 0;
            //else if (PatrolLastDir[index] == -1)
            //    randomDir = 1;
            //else if (PatrolLastDir[index] == 1)
            //    randomDir = -1;
            while (PatrolLastDir[index] == randomDir) {
                randomDir = Random.Range(-1, 3);
            }
        }
        else {    //当非碰撞时，不走反方向
            while (PatrolLastDir[index] == 0 && randomDir == 2 
                || PatrolLastDir[index] == 2 && randomDir == 0
                || PatrolLastDir[index] == 1 && randomDir == -1
                || PatrolLastDir[index] == -1 && randomDir == 1) {
                randomDir = Random.Range(-1, 3);
            }
        }
        //Debug.Log(isActive + " isActive " + "PatrolLastDir " + PatrolLastDir[index] + " -- randomDir " + randomDir);
        return randomDir;
    }

    public void addSingleMoving(GameObject sourceObj, Vector3 target, float speed) {
        this.runAction(sourceObj, CCMoveToAction.CreateSSAction(target, speed), this);
    }

    public void addCombinedMoving(GameObject sourceObj, Vector3[] target, float[] speed) {
        List<SSAction> acList = new List<SSAction>();
        for (int i = 0; i < target.Length; i++) {
            acList.Add(CCMoveToAction.CreateSSAction(target[i], speed[i]));
        }
        CCSequeneActions MoveSeq = CCSequeneActions.CreateSSAction(acList);
        this.runAction(sourceObj, MoveSeq, this);
    }
}
