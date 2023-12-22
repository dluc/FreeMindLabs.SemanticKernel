﻿// Copyright (c) Free Mind Labs, Inc. All rights reserved.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.KernelMemory;
using System.Reflection;
using FreeMindLabs.KernelMemory.Elasticsearch;

namespace UnitTests;

/// <summary>
/// Sets up dependency injection for unit tests.
/// </summary>
public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup()
    {
        // We read from the local appSettings.json and the same user secrets
        // as the Microsoft Semantic Kernel team.
        this._configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(Assembly.GetExecutingAssembly()) // Same secrets as SK and KM :smile:
            .Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Kernel Memory with Elasticsearch
        //services.AddElasticsearchAsVectorDb(this._configuration);

        IKernelMemoryBuilder b = new KernelMemoryBuilder(services)
                .WithElasticsearch(this._configuration)
                .WithOpenAIDefaults(this._configuration["OpenAI:ApiKey"] ?? throw new ArgumentException("OpenAI:ApiKey is required."));

        services.AddSingleton<IKernelMemory>(b.Build<MemoryServerless>());

        // TODO: Uses OpenAI API Key for now. Make more flexible.
    }
}
