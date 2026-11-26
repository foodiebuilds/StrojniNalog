using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using StrojniNalog.Api.DTO.Narocila;
using StrojniNalog.Api.Security;

namespace StrojniNalog.Api.Tests;

public class VarnostInValidacijaTests
{
    [Fact]
    public async Task VlogeIzKeycloakaPostanejoClaimi()
    {
        var identiteta = new ClaimsIdentity(
            [new Claim("realm_access", """{"roles":["organizator","offline_access"]}""")],
            "test");
        var transformacija = new KeycloakRolesTransformation();

        var rezultat = await transformacija.TransformAsync(new ClaimsPrincipal(identiteta));

        Assert.True(rezultat.IsInRole("organizator"));
        Assert.False(rezultat.IsInRole("offline_access"));
    }

    [Fact]
    public void PraznoNarociloNiVeljavno()
    {
        var dto = new UstvariNarociloDto();
        var napake = new List<ValidationResult>();

        var veljavno = Validator.TryValidateObject(dto, new ValidationContext(dto), napake, validateAllProperties: true);

        Assert.False(veljavno);
        Assert.Contains(napake, napaka => napaka.ErrorMessage == "Izberite vrsto storitve.");
    }
}
