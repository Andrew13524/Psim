using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Psim.Geometry2D;

namespace Geometry2DUnitTests
{

	[TestClass]
	public class PointTests
	{
		[TestMethod]
		public void TestConstructor()
		{
			Point p1 = new Point();
			Point p2 = new Point(1, 1);
			Assert.AreEqual(0, p1.X);
			Assert.AreEqual(0, p1.Y);
			Assert.AreEqual(1, p2.X);
			Assert.AreEqual(1, p2.Y);
		}
		[TestMethod]
		public void TestSetAndGet()
		{
			Point p1 = new Point();
			p1.SetCoords(1, 1);
			p1.GetCoords(out double x, out double y);
			Assert.AreEqual(1, x);
			Assert.AreEqual(1, y);
			p1.SetCoords(null, 2);
			p1.GetCoords(out x, out y);
			Assert.AreEqual(1, x);
			Assert.AreEqual(2, y);
			p1.SetCoords(2, null);
			p1.GetCoords(out x, out y);
			Assert.AreEqual(2, x);
			Assert.AreEqual(2, y);
		}
	}
	[TestClass]
	public class VectorTests
	{
		[TestMethod]
		public void TestConstructor()
		{
			Vector v1 = new Vector(1, -1);
			Assert.AreEqual(1, v1.DX);
			Assert.AreEqual(-1, v1.DY);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Vector(-10, 10));
		}
		[TestMethod]
		public void TestSet()
		{
			Vector v1 = new Vector();
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => v1.Set(-1.001, 1.001));
			v1.Set(-1, 1);
			Assert.AreEqual(-1, v1.DX);
			Assert.AreEqual(1, v1.DY);
		}
	}
	[TestClass]
	public class RectangleTests
	{
		[TestMethod]
		public void TestRectangle()
		{
			Rectangle r = new Rectangle(2, 3);
			Assert.AreEqual(2, r.Length);
			Assert.AreEqual(3, r.Width);
			Assert.AreEqual(6, r.Area);
		}
	}
}