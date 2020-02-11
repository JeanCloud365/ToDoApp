using System;
using Xunit;
using Shouldly;
using ToDoApp.Domain.Enumerations;

namespace ToDoApp.Domain.Tests
{
    public class StatusTests
    {
        [Fact]
        public void DoneStatusShouldHaveDoneName()
        {
            ToDoStatus.Done.Name.ShouldBe("Done");

        }

        [Fact]
        public void DoneStatusShouldHaveNotDoneName()
        {
            ToDoStatus.NotDone.Name.ShouldBe("NotDone");

        }

        [Fact]
        public void DoneStatusShouldHaveNotDoneIndex0()
        {
            ToDoStatus.NotDone.Id.ShouldBe(0);

        }

        [Fact]
        public void DoneStatusShouldHaveDoneIndex1()
        {
            ToDoStatus.Done.Id.ShouldBe(1);

        }
    }
}
