# Programátorská dokumentace

# Celkový beh programu
* Všetko beží v jednom formulári **MainForm**
* MainForm má stav hry reprezentovaný enumom **GameState** (MainMenu, Playing, GameOver, Leaderboard)
* Na základe stavu sa v OnPaint volá vykresľovanie MainMenu, Renderer, GameOverScreen alebo Leaderboard
* Ovládanie klávesnice sa spracúva v MainForm.MoveFunction, ktorá podľa stavu odovzdáva stlačené klávesy buď **MainMenu**, alebo **GameEngine**

## Rozdelenie do hlavných tried a ich interakcie
* MainForm
* GameSpace
* Pos
* BlokSystem
* Blok
* J,K,I...Blok
* GameEngine
* Renderer 
* CheatinBox
* MainMenu
* LeaderBoard
* GameOverScreen

### MainForm
* Je hlavný riadiaci objekt
* V OnPaint rozhoduje, čo sa má vykresliť podľa state
V MoveFunction odchytáva klávesy a posúva ich ďalej: pri MainMenu → mainMenu.HandleKey(), pri Playing → volá metódy engine (napr. MoveLeftIfPossible()), pri Leaderboard → ESC prepne späť na MainMenu, pri GameOver → ENTER reset hry
* Má Timer gameTimer, ktorý pravidelne posúva blok nadol (engine.MoveBlokDown()), jeho rýchlosť sa upravuje podla akutalneho score

### GameSpace
* Reprezentuje stav hracej plochy (dvojrozmerné pole / zoznam)
* Obsahuje položené bloky (ktoré už dopadli)
* Dáta z GameSpace sa používajú na vykreslenie celej plochy
### Pos
* Drží offsety

### BlokSystem
* Drží bloky a generuje ich

### Blok 
* Drží funkcie blokov

### J,K,I...Blok
* Drží polohy blokov v rotáciach + ich štart offset
* Každý blok dedí od Blok funkcie

### GameEngine
* Uchováva hernú logiku: aktuálny blok, pohyb, rotácie, kolízie, mazanie riadkov
* Má referenciu na **GameSpace** (herná mriežka)
* Vie poskytnúť aktuálne skóre (GetTtlScore())
* Keď blok dopadne, jeho dlaždice sa prenesú do GameSpace.grid

### Renderer
* Slúži na vizuálne vykreslenie hry (Draw(Graphics g, Size clientSize))
* Má referenciu na GameEngine, z ktorého si berie:
aktuálny blok (ten sa kreslí zvlášť), obsah GameSpace.grid (nakreslenie hernej plochy), skóre alebo iné údaje, ktoré chceme zobraziť
* Nepozná priamo klávesnicu ani MainForm, iba pasívne vykresľuje

### CheatinBox
* Vykresľuje cheatovacie funkcie
* Ukladá stav, či sa zadáva meno a aké meno užívateľ zadal

### MainMenu
* Zobrazuje menu a spracúva časť klávesnice počas GameState.MainMenu (prepínanie cheat módu, zadávanie mena)

### LeaderBoard
* Uchováva mapu (meno → najvyššie skóre)
* Vie uložiť a načítať zo súboru
* Vykreslí tabuľku cez Draw()
* Skóre sa pridáva cez leaderboard.AddScore() po skončení hry

### GameOverScreen
* Jednoduché vykreslenie obrazovky Game Over + skóre

# Reprezentácia dát 
### Bloky
* V pohybe sú uložené ako objekt (CurrentBlook) v **GameEngine**
* Po dopade za ich dlaždice zapíšu do GameSpace.grid

### Herná plocha
* GameSpace.grid obsahuje položené bloky a prázdne bolky, sú reprezentované čislom, 0 = prázny, !0 = plný
*Renderer kreslí plochu tak, že: vykreslí celé GameSpace.grid, navrch ešte aktuálny pohybujúci sa blok.