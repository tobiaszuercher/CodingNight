using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace DublettenCheck
{
	[TestClass]
	public class UnitTest1
	{
		readonly Directory directory = new Directory(".");

		[TestMethod]
		public void Given2DistinctFiles_WhenISammleKandidaten_IExpectNoDublette()
		{
			directory.AddFile("a", 0);
			directory.AddFile("b", 1);

			IDublettenprüfung sut = new DuplicateCheck(directory);
			var result = sut.Sammle_Kandidaten(".");

			Assert.AreEqual(0, result.Count());
		}

		[TestMethod]
		public void Given2EqualFiles_WhenISammleKandidaten_IExpectOneDublette()
		{
			directory.AddFile("a", 1);
			directory.AddFile("a", 1);

			IDublettenprüfung sut = new DuplicateCheck(directory);
			var result = sut.Sammle_Kandidaten(".");

			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public void GivenJustOneFileInDirectory_WhenISammleKandidaten_IExpectNoDublette()
		{
			directory.AddFile("a", 1);

			IDublettenprüfung sut = new DuplicateCheck(directory);
			var result = sut.Sammle_Kandidaten(".");

			Assert.AreEqual(0, result.Count());
		}

		[TestMethod]
		public void GivenThreeFiles_WhenISammleKandidaten_IExpectOneDublette()
		{
			directory.AddFile("a", 0);
			directory.AddFile("b", 1);
			directory.AddFile("a", 0); // erst als 3tes

			IDublettenprüfung sut = new DuplicateCheck(directory);
			var result = sut.Sammle_Kandidaten(".");

			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public void GivenFiveFiles_WhenISammleKandidaten_IExpectTwoDublettes()
		{
			directory.AddFile("a", 0);
			directory.AddFile("b", 1);
			directory.AddFile("b", 1);

			IDublettenprüfung sut = new DuplicateCheck(directory);
			var result = sut.Sammle_Kandidaten(".");

			Assert.AreEqual(1, result.Count());
		}
		
		[TestMethod]
		public void Given3SameFiles_WhenISammleKandidaten_IExpectOneDubletteWithThreeFiles()
		{
			directory.AddFile("a", 1);
			directory.AddFile("a", 1);
			directory.AddFile("a", 1);

			IDublettenprüfung sut = new DuplicateCheck(directory);
			var result = sut.Sammle_Kandidaten(".");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual(3, result.First().Dateipfade.Count());
		}
	}

	public class DuplicateCheck : IDublettenprüfung
	{
		private readonly Directory directory;

		public DuplicateCheck(Directory directory)
		{
			this.directory = directory;
		}

		public IEnumerable<IDublette> Sammle_Kandidaten(string pfad)
		{
			List<File> filesInDirectory = directory.GetFiles(pfad);
			if (filesInDirectory.Count < 2)
			{
				return Enumerable.Empty<IDublette>();
			}


			filesInDirectory.Sort((file, file1) => file.Name.CompareTo(file1.Name));

			var duplicates = new SortedSet<Dublette>();

			for (int i = 0; i < filesInDirectory.Count-1; i++)
			{
				if (filesInDirectory[i].Name == filesInDirectory[i + 1].Name)
				{
					duplicates.Add(new Dublette(filesInDirectory[i]));
					// duplicates.Add(new Dublette(filesInDirectory[i+1]));
				}
			}

			return duplicates;
		}

		public IEnumerable<IDublette> Sammle_Kandidaten(string pfad, Vergleichsmodi modus)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<IDublette> Prüfe_Kandidaten(IEnumerable<IDublette> kandidaten)
		{
			throw new NotImplementedException();
		}
	}


	public class Directory
	{
		List<File> files = new List<File>();
		public Directory(string path)
		{

		}

		public void AddFile(string name, int size)
		{
			Add(new File(name, size));
		}

		public void Add(File f1)
		{
			files.Add(f1);
		}

		public List<File> GetFiles(string pfad)
		{
			return files;
		}
	}

	public class File
	{
		private readonly int i;

		public string Name { get; set; }

		public File(string name, int i)
		{
			this.Name = name;
			this.i = i;
		}
	}


	public class Dublette : IDublette, IComparable<Dublette>
	{
		private readonly File file;

		public Dublette(File file)
		{
			this.file = file;
		}

		public IEnumerable<string> Dateipfade { get; private set; }

		public int CompareTo(Dublette other)
		{
			return this.file.Name.CompareTo(other.file.Name);
		}
	}


	public interface IDublettenprüfung
	{
		IEnumerable<IDublette> Sammle_Kandidaten(string pfad);
		IEnumerable<IDublette> Sammle_Kandidaten(string pfad, Vergleichsmodi modus);
		IEnumerable<IDublette> Prüfe_Kandidaten(IEnumerable<IDublette> kandidaten);
	}

	public interface IDublette 
	{
		IEnumerable<string> Dateipfade { get; }
		
		// ?????
		//string DateiPfadFile1
		//string DateiPfadFile2
	}

	public enum Vergleichsmodi
	{
		Größe_und_Name, Größe
	}
}
