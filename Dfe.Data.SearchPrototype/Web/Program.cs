using Dfe.Data.SearchPrototype.Infrastructure;
using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CleanArchitecture.CleanArchitecture.Application.UseCase;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using DfE.Data.ComponentLibrary.Infrastructure.CognitiveSearch;
using GovUk.Frontend.AspNetCore;
using Azure.Search.Documents.Models;
using Azure;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Azure.Search.Documents;
using Dfe.Data.SearchPrototype.Infrastructure.Options.Mapping;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Web.Models;
using Dfe.Data.SearchPrototype.Web.Mappers;
using Infrastructure = Dfe.Data.SearchPrototype.Infrastructure;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Json.Serialisation;
using Search = Dfe.Data.SearchPrototype.Search;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddGovUkFrontend();


// Start of IOC container registrations
//
//
builder.Services.AddAzureCognitiveSearchProvider(builder.Configuration);
builder.Services.AddScoped(typeof(ISearchServiceAdapter), typeof(CognitiveSearchServiceAdapter<Infrastructure.Establishment>));
builder.Services.AddDefaultMappers();
builder.Services.AddScoped<IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>, SearchByKeywordUseCase>();
builder.Services.AddSingleton(typeof(IMapper<Response<SearchResults<Infrastructure.Establishment>>, EstablishmentResults>), typeof(AzureSearchResponseToEstablishmentResultMapper));
builder.Services.AddSingleton<IMapper<SearchSettingsOptions, SearchOptions>, SearchOptionsToAzureOptionsMapper>();
builder.Services.AddSingleton<IMapper<SearchByKeywordResponse, SearchResultsViewModel>, SearchByKeywordResponseToViewModelMapper>();
builder.Services.AddSingleton<IMapper<Infrastructure.Establishment, Search.Establishment>, AzureSearchResultToEstablishmentMapper>();
builder.Services.AddSingleton<IMapper<EstablishmentResults, SearchByKeywordResponse>, ResultsToResponseMapper>();

builder.Services.AddOptions<SearchSettingsOptions>("establishments")
    .Configure<IConfiguration>(
        (settings, configuration) =>
            configuration.GetSection("AzureCognitiveSearchOptions:SearchEstablishment:SearchSettingsOptions").Bind(settings));

builder.Services.AddSingleton<IJsonObjectSerialiser, JsonObjectSerialiser>();
builder.Services.AddScoped<ISearchOptionsFactory, SearchOptionsFactory>();
//
//
// End of IOC container registrations

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }