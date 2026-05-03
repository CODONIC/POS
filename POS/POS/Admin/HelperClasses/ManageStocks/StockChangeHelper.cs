using System.Data;

public static class StockChangeHelper
{
    public static DataTable CreatePendingTable()
    {
        var table = new DataTable();
        table.Columns.Add("product_code", typeof(string));
        table.Columns.Add("product_name", typeof(string));
        table.Columns.Add("category", typeof(string));
        table.Columns.Add("change_type", typeof(string));
        table.Columns.Add("quantity", typeof(int));
        return table;
    }

    public static void AddChange(DataTable pendingTable, string code, string name,
                                  string category, string changeType, int quantity)
    {
        pendingTable.Rows.Add(code, name, category, changeType, quantity);
    }

    public static bool HasChanges(DataTable pendingTable) => pendingTable.Rows.Count > 0;
}