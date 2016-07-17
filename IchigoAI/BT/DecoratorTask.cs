//
// DecoratorTask.cs
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
    public class DecoratorTask : Task {
        
        private ITask _task;

        public ITask Task {
            get {
                return _task;
            }
            set {                
                _task = value;
            }
        }

        protected sealed override void onTick() {
            if (Task != null) {
                onDecorate(Task.Tick());
            } else {
                fail();
            }
        }

        protected virtual void onDecorate(Status result) {
        }

        public override bool Equals(object obj) {
            if (base.Equals(obj)) {
                var decorator = (DecoratorTask)obj;
                if (decorator._task != null && Task != null) {
                    return decorator._task.Equals(Task);
                }
                return decorator._task == Task;
            }
            return false;
        }
    }
}

