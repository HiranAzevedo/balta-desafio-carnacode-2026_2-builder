using Builder.Builder;
using Builder.Model;

namespace Builder;

public static class SalesReportPresets
{
    public static SalesReportBuilder.IOptionalStep StandardPdf(
        string title, DateTime start, DateTime end)
        => SalesReportBuilder.Create()
            .WithTitle(title)
            .WithFormat(ReportFormat.Pdf)
            .ForPeriod(start, end)
            .WithHeader("Relatório de Vendas")
            .WithFooter("Confidencial")
            .Layout(Orientation.Portrait, PageSize.A4, pageNumbers: true);

    public static SalesReportBuilder.IOptionalStep StandardExcel(
        string title, DateTime start, DateTime end)
        => SalesReportBuilder.Create()
            .WithTitle(title)
            .WithFormat(ReportFormat.Excel)
            .ForPeriod(start, end);
}

