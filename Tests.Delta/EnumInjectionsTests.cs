﻿using System.Linq;
using NUnit.Framework;
using Omu.ValueInjecter.Delta;
using Tests.Delta.Injections;

namespace Tests.Delta
{
    [TestFixture]
    public class EnumInjectionsTests
    {
        [Test]
        public void Test()
        {
            var e = new Entity
            {
                Color = Colors.Blue,
                Color2 = Colors.Green,
                Mood = Moods.VeryHappy,
                Mood2 = Moods.Great,
            };

            var dto = new Dto();
            dto.InjectFrom<EnumToInt>(e);

            var deltas = dto.DeltaResults();

            Assert.AreEqual(2, dto.Color);
            Assert.AreEqual(1, dto.Color2);
            Assert.AreEqual(2, dto.Mood);
            Assert.AreEqual(3, dto.Mood2);

            Assert.IsTrue(deltas.Any());
            Assert.IsTrue(deltas.Count == 4);
            Assert.IsTrue((Colors)deltas[0].SourceValue == e.Color);
            Assert.IsTrue((int)deltas[0].TargetValueOld == 0);
            Assert.IsTrue((int)deltas[0].TargetValueNew == dto.Color);

            var e2 = new Entity();
            e2.InjectFrom<IntToEnum>(dto);
            Assert.AreEqual(dto.Color, 2);
            Assert.AreEqual(dto.Color2, 1);
            Assert.AreEqual(dto.Mood, 2);
            Assert.AreEqual(dto.Mood2, 3);
        }

        public enum Colors
        {
            Red, Green, Blue
        }

        public enum Moods
        {
            Happy, Awesome, VeryHappy, Great
        }

        public class Entity
        {
            public Colors Color { get; set; }
            public Colors Color2 { get; set; }
            public Moods Mood { get; set; }
            public Moods Mood2 { get; set; }
        }

        public class Dto
        {
            public int Color { get; set; }
            public int Color2 { get; set; }
            public int Mood { get; set; }
            public int Mood2 { get; set; }
        }
    }
}
