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

    // Создаем сегменты
    var segment1 = new Segment(1, "Segment1", point1, point2, 10.0);
    var segment2 = new Segment(2, "Segment2", point2, point3, 15.0);
    var segment3 = new Segment(3, "Segment3", point3, point4, 12.0);
    var segment4 = new Segment(4, "Segment4", point4, point1, 8.0);

    // Добавляем сегменты в станцию
    station.Segments.AddRange(new List<Segment> { segment1, segment2, segment3, segment4 });

    // Создаем пути
    var path1 = new Path(1, "Path1");
    var path2 = new Path(2, "Path2");
    var path3 = new Path(3, "Path3");
    var path4 = new Path(4, "Path4");

    // Добавляем сегменты в пути
    path1.Segments.Add(segment1);
    path1.Segments.Add(segment2);

    path2.Segments.Add(segment2);
    path2.Segments.Add(segment3);

    path3.Segments.Add(segment3);
    path3.Segments.Add(segment4);

    path4.Segments.Add(segment4);
    path4.Segments.Add(segment1);

    // Добавляем пути в станцию
    station.Paths.AddRange(new List<Path> { path1, path2, path3, path4 });

    // Создаем парки
    var park1 = new Park(1, "Park1");
    var park2 = new Park(2, "Park2");
    var park3 = new Park(3, "Park3");

    // Добавляем пути в парки
    park1.Paths.Add(path1);
    park1.Paths.Add(path2);

    park2.Paths.Add(path3);
    park2.Paths.Add(path4);

    park3.Paths.Add(path2);
    park3.Paths.Add(path3);

    // Добавляем парки в станцию
    station.Parks.AddRange(new List<Park> { park1, park2, park3 });

    // Создаем два пути, не принадлежащих паркам
    var pathWithoutPark1 = new Path(5, "PathWithoutPark1");
    var pathWithoutPark2 = new Path(6, "PathWithoutPark2");

    // Добавляем сегменты в пути, не принадлежащие паркам
    pathWithoutPark1.Segments.Add(segment1);
    pathWithoutPark1.Segments.Add(segment3);

    pathWithoutPark2.Segments.Add(segment2);
    pathWithoutPark2.Segments.Add(segment4);

    // Добавляем пути в станцию
    station.Paths.AddRange(new List<Path> { pathWithoutPark1, pathWithoutPark2 });

    return station;
}