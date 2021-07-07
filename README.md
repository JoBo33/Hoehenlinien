# Hoehenlinien

[Aufgabe](https://www.matse-ausbildung.de/hoehenlinien.html)

## Datenstruktur

Die Daten, die aus der Datei ausgelesen werden, können in einem dreidimensionalen Array gespeichert werden. Die der erste Eintrag ist die x-Koordinate. Der zweite Eintrag ist die y-Koordinate und der letzte Eintrag ist die jeweilige Höhe. Bildlich dargestellt entsteht dadurch ein Raster mit einer Höhenangabe an jeder Ecke.

##### Beispiel Raster

![Beispiel Raster](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Example%20Grid.png "Beispiel Raster")


## Darstellung der Gui

##### Was wird benötigt?
- Diagramm zur Darstellung der sortierten verschiedenen Höhen mithilfe einer "ColumnSeries" aus der Oxyplot-Bibliothek
- Diagramm zur Darstellung der verschiedenen Höhen (Top-Down-View) mit Höhenlinien mithilfe einer "ContourSeries" aus der Oxyplot-Bibliothek
- Button zur Berechnung des Volumens
- Button zur Berechnung der optimalen Anzahl an LKW's
- Textbox zur Ausgabe der Lösung
- NumericUpDown zur Abfrage bis zu welcher Höhe der Hügel abgeflacht werden soll
- DataGridView als Tabelle für die Zeiten bei verschiedener Anzahl an LKW's
- Mehrere label zum Beschriften

##### Beispiel Gui
![Beispiel Gui](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Example%20Gui.png "Beispiel Gui")

