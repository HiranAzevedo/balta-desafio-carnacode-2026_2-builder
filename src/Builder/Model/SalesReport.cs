using Builder.Builder;

namespace Builder.Model;

public enum ReportFormat { Pdf, Excel, Html }
public enum Orientation { Portrait, Landscape }
public enum PageSize { A4, Letter }

public sealed class SalesReport
{
    public string Title { get; }
    public ReportFormat Format { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public bool IncludeHeader { get; }
    public string? HeaderText { get; }

    public bool IncludeFooter { get; }
    public string? FooterText { get; }

    public bool IncludeCharts { get; }
    public string? ChartType { get; }

    public bool IncludeSummary { get; }
    public IReadOnlyList<string> Columns { get; }
    public IReadOnlyList<string> Filters { get; }
    public string? SortBy { get; }
    public string? GroupBy { get; }
    public bool IncludeTotals { get; }

    public Orientation? Orientation { get; }
    public PageSize? PageSize { get; }
    public bool IncludePageNumbers { get; }
    public string? CompanyLogo { get; }
    public string? WaterMark { get; }

    internal SalesReport(SalesReportBuilder.State s)
    {
        Title = s.Title!;
        Format = s.Format!.Value;
        StartDate = s.StartDate!.Value;
        EndDate = s.EndDate!.Value;

        IncludeHeader = s.IncludeHeader;
        HeaderText = s.HeaderText;

        IncludeFooter = s.IncludeFooter;
        FooterText = s.FooterText;

        IncludeCharts = s.IncludeCharts;
        ChartType = s.ChartType;

        IncludeSummary = s.IncludeSummary;
        Columns = s.Columns.AsReadOnly();
        Filters = s.Filters.AsReadOnly();
        SortBy = s.SortBy;
        GroupBy = s.GroupBy;
        IncludeTotals = s.IncludeTotals;

        Orientation = s.Orientation;
        PageSize = s.PageSize;
        IncludePageNumbers = s.IncludePageNumbers;
        CompanyLogo = s.CompanyLogo;
        WaterMark = s.WaterMark;
    }

    public void Generate()
    {
        Console.WriteLine($"\n=== Gerando Relatório: {Title} ===");
        Console.WriteLine($"Formato: {Format}");
        Console.WriteLine($"Período: {StartDate:dd/MM/yyyy} a {EndDate:dd/MM/yyyy}");

        if (IncludeHeader) Console.WriteLine($"Cabeçalho: {HeaderText}");
        if (IncludeCharts) Console.WriteLine($"Gráfico: {ChartType}");

        Console.WriteLine($"Colunas: {string.Join(", ", Columns)}");

        if (Filters.Count > 0) Console.WriteLine($"Filtros: {string.Join(", ", Filters)}");
        if (!string.IsNullOrEmpty(GroupBy)) Console.WriteLine($"Agrupado por: {GroupBy}");
        if (IncludeFooter) Console.WriteLine($"Rodapé: {FooterText}");

        Console.WriteLine("Relatório gerado com sucesso!");
    }
}

