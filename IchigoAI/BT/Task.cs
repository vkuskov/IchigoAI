//
// Task.cs
//
// Author:
//       Vladimir Kuskov <vladimir.kuskov@hotmail.com>
//
// Copyright (c) 2016 Vladimir Kuskov
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using Newtonsoft.Json;

namespace IchigoAI.BT {

    [Serializable]
    public class Task : ITask {
        private TaskState _taskState;

        public string Name { get; set; }
        public string Comment { get; set; }
        [JsonIgnore]
        public Status Status { get; private set; }

        public Task() {
            Name = GetType().Name;
            _taskState = TaskState.Invalid;
            Status = Status.Running;
        }

        public Status Tick() {
            if (_taskState == TaskState.Invalid) {
                onInit();
                _taskState = TaskState.Initialized;
            }
            if (_taskState != TaskState.Execute) {
                onStart();
                _taskState = TaskState.Execute;
            }
            Status = Status.Running;
            onTick();
            if (Status != Status.Running) {
                onFinish();
                _taskState = TaskState.Finished;
            }
            return Status;
        }

        protected virtual void onInit() {
        }

        protected virtual void onStart() {
        }

        protected virtual void onFinish() {
        }

        protected virtual void onTick() {
        }

        protected void setStatus(Status status) {
            Status = status;
        }

        protected void success() {
            Status = Status.Success;
        }

        protected void fail() {
            Status = Status.Failure;
        }

        public override bool Equals(object obj) {
            if (obj.GetType() == GetType()) {
                var task = (Task)obj;
                return task.Name == Name;
            }
            return false;
        }
    }
}

