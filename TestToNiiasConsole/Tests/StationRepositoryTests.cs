using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestToNiiasConsole.Models;
using TestToNiiasConsole.Repositories;
using TestToNiiasConsole;
using Path = TestToNiiasConsole.Models.Path;

namespace TestToNiiasConsole.Tests
{
    [TestClass]
    public class StationRepositoryTests
    {
        /// <summary>
        /// Тест метода поиска кратчайшего пути.
        /// </summary>
        [TestMethod]
        public void FindShortestPathTest()
        {
            // Arrange
            var station = CreateTestStation(); // Создаем тестовую станцию
            var repository = new StationRepository();

            // Act
            var result = repository.FindShortestPath(station, station.Segments[0].StartPoint, station.Segments[2].EndPoint);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Item1); // Проверяем, что путь не пустой
            Assert.IsTrue(result.Item1.Length > 0); // Проверяем, что длина пути положительна
            Assert.AreEqual(result.Item1.Length, result.Item2); // Проверяем, что длина пути равна общей длине маршрута
        }

        /// <summary>
        /// Тест метода алгоритма заливки парка.
        /// </summary>
        [TestMethod]
        public void GetParksWithPaths_ShouldReturnParksWithPaths()
        {
            // Arrange
            var station = CreateTestStation(); // Создаем тестовую станцию
            var repository = new StationRepository(); // Создаем экземпляр репозитория

            // Act
            var result = repository.FillParks(station);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count); // Ожидаем, что два парка содержат пути
        }

        /// <summary>
        /// Проверка обновления списка смежности.
        /// </summary>
        [TestMethod]
        public void UpdateAdjacencyList_ShouldUpdateAdjacencyList()
        {
            // Arrange
            var station = CreateTestStation(); 

            // Act
            StationModel.UpdateAdjacencyList(station);

            // Assert
            Assert.IsNotNull(station.AdjacencyList);
        }

        private static StationModel CreateTestStation()
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
    }
}
