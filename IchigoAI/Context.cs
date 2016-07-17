// Copyright (c) 2016 Vladimir Kuskov
using System;
using System.Collections.Generic;

namespace IchigoAI {

    public struct CurrentTaskState {
        public TaskState state;
        public int counter;
    }

    public class Context {
        private List<CurrentTaskState> _perTaskState = new List<CurrentTaskState>();
        private List<object> _perTaskContext = new List<object>();

        public int CreateState() {
            var index = _perTaskState.Count;
            _perTaskState.Add(new CurrentTaskState() {
                state = TaskState.Invalid,
                counter = 0
            });
            return index;
        }

        public CurrentTaskState GetTaskState(int index) {
            return _perTaskState[index];
        }

        public void SetTaskState(int index, CurrentTaskState state) {
            _perTaskState[index] = state;
        }

        public int CreateTaskContext(object initial) {
            int index = _perTaskContext.Count;
            _perTaskContext.Add(initial);
            return index;
        }

        public object GetTaskContext(int index) {
            return _perTaskContext[index];
        }

        public void SetTaslContext(int index, object state) {
            _perTaskContext[index] = state;
        }

        public void Clear() {
            _perTaskState.Clear();
            _perTaskContext.Clear();
        }
    }
}

