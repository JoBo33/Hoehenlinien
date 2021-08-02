# Hoehenlinien

[Aufgabe](https://www.matse-ausbildung.de/hoehenlinien.html)

## Datenstruktur

Die Daten, die aus der Datei ausgelesen werden, können in einer Liste bestehend aus Arrays gespeichert werden. Die einzelnen Elemente der Liste sind die Messpunkte. Der erste Wert eines  Arrays ist die x-Koordinate, der zweite Wert ist die y-Koordinate und der dritte Wert stellt die Höhe dar. 
Die Liste könnte z. B. wie folgt aussehen:
```ruby
List<double[]> dataPoints =  new List<double[]>{ { 1, 1, 10 }, { 1, 2, 12.5 }, { 2, 1, 10 }, { 2, 2, 12.5 }, { 3, 2, 10 } };
```

##### Beispiel Raster

![Beispiel Raster](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Example%20Grid.png "Beispiel Raster")


## Einlesen der Dateidaten
Zuerst muss man die passende Bibliothek importieren 
```ruby
using System.IO;
```
Wenn dies erledigt ist, kann man mit einem "StreamReader" die Datei einlesen. Hierfür übergibt man bei der Instanziierung dem Konstruktor die Datei. Anschließend kann mit der Methode "ReadLine()" die Datei ausgelesen werden. Die Punkte sind folgenermaßen in die Datei eingetragen: 
1.0,2.0,4,2;4.0,2.0,5.8;...
Demnach muss mithilfe von Fallunterscheidungen geguckt werden, was die einzelnen Zahlen bedeuten und daher auch wo sie eingetragen werden müssen.


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

#### Problem beim obigen vorgehen
Wie erhält man die Funktion des Hügels, da lediglich das Raster gegeben ist?
Die oben genannte Methode die Höhenlinien darstellen zu können wurde verworfen, da ein Hügel nicht immer als Funktion darstellbar ist. Aus diesem Grund wird im Folgenden ein neuer Ansatz aufgeführt. 

#### Neuer Lösungsansatz zur Darstellung von Höhenlinien
Da verschiedene Punkte mit ihren jeweiligen Höhen angegeben werden, kann man verschiedene Höhen festlegen (z. B. jeden Meter) und gucken welche Punkte knapp unterhalb und knapp oberhalb der jeweiligen Höhe liegen. Anhand von diesen Punkten kann man Punkte bestimmen dessen Höhe die gesuchte Höhe ist. Mithilfe von Splines kann man im Anschluss die gefundenen Punkte so verbinden, dass diese die Höhenlinien bilden.

###### Beispiel 
Zur Darstellung des oben genannten Ansatzes nehme ich das einfache Raster vom Beginn der Dokumentation. Gesucht sind Punkte mit der Höhe 5.8m 
Dem Raster kann man entnehmendas unter anderem zwischen folgenden Punkten die Höhenlinie verlaufen muss:

1. Zwischen (1/3/5.5) und (2/3/6)
2. Zwischen (3/4/5.5) und (3/3/6)
3. Zwischen (3/2/6) und (4/2/5.5)
4. Zwischen (2/1/5.5) und (2/2/6)

Nun kann man jeweils zwischen den beiden gegebenen Punkte eine Grade erzeugen. Anschließend muss nur noch der Punkt (x/y/5,8) auf der Geraden gefunden werden.
Beispielrechnung mit den ersten Punkten:
![Beispiel Gerade](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Example%20Line.png "Beispiel Gerade")
Wann ist z = 5.8
5.8 = 5.5 + s * 0.5
0.6 = s
\
s in Gerade einsetzen
![Beispiel Punkt](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Example%20Point.png "Erhalte Punkt durch einsetzen")
Somit wurde der erste Punkt (1.6/3/5.8) gefunden der auf der Höhenlinie 5.8 liegt.

Dies muss nun noch für mindestens 3 weitere Punkte durchgeführt werden. Anschließend kann man mithilfe von Catmull-Rom-Splines die Höhenlinien bestimmen. Die Splines funktionieren indem man zwischen 2 Punkten ein eine Linie schafft, die abhängig von einer Gewichtung zwischen den beiden Punkten und den beiden nächsten Punkten ist. Dadurch werden die Höhenlinien allein unter der Bedingung der Punkte, die auf dieser Höhe liegen, bestimmt.

### Struktogramm Höhenlinien
![Beispiel Höhenlinien](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Struktogramm%20Determine%20contour%20lines.png "Struktogramm Höhenlinien")

Erst werden die Variablen vorbereitet. Die äußere For-Schleife beduetet das für jede Iteration eine Höhenlinie auf der Höhe i gesucht wird. Die innere Schleife ist für das Bestimmen von Punkten auf der Höhe i. Hierfür werden zuerst die Höhen aller senkrechten Nachbarpunkten verglichen und wenn möglich den Punkt zwischen diesen bestimmt, der auf Höhe i liegt. Anschlißend wird dasselbe zwischen horizontalen Nachbarknoten vollzogen wobei immer der Knoten j mit dem rechten Nachbarn verglichen wird, sodass der letzte Punkt einer Reihe übersprungen wird, da dieser keinen rechten Nachbarn besitzt. Zum Schluss, wenn alle Punkte einer Höhe gefunden wurden, kann die Höhenlinie mithilfe von Catmull-Rom-Splines gezeichnet werden. 

## Volumen berechnen
Für die Berechnung des Volumens wird die Massenberechnung mittels Höhenrost verwendet. Einfach erklärt berechnet man das Volumen indem man die Grundfläche von einem Rasterquadrat mit der durchschnittlichen Höhe der vier Ecken mulipliziert. Als Beispiel soll das Beispielraster vom Anfang genutzt werden. 
Da das Raster aus mehreren Quadraten besteht, kann man die durchschnittliche Höhe des gesamten Hügels bestimmen, indem man die einzelnen Ecken gewichtet. Die Gewichtung ist abhänging von der Position der Ecke. Ist sie in lediglich einem Quadrat, hat die Ecke eine Gewichtung von 1, ist sie in zwei Quadraten, hat sie eine Gewichtung von 2 usw..
Somit ergibt sich folgende Formel:

V = (Grundfläche * (4 * Summe(h4) + 3 * Summe(h3) + 2 * Summe(h2) + Summe(h1)) / 4  
Alternativ: Summe der einzelnen Rasterquadrate  
V = Summe(Vr), wobei Vr = Volumen Rasterquadrat = (a²*(h1+h2+h3+h4))/4

###### Beispiel 
![Beispiel Raster](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Example%20Grid.png "Beispiel Raster")

Grundfläche = 1 * 1 = 1  
Summe(h4) = 6+6+6+6 = 24  
Summe(h3) = 0  
Summe(h2) = 5,5+5,5+5,5+5,5+5,5+5,5+5,5+5,5 = 8 * 5,5 = 44  
Summe(h1) = 5+5+5+5 = 20  
  
V = (1 * (4 * 24 + 2 * 44 + 20)) / 4  
V = 51    

Somit besitzt der Hügel ein Volumen von 50 m³, unter der Bedingung, dass der Hügel bis auf Höhe des Meeresspiegels abgetragen werden soll. 

Alternativ:

V1 = 5.5  
V2 = 5.75  
V3 = 5.5  
V4 = 5.75  
V5 = 6  
V6 = 5.75  
V7 = 5.5  
V8 = 5.75  
V9 = 5.5  

V = 51  

Somit besitzt der Hügel ein Volumen von 50 m³, unter der Bedingung, dass der Hügel bis auf Höhe des Meeresspiegels abgetragen werden soll.

### Struktogramm zur Volumenberechnung

![Struktogramm Volumen](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/Struktogramm%20Calculate_Volume.png "Struktogramm Volumenberechnung")

Zu Beginn wird die Datei ausgelesen und die Variablen für die Berechnung vorbereitet. Mithilfe der ersten For-Schleife wird die Anzahl der Punkte bestimmt, die in einer Zeile des Rasters liegen. Anschlißend wird die Grundläche bestimmt und in der nächsten For-Schleife werden die Volumen von den einzelnen Rasterquadrate bestimmt. Hierbei werden die letzten Punkte einer Zeile übersprungen, da immer das Quadrat von i zu i+1 berechnet wird und im Falle von i = letzter Punkt einer Zeile wäre i+1 nicht Teil der Zeile und somit sind i und i+1 in diesem Fall nicht Teil eines Quadrates. Zuletzt werden die einzelnen Volumen auf das Gesamtvolumen addiert.

## UML-Klassendiagramm
![Klassendiagramm](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/UML-Classdiagram%20Hillproject.png "Beispiel Klassendiagramm")

Das Projekt besteht aus vier Klassen. Die Hauptklasse ist "Form1(Hillproject)". Die weiteren Klassen dienen der Unterstützung. In der Klasse "Calculation" weerden die Berechnungen durchgeführt, in der Klasse "Drawing" werden die Profile und Höhenlinien gezeichnet und die Klasse "EditingData" ist zum Bearbeiten der Messdaten. Die Hauptklasse ruft Methoden der anderen drei Klassen auf, daher ist die Beziehung zwischen den Klassen eine gerichtete Assoziation. Dasselbe gilt für die Beziehung zwischen "Drawing" und "EditingData".

## UML-Sequenzdiagramme 
##### Dateiauslesen
![Sequenzdiagramm Datei](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/UML-Sequency%20diagramm%20Read%20files.png "Sequenzdiagramm Dateiauslesen")

Die Klasse "Hillproject" erstellt ein Objekt der Klasse "OpenFileDialog" und ruft mit diesem die Methode "ShowDialog" auf. Anschließend wird der Name der ausgewählten Datei gespeichert. Daraufhin wird mit mithilfe eines "StreamReader" Objektes die ausgewählte Datei ausgelesen. Zuletzt wird die Liste durch die Methode "FillData" aus der Klasse "EditingData" befüllt und als Tabelle ausgegeben.

##### Volumenberechnung
![Sequenzdiagramm Volumen](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/UML-Sequnecy%20diagram%20volumecalculation.png "Sequenzdiagramm Volumenberechnung")

Nachdem die Datei erfolgreich ausgelesen wurde kann das Volumen des Hügels bestimmt werden. Zur Bestimmung des Volumens müssen die Messdaten zuerst in die Rastermusterreihenfolge gebracht werden. Anschließend werden die Höhen angepasst, da der Hügel sonst immer auf die Höhe des Meeresspiegels reduziert werden würde, was teilweise ein Loch im Boden hinterlassen kann. Bevor das Volumen entgülitg berechnet werden kann muss herausgefunden werden, wie viele Punkte in einer Zeile des Rasters sind. Anschließend wird das Volumen wie oben beschrieben berechnet und schlussendlich ausgegeben.

##### Anzahl LKW
![Sequenzdiagramm LKW](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/UML-Sequnecy%20diagram%20number%20of%20trucks.png "Sequenzdiagramm Anzahl LKW")

Was einem auf dem ersten Blick sofort auffällt ist, dass sich im Vergleich zur Volumenberechnung lediglich die Ausgbabe ändert. Hier wird nicht einfach das Volumen ausgeben, sonder tabellarisch die Zeit, die die LKW (abhängig von der Anzahl) brauchen um den Hügel abzutransportieren.

##### Hügelprofile
![Sequenzdiagramm Profil](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/UML-Sequnecy%20diagram%20hill%20profiles.png "Sequenzdiagramm Hügelprofile")

Auch hier bleibt der Anfang gleich zu den beiden vorherigen, anschließend werden jedoch die einzelnen Profile mithilfe einer For-Schleife ermittelt und ausgeben.

##### Höhenlinien 

![Sequenzdiagramm Höhenlinie](https://github.com/JoBo33/Hoehenlinien/blob/main/Example-Pictures/UML-Sequnecy%20diagram%20contour%20lines.png "Sequenzdiagramm Höhenlinie")

Das letzte Diagramm bildet die Höhenlinien ab. Auch hier ist der Beginn wieder gleich wie bei den anderen. Im Vergleich zu den Hügelprofilen beginnt die Schleifen nach dem finden der größten Höhe. Wenn alle Punkte einer Höhe gefunden wurden müssen diese noch sortiert werden. Ist dies erledigt kann man die Höhenlinie der derzeitigen Iteration mithilfe von Catmull-Rom-Splines zeichnen lassen. 







