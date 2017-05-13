﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Profiler
{
    public class LogicHandler : MonoBehaviour
    {

        public List<Logic> logics;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            foreach (Logic logic in logics) {
                logic.LogicUpdate();
            }
        }

        public void AddToLogicHandler(Logic logic) {
            logics.Add(logic);
        }
    }
}