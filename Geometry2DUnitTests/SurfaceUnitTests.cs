using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Phonon p = new Phonon(1);
            p.SetDirection(1, 1);

            BoundarySurface bRight = new BoundarySurface(SurfaceLocation.right, new Cell(10, 10));
            bRight.HandlePhonon(p);

            p.GetDirection(out double dx, out double dy);

            Assert.AreEqual(-1, dx);
            Assert.AreEqual(1, dy);

            p.SetDirection(1, 1); 

            BoundarySurface bTop = new BoundarySurface(SurfaceLocation.top, new Cell(10, 10));
            bTop.HandlePhonon(p);

            p.GetDirection(out dx, out dy);

            Assert.AreEqual(1, dx);
            Assert.AreEqual(-1, dy);

            p.SetDirection(1, -1); 

            BoundarySurface bBot = new BoundarySurface(SurfaceLocation.bot, new Cell(10, 10));
            bBot.HandlePhonon(p);

            p.GetDirection(out dx, out dy);

            Assert.AreEqual(1, dx);
            Assert.AreEqual(1, dy);

            p.SetDirection(-1, 1);

            BoundarySurface bLeft = new BoundarySurface(SurfaceLocation.left, new Cell(10, 10));
            bLeft.HandlePhonon(p);

            p.GetDirection(out dx, out dy);

            Assert.AreEqual(1, dx);
            Assert.AreEqual(1, dy);
        }
    }
}
