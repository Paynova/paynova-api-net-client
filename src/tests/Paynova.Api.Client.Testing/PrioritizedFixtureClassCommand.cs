using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace Paynova.Api.Client.Testing
{
    /// <summary>
    /// See <![CDATA[https://github.com/xunit/xunit/tree/v1/samples/PrioritizedFixtureExample]]>
    /// </summary>
    public class PrioritizedFixtureClassCommand : ITestClassCommand
    {
        private readonly TestClassCommand _cmd = new TestClassCommand();

        public object ObjectUnderTest
        {
            get { return _cmd.ObjectUnderTest; }
        }

        public ITypeInfo TypeUnderTest
        {
            get { return _cmd.TypeUnderTest; }
            set { _cmd.TypeUnderTest = value; }
        }

        public virtual int ChooseNextTest(ICollection<IMethodInfo> testsLeftToRun)
        {
            const int nextTestIndexFixedDueToOrderedList = 0;

            return nextTestIndexFixedDueToOrderedList;
        }

        public virtual Exception ClassFinish()
        {
            return _cmd.ClassFinish();
        }

        public virtual Exception ClassStart()
        {
            return _cmd.ClassStart();
        }

        public virtual IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo testMethod)
        {
            return _cmd.EnumerateTestCommands(testMethod);
        }

        public virtual IEnumerable<IMethodInfo> EnumerateTestMethods()
        {
            var sortedMethods = new SortedDictionary<int, List<IMethodInfo>>();

            foreach (var method in _cmd.EnumerateTestMethods())
            {
                var priority = 0;

                foreach (var attr in method.GetCustomAttributes(typeof(MyFactAttribute)))
                    priority = attr.GetPropertyValue<int>(MyFactAttribute.PriorityMemberName);

                GetOrCreate(sortedMethods, priority).Add(method);
            }

            return sortedMethods.Keys.SelectMany(priority => sortedMethods[priority]);
        }

        public bool IsTestMethod(IMethodInfo testMethod)
        {
            return _cmd.IsTestMethod(testMethod);
        }

        private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            TValue result;

            if (!dictionary.TryGetValue(key, out result))
            {
                result = new TValue();
                dictionary[key] = result;
            }

            return result;
        }
    }
}