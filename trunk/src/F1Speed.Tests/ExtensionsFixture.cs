using F1Speed.Core;
using NUnit.Framework;

namespace F1Speed.Tests
{
    [TestFixture]
    public class ExtensionsFixture
    {
        [Test]
        public void AsTimeString_returns_for_minutes_and_seconds()
        {
            float timeInSeconds = 87.4233f;

            Assert.AreEqual("1:27.423", timeInSeconds.AsTimeString());
        }
    }
}
