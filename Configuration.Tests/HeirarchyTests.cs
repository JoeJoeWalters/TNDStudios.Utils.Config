using FluentAssertions;
using System;
using System.Collections.Generic;
using TNDStudios.Utils.Configuration;
using TNDStudios.Utils.Configuration.Mocks;
using Xunit;

namespace Configuration.Tests
{
    public class HeirarchyTests
    {
        [Fact]
        public void Test1()
        {
            // ARRANGE
            TaxonomyContainer _container = new TenantTaxonomyContainer();

            // ACT
            Dictionary<String, TaxonomyProperty> _module1 = _container.Read("main", "Tenant1.Application1.Module1");
            Dictionary<String, TaxonomyProperty> _module2 = _container.Read("main", "Tenant1.Application1.Module2");

            // ASSERT
            _module1["API::Unchanging"].Value.Should().Be(_module2["API::Unchanging"].Value);
            _module1["API::Version"].Value.Should().Be("3.2");
            _module2["API::Version"].Value.Should().Be("3.3");
        }
    }
}
