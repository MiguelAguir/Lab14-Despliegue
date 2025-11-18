namespace ReportesAPI.Services;

public interface IReportService
{
    Task<IResult> GeneratePedidosPorClienteAsync();
    Task<IResult> GenerateTopProductosAsync();
}