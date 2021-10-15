using Microsoft.VisualStudio.TestTools.UnitTesting;
using Psim.Materials;
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
            DispersionData dData;
            dData.LaData = new double[] { -2.22e-7, 9260.0, 0.0 };
            dData.TaData = new double[] { -2.28e-7, 5240.0, 0.0 };
            dData.WMaxLa = 7.63916048e13;
            dData.WMaxTa = 3.0100793072e13;

            RelaxationData rData;
            rData.Bl = 1.3e-24;
            rData.Btn = 9e-13;
            rData.Btu = 1.9e-18;
            rData.BI = 1.2e-45;
            rData.W = 2.42e13;

            Sensor sensor = new Sensor(1, new Material(dData, rData), 1);

            Cell c1 = new Cell(5, 5, sensor);

            c1.AddPhonon(new Phonon(1));
            c1.AddPhonon(new Phonon(1));
            c1.AddPhonon(new Phonon(1));

            c1.AddIncPhonon(new Phonon(1));
            c1.AddIncPhonon(new Phonon(1));
            c1.AddIncPhonon(new Phonon(1));

            Assert.AreEqual(3, c1.Phonons.Count);

            c1.MergeIncPhonons();

            Assert.AreEqual(6, c1.Phonons.Count);

            Cell c2 = new Cell(0, 20, sensor);

            c2.AddPhonon(new Phonon(1));
            c2.AddPhonon(new Phonon(1));
            c2.AddPhonon(new Phonon(1));

            Assert.AreEqual(3, c2.Phonons.Count);

            c2.MergeIncPhonons();

            Assert.AreEqual(3, c2.Phonons.Count);

            Cell c3 = new Cell(-55, 20, sensor);

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
        public void TestMoveToNearestSurface()
        {
            DispersionData dData;
            dData.LaData = new double[] { -2.22e-7, 9260.0, 0.0 };
            dData.TaData = new double[] { -2.28e-7, 5240.0, 0.0 };
            dData.WMaxLa = 7.63916048e13;
            dData.WMaxTa = 3.0100793072e13;

            RelaxationData rData;
            rData.Bl = 1.3e-24;
            rData.Btn = 9e-13;
            rData.Btu = 1.9e-18;
            rData.BI = 1.2e-45;
            rData.W = 2.42e13;

            Sensor sensor = new Sensor(2, new Material(dData, rData), 1);

            Cell c1 = new Cell(10, 10, sensor);

            Phonon p1 = new Phonon(1);

            p1.SetCoords(6, 5);
            p1.SetDirection(1, 1);
            p1.DriftTime = 4.5;
            p1.Update(1, 1, Polarization.LA);
            c1.AddPhonon(p1);
            SurfaceLocation? surfaceLocation = c1.MoveToNearestSurface(p1);
            p1.GetCoords(out double px, out double py);
            p1.GetDirection(out double dx, out double dy);

            Assert.AreEqual(SurfaceLocation.right, surfaceLocation);
            Assert.AreEqual(10, px);
            Assert.AreEqual(9, py);
            Assert.AreEqual(1, dx);
            Assert.AreEqual(1, dy);

            Phonon p2 = new Phonon(1);

            p2.SetCoords(0, 0);
            p2.SetDirection(1, 0);
            p2.DriftTime = 11;
            p2.Update(1, 1, Polarization.LA);
            surfaceLocation = c1.MoveToNearestSurface(p2);
            p2.GetCoords(out px, out py);
            p2.GetDirection(out dx, out dy);

            Assert.AreEqual(SurfaceLocation.right, surfaceLocation);
            Assert.AreEqual(10, px);
            Assert.AreEqual(0, py);
            Assert.AreEqual(1, dx);
            Assert.AreEqual(0, dy);

            Phonon p3 = new Phonon(1);

            p3.SetCoords(7, 4);
            p3.SetDirection(-1, -1);
            p3.DriftTime = 4.5;
            p3.Update(1, 1, Polarization.LA);
            surfaceLocation = c1.MoveToNearestSurface(p3);
            p3.GetCoords(out px, out py);
            p3.GetDirection(out dx, out dy);

            Assert.AreEqual(SurfaceLocation.bot, surfaceLocation);
            Assert.AreEqual(3, px);
            Assert.AreEqual(0, py);
            Assert.AreEqual(-1, dx);
            Assert.AreEqual(-1, dy);

            Phonon p4 = new Phonon(1);

            p4.SetCoords(7, 8);
            p4.SetDirection(1, 1);
            p4.DriftTime = 4.5;
            p4.Update(1, 1, Polarization.LA);
            surfaceLocation = c1.MoveToNearestSurface(p4);
            p4.GetCoords(out px, out py);
            p4.GetDirection(out dx, out dy);

            Assert.AreEqual(SurfaceLocation.top, surfaceLocation);
            Assert.AreEqual(9, px);
            Assert.AreEqual(10, py);
            Assert.AreEqual(1, dx);
            Assert.AreEqual(1, dy);

            Phonon p5 = new Phonon(1);

            p5.SetCoords(7, 8);
            p5.SetDirection(-1, -1);
            p5.DriftTime = 8;
            p5.Update(1, 1, Polarization.LA);
            surfaceLocation = c1.MoveToNearestSurface(p5);
            p5.GetCoords(out px, out py);
            p5.GetDirection(out dx, out dy);

            Assert.AreEqual(SurfaceLocation.left, surfaceLocation);
            Assert.AreEqual(0, px);
            Assert.AreEqual(1, py);
            Assert.AreEqual(-1, dx);
            Assert.AreEqual(-1, dy);

            Cell c2 = new Cell(10, 10, sensor);

            Phonon p6 = new Phonon(1);

            p6.SetCoords(4, 5);
            p6.SetDirection(0, 0);
            p6.DriftTime = 4.5;
            p6.Update(1, 1, Polarization.LA);
            surfaceLocation = c2.MoveToNearestSurface(p6);
            p6.GetCoords(out px, out py);
            p6.GetDirection(out dx, out dy);

            Assert.AreEqual(null, surfaceLocation);
            Assert.AreEqual(4, px);
            Assert.AreEqual(5, py);
            Assert.AreEqual(0, dx);
            Assert.AreEqual(0, dy);
        }
    }
}
