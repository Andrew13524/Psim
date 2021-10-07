using Microsoft.VisualStudio.TestTools.UnitTesting;
using Psim.Materials;
using Psim.ModelComponents;
using Psim.Particles;
using System;
using System.Collections.Generic;
using System.Text;

namespace SurfaceUnitTests
{
    [TestClass]
    public class SurfaceUnitTests
    {
        [TestMethod]
        public void TestHandlePhonon()
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
            Phonon p = new Phonon(1);
            p.SetDirection(1, 1);

            BoundarySurface bRight = new BoundarySurface(SurfaceLocation.right, new Cell(10, 10, sensor));
            bRight.HandlePhonon(p);

            p.GetDirection(out double dx, out double dy);

            Assert.AreEqual(-1, dx);
            Assert.AreEqual(1, dy);

            p.SetDirection(1, 1);

            BoundarySurface bTop = new BoundarySurface(SurfaceLocation.top, new Cell(10, 10, sensor));
            bTop.HandlePhonon(p);

            p.GetDirection(out dx, out dy);

            Assert.AreEqual(1, dx);
            Assert.AreEqual(-1, dy);

            p.SetDirection(1, -1);

            BoundarySurface bBot = new BoundarySurface(SurfaceLocation.bot, new Cell(10, 10, sensor));
            bBot.HandlePhonon(p);

            p.GetDirection(out dx, out dy);

            Assert.AreEqual(1, dx);
            Assert.AreEqual(1, dy);

            p.SetDirection(-1, 1);

            BoundarySurface bLeft = new BoundarySurface(SurfaceLocation.left, new Cell(10, 10, sensor));
            bLeft.HandlePhonon(p);

            p.GetDirection(out dx, out dy);

            Assert.AreEqual(1, dx);
            Assert.AreEqual(1, dy);
        }
    }
}
