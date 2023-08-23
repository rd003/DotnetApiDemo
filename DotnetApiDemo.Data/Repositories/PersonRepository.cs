using Dapper;
using DotnetApiDemo.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DotnetApiDemo.Data.Repositories;

public class PersonRepository : IPersonRepository
{
    // ctrl+.
    private readonly IConfiguration _config;
    private readonly string? _connectionString;
    public PersonRepository(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("default");
    }

    public async Task AddPerson(Person person)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string sql = "insert into person (name,email) values(@name,@email)";
        await connection.ExecuteAsync(sql, new { person.Name, person.Email }, commandType: CommandType.Text);
    }

    public async Task UpdatePerson(Person person)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string sql = "update person set name=@name,email=@email where id=@id";
        await connection.ExecuteAsync(sql, new { person.Id, person.Name, person.Email }, commandType: CommandType.Text);
    }

    public async Task<IEnumerable<Person>> GetPeople()
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string sql = "select * from person";
        var people = await connection.QueryAsync<Person>(sql, commandType: CommandType.Text);
        return people;
    }

    public async Task<Person?> GetPersonById(int id)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string sql = "select * from person where id=@id";
        var people = await connection.QueryAsync<Person>(sql, new { id }, commandType: CommandType.Text);
        return people.FirstOrDefault();
    }

    public async Task DeletePerson(int id)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string sql = "delete from person where id=@id";
        var people = await connection.ExecuteAsync(sql, new { id }, commandType: CommandType.Text);
    }
}
