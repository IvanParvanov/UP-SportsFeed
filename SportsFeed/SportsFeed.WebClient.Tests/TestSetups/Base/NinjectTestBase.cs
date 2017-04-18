﻿using Ninject.MockingKernel.Moq;

using NUnit.Framework;

namespace SportsFeed.WebClient.Tests.TestSetups.Base
{
    public abstract class NinjectTestBase
    {
        public const string AjaxContextName = "ajax";
        public const string RegularContextName = "non-ajax";
        public const string UnauthContextName = "unauthorized";

        protected readonly MoqMockingKernel MockingKernel = new MoqMockingKernel();

        [TearDown]
        public void ResetMocks()
        {
            this.MockingKernel.Reset();
        }

        [OneTimeSetUp]
        public abstract void Init();
    }
}
