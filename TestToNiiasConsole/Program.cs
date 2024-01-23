using System.Collections.Generic;
using TestToNiiasConsole.Models;
using TestToNiiasConsole.Repositories;
using Path = TestToNiiasConsole.Models.Path;

// Создаем тестовую станцию
var station = CreateTestStation();

// Создаем экземпляр репозитория
var repository = new StationRepository();

while (true)
{
    Console.WriteLine("Выберите операцию:");
    Console.WriteLine("1. Вывести информацию о возможных заливках парков станции.");
    Console.WriteLine("2. Найти кратчайший путь между точками");
    Console.WriteLine("3. Вывод доступных парков.");
    Console.WriteLine("4. Вывод всех вершин парка.");
    Console.WriteLine("5. Вывод всех участков станции.");
    Console.WriteLine("6. Выйти.");

    // Считываем выбор пользователя
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            // Возможные заливки парков.
            repository.FillParks(station);
            break;
        case "2":
            // Найти кратчайший путь между точками
            Console.Write("Введите начальную точку (Id): ");
            int startId = int.Parse(Console.ReadLine());

            Console.Write("Введите конечную точку (Id): ");
            int endId = int.Parse(Console.ReadLine());

            var start = station.Segments.Find(s => s.StartPoint.Id == startId)?.StartPoint;
            var end = station.Segments.Find(s => s.EndPoint.Id == endId)?.EndPoint;

            if (start != null && end != null)
            {
                var result = repository.FindShortestPath(station, start, end);
                if (result != null)
                {
                    Console.WriteLine($"Кратчайший путь: {string.Join(" -> ", result.Item1.ToString())}");
                    Console.WriteLine($"Длина маршрута: {result.Item2}");
                }
            }
            else
            {
                Console.WriteLine("Неверные Id точек.");
            }
            break;
        case "3":
            // Вывод доступных парков.
            repository.PrintStationParks(station);
            break;
        //return;
        case "4":
            Console.Write("Введите номер парка (Id): ");
            int parkId = int.Parse(Console.ReadLine());
            repository.PrintParkVertices(station, parkId); 
            break;
        case "5":
            // Вывод участков станции.
            repository.PrintStationSegments(station);
            break;

        case "6":
            // Выйти.
            return;
        default:
            Console.WriteLine("Неверный выбор. Пожалуйста, выберите существующую операцию.");
            break;
    }
}


