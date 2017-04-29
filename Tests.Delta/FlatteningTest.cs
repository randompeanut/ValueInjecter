using System;
using System.Reflection;
using NUnit.Framework;
using Omu.ValueInjecter.Delta;
using Tests.SampleTypes;
using Tests.Utils;

namespace Tests.Delta
{
    [TestFixture]
    public class FlatteningTest
    {
        public class Boo
        {
            public Boo Parent { get; set; }
        }

        public class Foo
        {
            public Foo Foo1 { get; set; }
            public Foo Foo2 { get; set; }
            public string Name { get; set; }
            public string NameZype { get; set; }
            public int Age { get; set; }

            public Foo FooR
            {
                get
                {
                    return null;
                }
            }

            public string Prop1
            {
                set
                {
                    Name = value;
                }
            }
        }

        public class FlatFoo
        {
            public string Foo1Foo2Foo1Name { get; set; }
            public string Foo1Name { get; set; }
            public string Foo2Name { get; set; }
            public string Foo2NameZype { get; set; }
            public string Foo1Age { get; set; }
            public bool Age { get; set; }
            public string FooRName { get; set; }

            public string Foo1Prop1
            {
                set
                {
                    Foo1Name = value;
                }
            }
        }

        public class IntToStringFlat : Omu.ValueInjecter.Delta.Injections.FlatLoopInjection
        {
            protected override bool Match(string propName, PropertyInfo unflatProp, PropertyInfo targetFlatProp)
            {
                return propName == unflatProp.Name && unflatProp.PropertyType == typeof(int) && targetFlatProp.PropertyType == typeof(string);
            }

            protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
            {
                var val = sp.GetValue(source).ToString();
                tp.SetValue(target, val);
            }
        }

        [Test]
        public void Unflattening()
        {
            var flat = new FlatFoo
                           {
                               Foo1Foo2Foo1Name = "cool",
                               Foo1Name = "abc",
                               Foo2Name = "123",
                           };

            var unflat = new Foo();

            unflat.InjectFrom<Omu.ValueInjecter.Delta.Injections.UnflatLoopInjection>(flat);

            unflat.Foo1.Foo2.Foo1.Name.IsEqualTo("cool");
            unflat.Foo1.Name.IsEqualTo("abc");
            unflat.Foo2.Name.IsEqualTo("123");
        }

        [Test]
        public void Flattening()
        {
            var unflat = new Foo
                             {
                                 Name = "foo",
                                 Foo1 = new Foo
                                            {
                                                Name = "abc",
                                                Foo2 = new Foo { Foo1 = new Foo { Name = "inner" } }
                                            },
                                 Foo2 = new Foo { Name = "123", NameZype = "aaa" },
                             };

            var flat = new FlatFoo();

            flat.InjectFrom<Omu.ValueInjecter.Delta.Injections.FlatLoopInjection>(unflat);

            flat.Foo2NameZype.IsEqualTo("aaa");
        }

        [Test]
        public void GenericFlatTest()
        {
            var foo = new Foo { Foo1 = new Foo { Age = 18 } };
            var flat = new FlatFoo();

            flat.InjectFrom<IntToStringFlat>(foo);
            flat.Foo1Age.IsEqualTo("18");
        }

        [Test]
        public void BasicTest()
        {
            var customer = GetCustomer();
            var res = new CustomerInput();

            res.InjectFrom<Omu.ValueInjecter.Delta.Injections.FlatLoopInjection>(customer);
            res.InjectFrom<Omu.ValueInjecter.Delta.Injections.UnflatLoopInjection>(customer);
        }

        private static Customer GetCustomer()
        {
            var customer = new Customer { FirstName = "Art", LastName = "Vandelay", Id = 123, RegDate = DateTime.UtcNow };
            return customer;
        }
    }
}