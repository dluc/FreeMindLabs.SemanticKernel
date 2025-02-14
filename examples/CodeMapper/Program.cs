﻿// Copyright (c) Free Mind Labs, Inc. All rights reserved.

using Microsoft.KernelMemory;

namespace TestApplication;

internal class Program
{
    public static void Main(string[] args)
    {
        // Concatenate our 'WithElasticsearch()' after 'WithOpenAIDefaults()' from the core nuget
        var test1 = new KernelMemoryBuilder()
            .WithOpenAIDefaults("api key")
            .WithElasticsearch(
                endpoint: "https://localhost:9200",
                userName: "elastic",
                password: "changeme",
                certificateFingerPrint: "1234567890"
            )
            .Build();

        // Concatenate our 'WithElasticsearch()' before 'WithOpenAIDefaults()' from the core nuget
        var test2 = new KernelMemoryBuilder()
            .WithElasticsearch(endpoint: "https://localhost:9200",
                userName: "elastic",
                password: "changeme",
                certificateFingerPrint: "1234567890")
            .WithOpenAIDefaults("api key")
            .Build();

        // Concatenate our 'WithElasticsearch()' before and after KM builder extension methods from the core nuget
        var test3 = new KernelMemoryBuilder()
            .WithSimpleFileStorage()
            .WithElasticsearch(endpoint: "https://localhost:9200",
                userName: "elastic",
                password: "changeme",
                certificateFingerPrint: "1234567890")
            .WithOpenAIDefaults("api key")
            .Build();

        Console.WriteLine("Test complete");
    }
}
