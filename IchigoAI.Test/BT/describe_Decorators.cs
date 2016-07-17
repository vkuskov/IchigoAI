//
// describe_Decorators.cs
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
using IchigoAI;
using IchigoAI.BT;
using IchigoAI.BT.Tasks;

namespace IchigoAI.Test.BT {
    
    public class describe_Decorators : TaskSpec {
        ITask _task;

        void before_each() {
            _task = Substitute.For<ITask>();
        }

        void describe_Inverter() {
            Inverter inverter = null;
            beforeEach = () => {
                inverter = new Inverter();
                inverter.Task = _task;
            };
            it["Should run if task run"] = () => {
                _task.Tick().Returns(Status.Running);
                inverter.Tick().should_be(Status.Running);
            };
            it["Should fail if task succeeded"] = () => {
                _task.Tick().Returns(Status.Success);
                inverter.Tick().should_be(Status.Failure);
            };
            it["Should success if task failed"] = () => {
                _task.Tick().Returns(Status.Failure);
                inverter.Tick().should_be(Status.Success);
            };
            it["Should serialize"] = () => testSerialize(inverter);
        }

        void describe_Repeater() {
            Repeater repeater = null;
            beforeEach = () => {
                repeater = new Repeater();
                repeater.Task = _task;
            };
            it["Should run if task run"] = () => {
                _task.Tick().Returns(Status.Running);
                repeater.Tick().should_be(Status.Running);
            };
            it["Should run if task succeeded"] = () => {
                _task.Tick().Returns(Status.Success);
                repeater.Tick().should_be(Status.Running);
            };
            it["Should run if task failed"] = () => {
                _task.Tick().Returns(Status.Failure);
                repeater.Tick().should_be(Status.Running);
            };
            it["Should serialize"] = () => testSerialize(repeater);
        }

        void describe_Succeeder() {
            Succeeder succeeder = null;
            beforeEach = () => {
                succeeder = new Succeeder();
                succeeder.Task = _task;
            };
            it["Should run if task run"] = () => {
                _task.Tick().Returns(Status.Running);
                succeeder.Tick().should_be(Status.Running);
            };
            it["Should success if task succeeded"] = () => {
                _task.Tick().Returns(Status.Success);
                succeeder.Tick().should_be(Status.Success);
            };
            it["Should success if task failed"] = () => {
                _task.Tick().Returns(Status.Failure);
                succeeder.Tick().should_be(Status.Success);
            };
            it["Should serialize"] = () => testSerialize(succeeder);
        }

        void describe_RepeatUntilFail() {
            RepeatUntilFail repeatUntilFail = null;
            beforeEach = () => {
                repeatUntilFail = new RepeatUntilFail();
                repeatUntilFail.Task = _task;
            };
            it["Should run if task run"] = () => {
                _task.Tick().Returns(Status.Running);
                repeatUntilFail.Tick().should_be(Status.Running);
            };
            it["Should run if task succeeded"] = () => {
                _task.Tick().Returns(Status.Success);
                repeatUntilFail.Tick().should_be(Status.Running);
            };
            it["Should success if task failed"] = () => {
                _task.Tick().Returns(Status.Failure);
                repeatUntilFail.Tick().should_be(Status.Success);
            };
            it["Should serialize"] = () => testSerialize(repeatUntilFail);
        }

        void testSerialize(DecoratorTask task) {
            task.Task = new Success();
            checkSerialization(task);
        }

    }
}

