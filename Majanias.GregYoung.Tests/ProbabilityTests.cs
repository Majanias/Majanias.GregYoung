using System;
using Majanias.GregYoung;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Majanias.GregYoung.Tests
{
    [TestClass]
    public class ProbabilityTests
    {
        [TestMethod]
        public void Constructor_value_too_high()
        {
            try
            {
                var p = new Probability(1.01m);
                Assert.Fail("Should've thrown an OutOfRangeException");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("value", ex.ParamName);
            }
        }

        [TestMethod]
        public void Constructor_value_too_low()
        {
            try
            {
                var p = new Probability(-0.01m);
                Assert.Fail("Should've thrown an OutOfRangeException");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("value", ex.ParamName);
            }
        }

        [TestMethod]
        public void InverseOf_1_is_zero()
        {
            var p = new Probability(1m).InverseOf();
            Assert.AreEqual(new Probability(0m), p);
        }

        [TestMethod]
        public void InverseOf_zero_is_1()
        {
            var p = new Probability(0m).InverseOf();
            Assert.AreEqual(new Probability(1m), p);
        }
        
        [TestMethod]
        public void InverseOf_0_5_is_0_5()
        {
            var p = new Probability(0.5m).InverseOf();
            Assert.AreEqual(new Probability(0.5m), p);
        }

        [TestMethod]
        public void InverseOf_0_75_is_0_25()
        {
            var p = new Probability(0.75m).InverseOf();
            Assert.AreEqual(new Probability(0.25m), p);
        }

        [TestMethod]
        public void CombinedWith_including_0_probability_equals_zero()
        {
            var p0 = new Probability(0m);
            var p050 = new Probability(0.5m);
            var p1 = new Probability(1m);

            var expectedResult = new Probability(0m);
            Assert.AreEqual(expectedResult, p0.CombinedWith(p1));
            Assert.AreEqual(expectedResult, p1.CombinedWith(p0));
            Assert.AreEqual(expectedResult, p050.CombinedWith(p0));
            Assert.AreEqual(expectedResult, p0.CombinedWith(p050));
        }

        [TestMethod]
        public void CombinedWith_1_and_1_equals_1()
        {
            var p1 = new Probability(1m);
            var p2 = new Probability(1m);

            Assert.AreEqual(new Probability(1m), p1.CombinedWith(p2));
            Assert.AreEqual(new Probability(1m), p2.CombinedWith(p1));
        }

        [TestMethod]
        public void CombinedWith_0_25_and_0_75_equals_0_1875()
        {
            var p1 = new Probability(0.25m);
            var p2 = new Probability(0.75m);

            Assert.AreEqual(new Probability(0.1875m), p1.CombinedWith(p2));
            Assert.AreEqual(new Probability(0.1875m), p2.CombinedWith(p1));
        }

        [TestMethod]
        public void Either_including_0_probability_equals_other_value()
        {
            var p0 = new Probability(0m);
            var p050 = new Probability(0.5m);
            var p1 = new Probability(1m);

            Assert.AreEqual(new Probability(1m), p0.Either(p1));
            Assert.AreEqual(new Probability(1m), p1.Either(p0));
            Assert.AreEqual(new Probability(0.5m), p0.Either(p050));
            Assert.AreEqual(new Probability(0.5m), p050.Either(p0));
        }

        [TestMethod]
        public void Either_1_and_1_equals_1()
        {
            var p1 = new Probability(1m);
            var p2 = new Probability(1m);

            Assert.AreEqual(new Probability(1m), p1.Either(p2));
            Assert.AreEqual(new Probability(1m), p2.Either(p1));
        }

        [TestMethod]
        public void Either_0_25_and_0_75_equals_0_8125()
        {
            var p1 = new Probability(0.25m);
            var p2 = new Probability(0.75m);

            Assert.AreEqual(new Probability(0.8125m), p1.Either(p2));
            Assert.AreEqual(new Probability(0.8125m), p2.Either(p1));
        }

        [TestMethod]
        public void OperatorValidations()
        {
            var p0 = new Probability(0m);
            var p1 = new Probability(0.25m);
            var p2 = new Probability(0.75m);

            Assert.IsTrue(p0 < p1);
            Assert.IsTrue(p1 < p2);
            Assert.IsFalse(p0 < p0);
            Assert.IsFalse(p1 < p1);
            Assert.IsFalse(p2 < p2);
            Assert.IsFalse(p1 < p0);
            Assert.IsFalse(p2 < p0);
            Assert.IsFalse(p2 < p1);

            Assert.IsFalse(p0 > p1);
            Assert.IsFalse(p1 > p2);
            Assert.IsFalse(p0 > p0);
            Assert.IsFalse(p1 > p1);
            Assert.IsFalse(p2 > p2);
            Assert.IsTrue(p1 > p0);
            Assert.IsTrue(p2 > p0);
            Assert.IsTrue(p2 > p1);

            Assert.IsFalse(p0 == p1);
            Assert.IsFalse(p0 == p2);
            Assert.IsFalse(p1 == p2);
            Assert.IsTrue(p0 == p0);
            Assert.IsTrue(p1 == p1);
            Assert.IsTrue(p2 == p2);

            Assert.IsTrue(p0 != p1);
            Assert.IsTrue(p0 != p2);
            Assert.IsTrue(p1 != p2);
            Assert.IsFalse(p0 != p0);
            Assert.IsFalse(p1 != p1);
            Assert.IsFalse(p2 != p2);

            Assert.IsTrue(p0 <= p1);
            Assert.IsTrue(p1 <= p2);
            Assert.IsTrue(p0 <= p0);
            Assert.IsTrue(p1 <= p1);
            Assert.IsTrue(p2 <= p2);
            Assert.IsFalse(p1 <= p0);
            Assert.IsFalse(p2 <= p0);
            Assert.IsFalse(p2 <= p1);

            Assert.IsFalse(p0 >= p1);
            Assert.IsFalse(p1 >= p2);
            Assert.IsTrue(p0 >= p0);
            Assert.IsTrue(p1 >= p1);
            Assert.IsTrue(p2 >= p2);
            Assert.IsTrue(p1 >= p0);
            Assert.IsTrue(p2 >= p0);
            Assert.IsTrue(p2 >= p1);
        }

        [TestMethod]
        public void OperatorValidationsWithNulls()
        {
            var p0 = new Probability(0m);
            var p1 = new Probability(0.25m);
            var p2 = new Probability(0.75m);

            Assert.IsFalse(p0 < null);
            Assert.IsFalse(p1 < null);
            Assert.IsFalse(p0 < null);
            Assert.IsTrue(null < p0);
            Assert.IsTrue(null < p1);
            Assert.IsTrue(null < p2);

            Assert.IsTrue(p0 > null);
            Assert.IsTrue(p1 > null);
            Assert.IsTrue(p0 > null);
            Assert.IsFalse(null > p0);
            Assert.IsFalse(null > p1);
            Assert.IsFalse(null > p2);

            Assert.IsFalse(p0 == null);
            Assert.IsFalse(p1 == null);
            Assert.IsFalse(p2 == null);
            Assert.IsFalse(null == p0);
            Assert.IsFalse(null == p1);
            Assert.IsFalse(null == p2);

            Assert.IsTrue(p0 != null);
            Assert.IsTrue(p1 != null);
            Assert.IsTrue(p2 != null);
            Assert.IsTrue(null != p0);
            Assert.IsTrue(null != p1);
            Assert.IsTrue(null != p2);

            Assert.IsFalse(p0 <= null);
            Assert.IsFalse(p1 <= null);
            Assert.IsFalse(p2 <= null);
            Assert.IsTrue(null <= p0);
            Assert.IsTrue(null <= p1);
            Assert.IsTrue(null <= p2);

            Assert.IsTrue(p0 >= null);
            Assert.IsTrue(p1 >= null);
            Assert.IsTrue(p2 >= null);
            Assert.IsFalse(null >= p0);
            Assert.IsFalse(null >= p1);
            Assert.IsFalse(null >= p2);
        }
    }
}
