using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestToNiiasConsole.Models;
using static System.Collections.Specialized.BitVector32;

namespace TestToNiiasConsole.Repositories
{
    public class StationReposirory
    {
        /// <summary>
        /// Метод для поиска кратчайшего пути между двумя точками на станции
        /// </summary>
        /// <param name="station"> Станция. </param>
        /// <param name="start"> Начальная точка.</param>
        /// <param name="end"> Конечная точка. </param>
        /// <returns> Возвращает массив точек кратчайшего маршрута и общую длину. </returns>
        public static Tuple<Point[], double> FindShortestPath(StationModel station, Point start, Point end)
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

            // Построение массива точек пути от конечной точки к начальной
            var path = new List<Point>();
            var currentPoint = end;

            while (currentPoint != null)
            {
                path.Insert(0, currentPoint);

                if (currentPoint != start)
                {
                    var edge = station.AdjacencyList[currentPoint].FirstOrDefault(s => Math.Abs(distances[currentPoint] - (distances[s.EndPoint] + s.Length)) < double.Epsilon);

                    if (edge != null)
                    {
                        currentPoint = edge.StartPoint;
                    }
                    else
                    {
                        // Не удалось восстановить путь
                        Console.WriteLine("Путь не существует.");
                        return null;
                    }
                }
                else
                {
                    currentPoint = null;
                }
            }

            // Вычисление длины маршрута
            double totalDistance = distances[end];

            return new Tuple<Point[], double>(path.ToArray(), totalDistance);
        }

        /// <summary>
        /// Вывод всех вершин, принадлежащих конкретному парку.
        /// </summary>
        /// <param name="station"></param>
        /// <param name="parkName"></param>
        public static void PrintParkVertices(StationModel station, int parkId)
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
    }
}
