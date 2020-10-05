namespace Application.UnitTests.Configuration
{
    public class TypedTestBase<T> : TestBase
    {
        protected T Sut => _mock.Create<T>();
    }
}