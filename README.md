# Zápočtový program: Tetris - WinFormApp



## Specifikace
Hra je kompletne ovládaná klávesnicou.
Hra začína start screenom, na ktorom sa dá zadať meno, zobraziť leaderboard, zapnúť cheaty.
Leaderboard sa ukládá, takže, keď si hráč zadá svoje meno, môže si vylepšovať a ukladať svoje score.
Pri stlačení ENTER sa mení scéna a začínam nám bežať hlavná časť - Tetris.
Generujú sa nám náhodné bloky, ktoré začínajú padať smerom dole, rýchlosť ktorou padajú je závislá na výške score.
Keď sa naplní akýkolvek riadok, tento riadok sa vyprázdni, pripočíta sa score a všetky riadky nad ním spadnú dole.
Generované bloki môže užívateľ rotovať, pohybovať doľava - doprava a prípadne ich len hned zhodit dole.
Pri zapnutí cheatov užívateľ vidí aký budúci blok.
Užívatel stále vidí svoje aktualne skóre.
Pri prehre - akýkolvek položený blok, ktorý sa dotýka horných dvoch riadkov, sa spúšta GameOver screen na ktorom vidíme naše score a po stlačení ENTER nás to posiela spať na start screen, kde si môžeme upravit / nechať naše meno a hrať ďalej.

## Instalace a spuštění

Pro spuštění přejděte do složky `JMENO_PROJEKTU` a spusťte příkaz `dotnet run`.
Pre spustenie som pridal ešte zip, kde sa nachádza exe a leaderboard.txt, oba musia byť v rovnakom adresári pre správne fungovanie.

## Dokumentace

* [Uživatelská dokumentace](docs/user.md)
* [Ukázky použití](docs/examples.md)
* [Programátorská dokumentace](docs/programmer.md)