static StationModel CreateTestStation()
{
    // Создаем станцию
    var station = new StationModel();

    // Создаем точки
    var point1 = new Point(1, "Point1");
    var point2 = new Point(2, "Point2");
    var point3 = new Point(3, "Point3");
    var point4 = new Point(4, "Point4");
    var point5 = new Point(5, "Point5");
    var point6 = new Point(6, "Point6");
    var point7 = new Point(7, "Point7");
    var point8 = new Point(8, "Point8");
    var point9 = new Point(9, "Point9");
    var point10 = new Point(10, "Point10");
    var point11 = new Point(11, "Point11");
    var point12 = new Point(12, "Point12");
    var point13 = new Point(13, "Point13");
    var point14 = new Point(14, "Point14");
    var point15 = new Point(15, "Point15");
    var point16 = new Point(16, "Point16");
    var point17 = new Point(17, "Point17");
    var point18 = new Point(18, "Point18");
    var point19 = new Point(19, "Point19");
    var point20 = new Point(20, "Point20");
    var point21 = new Point(21, "Point21");
    var point22 = new Point(22, "Point22");
    var point23 = new Point(23, "Point23");
    var point24 = new Point(24, "Point24");
    var point25 = new Point(25, "Point25");
    var point26 = new Point(26, "Point26");
    var point27 = new Point(27, "Point27");
    var point28 = new Point(28, "Point28");
    var point29 = new Point(29, "Point29");

    // Создаем сегменты
    var segment1 = new Segment(1, "Segment1", point1, point2, 10.0);
    var segment2 = new Segment(2, "Segment2", point2, point3, 15.0);
    var segment3 = new Segment(3, "Segment3", point3, point4, 12.0);
    var segment4 = new Segment(4, "Segment4", point4, point5, 8.0);
    var segment5 = new Segment(5, "Segment5", point5, point6, 9.0);
    var segment6 = new Segment(6, "Segment6", point2, point8, 6.0);
    var segment7 = new Segment(7, "Segment7", point8, point9, 4.0);
    var segment8 = new Segment(8, "Segment8", point9, point10, 10.0);
    var segment9 = new Segment(9, "Segment9", point1, point7, 7.0);
    var segment10 = new Segment(10, "Segment10", point7, point9, 14.0);
    var segment11 = new Segment(11, "Segment11", point6, point10, 8.0);
    var segment12 = new Segment(12, "Segment12", point10, point11, 3.0);
    var segment13 = new Segment(13, "Segment13", point11, point12, 9.0);
    var segment14 = new Segment(14, "Segment14", point12, point13, 8.0);

    var segment15 = new Segment(15, "Segment15", point14, point15, 8.0);
    var segment16 = new Segment(16, "Segment16", point15, point16, 7.0);
    var segment17 = new Segment(17, "Segment17", point16, point17, 10.0);
    var segment18 = new Segment(18, "Segment18", point17, point18, 8.0);
    var segment19 = new Segment(19, "Segment19", point15, point20, 3.0);
    var segment20 = new Segment(20, "Segment20", point16, point20, 4.0);
    var segment21 = new Segment(21, "Segment21", point19, point20, 11.0);
    var segment22 = new Segment(22, "Segment22", point20, point21, 12.0);
    var segment23 = new Segment(23, "Segment23", point18, point21, 8.0);

    var segment24 = new Segment(24, "Segment24", point22, point23, 7.0);
    var segment25 = new Segment(25, "Segment25", point23, point24, 6.0);
    var segment26 = new Segment(26, "Segment26", point24, point25, 9.0);

    var segment27 = new Segment(27, "Segment27", point26, point27, 10.0);
    var segment28 = new Segment(28, "Segment28", point27, point28, 8.0);
    var segment29 = new Segment(29, "Segment29", point28, point29, 7.0);

    // Добавляем сегменты в станцию
    station.Segments.AddRange(new List<Segment> { segment1, segment2, segment3, segment4, segment5, segment6, segment7, segment8, segment9, segment10,
                                                  segment11, segment12, segment13, segment14, segment15, segment16, segment17, segment18, segment19, segment20,
                                                  segment21, segment22, segment23, segment24, segment25, segment26, segment27, segment28, segment29});

    // Создаем пути
    var path1 = new Path(1, "Path1");
    var path2 = new Path(2, "Path2");
    var path3 = new Path(3, "Path3");
    var path4 = new Path(4, "Path4");
    var path5 = new Path(5, "Path5");
    var path6 = new Path(6, "Path6");
    var path7 = new Path(7, "Path7");
    var path8 = new Path(8, "Path8");

    // Добавляем сегменты в пути
    path1.Segments.Add(segment1);
    path1.Segments.Add(segment2);
    path1.Segments.Add(segment3);
    path1.Segments.Add(segment4);
    path1.Segments.Add(segment5);

    path2.Segments.Add(segment1);
    path2.Segments.Add(segment6);
    path2.Segments.Add(segment7);
    path2.Segments.Add(segment8);

    path3.Segments.Add(segment9);
    path3.Segments.Add(segment10);
    path3.Segments.Add(segment8);

    path4.Segments.Add(segment14);
    path4.Segments.Add(segment13);

    path5.Segments.Add(segment15);
    path5.Segments.Add(segment16);
    path5.Segments.Add(segment17);
    path5.Segments.Add(segment18);

    path6.Segments.Add(segment21);
    path6.Segments.Add(segment22);

    path7.Segments.Add(segment24);
    path7.Segments.Add(segment25);
    path7.Segments.Add(segment26);

    path8.Segments.Add(segment27);
    path8.Segments.Add(segment28);
    path8.Segments.Add(segment29);

    // Добавляем пути в станцию
    station.Paths.AddRange(new List<Path> { path1, path2, path3, path4, path5, path6, path7, path8 });

    // Создаем парки
    var park1 = new Park(1, "Park1");
    var park2 = new Park(2, "Park2");

    // Добавляем пути в парки
    park1.Paths.Add(path1);
    park1.Paths.Add(path2);
    park1.Paths.Add(path3);
    park1.Paths.Add(path4);

    park2.Paths.Add(path5);
    park2.Paths.Add(path6);

    // Добавляем парки в станцию
    station.Parks.AddRange(new List<Park> { park1, park2 });

    return station;
}