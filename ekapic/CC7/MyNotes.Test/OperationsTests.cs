using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyNotes.Data;
using System.Collections.Generic;

namespace MyNotes.Test
{
    [TestClass]
    public class OperationsTests
    {

        [TestMethod]
        public void Test_AddNote_WithTextProceedsOk()
        {
            var repo = new Mock<INotesRepository>();
            repo
                .Setup(x => x.AddNote(It.IsAny<string>()))
                .Verifiable();
            var ops = new Operations(repo.Object);

            ops.AddNote("Hello world!");

            repo.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_AddNote_WithNoTextFails()
        {
            var repo = new Mock<INotesRepository>();
            var ops = new Operations(repo.Object);

            ops.AddNote("");
        }

        [TestMethod]
        public void Test_ShowNotes_WithNoNotes()
        {
            var repo = new Mock<INotesRepository>();
            var ops = new Operations(repo.Object);

            var notesCount = ops.ShowNotes();

            Assert.AreEqual(0, notesCount);
        }

        [TestMethod]
        public void Test_ShowNotes_WithOneNote()
        {
            var repo = new Mock<INotesRepository>();
            repo
                .Setup(x => x.GetNotes())
                .Returns(
                    new List<Note>() {
                        new Note() { Date = DateTime.Now, Text = "aaa" }
                    }                          
                );

            var ops = new Operations(repo.Object);

            var notesCount = ops.ShowNotes();

            Assert.AreEqual(1, notesCount);
        }

        [TestMethod]
        public void Test_ShowNotes_WithTwoNotes()
        {
            var repo = new Mock<INotesRepository>();
            repo
                .Setup(x => x.GetNotes())
                .Returns(
                    new List<Note>() {
                        new Note() { Date = DateTime.Now, Text = "aaa" },
                        new Note() { Date = DateTime.Now, Text = "aaa" }
                    }
                );
            var ops = new Operations(repo.Object);

            var notesCount = ops.ShowNotes();

            Assert.AreEqual(2, notesCount);
        }
    }
}
