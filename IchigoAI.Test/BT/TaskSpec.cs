//
// TaskSpec.cs
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
using Newtonsoft.Json;
using System.IO;
using System.Text;
using IchigoAI.BT;

namespace IchigoAI.Test.BT {
    
    public abstract class TaskSpec : nspec {
        protected Context _context;

        void before_each() {
            _context = new Context();
        }

        protected void initContext(ITask task) {
            task.InitContext(_context);
        }

        protected Status tick(ITask task) {
            return task.Tick(_context);
        }

        protected void setReturn(ITask task, Status status) {
            task.Tick(Arg.Is(_context)).Returns(status);       
        }

        protected void checkSerialization(Task task) {
            JsonSerializerSettings settings = new JsonSerializerSettings() {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full
            };
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var serializer = JsonSerializer.Create(settings);
            var writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, task);
            Console.WriteLine("Serializing task type {0} to Json", task.GetType());
            Console.WriteLine("==================================================================");
            Console.WriteLine(sb.ToString());
            Console.WriteLine("==================================================================");
            var sr = new StringReader(sb.ToString());
            var reader = new JsonTextReader(sr);
            var deserializer = JsonSerializer.Create(settings);
            var deserialized = deserializer.Deserialize(reader);
            deserialized.should_be(task);
        }
    }
}

