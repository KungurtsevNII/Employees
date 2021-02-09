using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace Employees.API.Configuration
{
    public static class SerilogExtensions
    {
        public static ILogger CreteSerilog(this ILogger logger)
        {
             return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions
                {
                    
                })
                .CreateLogger();
        }
    }
}