using Microsoft.Extensions.DependencyInjection;

namespace FluentAssertions.Microsoft.Extensions.DependencyInjection
{
    public static class AssertionExtensions
    {
        public static ServiceCollectionAssertions Should(this IServiceCollection services)
        {
            return new ServiceCollectionAssertions(services);
        }
    }
}
