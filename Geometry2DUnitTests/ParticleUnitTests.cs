using Microsoft.VisualStudio.TestTools.UnitTesting;
using Psim.Particles;

namespace ParticleUnitTests
{
	[TestClass]
	public class PhononTests
    {
		[TestMethod]
		public void TestDrift()
        {
			Phonon p1 = new Phonon(1);

			p1.SetCoords(1,1);
			p1.SetDirection(1, 1);
			p1.Update(0, 5, Polarization.LA);
			p1.Drift(5);
			p1.GetCoords(out double p1x, out double p1y);

			Assert.AreEqual(26, p1x);     // 1 += (1 * 5 * 5) = 26
			Assert.AreEqual(26, p1y);     // 1 += (1 * 5 * 5) = 26

			Phonon p2 = new Phonon(1);

			p2.SetCoords(1, 1);
			p2.SetDirection(1, 1);
			p2.Update(0, 5, Polarization.LA);
			p2.Drift(-5);
			p2.GetCoords(out double p2x, out double p2y);

			Assert.AreEqual(-24, p2x);     // 1 += (1 * 5 * -5) = -24
			Assert.AreEqual(-24, p2y);     // 1 += (1 * 5 * -5) = -24

			Phonon p3 = new Phonon(1);

			p3.SetCoords(1, 1);
			p3.SetDirection(1, 1);
			p3.Update(0, 5, Polarization.LA);
			p3.Drift(0);
			p3.GetCoords(out double p3x, out double p3y);

			Assert.AreEqual(1, p3x);     // 1 += (1 * 5 * 0) = 1
			Assert.AreEqual(1, p3y);     // 1 += (1 * 5 * 0) = 1

			Phonon p4 = new Phonon(1);

			p4.SetCoords(5, -5);
			p4.SetDirection(-1, 1);
			p4.Update(0, 10, Polarization.LA);
			p4.Drift(100);
			p4.GetCoords(out double p4x, out double p4y);

			Assert.AreEqual(-995, p4x);     // 5 += (-1 * 10 * 100) = -995
			Assert.AreEqual(995, p4y);      // -5 += (1 * 10 * 100) = 995
		}
	}
}