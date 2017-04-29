﻿using System;
using System.Reflection;
using NUnit.Framework;
using Omu.ValueInjecter.Delta;
using Tests.Utils;

namespace Tests.Delta
{
    [TestFixture]
    public class FlatLoopInjectionTests
    {
        public class Foo
        {
            public string Name { get; set; }
            public int a { get; set; }
            public string _a { get; set; }
            public bool b { get; set; }
            public DateTime d { get; set; }
            public Foo Parent { get; set; }
            public bool Bool { get; set; }
        }

        public class Flat
        {
            public string ParentName { get; set; }
            public int Parenta { get; set; }
            public string Parent_a { get; set; }
            public string Parentb { get; set; }
            public string ParentParentName { get; set; }
            public string oO { get; set; }
            public string d { get; set; }
            public object Bool { get; set; }
        }

        [Test]
        public void FlatTest()
        {
            var f = new Foo
                        {
                            Parent = new Foo
                                         {
                                             _a = "aaa",
                                             a = 23,
                                             b = true,
                                             Name = "v"
                                         }
                        };

            var flat = new Flat();

            flat.InjectFrom<Omu.ValueInjecter.Delta.Injections.FlatLoopInjection>(f);
            flat.Parent_a.IsEqualTo(f.Parent._a);
            flat.Parenta.IsEqualTo(f.Parent.a);
            flat.Parentb.IsEqualTo(null);
            flat.ParentName.IsEqualTo(f.Parent.Name);
            flat.ParentParentName.IsEqualTo(null);
            flat.oO.IsEqualTo(null);
            flat.d.IsEqualTo(null);
        }

        public class FlatBoolToString : Omu.ValueInjecter.Delta.Injections.FlatLoopInjection
        {
            protected override bool Match(string propName, PropertyInfo unflatProp, PropertyInfo targetFlatProp)
            {
                return propName == unflatProp.Name && unflatProp.PropertyType == typeof(bool) && targetFlatProp.PropertyType == typeof(string);
            }

            protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
            {
                var val = sp.GetValue(source).ToString();
                tp.SetValue(target, val);
            }
        }

        [Test]
        public void GenericFlatTest()
        {
            var f = new Foo
            {
                Parent = new Foo
                {
                    _a = "aaa",
                    a = 23,
                    b = true,
                    Name = "v"
                }
            };

            var flat = new Flat();

            flat.InjectFrom<FlatBoolToString>(f);
            flat.Parentb.IsEqualTo("True");
            flat.Bool.IsEqualTo(null);
        }
    }
}