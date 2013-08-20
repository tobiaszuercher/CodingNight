using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Dubletten
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Sammlekandidaten_FindNothing()
        {
            // Arange
            var service = new MockFileService();
            service.Setup("C:/nothing", new List<string>());
            var dublettenprüfung = new Dublettenprüfung(service);

            // Act
            var result = dublettenprüfung.Sammle_Kandidaten("C:/nothing");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Sammlekandidaten_FindDublette()
        {
            // Arange
            var service = new MockFileService();
            service.Setup("C:/dubletten", new List<string> { "C:/dubletten/DuplicateFile", "C:/dubletten/DuplicateFile" });
            var dublettenprüfung = new Dublettenprüfung(service);

            // Act
            var result = dublettenprüfung.Sammle_Kandidaten("C:/dubletten");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void Sammlekandidaten_FindDubletteWithTwoFiles()
        {
            // Arange
            var service = new MockFileService();
            service.Setup("C:/dubletten", new List<string> { "C:/dubletten/DuplicateFile", "C:/dubletten/subdir/DuplicateFile" });
            var dublettenprüfung = new Dublettenprüfung(service);

            // Act
            var result = dublettenprüfung.Sammle_Kandidaten("C:/dubletten");

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(2, result.First().Dateipfade.Count());
            Assert.IsTrue(result.First().Dateipfade.Contains("C:/dubletten/DuplicateFile"));
            Assert.IsTrue(result.First().Dateipfade.Contains("C:/dubletten/subdir/DuplicateFile"));
        }

        [TestMethod]
        public void Sammlekandidaten_FindNoDubletteWithTwoFiles()
        {
            // Arange
            var service = new MockFileService();
            service.Setup("C:/dubletten", new List<string> { "C:/dubletten/DuplicateFile", "C:/dubletten/DuplicateFile1" });
            var dublettenprüfung = new Dublettenprüfung(service);

            // Act
            var result = dublettenprüfung.Sammle_Kandidaten("C:/dubletten");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Sammlekandidaten_FindTwoDublettenWithEachTwoFiles()
        {
            // Arange
            var service = new MockFileService();
            service.Setup("C:/dubletten", new List<string> {
                "C:/dubletten/DuplicateTwo",
                "C:/dubletten/sub/NoDuplicate",
                "C:/dubletten/sub2/DuplicateTwo",
                "C:/dubletten/ReallyNoDuplicate",
                "C:/dubletten/DuplicateFile",
                "C:/dubletten/subdir/DuplicateFile" });
            var dublettenprüfung = new Dublettenprüfung(service);

            // Act
            var result = dublettenprüfung.Sammle_Kandidaten("C:/dubletten");

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result.ToList()[0].Dateipfade.Count());
            Assert.AreEqual(2, result.ToList()[1].Dateipfade.Count());


            AssertDubletteHasPaths(result, "C:/dubletten/DuplicateTwo", "C:/dubletten/sub2/DuplicateTwo");
            AssertDubletteHasPaths(result, "C:/dubletten/DuplicateFile", "C:/dubletten/subdir/DuplicateFile");
        }

        private void AssertDubletteHasPaths(IEnumerable<IDublette> result, params string[] p)
        {
            foreach (var dublette in result)
            {
                var count = p.Count();
                foreach (var path in p)
                {
                    if (dublette.Dateipfade.Contains(path))
                    {
                        count--;
                    }
                }

                if (count == 0)
                {
                    return;
                }
            }

            Assert.Fail();
        }
    }
}
