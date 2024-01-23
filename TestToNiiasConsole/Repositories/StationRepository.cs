using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestToNiiasConsole.Models;
using static System.Collections.Specialized.BitVector32;
using Path = TestToNiiasConsole.Models.Path;

namespace TestToNiiasConsole.Repositories
{
    public class StationRepository
    {
        /// <summary>
        /// Метод для поиска кратчайшего пути между двумя точками на станции на основе алгоритма Дейкстры.
        /// </summary>
        /// <param name="station"> Станция. </param>
        /// <param name="start"> Начальная точка.</param>
        /// <param name="end"> Конечная точка. </param>
        /// <returns> Возвращает список участков кратчайшего маршрута и его общую длину. </returns>
        public Tuple<Segment[], double> FindShortestPath(StationModel station, Point start, Point end)
        {
            // Обновляем списки смежности
            StationModel.UpdateAdjacencyList(station);

            Dictionary<Point, double> distances = new Dictionary<Point, double>();
            Dictionary<Point, Point> previous = new Dictionary<Point, Point>();
            HashSet<Point> vertices = new HashSet<Point>(station.AdjacencyList.Keys);

            // Инициализация расстояний и предыдущих вершин
            foreach (var point in station.AdjacencyList.Keys)
            {
                distances[point] = double.PositiveInfinity;
                previous[point] = null;
            }

            // Расстояние от начальной точки до самой себя равно 0
            distances[start] = 0;

            while (vertices.Count > 0)
            {
                var current = vertices.OrderBy(v => distances[v]).First();
                vertices.Remove(current);

                if (distances[current] == double.PositiveInfinity)
                {
                    break; // Если расстояние до текущей вершины равно бесконечности, значит, не существует пути
                }

                foreach (var neighborSegment in station.AdjacencyList[current])
                {
                    var neighborPoint = neighborSegment.EndPoint;
                    var alternativeDistance = distances[current] + neighborSegment.Length;

                    if (alternativeDistance < distances[neighborPoint])
                    {
                        distances[neighborPoint] = alternativeDistance;
                        previous[neighborPoint] = current;
                    }
                }
            }

            // Построение массива сегментов пути от конечной точки к начальной
            var pathSegments = new List<Segment>();
            var currentPoint = end;

            while (currentPoint != null)
            {
                if (previous[currentPoint] != null)
                {
                    var edge = station.AdjacencyList[currentPoint].FirstOrDefault(s => s.EndPoint == previous[currentPoint]);
                    if (edge != null)
                    {
                        pathSegments.Insert(0, edge);
                    }
                    else
                    {
                        // Не удалось восстановить путь
                        Console.WriteLine("Путь не существует.");
                        return null;
                    }
                }

                currentPoint = previous[currentPoint];
            }

            // Вычисление длины маршрута
            double totalDistance = distances[end];

            return new Tuple<Segment[], double>(pathSegments.ToArray(), totalDistance);
        }

            /// <summary>
            /// Вывод всех вершин, принадлежащих конкретному парку.
            /// </summary>
            /// <param name="station"> Станция. </param>
            /// <param name="parkId"> Идентификатор парка. </param>
            public void PrintParkVertices(StationModel station, int parkId)
        {
            // Проверяем, что станция не является null
            if (station == null)
            {
                Console.WriteLine("Станция не инициализирована.");
                return;
            }

            // Создаем множество для хранения вершин парка
            HashSet<Point> parkVertices = new HashSet<Point>();

            string parkName = "";
            // Проходим по всем паркам
            foreach (var park in station.Parks)
            {
                // Проверяем, что текущий парк имеет нужное имя
                if (park.Id == parkId)
                {
                    // Проходим по всем путям в парке
                    foreach (var path in park.Paths)
                    {
                        // Проходим по всем участкам в пути и добавляем вершины в множество
                        foreach (var segment in path.Segments)
                        {
                            parkVertices.Add(segment.StartPoint);
                            parkVertices.Add(segment.EndPoint);
                        }
                    }
                }
                parkName = park.Name;
            }

            // Выводим вершины парка
            Console.WriteLine($"Вершины парка '{parkName}':");
            foreach (var vertex in parkVertices)
            {
                Console.WriteLine($"{vertex.Id}, {vertex.Name}");
            }
        }

        /// <summary>
        /// Вывод всех участков станции.
        /// </summary>
        /// <param name="station"> Станция. </param>
        public void PrintStationSegments(StationModel station)
        {
            if (!station.Segments.Any())
            {
                Console.WriteLine("На стации не добавлены участки.");
            }
            else
            {
                foreach (var segment in station.Segments)
                {
                    Console.WriteLine($"Участок {segment.Id}. {segment.Name}");
                }
            }
        }

        /// <summary>
        /// Вывод списка доступных парков на станции.
        /// </summary>
        /// <param name="station"> Станция. </param>
        public void PrintStationParks(StationModel station)
        {
            if(!station.Parks.Any())
            {
                Console.WriteLine("На станции нет доступных парков.");
            }
            else
            {
                foreach ( var park in station.Parks)
                {
                    Console.WriteLine($"Парк {park.Id}. {park.Name}");
                }
            }
        }

        /// <summary>
        /// Алгоритм заливки парка. Выводит список парков с доступными путями.
        /// </summary>
        /// <param name="station"> Станция. </param>
        public List<Park> FillParks(StationModel station)
        {
            List<Park> parksWithPaths = new List<Park>();

            foreach (var park in station.Parks)
            {
                Console.WriteLine($"Парк {park.Id} {park.Name}");
                if (park.Paths.Any())
                {
                    parksWithPaths.Add(park);
                    foreach (var path in park.Paths)
                    {
                        Console.Write($"Путь {path.Id} {path.Name} ");
                    }
                }
                Console.WriteLine();
            }

            return parksWithPaths;
        }
    }
}
