using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SamplePoc.Sql.Options
{
    public class SqlConfigureOptions : IConfigureOptions<SqlOptions>
    {
        private readonly IConfiguration _configuration;

        public SqlConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(SqlOptions options)
        {
            options.SqlConnection = _configuration.GetConnectionString("SqlConnection");
        }
    }
}
