using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace alquimia.Tests.TestUtils
{
    public static class MoqExtensions
    {
        public static Mock<DbSet<T>> ReturnsDbSet<T>(this Mock<DbSet<T>> mockSet, IEnumerable<T> list) where T : class
        {
            var queryable = list.AsQueryable();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return mockSet;
        }
    }
}
