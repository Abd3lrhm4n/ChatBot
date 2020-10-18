using Chatbot.Data;
using Chatbot.Repository;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestChatbot
{
    public class WordClassificationRepoTest
    {
        private readonly WordClassificationRepository _wordClassification;
        private readonly Mock<ApplicationDbContext> _wordClassificationMock = new Mock<ApplicationDbContext>();

        public WordClassificationRepoTest()
        {
            _wordClassification = new WordClassificationRepository(_wordClassificationMock.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetClassificationInMessage_TestAsync()
        {
            //string text = "Hello World";
            //var classifications = await _wordClassification.GetClassificationInMessageAsync(text);

            //Assert.Equal(new List<int> { 1 }, classifications);
        }
    }
}
