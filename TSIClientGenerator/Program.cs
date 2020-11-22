using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Generation;
using NSwag.CodeGeneration.TypeScript;
using NSwag;
using NSwag.SwaggerGeneration.WebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NJsonSchema;

namespace TSIClientGenerator
{
    public static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GenerateTypeScriptClients(GenerateSwaggerDoc(), args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught: {e.Message}");

                throw;
            }
        }

        private static SwaggerDocument GenerateSwaggerDoc()
        {
            var generator = new WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings
            {
                IsAspNetCore = true,
                DefaultPropertyNameHandling = PropertyNameHandling.CamelCase
            });

            var assembly = typeof(ControllerBase).Assembly;
            var baseClass = typeof(ControllerBase);
            var types = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(baseClass))
                .ToArray();

            Console.WriteLine($"Generating swagger from types: {string.Join(",", types.Select(t => t.Name))}");

            var doc = generator.GenerateForControllersAsync(types).Result;

            Console.WriteLine("Done generating swagger");
            return doc;
        }

        private static void GenerateTypeScriptClients(SwaggerDocument swaggerSpecification, string path)
        {
            Console.WriteLine("Start generating ts client");
            var settings = new SwaggerToTypeScriptClientGeneratorSettings
            {
                Template = TypeScriptTemplate.Angular,
                GenerateOptionalParameters = false,
                HttpClass = HttpClass.HttpClient,
                InjectionTokenType = InjectionTokenType.InjectionToken,
                OperationNameGenerator = new MultipleClientsFromOperationIdWithResourceNameGenerator(),
                RxJsVersion = 6,
                UseSingletonProvider = true,
                UseTransformOptionsMethod = false,
                UseTransformResultMethod = false,
                WrapResponses = false
            };


            var generator = new SwaggerToTypeScriptClientGenerator(swaggerSpecification, settings);

            var code = generator.GenerateFile();
            Console.WriteLine("Done generating ts client");
            if (File.Exists(path))
            {
                Console.WriteLine("Deleted old file");
                File.Delete(path);
            }
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            File.WriteAllText(path, code);
            Console.WriteLine($"Done writing new file. path: {path}");
        }
    }
}
