// Copyright (c) 2016 Vladimir Kuskov
using System;
using System.Collections.Generic;

namespace IchigoAI {

    internal struct CurrentTaskState {
        public TaskState state;
        public int counter;

        public override string ToString() {
            return string.Format("[state={0} counter={1}]", state, counter);
        }
    }

    public class Context {
        private List<CurrentTaskState> _perTaskState = new List<CurrentTaskState>();
        private List<object> _perTaskContext = new List<object>();

        internal int createState() {
            var index = _perTaskState.Count;
            _perTaskState.Add(new CurrentTaskState() {
                state = TaskState.Invalid,
                counter = 0
            });
            return index;
        }

        internal CurrentTaskState getTaskState(int index) {
            return _perTaskState[index];
        }

        internal void setTaskState(int index, CurrentTaskState state) {
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

        public void SetTaskContext(int index, object state) {
            _perTaskContext[index] = state;
        }

        public void Clear() {
            _perTaskState.Clear();
            _perTaskContext.Clear();
        }
    }
}

