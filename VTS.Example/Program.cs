﻿// See https://aka.ms/new-console-template for more information

using System.Threading.Tasks;
using VTS.Core;

namespace VTS.Example
{
    internal abstract class Program
    {
        public static async Task Main(string[] args)
        {
            var logger = new ConsoleVTSLoggerImpl();
            var websocket = new WebSocketNetCoreImpl(logger);
            var jsonUtility = new NewtonsoftJsonUtilityImpl();
            var tokenStorage = new TokenStorageImpl("");

            var plugin = new CoreVTSPlugin(logger, 100, "My first plugin", "My Name", "");

            try
            {
                await plugin.InitializeAsync(websocket, jsonUtility, tokenStorage, () => logger.LogWarning("Disconnected!"));
                logger.Log("Connected!");

                var apiState = await plugin.GetAPIStateAsync();
                logger.Log("Using VTubeStudio " + apiState.data.vTubeStudioVersion);
                
                var currentModel = await plugin.GetCurrentModelAsync();
                logger.Log("The current model is: " + currentModel.data.modelName);
            }
            catch (VTSException e)
            {
                logger.LogError(e);
            }
        }
    }
}