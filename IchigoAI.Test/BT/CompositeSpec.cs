//
// CompositeSpec.cs
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
using NSpec;
using NSubstitute;
using IchigoAI.BT;
using IchigoAI.BT.Tasks;

namespace IchigoAI.Test.BT {
    
    public abstract class CompositeSpec : TaskSpec {
        protected ITask[] _tasks;

        protected void clearCalls() {
            foreach (var it in _tasks) {
                it.ClearReceivedCalls();
            }
        }

        protected void setAll(Status status) {
            foreach (var it in _tasks) {
                it.Tick().Returns(status);
            }
        }

        protected void addAllTo(CompositeTask task) {
            foreach (var it in _tasks) {
                task.Tasks.Add(it);
            }
        }

        void before_each() {
            _tasks = new ITask[3];
            for (int i = 0; i < _tasks.Length; ++i) {
                _tasks[i] = Substitute.For<ITask>();
            }
        }

        protected void testSerialization(CompositeTask task) {
            task.Tasks.Clear();
            task.Tasks.Add(new Success());
            var inverter = new Inverter();
            inverter.Task = new Failure();
            task.Tasks.Add(inverter);
            task.Tasks.Add(new Failure());
            it["Should serialize"] = () => {
                checkSerialization(task);
            };
        }
    }
}

