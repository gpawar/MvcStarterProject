using System;
using System.Security.Principal;
using BankAccount.Tests;
using MvcStarterProject.Config;
using MvcStarterProject.Config.StructureMap;
using StructureMap;
using System.Diagnostics;

namespace MvcStarterProject.Tests.IntegrationTests
{
    public abstract class IntegrationTest : Specification
    {
        protected override void Before_Establish_context()
        {
            if (!Bootstrapper.AlreadyStarted)
                Bootstrapper.Bootstrap();

            base.Before_Establish_context();
        }
    }
}