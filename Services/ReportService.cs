using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using ReportesAPI.Data;
namespace ReportesAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _db;

        public ReportService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IResult> GeneratePedidosPorClienteAsync()
        {
            var data = await _db.Orders
                .Include(o => o.Client) 
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Select(o => new
                {
                    Cliente = o.Client.Name,         
                    Fecha = o.OrderDate.ToString("yyyy-MM-dd"),
                    Productos = o.OrderDetails.Select(od => new
                    {
                        NombreProducto = od.Product.Name, 
                        od.Quantity,
                        Total = od.Quantity * od.Product.Price
                    }).ToList()
                })
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Pedidos");

            ws.Cell(1, 1).Value = "Cliente";
            ws.Cell(1, 2).Value = "Fecha";
            ws.Cell(1, 3).Value = "Producto";
            ws.Cell(1, 4).Value = "Cant.";
            ws.Cell(1, 5).Value = "Total";
            ws.Range(1, 1, 1, 5).Style.Font.Bold = true;

            int row = 2;
            foreach (var pedido in data)
            {
                bool first = true;
                foreach (var prod in pedido.Productos)
                {
                    ws.Cell(row, 1).Value = first ? pedido.Cliente : "";
                    ws.Cell(row, 2).Value = first ? pedido.Fecha : "";
                    ws.Cell(row, 3).Value = prod.NombreProducto;
                    ws.Cell(row, 4).Value = prod.Quantity;
                    ws.Cell(row, 5).Value = prod.Total;
                    row++;
                    first = false;
                }
            }

            ws.Columns().AdjustToContents();
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return Results.File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PedidosPorCliente.xlsx");
        }

        public async Task<IResult> GenerateTopProductosAsync()
        {
            var data = await _db.OrderDetails
                .Include(od => od.Product)
                .GroupBy(od => od.Product.Name)
                .Select(g => new
                {
                    Producto = g.Key,
                    Cantidad = g.Sum(od => od.Quantity),
                    Ingresos = g.Sum(od => od.Quantity * od.Product.Price)
                })
                .OrderByDescending(x => x.Cantidad)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Top Productos");

            ws.Cell(1, 1).Value = "Producto";
            ws.Cell(1, 2).Value = "Cantidad Vendida";
            ws.Cell(1, 3).Value = "Ingresos";
            ws.Range(1, 1, 1, 3).Style.Font.Bold = true;

            for (int i = 0; i < data.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = data[i].Producto;
                ws.Cell(i + 2, 2).Value = data[i].Cantidad;
                ws.Cell(i + 2, 3).Value = data[i].Ingresos;
            }

            ws.Columns().AdjustToContents();
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return Results.File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TopProductos.xlsx");
        }
    }
}