using System.Linq;
using System.Threading.Tasks;
using ca_testTask.Handlers;
using ca_testTask.Services;
using Moq;
using Xunit;

namespace ca_testTask.Tests
{
    public sealed class FileSearcherTest
    {
        private readonly Mock<ISurfaceScanner> _mock = new Mock<ISurfaceScanner>(MockBehavior.Strict);

        public FileSearcherTest()
        {
            _mock.Setup(s => s.ScanFilesAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new[] {"c:\\f\\bla\\ra\\t.dat"});
        }

        [Fact]
        public async Task GetFilePaths_AllAction_ReturnedAsIs()
        {
            // Arrange
            var sut = new AllFileSearcher(_mock.Object);
            // Act
            var result = await sut.GetFilePaths("c:\\");

            // Assert
            Assert.Equal(result.First(), "f\\bla\\ra\\t.dat");
        }

        [Fact]
        public async Task GetFilePaths_CppAction_ReturnedWithNewEnd()
        {
            // Arrange
            var sut = new CppFileSearcher(_mock.Object);
            // Act
            var result = await sut.GetFilePaths("c:\\");

            // Assert
            Assert.Equal(result.First(), "f\\bla\\ra\\t.dat /");
        }

        [Fact]
        public async Task GetFilePaths_Reversed1Action_ReturnedReversed1()
        {
            // Arrange
            var sut = new Reversed1FileSearcher(_mock.Object);
            // Act
            var result = await sut.GetFilePaths("c:\\");

            // Assert
            Assert.Equal(result.First(), "t.dat\\ra\\bla\\f");
        }

        [Fact]
        public async Task GetFilePaths_AllAction_ReturnedReversed2()
        {
            // Arrange
            var sut = new Reversed2FileSearcher(_mock.Object);
            // Act
            var result = await sut.GetFilePaths("c:\\");

            // Assert
            Assert.Equal(result.First(), "tad.t\\ar\\alb\\f");
        }
    }
}
