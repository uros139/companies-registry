﻿using CompaniesRegistry.Application.Abstractions.Messaging;
using CompaniesRegistry.Application.Features.Companies.GetById;

namespace CompaniesRegistry.Application.Features.Companies.Create;

public sealed record CreateCompanyCommand : ICommand<CompanyResponse>
{
    public string Name { get; set; } = String.Empty;
    public string Exchange { get; set; } = String.Empty;
    public string Ticker { get; set; } = String.Empty;
    public string Isin { get; set; } = String.Empty;
    public string? WebSite { get; set; }
}
