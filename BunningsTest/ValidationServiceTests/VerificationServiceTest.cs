using System.Collections.Generic;
using System.Linq;
using Bunnings;
using NUnit.Framework;

namespace BunningsTest.ValidationServiceTests
{
    public class VerificationServiceTest
    {
        [Test]
        public void Given_Four_Arguments_That_Are_Valid_Files_Then_ValidInputs_Should_Return_True()
        {
            //arrange
            List<string> rawInput = new List<string>() {"barcodesA.csv", "barcodesB.csv", "catalogA.csv", "catalogB.csv"};
            List<string> input =  rawInput.Select(x => $"ValidationServiceTests/Inputs/{x}").ToList();

            //Act
            var verificationService = new VerificationService();
            var validInput = verificationService.ValidInputs(input);
            
            //Assert
            Assert.IsTrue(validInput);
            Assert.IsNull(verificationService.Message);
        }
        
        [Test]
        public void Given_Four_Arguments_That_Are_Not_Valid_Files_Then_ValidInputs_Should_Return_False()
        {
            //arrange
            List<string> rawInput = new List<string>() {"doesNotExist.csv", "doesNotExist.csv", "doesNotExist.csv", "doesNotExist.csv"};
            List<string> input =  rawInput.Select(x => $"ValidationServiceTests/Inputs/{x}").ToList();

            //Act
            var verificationService = new VerificationService();
            var validInput = verificationService.ValidInputs(input);
            
            //Assert
            Assert.IsFalse(validInput);
            Assert.IsNotEmpty(verificationService.Message);
        }
        
        [Test]
        public void Given_less_than_four_Arguments_Then_ValidInputs_Should_Return_False()
        {
            //arrange
            List<string> input = new List<string>(){"a", "b", "c"};
            
            //Act
            var verificationService = new VerificationService();
            var validInput = verificationService.ValidInputs(input);
            
            //Assert
            Assert.IsFalse(validInput);
            Assert.IsNotEmpty(verificationService.Message);
        } 
        
        [Test]
        public void Given_more_than_four_Arguments_Then_ValidInputs_Should_Return_False()
        {
            //arrange
            List<string> input = new List<string>(){"a", "b", "c","d","e"};
            
            //Act
            var verificationService = new VerificationService();
            var validInput = verificationService.ValidInputs(input);
            
            //Assert
            Assert.IsFalse(validInput);
            Assert.IsNotEmpty(verificationService.Message);
        }
        
    }
}