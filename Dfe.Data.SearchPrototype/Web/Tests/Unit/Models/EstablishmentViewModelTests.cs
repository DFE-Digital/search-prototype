using Dfe.Data.SearchPrototype.Web.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dfe.Data.SearchPrototype.Web.Tests.Unit.Models;

public class EstablishmentViewModelTests
{
    [Fact]
    public void AddressAsString_ReturnsFormattedString()
    {
        EstablishmentViewModel establishmentViewModel = new()
        {
            Urn = "90909",
            Name = "name",
            Address = new()
            {
                Street = "street",
                Locality = "locality",
                Address3 = "address3",
                Town = "town",
                Postcode = "postcode",
            }
        };
        var expected = "street, locality, address3, town, postcode";
        var result = establishmentViewModel.AddressAsString();

        Assert.Equal(expected, result);
        result.Should().Be(expected);
    }

    [Fact]
    public void AddressAsString_NullLocality_ReturnsFormattedString()
    {
        EstablishmentViewModel establishmentViewModel = new()
        {
            Urn = "90909",
            Name = "name",
            Address = new()
            {
                Street = "street",
                Address3 = "address3",
                Town = "town",
                Postcode = "postcode",
            }
        };
        var expected = "street, address3, town, postcode";
        var result = establishmentViewModel.AddressAsString();

        Assert.Equal(expected, result);
        result.Should().Be(expected);
    }

}
