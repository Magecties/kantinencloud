using Azure.Data.Tables;
using IBAS_kantine;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=kantinestoragereal;AccountKey=OFbRf6vsc2JK4Oi3Frw1lxEYkACDBfWNSbsy69fM43339KUFnnAXbs1YGeBl1ItHioZ3kvhhIvWb+AStly/KQw==;BlobEndpoint=https://kantinestoragereal.blob.core.windows.net/;QueueEndpoint=https://kantinestoragereal.queue.core.windows.net/;TableEndpoint=https://kantinestoragereal.table.core.windows.net/;FileEndpoint=https://kantinestoragereal.file.core.windows.net/;"; 
    private readonly string tableName = "menuen"; 
    private TableClient tableClient;

    public List<MenuItem> MenuItems { get; set; }

    public IndexModel()
    {
        tableClient = new TableClient(connectionString, tableName);
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
}
