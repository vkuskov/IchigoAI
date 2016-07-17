//
// describe_DecoratorTask.cs
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

namespace IchigoAI.Test.BT {
    
    public class describe_DecoratorTask : TaskSpec {
        DecoratorTask _task;

        void before_each() {
            _task = new DecoratorTask();
        }

        void describe_decorator() {
            it["Should have null as a task by default"] = () => _task.Task.should_be_null();
            it["Should have task that we set in"] = () => {
                var subTask = Substitute.For<ITask>();
                _task.Task = subTask;
                _task.Task.should_be(subTask);
            };
            it["Should tick it's subtask"] = () => {
                var subTask = Substitute.For<ITask>();
                _task.Task = subTask;
                tick(_task);
                subTask.Received().Tick(Arg.Is(_context));
            };
            it["Should fail when tick without subtask"] = () => tick(_task).should_be(Status.Failure);
        }
    }
}

