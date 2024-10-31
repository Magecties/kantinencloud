namespace IBAS_kantine
{
    public class MenuItem
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Dag { get; set; }
        public string VarmRet { get; set; }
        public string KoldRet { get; set; }
    }
}
