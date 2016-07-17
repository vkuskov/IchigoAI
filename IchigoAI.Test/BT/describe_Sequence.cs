﻿//
// describe_Sequence.cs
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
    
    public class describe_Sequence : CompositeSpec {

        private Sequence _sequence;

        void before_each() {
            _sequence = new Sequence();
            addAllTo(_sequence);
        }

        void describe_sequence() {            
            it["Should success and call all tasks if all nodes were successful"] = () => {
                setAll(Status.Success);
                _sequence.Tick().should_be(Status.Success);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].Received().Tick();
            };
            it["Should fail and call two tasks if second task failed"] = () => {
                setAll(Status.Success);
                _tasks[1].Tick().Returns(Status.Failure);
                _sequence.Tick().should_be(Status.Failure);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
            };
            it["Should be running and call two tasks if second task is running"] = () => {
                setAll(Status.Success);
                _tasks[1].Tick().Returns(Status.Running);
                _sequence.Tick().should_be(Status.Running);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
            };
            it["Should move to last task and run if second task finished running and last task started"] = () => {
                setAll(Status.Success);
                _tasks[1].Tick().Returns(Status.Running);
                _sequence.Tick().should_be(Status.Running);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
                clearCalls();
                _tasks[1].Tick().Returns(Status.Success);
                _tasks[2].Tick().Returns(Status.Running);
                _sequence.Tick().should_be(Status.Running);
                _tasks[0].DidNotReceive().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].Received().Tick();
            };
            it["Should start again after success"] = () => {
                setAll(Status.Success);
                _sequence.Tick().should_be(Status.Success);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].Received().Tick();
                clearCalls();
                _sequence.Tick().should_be(Status.Success);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].Received().Tick();
            };
            it["Should start from again after failure"] = () => {
                setAll(Status.Success);
                _tasks[1].Tick().Returns(Status.Failure);
                _sequence.Tick().should_be(Status.Failure);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
                clearCalls();
                _sequence.Tick().should_be(Status.Failure);
                _tasks[0].Received().Tick();
                _tasks[1].Received().Tick();
                _tasks[2].DidNotReceive().Tick();
            };
        }
    }
}

