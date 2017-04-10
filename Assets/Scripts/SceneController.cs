using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Patrols {
    public class Diretion {
        public const int UP = 0;
        public const int DOWN = 2;
        public const int LEFT = -1;
        public const int RIGHT = 1;
    }

    public class FenchLocation {
        public const float FenchHori = -18.0f;
        public const float FenchVertLeft = -12.6f;
        public const float FenchVertRight = -6.5f;
    }

    public interface IUserAction {
        void heroMove(int dir);
    }

    public interface IAddAction {
        void addRandomMovement(GameObject sourceObj, bool isActive);
        void addSingleMoving(GameObject sourceObj, Vector3 target, float speed);
        void addCombinedMoving(GameObject sourceObj, Vector3[] target, float[] speed);
    }

    public class SceneController : System.Object, IUserAction, IAddAction {
        private static SceneController instance;
        private GameModel myGameModel;

        public static SceneController getInstance() {
            if (instance == null)
                instance = new SceneController();
            return instance;
        }

        internal void setGameModel(GameModel _myGameModel) {
            if (myGameModel == null) {
                myGameModel = _myGameModel;
            }
        }

        /*********************实现IUserAction接口*********************/
        public void heroMove(int dir) {
            myGameModel.heroMove(dir);
        }

        /*********************实现IAddAction接口*********************/
        public void addRandomMovement(GameObject sourceObj, bool isActive) {
            myGameModel.addRandomMovement(sourceObj, isActive);
        }

        public void addSingleMoving(GameObject sourceObj, Vector3 target, float speed) {
            myGameModel.addSingleMoving(sourceObj, target, speed);
        }

        public void addCombinedMoving(GameObject sourceObj, Vector3[] target, float[] speed) {
            myGameModel.addCombinedMoving(sourceObj, target, speed);
        }
    }
}

