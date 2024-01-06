# ZH_Sablon

ZH sablonok eva zh-ra.
Mindhárom keretrendszerre 4 sablon:
- Alap: nxn-es dinamikus gombrács állítható tulajdonságokkal
        új nxn-es játék, ablak újraméretezéssel
- Persistence: Alap + Mentés/Betöltés funkció
               WPF-ben és MAUI-ban fókuszváltásnál automatikus mentés/betöltés. WinFormsban nincs, de ugyanúgy megoldható
               MAUI-ban FileSystem.AppDataDirectory-ba való mentés. Külön screen helyett popupokkal van megvalósítva
               WPF-ben és WinForms-ban OpenFileDialog és SaveFileDialog.
               A Persistence nincs befecskendezve a modellbe, hanem az App köti össze őket, mert ez egyszerűbb (és szerintem logikusabb/jobb/*tisztább*)
- Timer: Alap + Timer
         A timer a modellben van, lehet állítani az intervallumot.
         Számon tarja az eltelt időt is
         Pause/Resume
         WPF-ben és Maui-ban fókuszvesztésnél a játék szünetel.
- Timer + Persistence: Minden egyben
                       WPF-ben és Maui-ban fókuszváltásnál van automatikus mentés/betöltés, fokúszvesztésnél szüntetlés.
                       Ha az ablak fókuszba kerül, a játék folytatódik annak ellenére hogy látszólag nincs ilyen funkcionalitás megvalósítva.
                           Ez azért van, mert amikor az ablak fókuszba kerül, be fog töltődni a félbeszakított játék, és a betöltés folytatja is a játékot.
                           Ha nem szeretnéd, hogy a játék folytatódjon amikor az ablak fókuszba kerül, akkor a viewmodel konstruktorának adj meg egy bool toStartGame
                           paramétert, és a konstruktor végén a _model.StartGame() sort írd át if (toStartGame) _model.StartGame() - re így tudod majd szabályozni melyik
                           konstruktorhíváskor indítsa/ne indítsa el a játékot.

  Sok sikert!
