using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Inheritanace_BankAccount.Data.Models
{
    public class MondayBoardModels
    {
        [JsonPropertyName("data")]
        public BoardsData Data { get; set; }

        [JsonPropertyName("extensions")]
        public Extensions Extensions { get; set; }
    }

    public class BoardsData
    {
        [JsonPropertyName("boards")]
        public List<Board> Boards { get; set; }
    }

    public class Board
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("items_page")]
        public ItemsPage ItemsPage { get; set; }

        public List<Item> Items => ItemsPage?.Items;
    }

    public class ItemsPage
    {
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("column_values")]
        public List<ColumnValue> ColumnValues { get; set; }
    }

    public class ColumnValue
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Extensions
    {
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }
    }
}
