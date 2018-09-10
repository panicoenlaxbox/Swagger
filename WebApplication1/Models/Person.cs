using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class HttpError
    {
        public string Description { get; set; }
        public string Data { get; set; }
    }
    public class Address
    {
        public string PostalCode { get; set; }
    }

    public class Person<T>
    {
        public Person()
        {
            
        }

        public Person(int id, string firstName, string lastName, int age, IEnumerable<T> data)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Data = data;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public IEnumerable<T> Data { get; set; }

        protected bool Equals(Person<T> other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Person<T>) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    public class PersonWrapper
    {
        public IEnumerable<Person<Address>> People { get; set; }
    }

    public class Person2Wrapper
    {
        public IEnumerable<Person<Address>> People { get; set; }
    }

    /// <summary>
    /// Respuesta del usuario
    /// </summary>
    public class UsersQueryResponse
    {
        public UsersQueryResponse(PaginatedResult<UserDto> paginatedResult)
        {
            PaginatedResult = paginatedResult;
        }
        public PaginatedResult<UserDto> PaginatedResult { get; }

        public class UserDto
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Image { get; set; }
            public string UserType { get; set; }
        }
    }

    public class PaginatedResult<T>
    {
        public IEnumerable<T> Rows { get; }
        public int TotalResults { get; }
        public int First { get; }
        public int Size { get; }

        public PaginatedResult(int first, int size, int totalResults, IEnumerable<T> rows)
        {
            First = first;
            Size = size;
            TotalResults = totalResults;
            Rows = rows;
        }
    }
}
