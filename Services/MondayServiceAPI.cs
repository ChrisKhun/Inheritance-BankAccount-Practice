using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Inheritanace_BankAccount.Data.Models;
using Inheritanace_BankAccount.Data;
using Microsoft.Extensions.Options;

namespace Inheritanace_BankAccount.Services
{
    public class MondayServiceAPI
    {
        private readonly HttpClient _http;
        private readonly long _boardId; 

        public MondayServiceAPI()
        {
            var config = JsonDocument.Parse(File.ReadAllText("appsettings.json"));
            var apiToken = config.RootElement
                .GetProperty("Monday")
                .GetProperty("ApiToken")
                .GetString();

            _boardId = config.RootElement 
                .GetProperty("Monday")
                .GetProperty("BoardIds")[0]
                .GetInt64();

            _http = new HttpClient();
            _http.DefaultRequestHeaders.Add("Authorization", apiToken);
            _http.DefaultRequestHeaders.Add("API-Version", "2026-01");
        }

        public async Task LogTransactionAsync() 
        { // log every transaction and transfer
            var mutation = $@"mutation {{
                create_item (
                    board_id: {_boardId},
                    item_name: ""Test Item""
                ) {{
                    id
                }}
            }}";
            var payload = JsonSerializer.Serialize(new { query = mutation });
            var response = await _http.PostAsync("https://api.monday.com/v2",
                new StringContent(payload, Encoding.UTF8, "application/json"));

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
        }

        public async Task LogNewAccountAsync(string accountName, decimal initialDeposit)
        { // log every account creation

        }

        public async Task LogDeletedAccountAsync(string accountName)
        { // log every account deletion

        }
    }
}