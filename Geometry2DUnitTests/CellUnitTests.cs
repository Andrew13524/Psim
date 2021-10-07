using Microsoft.VisualStudio.TestTools.UnitTesting;
using Psim.ModelComponents;
using Psim.Particles;

namespace CellUnitTests
{
    [TestClass]
    public class CellUnitTests
    {
        [TestMethod]
        public void TestMergePhonons()
        {
            Cell c1 = new Cell(5, 5);

            c1.AddPhonon(new Phonon(1));
            c1.AddPhonon(new Phonon(1));
            c1.AddPhonon(new Phonon(1));

            c1.AddIncPhonon(new Phonon(1));
            c1.AddIncPhonon(new Phonon(1));
            c1.AddIncPhonon(new Phonon(1));

            Assert.AreEqual(3, c1.Phonons.Count);

            c1.MergeIncPhonons();

            Assert.AreEqual(6, c1.Phonons.Count);

            Cell c2 = new Cell(0, 20);

            c2.AddPhonon(new Phonon(1));
            c2.AddPhonon(new Phonon(1));
            c2.AddPhonon(new Phonon(1));

            Assert.AreEqual(3, c2.Phonons.Count);

            c2.MergeIncPhonons();

            Assert.AreEqual(3, c2.Phonons.Count);

            Cell c3 = new Cell(-55, 20);

            c3.AddPhonon(new Phonon(1));
            c3.AddPhonon(new Phonon(1));
            c3.AddPhonon(new Phonon(1));

            c3.AddIncPhonon(new Phonon(1));
            c3.AddIncPhonon(new Phonon(1));
            c3.AddIncPhonon(new Phonon(1));
            c3.AddIncPhonon(new Phonon(1));
            c3.AddIncPhonon(new Phonon(1));
            c3.AddIncPhonon(new Phonon(1));

            Assert.AreEqual(3, c3.Phonons.Count);

            c3.MergeIncPhonons();

            Assert.AreEqual(9, c3.Phonons.Count);
        }

        [TestMethod]
        public void MoveToNearestSurface()
        {
            Cell c1 = new Cell(10, 10);

            Phonon p1 = new Phonon(1);

            p1.SetCoords(6, 5);
            p1.SetDirection(1, 1);
            c1.AddPhonon(p1);
            SurfaceLocation? surfaceLocation = c1.MoveToNearestSurface(p1);
            p1.GetCoords(out double px, out double py);
            p1.GetDirection(out double dx, out double dy);

            Assert.AreEqual(SurfaceLocation.right, surfaceLocation);
            Assert.AreEqual(10, px);
            Assert.AreEqual(9, py);
            Assert.AreEqual(-1, dx);
            Assert.AreEqual(1, dy);

            p1.SetCoords(0, 0);
            p1.SetDirection(1, 0);
            surfaceLocation = c1.MoveToNearestSurface(p1);
            p1.GetCoords(out px, out py);
            p1.GetDirection(out dx, out dy);

            Assert.AreEqual(SurfaceLocation.right, surfaceLocation);
            Assert.AreEqual(10, px);
            Assert.AreEqual(0, py);
            Assert.AreEqual(-1, dx);
            Assert.AreEqual(0, dy);

            p1.SetCoords(7, 4);
            p1.SetDirection(-1, -1);
            surfaceLocation = c1.MoveToNearestSurface(p1);
            p1.GetCoords(out px, out py);
            p1.GetDirection(out dx, out dy);

            Assert.AreEqual(SurfaceLocation.bot, surfaceLocation);
            Assert.AreEqual(3, px);
            Assert.AreEqual(0, py);
            Assert.AreEqual(-1, dx);
            Assert.AreEqual(1, dy);

            p1.SetCoords(7, 8);
            p1.SetDirection(1, 1);
            surfaceLocation = c1.MoveToNearestSurface(p1);
            p1.GetCoords(out px, out py);
            p1.GetDirection(out dx, out dy);

            Assert.AreEqual(SurfaceLocation.top, surfaceLocation);
            Assert.AreEqual(9, px);
            Assert.AreEqual(10, py);
            Assert.AreEqual(1, dx);
            Assert.AreEqual(-1, dy);

            p1.SetCoords(7, 8);
            p1.SetDirection(-1, -1);
            surfaceLocation = c1.MoveToNearestSurface(p1);
            p1.GetCoords(out px, out py);
            p1.GetDirection(out dx, out dy);

            Assert.AreEqual(SurfaceLocation.left, surfaceLocation);
            Assert.AreEqual(0, px);
            Assert.AreEqual(1, py);
            Assert.AreEqual(1, dx);
            Assert.AreEqual(-1, dy);

            Cell c2 = new Cell(10, 10);

            Phonon p2 = new Phonon(1);

            p2.SetCoords(4, 5);
            p2.SetDirection(0, 0);
            surfaceLocation = c2.MoveToNearestSurface(p2);
            p2.GetCoords(out px, out py);
            p2.GetDirection(out dx, out dy);

            Assert.AreEqual(null, surfaceLocation);
            Assert.AreEqual(4, px);
            Assert.AreEqual(5, py);
            Assert.AreEqual(0, dx);
            Assert.AreEqual(0, dy);
        }
    }
}
