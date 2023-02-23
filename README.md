# ConsoleApp-Dataedo
Lista poprawek:
- poprawiona nazwa pliku csv
- wydzielenie kodu z metody ImportAndPrintData do osobnych metod: oddzielne do tworzenia obiektu, normalizacji danych, do przypisywania dzieci, tj. przypisywanie tabel do baz danych i kolumn do tabel, oraz osobna do wypisywania danych
- wydzielenie osobnych klas do przedstawienia baz danych, tabel i kolumn, zamiast jednej klasy ImportedObject - poprawia to czytelność kodu
- użycie zmiennej printData, wcześniej zmienna była parametrem metody, ale nie była nigdzie używana
- dodanie bloku try catch przy próbie zczytywania danych z pliku
- dodanie sprawdzenia czy zczytana linia w pliku nie jest pusta
