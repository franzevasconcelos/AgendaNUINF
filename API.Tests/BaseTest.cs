using AgendaNUINF.API.Mapeamentos;
using AutoMapper;
using NUnit.Framework;

namespace API.Tests {
    public abstract class BaseTest {
        [OneTimeSetUp]
        public void TestFixtureSetup() {
                Mapper.Initialize(cfg => cfg.AddProfiles(typeof(PessoaMapper).Assembly));
        }

        [OneTimeTearDown]
        public void TestTearDown() {
            Mapper.Reset();
        }
    }
}