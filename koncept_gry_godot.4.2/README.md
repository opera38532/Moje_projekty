Bit wizard to projekt gry stworzony w oparciu o silnik godot 4.2 przy pomocy wbudowanego języka gdscript

Funkcjonalność:
    -implementacja prostego systemu walki turowej w stylu JRPG, zdrowie przeciwników wyświetlane jest w systemie binarnym a zadaniem gracza jest pokonać przeciwników najmniejszym kosztem zdrowia przy pomocy akcji operujących na zapisie binarnym zdrowia przeciwników, np. bit shift, bit flip, bit rotation... każdy z przeciwników prezentuje inne zachowanie
    -biały (normal): zawsze atakuje za 1 obrażeń
    -fioletowy (archer): atakuje x obrażeń gdzie x to ilość jedynek w zapisie bbinarnym jego ilości zdrowia
    -pomarańczowy (berserk): atakuje za połowe brakującego zdrowia

Skrypty godne uwagi:
    -Scenes and scripts/Temporary/env_1.gd
zarząda kolejnością walki oraz usuwa pokonanych przeciwników
    -Scenes and scripts/FightUi/action_panel.gd
generuje proceduralnie Ui na podstawie dostępnych akcji
    -Scenes and scripts/Binary display/binary_display_unit.gd
generuje oraz zarzadza paskiem zdrowia przeciwnika

Jak poprawnie przeglądać projekt oraz uruchomić aplikacje:
-Zaimportować folder jako projekt w aplikacji Godot 4.5.1
-Uruchomić projekt w trybie edycji
-Uruchom projekt (F5)
alternatywnie:
-plik wykonywalny Bit wizard.exe


