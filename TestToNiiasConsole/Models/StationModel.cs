using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TestToNiiasConsole.Models
{
    /// <summary>
    /// Точки.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Идентификатор точки.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название точки.
        /// </summary>
        public string Name { get; set; }

        public Point(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    /// <summary>
    /// Участок.
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// Идентификатор участка.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название участка.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Начальная точка участка.
        /// </summary>
        public Point StartPoint { get; set; }

        /// <summary>
        /// Конечная точка участка.
        /// </summary>
        public Point EndPoint { get; set; }

        /// <summary>
        /// Расстояние между начальной и конечной точкой участка.
        /// </summary>
        public double Length { get; set; }

        public Segment(int id, string name, Point startPoint, Point endPoint, double length)
        {
            Id = id;
            Name = name;
            StartPoint = startPoint;
            EndPoint = endPoint;
            Length = length;
        }
    }

    /// <summary>
    /// Путь, состоит из участков.
    /// </summary>
    public class Path
    {
        /// <summary>
        /// Идентификатор пути.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название пути.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список участков, из которых состоит путь.
        /// </summary>
        public List<Segment> Segments { get; set; }

        public Path(int id, string name)
        {
            Id = id;
            Name = name;
            Segments = new List<Segment>();
        }
    }

    /// <summary>
    /// Парк, может включать в себя несколько путей.
    /// </summary>
    public class Park
    {
        /// <summary>
        /// Идентификатор парка.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название парка.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список путей, входящих в парк.
        /// </summary>
        public List<Path> Paths { get; set; }

        public Park(int id, string name)
        {
            Id = id;
            Name = name;
            Paths = new List<Path>();
        }
    }

    /// <summary>
    /// Модель станции.
    /// Станция может включать в себя парки, отдельные пути и участки.
    /// </summary>
    public class StationModel
    {
        /// <summary>
        /// Список участков вне путей.
        /// </summary>
        public List<Segment> Segments { get; set; }

        /// <summary>
        /// Список путей вне парка.
        /// </summary>
        public List<Path> Paths { get; set; }

        /// <summary>
        /// Список парков.
        /// </summary>
        public List<Park> Parks { get; set; }

        /// <summary>
        /// Список смежности для хранения структуры графа станции.
        /// </summary>
        public Dictionary<Point, List<Segment>> AdjacencyList { get; set; }

        public StationModel()
        {
            Segments = new List<Segment>();
            Paths = new List<Path>();
            Parks = new List<Park>();
            AdjacencyList = new Dictionary<Point, List<Segment>>();
        }

        /// <summary>
        /// Обновление списка смежности.
        /// </summary>
        public static void UpdateAdjacencyList(StationModel station)
        {
            // Построение списков смежности на основе сегментов станции
            station.AdjacencyList = BuildAdjacencyList(station.Segments);
        }

        // Метод для построения списков смежности
        private static Dictionary<Point, List<Segment>> BuildAdjacencyList(List<Segment> segments)
        {
            // Словарь, который будет содержать списки смежности для каждой точки
            Dictionary<Point, List<Segment>> adjacencyList = new Dictionary<Point, List<Segment>>();

            // Проход по всем сегментам станции для построения списков смежности
            foreach (var segment in segments)
            {
                // Если точка старта сегмента не существует в словаре, добавляем ее
                if (!adjacencyList.ContainsKey(segment.StartPoint))
                {
                    adjacencyList[segment.StartPoint] = new List<Segment>();
                }

                // Если точка конца сегмента не существует в словаре, добавляем ее
                if (!adjacencyList.ContainsKey(segment.EndPoint))
                {
                    adjacencyList[segment.EndPoint] = new List<Segment>();
                }

                // Добавляем сегменты в списки смежности соответствующих точек
                adjacencyList[segment.StartPoint].Add(segment);
                adjacencyList[segment.EndPoint].Add(segment);
            }

            return adjacencyList;
        }
    }
}
