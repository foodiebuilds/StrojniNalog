# Pregled projekta

Pregled prevzetega projekta StrojniNalog (izvorni repozitorij `anzo26/StrojniNalog`, stanje ob prevzemu: commit `4ef9802`). Aplikacijo sem zagnal lokalno z Docker Compose, preizkusil potek z naročnikom, organizatorjem in dvema izvajalcema ter pregledal kodo.

## Implementirane funkcionalnosti MVP

| Št. | Funkcionalnost | Stanje |
| --- | --- | --- |
| 1 | Oddaja naročila za storitev | Implementirano |
| 2 | Obravnava naročila | Implementirano |
| 3 | Priprava in razporejanje delovnega naloga | Implementirano |
| 4 | Preverjanje zasedenosti | Implementirano |
| 5 | Spremljanje poteka izvedbe | Implementirano |
| 6 | Evidentiranje opravljenega dela | Implementirano |
| 7 | Preverjanje in zaključevanje naloga | Implementirano |

Delujejo tudi registracija naročnikov, prijava in dostop po vlogah (Keycloak). Funkcionalnosti 8–12 niso implementirane, kar je skladno z obsegom MVP v `docs/02-funkcionalnosti-in-obseg.md`.

## Ustreznost uporabniškim zgodbam

Pregledal sem US-01 do US-05.

| Zgodba | Zadošča | Kako je izpolnjena |
| --- | --- | --- |
| US-01 Oddaja naročila | Da | Naročnik izbere aktivno storitev ter vnese opis, lokacijo in želeni termin. Brez teh podatkov ali z neveljavnim terminom se naročilo ne shrani. Po oddaji je naročilo v stanju `ODDANO`, naročnik pa vidi njegovo številko. |
| US-02 Obravnava naročila | Da | Samo organizator lahko potrdi ali zavrne naročilo v stanju `ODDANO`. Zavrnitev brez razloga ni mogoča. Naročnik pri naročilu vidi odločitev in razlog zavrnitve. |
| US-03 Priprava delovnega naloga | Da | Nalog je mogoče pripraviti samo za potrjeno naročilo brez naloga. Obvezni so stroj, izvajalec, začetek in konec, konec mora biti po začetku. Shranjen nalog je v stanju `NACRTOVAN`, drugi nalog za isto naročilo je zavrnjen. |
| US-04 Preverjanje zasedenosti | Da | Prekrivanje s terminom istega stroja ali izvajalca je zavrnjeno, obvestilo pove, kaj je zasedeno. Termin, ki se začne ob koncu drugega, je dovoljen. Sočasne rezervacije prepreči omejitev v bazi (`EX_DelovniNalogi_StrojTermin`, `EX_DelovniNalogi_IzvajalecTermin`). |
| US-05 Spremljanje poteka izvedbe | Da | Izvajalec vidi svoje naloge, označi začetek (`V_IZVAJANJU`) in nato konec dela. Naročnik vidi stanje naloga pri svojem naročilu, organizator vidi vse naloge. Izvajalec ne more spreminjati naloga drugega izvajalca. |
