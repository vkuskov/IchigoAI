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

namespace IchigoAI.BT {

    [Serializable]
    public class Task : ITask {
        protected int TaskStateIndex { get; private set; }

        public string Name { get; set; }
        public string Comment { get; set; }

        public Task() {
            Name = GetType().Name;
            TaskStateIndex = -1;
        }

        public void InitContext(Context context) {
            if (context == null)
                throw new NullReferenceException("Can't init null task context");
            int stateIndex = context.createState();
            // Task tree should be determenistic
            // So if we have got different index then tree was changed and our context is invalid
            if (TaskStateIndex >= 0 && stateIndex != TaskStateIndex)
                throw new InvalidOperationException(
                    string.Format("Task already had state index {0} but context returned {1}. Context is different or task tree was changed", TaskStateIndex, stateIndex));
            onInitContext(context);
            TaskStateIndex = stateIndex;
        }

        public Status Tick(Context context) {
            if (TaskStateIndex < 0)
                throw new InvalidOperationException("Context isn't initializd for this task or task tree");
            if (context == null)
                throw new NullReferenceException("context is null");
            var taskState = getTaskState(context);
            if (taskState == TaskState.Invalid) {
                onInit(context);
                taskState = TaskState.Initialized;
            }
            if (taskState != TaskState.Execute) {
                onStart(context);
                taskState = TaskState.Execute;
            }
            var status = onTick(context);
            if (status != Status.Running) {
                onFinish(context);
                taskState = TaskState.Finished;
            }
            setTaskState(context, taskState);
            return status;
        }

        private TaskState getTaskState(Context context) {
            return context.getTaskState(TaskStateIndex).state;
        }

        private void setTaskState(Context context, TaskState state) {
            var taskState = context.getTaskState(TaskStateIndex);
            taskState.state = state;
            context.setTaskState(TaskStateIndex, taskState);
        }

        protected virtual void onInitContext(Context context) {
        }

        protected virtual void onInit(Context context) {
        }

        protected virtual void onStart(Context context) {
        }

        protected virtual void onFinish(Context context) {
        }

        protected virtual Status onTick(Context context) {
            return Status.Failure;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
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

