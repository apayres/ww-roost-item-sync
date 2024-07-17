using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Roost.ItemSync.ETL;
using System;
using System.Threading.Tasks;

namespace Roost.ItemSync.Functions
{
    public class Functions
    {
        private readonly ILogger<Functions> _logger;
        private readonly IProcess _process;

        public Functions(IProcess itemSyncProcess, ILogger<Functions> logger)
        {
            _process = itemSyncProcess;
            _logger = logger;
        }

        [FunctionName("RoostItemSync")]
        public async Task Run([TimerTrigger("0 30 23 * * *", RunOnStartup = true)] TimerInfo timer)
        {
            _logger.LogInformation("Item Sync: Begin");

            try
            {
                await _process.SyncItems();
                _logger.LogInformation("Item Sync: Complete");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Item Sync: Failed, {ex.Message}");
            }
        }
    }
}
