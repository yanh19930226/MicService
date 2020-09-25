using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Project.Api.Applicatons.Queries
{
    public class ProjectQueries : IProjectQueries
    {
        public readonly string _connStr;
        public ProjectQueries(string connStr)
        {
            _connStr = connStr;
        }
        public async Task<dynamic> GetProjectDetail(int projectId)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                var sql = @"SELECT 
                            projects.Company
                            projects.Province
                            FROM projects 
                            Inner JOIN projectviewers
                            On projects.Id=projectviewers.ProjectId
                            WHERE projects.Id=@projectId";
                var res = await conn.QueryAsync<dynamic>(sql, new { projectId });
                return res;
            }
        }
        public async Task<dynamic> GetProjectsByUserId(int userId)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                var sql = @"SELECT 
                            projects.Company,
                            projects.UserId,
                            projects.Province
                            FROM projects 
                            WHERE projects.UserId=@userId";
                var res = await conn.QueryAsync<dynamic>(sql, new { userId });
                return res;
            }
        }
    }
}
