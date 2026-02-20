using Builder.Model;

namespace Builder.Builder;

public static class SalesReportBuilder
{
    // estado mutável interno do builder
    public sealed class State
    {
        public string? Title;
        public ReportFormat? Format;
        public DateTime? StartDate;
        public DateTime? EndDate;

        public bool IncludeHeader;
        public string? HeaderText;

        public bool IncludeFooter;
        public string? FooterText;

        public bool IncludeCharts;
        public string? ChartType;

        public bool IncludeSummary;

        public List<string> Columns = [];
        public List<string> Filters = [];

        public string? SortBy;
        public string? GroupBy;
        public bool IncludeTotals;

        public Orientation? Orientation;
        public PageSize? PageSize;
        public bool IncludePageNumbers;
        public string? CompanyLogo;
        public string? WaterMark;
    }

    // Step interfaces
    public static ITitleStep Create() => new Implementation(new State());

    public interface ITitleStep { IFormatStep WithTitle(string title); }
    public interface IFormatStep { IPeriodStep WithFormat(ReportFormat format); }
    public interface IPeriodStep { IOptionalStep ForPeriod(DateTime start, DateTime end); }

    public interface IOptionalStep
    {
        // opções fluentes
        IOptionalStep WithHeader(string headerText);
        IOptionalStep WithFooter(string footerText);
        IOptionalStep WithCharts(string chartType);
        IOptionalStep WithSummary();

        IOptionalStep AddColumn(string column);
        IOptionalStep AddColumns(params string[] columns);
        IOptionalStep AddFilter(string filter);
        IOptionalStep AddFilters(params string[] filters);

        IOptionalStep SortBy(string sortBy);
        IOptionalStep GroupBy(string groupBy);
        IOptionalStep WithTotals();

        IOptionalStep Layout(Orientation orientation, PageSize pageSize, bool pageNumbers = false);
        IOptionalStep WithCompanyLogo(string logoPath);
        IOptionalStep WithWatermark(string watermark);

        SalesReport Build();
    }



    private sealed class Implementation(State s) : ITitleStep, IFormatStep, IPeriodStep, IOptionalStep
    {
        private readonly State _s = s;

        public IFormatStep WithTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title é obrigatório");
            }

            _s.Title = title.Trim();
            return this;
        }

        public IPeriodStep WithFormat(ReportFormat format)
        {
            _s.Format = format;
            return this;
        }

        public IOptionalStep ForPeriod(DateTime start, DateTime end)
        {
            _s.StartDate = start;
            _s.EndDate = end;
            return this;
        }

        public IOptionalStep WithHeader(string headerText)
        {
            _s.IncludeHeader = true;
            _s.HeaderText = headerText;
            return this;
        }

        public IOptionalStep WithFooter(string footerText)
        {
            _s.IncludeFooter = true;
            _s.FooterText = footerText;
            return this;
        }

        public IOptionalStep WithCharts(string chartType)
        {
            _s.IncludeCharts = true;
            _s.ChartType = chartType;
            return this;
        }

        public IOptionalStep WithSummary()
        {
            _s.IncludeSummary = true;
            return this;
        }

        public IOptionalStep AddColumn(string column)
        {
            _s.Columns.Add(column);
            return this;
        }

        public IOptionalStep AddColumns(params string[] columns)
        {
            _s.Columns.AddRange(columns);
            return this;
        }

        public IOptionalStep AddFilter(string filter)
        {
            _s.Filters.Add(filter);
            return this;
        }

        public IOptionalStep AddFilters(params string[] filters)
        {
            _s.Filters.AddRange(filters);
            return this;
        }

        public IOptionalStep SortBy(string sortBy)
        {
            _s.SortBy = sortBy;
            return this;
        }
        public IOptionalStep GroupBy(string groupBy)
        {
            _s.GroupBy = groupBy;
            return this;
        }

        public IOptionalStep WithTotals()
        {
            _s.IncludeTotals = true;
            return this;
        }

        public IOptionalStep Layout(Orientation orientation, PageSize pageSize, bool pageNumbers = false)
        {
            _s.Orientation = orientation;
            _s.PageSize = pageSize;
            _s.IncludePageNumbers = pageNumbers;
            return this;
        }

        public IOptionalStep WithCompanyLogo(string logoPath) { _s.CompanyLogo = logoPath; return this; }
        public IOptionalStep WithWatermark(string watermark) { _s.WaterMark = watermark; return this; }

        public SalesReport Build()
        {
            // validações centrais
            if (_s.Format is null)
            {
                throw new InvalidOperationException("Format é obrigatório");
            }

            if (_s.StartDate is null || _s.EndDate is null)
            {
                throw new InvalidOperationException("Período é obrigatório");
            }

            if (_s.StartDate > _s.EndDate)
            {
                throw new InvalidOperationException("StartDate deve ser <= EndDate");
            }

            if (_s.Columns.Count == 0)
            {
                throw new InvalidOperationException("Ao menos 1 coluna é obrigatória");
            }

            if (_s.IncludeHeader && string.IsNullOrWhiteSpace(_s.HeaderText))
            {
                throw new InvalidOperationException("HeaderText obrigatório quando IncludeHeader = true");
            }

            if (_s.IncludeFooter && string.IsNullOrWhiteSpace(_s.FooterText))
            {
                throw new InvalidOperationException("FooterText obrigatório quando IncludeFooter = true");
            }

            if (_s.IncludeCharts && string.IsNullOrWhiteSpace(_s.ChartType))
            {
                throw new InvalidOperationException("ChartType obrigatório quando IncludeCharts = true");
            }

            return new SalesReport(_s);
        }
    }
}

