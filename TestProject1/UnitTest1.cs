using FluentAssertions;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var tt = false;

            tt.Should().BeFalse();
        }
    }
}