//
// describe_Parallel.cs
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
    public class describe_SimpleParallel : CompositeSpec {
        private SimpleParallel _parallel;

        void before_each() {
            _parallel = new SimpleParallel();
            addAllTo(_parallel);
        }

        void describe_parallel() {
            it["Should execute all tasks if they're running"] = () => {
                setAll(Status.Running);
                _parallel.Tick().should_be(Status.Running);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].Received().Tick();
            };
            it["Should success if one task succeded"] = () => {
                setAll(Status.Running);
                _tasks[1].Tick().Returns(Status.Success);
                _parallel.Tick().should_be(Status.Success);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
            };
            it["Should fail if one task failed"] = () => {
                setAll(Status.Running);
                _tasks[1].Tick().Returns(Status.Failure);
                _parallel.Tick().should_be(Status.Failure);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
            };
            it["Should start over if all tasks are running"] = () => {
                setAll(Status.Running);
                _parallel.Tick().should_be(Status.Running);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].Received().Tick();
                clearCalls();
                _parallel.Tick().should_be(Status.Running);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].Received().Tick();
            };
            it["Should start over if one task succeded"] = () => {
                setAll(Status.Running);
                _tasks[1].Tick().Returns(Status.Success);
                _parallel.Tick().should_be(Status.Success);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
                clearCalls();
                _parallel.Tick().should_be(Status.Success);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
            };
            it["Should start over if one task failed"] = () => {
                setAll(Status.Running);
                _tasks[1].Tick().Returns(Status.Failure);
                _parallel.Tick().should_be(Status.Failure);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
                clearCalls();
                _parallel.Tick().should_be(Status.Failure);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
            };
        }
    }
}

