using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GradeTrackerUnitTests;

[TestClass]
public class ExampleTestClass
{
    [TestMethod]
    public void Test_ShouldAlwaysPass_WhenCalled()
    {
        true.Should().BeTrue();
        Assert.IsTrue(true);
        Assert.AreEqual(true, true);
    }
}