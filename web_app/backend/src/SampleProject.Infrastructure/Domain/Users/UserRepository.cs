using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Exceptions;
using SampleProject.Domain.Users;
using SampleProject.Infrastructure.Database;

namespace SampleProject.Infrastructure.Domain.Users;

public class UserRepository : IUserRepository
{
    private static readonly string TableName = $"{nameof(User).ToLower()}";
    
    private readonly ApplicationContext _context;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public UserRepository(ISqlConnectionFactory sqlConnectionFactory, ApplicationContext context)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _context = context;
    }

    public async Task<Guid> AddAsync(User user)
    {
        var dynamicParameter = new DynamicParameters();
        dynamicParameter.Add(nameof(user.Username), user.Username);
        dynamicParameter.Add(nameof(user.Role), user.Role);
        dynamicParameter.Add(nameof(user.PasswordHash), user.PasswordHash);
        dynamicParameter.Add(nameof(user.Email), user.Email);
        dynamicParameter.Add(nameof(user.CreatedAt), user.CreatedAt);

        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = $"INSERT INTO \"{TableName}\" (\"{nameof(user.Username)}\", \"{nameof(user.Role)}\", \"{nameof(user.PasswordHash)}\", " +
                  $"                             \"{nameof(user.Email)}\", \"{nameof(user.CreatedAt)}\") " +
                  $"VALUES ({DatabaseConstants.ParametersPrefix}{nameof(user.Username)}, " +
                  $"        {DatabaseConstants.ParametersPrefix}{nameof(user.Role)}, " +
                  $"        {DatabaseConstants.ParametersPrefix}{nameof(user.PasswordHash)}, " +
                  $"        {DatabaseConstants.ParametersPrefix}{nameof(user.Email)}, " +
                  $"        {DatabaseConstants.ParametersPrefix}{nameof(user.CreatedAt)}) " +
                  $"RETURNING \"{nameof(user.Id)}\"";
        
        var userId = await connection.ExecuteScalarAsync<Guid>(sql, dynamicParameter);

        return userId;
    }

    public async Task<User> GetByIdAsync(Guid userId)
    {
        var dynamicParameter = new DynamicParameters();
        dynamicParameter.Add(nameof(userId), userId);

        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = $"SELECT * FROM \"{TableName}\" WHERE \"Id\" = {DatabaseConstants.ParametersPrefix}{nameof(userId)}";

        var user = await connection.QuerySingleOrDefaultAsync<User>(sql, dynamicParameter);

        return user;
    }

    public async Task<User> GetByUsername(string username)
    {
        var dynamicParameter = new DynamicParameters();
        dynamicParameter.Add(nameof(username), username);

        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = $"SELECT * " +
                  $"FROM \"{TableName}\" " +
                  $"WHERE \"{nameof(User.Username)}\" = {DatabaseConstants.ParametersPrefix}{nameof(username)} ";

        var user = await connection.QuerySingleOrDefaultAsync<User>(sql, dynamicParameter);

        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = $"SELECT * FROM \"{TableName}\"";

        var users = await connection.QueryAsync<User>(sql);

        return users;
    }

    public async Task UpdateAsync(User user)
    {
        var dynamicParameters = DatabaseHelper.GetDynamicParameters(user);
        var fieldToUpdateList = DatabaseHelper.GetFieldToUpdateList(user);
        
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = $"UPDATE \"{TableName}\" " +
                  $"SET {string.Join(", ", fieldToUpdateList)} " +
                  $"WHERE \"{nameof(user.Id)}\" = {DatabaseConstants.ParametersPrefix}{nameof(user.Id)}";

        await connection.ExecuteAsync(sql, dynamicParameters);
    }

    public async Task RemoveAsync(Guid userId)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add(nameof(userId), userId);
        
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = $"DELETE FROM \"{TableName}\" " +
                  $"WHERE \"Id\" = {DatabaseConstants.ParametersPrefix}{nameof(userId)}";

        await connection.ExecuteAsync(sql, dynamicParameters);
    }
}