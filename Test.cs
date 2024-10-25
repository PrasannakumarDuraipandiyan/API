using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Telerik.Documents.SpreadsheetStreaming;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace YourNamespace
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpreadsheetDownloadController : ControllerBase
    {
        private readonly string _connectionString = "Your Oracle Connection String";

        [HttpGet("download")]
        public async Task<IActionResult> DownloadSpreadsheet()
        {
            var query = "SELECT * FROM YourTable"; // Replace with your actual query

            using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new OracleCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess);

            using var memoryStream = new MemoryStream();
            using (var exporter = SpreadExporter.CreateExporter(SpreadDocumentFormat.Xlsx, memoryStream))
            {
                var worksheet = exporter.CreateWorksheet();
                worksheet.WriteRow(reader.GetColumnSchema().Select(col => col.ColumnName).ToArray());

                while (await reader.ReadAsync())
                {
                    var row = new object[reader.FieldCount];
                    reader.GetValues(row);
                    worksheet.WriteRow(row);
                }
            }

            var fileBytes = memoryStream.ToArray();
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
        }
    }
}
