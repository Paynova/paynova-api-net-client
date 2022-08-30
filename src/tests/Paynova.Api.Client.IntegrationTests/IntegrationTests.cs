﻿using System;
using Paynova.Api.Client.IntegrationTests.Fixtures;
using Xunit;

namespace Paynova.Api.Client.IntegrationTests
{
    public abstract class IntegrationTests<TFixture> : IntegrationTests, IClassFixture<TFixture> where TFixture : class, IResetableFixture, new()
    {
        protected readonly TestStateRecycle _testStateRecycle;

        protected TFixture TestState { get; set; }

        protected IntegrationTests(TFixture data, TestStateRecycle testStateRecycle = TestStateRecycle.PerClass)
        {
            _testStateRecycle = testStateRecycle;

            if (_testStateRecycle == TestStateRecycle.PerTest)
                data.Reset();

            TestState = data;
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (IsDisposed || !disposing)
                return;

            TryAndDispose(Client);
        }

        public enum TestStateRecycle
        {
            PerClass,
            PerTest
        }
    }

    public abstract class IntegrationTests : Testing.Tests, IDisposable
    {
        protected TestEnvironment Environment { get; private set; }
        protected IPaynovaClient Client { get; private set; }
        protected bool IsDisposed { get; private set; }

        protected IntegrationTests()
        {
            Environment = IntegrationTestsRuntime.Environment;
            Client = IntegrationTestsRuntime.CreateClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed || !disposing)
                return;

            TryAndDispose(Client);
        }

        protected virtual void TryAndDispose<T>(T i)
        {
            var obj = i as IDisposable;

            if (obj == null)
                return;

            obj.Dispose();
        }
    }
}