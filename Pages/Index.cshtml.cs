using Azure.Data.Tables;
using Azure.Identity; 
using IBAS_kantine;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly string tableName = "menuen";
    private TableClient tableClient;

    public List<MenuItem> MenuItems { get; set; }

    public IndexModel()
    {
        var tableEndpoint = new Uri("https://kantinestoragereal.table.core.windows.net");
        tableClient = new TableClient(tableEndpoint, tableName, new DefaultAzureCredential());
    }

    public async Task OnGetAsync()
    {
        MenuItems = new List<MenuItem>();

        // Hent data fra tabellen
        await foreach (var entity in tableClient.QueryAsync<TableEntity>())
        {
            var menuItem = new MenuItem
            {
                PartitionKey = entity.PartitionKey,
                RowKey = entity.RowKey,
                Dag = entity.ContainsKey("Dag") && entity["Dag"] is string dagValue ? dagValue : null,
                VarmRet = entity.ContainsKey("VarmRet") && entity["VarmRet"] is string varmRetValue ? varmRetValue : null,
                KoldRet = entity.ContainsKey("KoldRet") && entity["KoldRet"] is string koldRetValue ? koldRetValue : null
            };
            MenuItems.Add(menuItem);
        }
    }
}
