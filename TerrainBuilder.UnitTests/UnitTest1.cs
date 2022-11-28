using NUnit.Framework;
using TerrainBuilder.Services;
using TerrainBuilder.Data;
using TerrainBuilder.Models;
using System.Threading.Tasks;
using Moq;
using TerrainBuilder.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TerrainBuilder.UnitTests
{
    [TestFixture] //по желание, но е по добре да се ползва
    public class Tests
    {
       private readonly ApplicationDbContext _context;

        [SetUp]
        //OneTimeSetup - not good
        public void Setup()
        {
        }

        [Test]
        public async Task Test1_GenerateTerrainWithCorrectData_Returns_GenerateTerrainViewModel()
        {
            // Arrange - prepare input data and entrance condistions
            // Act: Invoke the action for testing
            // Assert - check the output for exit conditions
            // Polzava se nUnit
            // Тестват се services, a не контролера, защото в контролера трябва да има само проверка на данните (валидация)

            //ARRANGE

            //150, 150, 83681.452163, 41294.53462, 5, 2
            //Mock<ApplicationDbContext> mockApplicationDBcontext = new Mock<ApplicationDbContext>();
            
            TerrainService ts = new TerrainService(_context);

           //ACT
                // Actual test code here.
                GenerateTerrainViewModel gtvm = await ts.GenerateTerrain(150, 150, 83681.452163, 41294.53462, 5, 2);
       
           //ASSERT

            if(gtvm == null)
            {
              
                Assert.Fail();
            }
            else
            {
                Assert.Fail();
            }
           // Assert.Fail();
            
            //terrainsv.GenerateTerrain(l, w, double.Parse(x), double.Parse(y), oct, inf);
            //public async Task<GenerateTerrainViewModel> GenerateTerrain(int l, int w, double offX, double offY, int oct, double inf)

           // Assert.Pass();
        }

        [Test]
        public void GenerateTerrain_Noise1D()
        {
            //ARRANGE
            TerrainService ts = new TerrainService(_context);

            //ACT
            double noiseTest = ts.Noise1D(2.34);

            //ASSERT
            if (noiseTest > 2)
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
           
        }

        [Test]
        public async Task MeetingStatus_Create()
        {
            //ARRANGE


            //ACT
            MeetingStatusController vr = new MeetingStatusController(_context);

            //ASSERT
            var viewResult = await vr.Details(2) as ViewResult; 
            
            Assert.IsInstanceOf<ViewResult>(viewResult);

            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //    viewResult.ViewData.Model);

        }

        [Test]
        public void About()
        {
            //// Arrange
            //HomeController controller = new HomeController();
            //// Act
            //ViewResult result = controller.About() as ViewResult;
            //// Assert
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
        //[TearDown] изпълнява се след тестовете
    }
}