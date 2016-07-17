//
// describe_CompositeTask.cs
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
    
    public class describe_CompositeTask : CompositeSpec {

        CompositeTask _task;

        void before_each() {
            _task = new CompositeTask();
        }

        void describe_composite_task() {
            it["Should have 0 nodes"] = () => _task.Tasks.Count.should_be(0);
            it["Should have nodes with same order that we set to it"] = () => {
                var child1 = Substitute.For<ITask>();
                var child2 = Substitute.For<ITask>();
                _task.Tasks.Add(child1);
                _task.Tasks.Add(child2);
                _task.Tasks.Count.should_be(2);
                _task.Tasks[0].should_be(child1);
                _task.Tasks[1].should_be(child2);
            };
            it["Should fail without nodes"] = () => _task.Tick().should_be(Status.Failure);
            it["Should serialize"] = () => testSerialization(_task);
        }
    }
}

