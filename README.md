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


## Darstellung der Höhenlinien

##### Wie kann ein 3D-Sachverhalt in 2D gezeichnet werden?
Offensichtlich ist ein Hügel ein dreidimensionales Objekt, doch wie kann man dieses in ein "normales" Koordinatensystem zeichnen. Wie der Name des Projektes schon verrät, kann man dies mithife der Höhenlinien. Der Hügel lässt sich als im Allgemeinen als f(x,y) = z (= c) darstellen. Der Funktionswert z gibt dabei die Höhe des Hügels am Punkt (x/y) an. Wenn man diesen Sachverhalt in einem zweidimensionalen Koordinatensystem zeichnen möchte, kann man sich das vorstellen, als ob man von oben auf den Hügel guckt, wobei die einzelnen Linien alle Punkte mit derselben Höhe sind. Daher können die Linien sich nicht schneiden. Um die einzelnen Linien herauszufinden muss die Gleichung z. B. nach y umgeformt werden und im Anschluss ein Wert für c eingesetzt werden. Dadurch entsteht eine Funktion f(x) die bekanntlich eingezeichnet werden kann. Sie stellt somit alle Punkte der eingesetzten Höhe c dar.

###### Beispiel 
Gegeben sei eine Fuktion f(x,y) = -((x-4)²+(y-4)²)+4, welche einen Hügel darstellt. Zeichne den Hügel mithilfe der Höhenlinien von z=1, z=2, z=3.

c = -((x-4)²+(y-4)²)+4

nach y umformen
                c-4 = -((x-4)²+(y-4)²)
               -c+4 = (x-4)²+(y-4)²
        -c+4-(x-4)² = (y-4)²
  sqrt(-c+4-(x-4)²) = y-4
sqrt(-c+4-(x-4)²)+4 = y

Jetzt nur noch die verschiedenen Werte für c einsetzten und man erhält die Höhenlinien (Bedenke, dass einmal in die positive Wurzel einzusetzen ist **und** in die negative Wurzel. 

![Höhenlinien Beispiel](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Example%20Calculation.png)
##### Wie werden Höhenlinien in diesem Projekt dargestellt?
Wie oben bereits erwähnt werden mithilfe einer "ContourSeries" die Höhenlinien dargestellt. Hierfür initialisiert man eine "ContourSeries". Bei der Initialisierung werden die Achsenlängen festgelegt und die Funktionwerte mit den dazugehörigen Koordinate übergeben, wodurch die Höhenlinien gezeichnet werden können. 

###### Beispiel
```ruby
        PlotModel model = new PlotModel { Title = "ContourSeries" };
        
        public void DrawContours()
        {

            double x0 = 0;
            double x1 = 6;
            double y0 = 0;
            double y1 = 6;

            //generate values
            Func<double, double, double> peaks = (x, y) => -(Math.Pow(x - 4, 2) + Math.Pow(y - 4, 2)) + 4;
            double[] xx = ArrayBuilder.CreateVector(x0, x1, 100);
            double[] yy = ArrayBuilder.CreateVector(y0, y1, 100);
            double[,] peaksData = ArrayBuilder.Evaluate(peaks, xx, yy);

            ContourSeries cs = new ContourSeries
            {
                Color = OxyColors.Black,
                LabelBackground = OxyColors.White,
                ColumnCoordinates = yy,
                RowCoordinates = xx,
                Data = peaksData
            };
            model.Series.Add(cs);
        }
```
Wenn diese Methode mit der Funktion **f(x,y) = -((x-4)²+(y-4)²)+4** (siehe oben) asuführt, kommt folgendes Ergebnis.

![Beispiel ContourSeries](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Example%20ContourSeries.png "Beispiel ContourSeries")

## Problem beim obigen vorgehen
Wie erhält man die Funktion des Hügels, da lediglich das Raster gegeben ist?


## Volumen berechnen
Für die Berechnung des Volumens wird die Massenberechnung mittels Höhenrost verwendet. Einfach erklärt berechnet man das Volumen indem man die Grundfläche von einem Rasterquadrat mit der durchschnittlichen Höhe der vier Ecken mulipliziert. Als Beispiel soll das Beispielraster vom Anfang genutzt werden. 
Da das Raster aus mehreren Quadraten besteht, kann man die durchschnittliche Höhe des gesamten Hügels bestimmen, indem man die einzelnen Ecken gewichtet. Die Gewichtung ist abhänging von der Position der Ecke. Ist sie in lediglich einem Quadrat, hat die Ecke eine Gewichtung von 1, ist sie in zwei Quadraten, hat sie eine Gewichtung von 2 usw..
Somit ergibt sich folgende Formel:

V = (Grundfläche * (4 * Summe(h4) + 3 * Summe(h3) + 2 * Summe(h2) + Summe(h1)) / 4

###### Beispiel 
![Beispiel Raster](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Example%20Grid.png "Beispiel Raster")

Grundfläche = 1 * 1 = 1  
Summe(h4) = 6+6+6+6 = 24  
Summe(h3) = 0  
Summe(h2) = 5,5+5,5+5,5+5,5+5,5+5,5+5,5+5,5 = 8 * 5,5 = 42  
Summe(h1) = 5+5+5+5 = 20  
  
V = (1 * (4 * 24 + 2 * 42 + 20)) / 4  
V = 50  
  
Somit besitzt der Hügel ein Volumen von 50 m³.









