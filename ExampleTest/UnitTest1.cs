using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExampleTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestFailingDummyProgramWithDefaultSettings()
        {
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            var solution = await ws.OpenSolutionAsync(@"..\FailingProgram\DummyProgram.sln");
            Assert.AreEqual(1, solution.Projects.Count());

            foreach (var project in solution.Projects)
            {
                var comp = await project.GetCompilationAsync();
                Assert.IsTrue(comp.References.Any(x=>x.Display.EndsWith(@"\mscorlib.dll")), "Reference to mscorlib.dll not found found");
            }
        }

        [TestMethod]
        public async Task TestFailingDummyProgramWithx86BuildConfig()
        {
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            var solution = await ws.OpenSolutionAsync(@"..\FailingProgram\DummyProgram.sln");
            Assert.AreEqual(1, solution.Projects.Count());

            foreach (var project in solution.Projects)
            {
                var modifiedProject = project.WithCompilationOptions(project.CompilationOptions.WithPlatform(Platform.X86));
                var comp = await project.GetCompilationAsync();
                Assert.IsTrue(comp.References.Any(x => x.Display.EndsWith(@"\mscorlib.dll")), "Reference to mscorlib.dll not found found");
            }
        }

        [TestMethod]
        public async Task TestWorkingDummyProgramWithDefaultSettings()
        {
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            var solution = await ws.OpenSolutionAsync(@"..\WorkingProgram\DummyProgram.sln");
            Assert.AreEqual(1, solution.Projects.Count());

            foreach (var project in solution.Projects)
            {
                
                var comp = await project.GetCompilationAsync();
                Assert.IsTrue(comp.References.Any(x => x.Display.EndsWith(@"\mscorlib.dll")), "Reference to mscorlib.dll not found found");
            }
        }
    }
}
