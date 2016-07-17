// Copyright (c) 2016 Vladimir Kuskov
using System;
using NSpec;

namespace IchigoAI.Test {
    public class describe_Context : nspec {
        private Context _context;

        void before_each() {
            _context = new Context();
        }

        void describe_task_state() {
            it["Should crate task state indices"] = () => {
                for (int i = 0; i < 100; ++i) {
                    _context.CreateState().should_be(i);
                }
            };
            context["Store and restore state"] = () => {
                beforeEach = () => {
                    _context.CreateState();
                };
                it["Stored state should be same as restored"] = () => {
                    var state = new CurrentTaskState() {
                        state = TaskState.Finished,
                        counter = 666
                    };
                    _context.SetTaskState(0, state);
                    var restored = _context.GetTaskState(0);
                    restored.state.should_be(state.state);
                    restored.counter.should_be(state.counter);
                };
            };
        }

        void desrive_tast_context() {
            it["Should create task context indices"] = () => {
                for (int i = 0; i < 100; ++i) {
                    _context.CreateTaskContext(null).should_be(i);
                }
            };
            context["Store and restore context"] = () => {
                beforeEach = () => {
                    _context.CreateTaskContext("hello!");
                };
                it["Context should be same as created"] = () => {
                    _context.GetTaskContext(0).should_be("hello!");
                };
                it["Stored context should be same as restored"] = () => {
                    _context.SetTaskContext(0, "World");
                    _context.GetTaskContext(0).should_be("World");
                };
            };
        }
    }
}

