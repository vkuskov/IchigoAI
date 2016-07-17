//
// CompositeTask.cs
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
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IchigoAI.BT {

    [Serializable]
    public class CompositeTask : Task {
        private List<ITask> _tasks = new List<ITask>();

        public List<ITask> Tasks {
            get {
                return _tasks;
            }
        }

        protected override void onTick() {
            if (_tasks.Count > 0) {
                onComposite(_tasks);
            } else {
                fail();
            }
        }

        protected virtual void onComposite(List<ITask> tasks) {
        }

        public override bool Equals(object obj) {
            if (base.Equals(obj)) {
                var composite = (CompositeTask)obj;
                if (composite._tasks.Count == _tasks.Count) {
                    for (int i = 0; i <_tasks.Count; ++i) {
                        var left = _tasks[i];
                        var right = composite._tasks[i];
                        if (left != null && right != null) {
                            if (!left.Equals(right)) {
                                return false;
                            }
                        } else if (left != right) {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}

