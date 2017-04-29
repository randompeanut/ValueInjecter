using NUnit.Framework;
using Omu.ValueInjecter.Delta;
using Tests.SampleTypes.PrivateSetter;

namespace Tests.Delta
{
    [TestFixture]
    public class PrivateSetterTests
    {
        [Test]
        public void ShouldNotUsePrivateGetterProperty()
        {
            var src = new Privategetset { Pget = "hi", BasePget = "hi2" };

            var res = new Publicgetset();

            res.InjectFrom(src);
            Assert.IsNull(res.Pget);
            Assert.IsNull(res.BasePget);
            
            res.InjectFrom<Omu.ValueInjecter.Delta.Injections.LoopInjection>(src);
            res.InjectFrom<Omu.ValueInjecter.Delta.Injections.FlatLoopInjection>(src);
            res.InjectFrom<Omu.ValueInjecter.Delta.Injections.UnflatLoopInjection>(src);
            Assert.IsNull(res.Pget);
            Assert.IsNull(res.BasePget);
        }

        [Test]
        public void ShouldNotUsePrivateSetterProperty()
        {
            var src = new
                {
                    Pset = "hi",
                    BasePset = "hi2",
                };

            var res = new Privategetset();
            res.InjectFrom(src);

            Assert.IsNull(res.Pset);
            Assert.IsNull(res.BasePset);
            
            res.InjectFrom<Omu.ValueInjecter.Delta.Injections.LoopInjection>(src);
            res.InjectFrom<Omu.ValueInjecter.Delta.Injections.FlatLoopInjection>(src);
            res.InjectFrom<Omu.ValueInjecter.Delta.Injections.UnflatLoopInjection>(src);
            Assert.IsNull(res.Pset);
            Assert.IsNull(res.BasePset);
        }
    }
}