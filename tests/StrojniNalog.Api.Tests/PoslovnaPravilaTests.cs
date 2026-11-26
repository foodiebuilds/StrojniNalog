using Microsoft.EntityFrameworkCore;
using StrojniNalog.Api.Bll;
using StrojniNalog.Api.Bll.Enums;
using StrojniNalog.Api.Data;
using StrojniNalog.Api.DTO.DelovniNalogi;
using StrojniNalog.Api.DTO.Narocila;
using StrojniNalog.Api.Models.Enums;

namespace StrojniNalog.Api.Tests;

// Pravila v teh testih se preverijo pred dostopom do baze, zato baza ni potrebna.
public class PoslovnaPravilaTests
{
    private static AppDbContext UstvariKontekst()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql("Host=localhost;Database=testi")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task ZavrnitevNarocilaBrezRazlogaJeZavrnjena()
    {
        var bll = new NarociloBll(UstvariKontekst());

        var rezultat = await bll.ZavrniAsync(1, new ZavrniNarociloDto { Razlog = " " });

        Assert.Equal(ObravnavaNarocilaRezultat.NeveljavenRazlog, rezultat);
    }

    [Fact]
    public async Task NalogSKoncemPredZacetkomJeZavrnjen()
    {
        var bll = new DelovniNalogBll(UstvariKontekst(), null!, null!);
        var zacetek = new DateTimeOffset(2026, 12, 1, 10, 0, 0, TimeSpan.Zero);
        var dto = new PripraviDelovniNalogDto
        {
            NarociloId = 1,
            StrojId = 1,
            IzvajalecId = 1,
            PredvideniZacetek = zacetek,
            PredvideniKonec = zacetek.AddHours(-2),
        };

        var odgovor = await bll.PripraviAsync(dto);

        Assert.Equal(PripravaNalogaRezultat.NeveljavenTermin, odgovor.Rezultat);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1001)]
    public async Task OddajaEvidenceZNeveljavnimiUramiJeZavrnjena(int ure)
    {
        var bll = new EvidencaDelaBll(UstvariKontekst());
        var dto = new OddajEvidencoDto { OpravljeneUre = ure, OpisIzvedbe = "Izkop jarka" };

        var rezultat = await bll.OddajAsync(1, 1, dto);

        Assert.Equal(EvidencaDelaRezultat.NeveljavneUre, rezultat);
    }

    [Fact]
    public async Task OddajaEvidenceBrezOpisaJeZavrnjena()
    {
        var bll = new EvidencaDelaBll(UstvariKontekst());
        var dto = new OddajEvidencoDto { OpravljeneUre = 8, OpisIzvedbe = " " };

        var rezultat = await bll.OddajAsync(1, 1, dto);

        Assert.Equal(EvidencaDelaRezultat.ManjkajocOpis, rezultat);
    }

    [Fact]
    public async Task VrnitevEvidenceBrezRazlogaJeZavrnjena()
    {
        var bll = new ObravnavaEvidenceBll(UstvariKontekst());

        var rezultat = await bll.VrniAsync(1, "");

        Assert.Equal(ObravnavaEvidenceRezultat.ManjkajocRazlog, rezultat);
    }
}
